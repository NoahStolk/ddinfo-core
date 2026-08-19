using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.HitLog;

public sealed record EnemyHitLog(int EntityId, EntityType EntityType, int SpawnTick, IReadOnlyList<EnemyHitLogEvent> Hits);
