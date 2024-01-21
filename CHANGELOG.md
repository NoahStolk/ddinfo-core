# Changelog

This library uses [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## 0.9.1

### Added

- Support for loudness values has been added. A loudness asset is now automatically generated if needed.
- Added `float? loudnessValue` parameter to `AudioModBinaryBuilder.AddAudio` method.

### Changed

- Loudness file is now extracted to `loudness.ini` instead of `loudness.wav`.

## 0.9.0

Rewrote the Mod library. The API is now much more stable and easier to use.

### Added

- Added `AssetExtractionResult` record which is used to return the result of an asset extraction.
  - This allows for proper shader extraction where a single asset contains multiple files.
  - The `ModBinary.ExtractAsset` methods now return an instance of this type.

### Changed

- Renamed "chunks" to "TOC entries".
  - Renamed `ModBinaryChunk` to `ModBinaryTocEntry`.
  - Renamed `ModBinaryBuilder.Chunks` to `ModBinaryBuilder.TocEntries`.
  - Renamed `ModBinaryToc.Chunks` to `ModBinaryToc.Entries`.
- `ModBinaryBuilder` is now abstract.
  - The `ModBinaryBuilder.AddAsset` method has been removed.
  - Use the `AudioModBinaryBuilder` and `DdModBinaryBuilder` classes to create audio and dd mods respectively.
  - The `AudioModBinaryBuilder` class exposes an `AddAudio` method.
  - The `DdModBinaryBuilder` class exposes `AddMesh`, `AddObjectBinding`, `AddShader`, and `AddTexture` methods.

### Removed

- Removed `AssetTypeExtensions.GetFileExtension` and `AssetTypeExtensions.ToDisplayString` methods.
- Removed `AssetConverter` class.

## 0.8.1

### Added

- Added `IEventData.CloneEventData` extension method.

## 0.8.0

### Changed

- Upgraded to .NET 8.0.

## 0.7.6

### Added

- Added `CreateDefault` methods to all event data types.

### Changed

- Updated `SixLabors.ImageSharp` from 3.0.2 to 3.1.2.

## 0.7.5

### Changed

- `EnablePreviewFeatures` has been turned off since we don't use any preview features.

## 0.7.4

### Added

- Added `ReplayEventsData.GetEntityTypeIncludingNegated` method, which tries to resolve negated pede entity IDs as well. These are used in `HitEventData` when a dead pede segment is hit.

### Changed

- `ReplayEventsData.GetEntityType` now returns `null` instead of throwing an `ArgumentOutOfRangeException` when the entity type is not found.

## 0.7.3

### Removed

- Removed `EntityTypeExtensions.GetColor` method.

## 0.7.2

### Removed

- Removed `DeathEventData`. This is now simply parsed as `HitEventData`.

## 0.7.1

### Fixed

- Fixed not updating referring entity IDs when inserting or removing spawn events. Entity IDs that no longer refer to an existing entity are set to -1. This applies to:
  - `BoidSpawnEventData.SpawnerEntityId`
  - `EntityOrientationEventData.EntityId`
  - `EntityPositionEventData.EntityId`
  - `EntityTargetEventData.EntityId`
  - `HitEventData.EntityIdA`
  - `HitEventData.EntityIdB`
  - `SpiderEggSpawnEventData.SpawnerEntityId`
  - `TransmuteEventData.EntityId`

## 0.7.0

Working with replay events has been rewritten. All problems have been fixed and the API is now mostly stable.

### Added

- Added `ReplayEventsData.SpawnEventCount` property.
- Added `ReplayEventsData.GetEntityType` method.
- Added `ReplayEvent` and `EntitySpawnReplayEvent` records. These types have `internal` constructors and can only be created internally.

### Changed

- All event structures have been renamed to end with 'Data', for example `BoidSpawnEvent` is now `BoidSpawnEventData`.
- All event structures have been moved to the `DevilDaggersInfo.Core.Replay.Events.Data` namespace.
- Event structures no longer contain data that is not written to the replay events buffer. This means that the `EntityId` property has been removed from all event structures.
- `IEvent` interface has been replaced with `IEventData` interface.
- `IEntitySpawnEvent` interface has been replaced with `ISpawnEventData` interface.
- Event structures are now wrapped in `ReplayEvent` or `EntitySpawnReplayEvent` records. The `EntityId` property is now stored in `EntitySpawnReplayEvent`.
- `ReplayEventsData.AddEvent` and `ReplayEventsData.InsertEvent` now take an `IEventData` instead of what was previously an `IEvent`.
- `ReplayEventsData.Events` is now of type `IReadOnlyList<ReplayEvent>`. This type is now used in various other places as well.

### Removed

- Removed `ReplayEventsData.ChangeEntityType` method. This was a temporary method that is no longer needed.
- Removed `ReplayEventsData.EntityTypes` property. You can now use the `ReplayEventsData.GetEntityType` method instead.

## 0.6.0

Replays can now be edited. This API is still a work in progress and currently has a couple problems which will be fixed later. See the remarks in the `ReplayEventsData` class for more information.

### Added

- Added `ReplayEventsData.InsertEvent` and `ReplayEventsData.RemoveEvent` methods.
- Added `ReplayEventsData.ChangeEntityType` method. This is a temporary method that will be removed in the future.

### Changed

- Spawn events now have a public setter for `EntityId`. This should never be used and will be removed in the future.
- `ReplayEventsData.AddEvent` now overwrites the `EntityId` to be correct. This will change in the future in a way that doesn't let you pass an `EntityId` to this method at all.

## 0.5.0

### Changed

- Replay events are now reference types instead of value types. They were already being boxed, so this change should not affect performance. This fixes some issues where the values would be copied and couldn't be edited directly.

## 0.4.0

### Changed

- Replay events are no longer immutable.
- Most replay event fields are now fields instead of properties. Values that are not supposed to be edited (in other words, are not written to the replay events buffer) are still properties.

### Removed

- Removed `IInputsEvent` interface.

## 0.3.0

### Changed

- Renamed `AssetContainer.GetIsProhibited` to `AssetContainer.IsProhibited`.

### Fixed

- Fixed allocating memory when calling `AssetContainer.IsProhibited`.

## 0.2.0

### Added

- Added `ModBinaryToc.EnableAllAssets` and `ModBinaryToc.DisableProhibitedAssets` methods.

## 0.1.1

### Fixed

- Fixed not including `SixLabors.ImageSharp` dependency in the NuGet package.

## 0.1.0

- Initial public release. This library has already been in development for a long time, just without a version number. It is still not considered finished and breaking changes will happen until 1.0.0 releases.
