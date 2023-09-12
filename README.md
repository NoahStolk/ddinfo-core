(Note: This code was originally part of the [DevilDaggersInfo](https://github.com/NoahStolk/DevilDaggersInfo) monorepo. It was moved to a separate repository on September 12th, 2023.)

# ddinfo-core

The DevilDaggersInfo project offers a couple libraries for parsing and creating Devil Daggers files. This is what the website, the web server, and the tools depend on internally.

## Spawnset files

Spawnsets consist of an arena, a set of spawns, practice values, and some other small features. All features are supported in the `DevilDaggersInfo.Core.Spawnset` library.

## Replay files

Devil Daggers replay files can be interpreted by the `DevilDaggersInfo.Core.Replay` library. The library can also create replay files. The library understands almost all replay data and is still in development.

## Mod files

Mods can be created for Devil Daggers using the `DevilDaggersInfo.Core.Mod` library. The library can extract all the original Devil Daggers assets, and also recompile custom assets into mods.

Audio (.wav), meshes (.obj), object bindings (text), GLSL shaders (text), and textures (.png) are supported.

Particle files are not supported (yet).

## Reading game memory

The tools app provides a way to read live game memory from the game in real time. This is primarily used for custom leaderboards, but it can also be used for other purposes, such as for practice or understanding game mechanics.

## Wiki data

Mainly exposes information about the game, such as information about enemies, death types, upgrades, daggers, and older game versions.
