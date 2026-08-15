# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

A set of libraries for parsing and building Devil Daggers file formats (spawnsets, replays, mods), reading game memory, and exposing static game data. Published to NuGet as a single package, `DevilDaggersInfo.Core`. Originally part of the [ddinfo-web](https://github.com/NoahStolk/ddinfo-web) repository; split out in September 2023.

## Commands

Everything runs from the repo root; the solution is `src/DevilDaggersInfo.Core.slnx` (SLNX format, requires a recent SDK).

```sh
dotnet build src/DevilDaggersInfo.Core.slnx -c Release
dotnet test --solution src/DevilDaggersInfo.Core.slnx -c Release --no-build

# Single test project
dotnet test --project src/test/DevilDaggersInfo.Core.Replay.Test

# Single test class / filtered (path segments are /assembly/namespace/class/method)
dotnet test --project src/test/DevilDaggersInfo.Core.Spawnset.Test -- --treenode-filter "/*/*/GemStateTests/*"

# Pack (what CI pushes to nuget.org on push to main)
dotnet pack src/DevilDaggersInfo.Core -c Release -o .
```

Toolchain is pinned by `global.json` to SDK 10.0.100 (`rollForward: latestMajor`); the target framework is `net10.0` with `LangVersion` 14.0, both set once in `src/Directory.Build.props` for every project.

Build output goes to `src/artifacts/` and `src/test/artifacts/` (`UseArtifactsOutput`; the test tree gets its own because `src/test/Directory.Build.props` is the nearest props file there).

Tests run on **TUnit** over **Microsoft.Testing.Platform**, not VSTest — `global.json` selects the runner, so `dotnet test` needs `--solution`/`--project` rather than a bare path, and VSTest options like `--filter` do not apply. Each test project is an `Exe`; the `TUnit` package supplies the entry point, the MTP wiring, and global usings for `TUnit.Core`/`TUnit.Assertions`.

## Project layout and dependency graph

All projects live under `src/`. Dependencies form a shallow DAG:

```
Wiki  ──────► Spawnset ──► Replay
  ▲              ▲
  └── CriteriaExpression ──► Common ◄── GameData
Asset ──► Mod
Encryption (standalone)
```

`DevilDaggersInfo.Core` is a facade project with no code of its own. It `ProjectReference`s every library with `IncludeAssets` set to that library's DLL and a `CopyProjectReferencesToPackage` target, so all nine assemblies ship inside one NuGet package. **When adding a new library project, it must be added to both the `.slnx` and to `DevilDaggersInfo.Core.csproj`'s "Internal dependencies to pack" item group**, or it silently won't be packaged.

Library responsibilities:

- **Spawnset** — `SpawnsetBinary` is an immutable `record` covering the whole file format (arena tiles, spawns, practice settings, shrink values). `View/` contains derived read models (`SpawnsView`, `GemState`) used to present spawn data rather than store it.
- **Replay** — `ReplayBinary<THeader>` is generic over `IReplayBinaryHeader<T>` (static abstract members), with `LocalReplayBinaryHeader` and `LeaderboardReplayBinaryHeader` as the two implementations; they differ in whether events are length-prefixed. `ReplayEventsParser`/`ReplayEventsCompiler` handle the zlib-compressed event stream (note the manual 2-byte ZLIB header skip). Events are `ReplayEvent` wrappers around `IEventData` implementations in `Events/Data/`, dispatched on a byte event type. `PostProcessing/` derives higher-level views from the raw event list (hit logs, enemy timelines, statistics, movement/sound simulation) — this is where interpretation lives; the parser stays faithful to the binary.
- **Mod** — `ModBinary` reads a TOC (`ModBinaryToc`) and lazily materializes assets through a `ModBinaryReadFilter`, so callers can parse headers without loading megabytes of asset data. `FileHandling/` converts between game-internal representations and user-facing formats (obj/glsl/png/wav); `Builders/` recompiles them back into `dd`/`audio` mod binaries.
- **Asset** — static tables (`DdTextures`, `DdMeshes`, `DdShaders`, `DdObjectBindings`, `AudioAudio`) describing every original game asset. Consumed by Mod.
- **Wiki** — static game data (enemies, deaths, upgrades, daggers) partitioned per `GameVersion` (`DeathsV1_0` … `DeathsV3_2`), with dispatch methods like `Deaths.GetDeaths(gameVersion)`.
- **GameData** — a rewrite of Wiki behind an `IGameData` interface (currently only `V3_2`). Intended to eventually replace Wiki; prefer extending it over Wiki for new work, but don't remove Wiki APIs without a changelog entry.
- **CriteriaExpression** — small parser/compiler for custom leaderboard criteria expressions, round-trippable between `string` and a ≤64-byte binary form.
- **Common** — `GameTime` (fixed-point time in 10,000 game units per second; use this instead of raw floats/doubles for game timings) and formatting helpers.
- **Encryption** — AES + Base32 wrappers used for leaderboard payloads.

Each library uses a `_Imports.cs` file for `global using` declarations; add namespace-wide usings there rather than repeating them per file.

## Binary format documentation

`docs/game-formats/` contains reverse-engineered specs for the formats this repo parses (spawnset, local/leaderboard replay, replay events, mod binary, game memory layout, death types, leaderboard API). Read the relevant doc before changing parsing code — the byte offsets and version-dependent branches there are the source of truth for the implementations.

## Conventions

- `.editorconfig` mandates **tabs** for indentation (spaces only in `.csproj`/`.yml`), UTF-8, final newline.
- Analysis is strict: `AnalysisMode=All`, `WarningsAsErrors=nullable`, plus StyleCop, Roslynator, SonarAnalyzer, and Nullable.Extended as global package references. New code is expected to build warning-free; suppress narrowly (`#pragma` with a comment) rather than loosening the global config. Test projects relax some rules via `src/test/Tests.globalconfig`.
- NuGet versions are managed centrally in `src/Directory.Packages.props` (`ManagePackageVersionsCentrally`); `PackageReference` elements carry no `Version` attribute.
- Parsing APIs consistently offer both a throwing `Parse`/constructor and a `TryParse` returning `bool` with a `[NotNullWhen(true)]` out parameter. Follow that pair when adding new parsers.
- Hot paths deliberately avoid LINQ and allocations (see the loop-based lookups in `AssetContainer` and `IGameData`); keep that style in data-lookup code.
- Test classes are `internal sealed` (TUnit discovers them fine, and `public` would trip CA1515), tests are `[Test]`-marked `async Task` methods, and parameterised cases use `[Arguments(...)]`. Every TUnit assertion returns an awaitable and **must** be awaited — an un-awaited `Assert.That(...)` never runs. Two traps worth knowing: `IsEquivalentTo` ignores element order unless you pass `CollectionOrdering.Matching`, and `IsEqualTo` will not compare across numeric types, so match the expected literal to the actual's type (`IsEqualTo((byte)1)`).
- The package version lives in `src/DevilDaggersInfo.Core/DevilDaggersInfo.Core.csproj`. `CHANGELOG.md` follows semantic versioning and documents every release, including small changes like sealing a class or a dependency bump — record any user-visible change there under the `[unreleased]` heading. Bumping the version is a separate, deliberate release step: CI packs and pushes to nuget.org on every push to `main`, but with `--skip-duplicate`, so an unchanged version is simply a no-op. Land changes under `[unreleased]`, then rename that heading and bump the version when you actually want to publish.
