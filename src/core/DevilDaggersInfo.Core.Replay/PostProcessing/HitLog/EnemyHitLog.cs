using DevilDaggersInfo.Types.Core.Replays;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.HitLog;

public record EnemyHitLog(int EntityId, EntityType EntityType, int SpawnTick, IReadOnlyList<EnemyHitLogEvent> Hits);