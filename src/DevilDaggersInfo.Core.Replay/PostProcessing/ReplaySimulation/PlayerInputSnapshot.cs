using DevilDaggersInfo.Core.Replay.Events.Enums;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.ReplaySimulation;

public record struct PlayerInputSnapshot(bool Left, bool Right, bool Forward, bool Backward, JumpType Jump, ShootType Shoot, ShootType ShootHoming, short MouseX, short MouseY);
