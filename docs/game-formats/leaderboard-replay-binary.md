# Leaderboard replay binary

| Data type                         | Size in bytes                | Meaning                      | Default value        |
|-----------------------------------|------------------------------|------------------------------|----------------------|
| -                                 | 7                            | Identifier                   | DF_RPL2              |
| int16                             | 2                            | Username length              | N/A (depends on run) |
| UTF-8 (? TODO)                    | Username length              | Username                     | N/A (depends on run) |
| int16                             | 2                            | ? length                     | ?                    |
| ?                                 | ? length                     | ?                            | ?                    |
| [Replay events](replay-events.md) | Compressed event data length | Compressed event data        | N/A (depends on run) |
