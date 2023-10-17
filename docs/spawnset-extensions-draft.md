# Spawnset Extensions

Spawnset extensions are mods defined by the community. The purpose of this document is to describe the format for these extensions.

The purpose of these extensions is mainly that it should theoretically be possible for Devil Daggers to implement these extensions in the future. Of course this is up to the developer.

To make implementing these extensions as flexible as possible, the format allows skipping over unsupported extensions. Anyone writing a spawnset parser or Devil Daggers clone, as well as the developer, can choose to support as many or as few extensions as they want.

Each extension consists of a globally unique extension id, followed by extension-specific data. To allow skipping over unsupported extensions, the length of the data buffer must be included as well.

Extensions are **immutable**, meaning that once they are accepted, they can never change. If a change is needed, a new extension with a new unique id must be created.

When multiple conflicting extensions are used (for example, two extensions that both change the dagger speed), they are applied in the order they are defined in the file. The last one will overwrite the previous data (individually per field).

## Format

### Header

| Data type | Name                      | Remarks                                                                                                             |
|-----------|---------------------------|---------------------------------------------------------------------------------------------------------------------|
| `i32`     | Extensions format version | If any breaking changes occur, this number will be incremented. The default and only valid version now is `1`.      |
| `i32`     | Extension count           | The amount of spawnset extensions present. Can be used to skip over the entire spawnset extension buffer if needed. |

### Extension list

| Data type | Name                               | Remarks                                  |
|-----------|------------------------------------|------------------------------------------|
| `i32`     | Extension id                       | The globally unique id of the extension. |
| `i32`     | Extension data length              | The length of the extension data buffer. |
| N/A       | Extension data                     | Extension-specific data.                 |

### Extensions

#### Player extension

ID: 1

| Data type   | Name                   | Remarks                                                            | Default value |
|-------------|------------------------|--------------------------------------------------------------------|---------------|
| `u8 (bool)` | Enable movement        | If false, the player cannot move.                                  | True          |
| `u8 (bool)` | Enable jumping         | If false, the player cannot jump.                                  | True          |
| `u8 (bool)` | Enable shooting        | If false, the player cannot shoot normal daggers.                  | True          |
| `u8 (bool)` | Enable shooting homing | If false, the player cannot shoot homing daggers.                  | True          |
| `f32`       | Player spawn X         | The player's spawn position X (works the same as the race dagger). | 0             |
| `f32`       | Player spawn Z         | The player's spawn position Z (works the same as the race dagger). | 0             |

#### Upgrade extension

ID: 2

| Data type   | Name                      | Remarks                                                            | Default value |
|-------------|---------------------------|--------------------------------------------------------------------|---------------|
| `i32`       | Gems needed for Level 2   | The amount of gems needed to upgrade to level 2.                   | 10            |
| `i32`       | Gems needed for Level 3   | The amount of gems needed to upgrade to level 3.                   | 70            |
| `i32`       | Homing needed for Level 4 | The amount of Level 3 homing daggers needed to upgrade to level 4. | 150           |

#### Shot extension

ID: 3

| Data type | Name        -                    | Remarks                                               | Default value |
|-----------|----------------------------------|-------------------------------------------------------|---------------|
| `i32`     | Level 1 normal dagger shot count | The amount of normal daggers shot using Level 1 hand. | 10            |
| `i32`     | Level 2 normal dagger shot count | The amount of normal daggers shot using Level 2 hand. | 20            |
| `i32`     | Level 3 normal dagger shot count | The amount of normal daggers shot using Level 3 hand. | 40            |
| `i32`     | Level 4 normal dagger shot count | The amount of normal daggers shot using Level 4 hand. | 60            |
| `i32`     | Level 3 homing dagger shot count | The amount of homing daggers shot using Level 3 hand. | 20            |
| `i32`     | Level 4 homing dagger shot count | The amount of homing daggers shot using Level 4 hand. | 30            |

#### Tile shrink extension

ID: 4

| Data type | Name       | Remarks                      | Default value |
|-----------|------------|------------------------------|---------------|
| `i32`     | Tile count | The amount of tiles changed. | 0             |

Per tile:

| Data type | Name                                 | Remarks                                                          | Default value |
|-----------|--------------------------------------|------------------------------------------------------------------|---------------|
| `i8`      | Tile X coordinate                    | Signed tile X coordinate (-128 to 127)                           | 0             |
| `i8`      | Tile Z coordinate                    | Signed tile Z coordinate (-128 to 127)                           | 0             |
| `f32`     | Tile shrink start time               | The time at which the tile starts to shrink.                     | 0             |
| `f32`     | Tile shrink fall time                | The time at which the tile starts to rapidly fall into the void. | 0             |
| `f32`     | Tile shrink speed (units per second) | The speed at which the tile shrinks.                             | 4 (?)         |

- rapid extension
  - need to think about how to do this
- squid extension
  - gem hp
    - normal dagger
    - lvl3homing
    - lvl4homing
    - splash
  - skull gush count
  - speed?
- pede extension
  - gem hp
    - normal dagger
    - lvl3homing
    - lvl4homing
    - splash
  - segment count
  - speed?
- levi extension
  - beckon interval
  - gem hp
    - normal dagger
    - lvl3homing
    - lvl4homing
    - splash
- orb extension
  - hp
	- normal dagger
	- lvl3homing
	- lvl4homing
	- splash
  - speed?
- enemy gem drop on kill mod
