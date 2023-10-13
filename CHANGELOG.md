# Changelog

This library uses [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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
