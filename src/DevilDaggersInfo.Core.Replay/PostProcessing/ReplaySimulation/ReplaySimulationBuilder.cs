using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Spawnset;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.ReplaySimulation;

// TODO: Move to ddinfo-tools.
public static class ReplaySimulationBuilder
{
	public static ReplaySimulation Build(ReplayBinary<LocalReplayBinaryHeader> replay)
	{
		ReplayEvent initialInputsEvent = replay.Events.FirstOrDefault(e => e.Data is InitialInputsEventData) ?? throw new InvalidOperationException("Replay does not contain an initial inputs event.");
		float lookSpeed = ((InitialInputsEventData)initialInputsEvent.Data).LookSpeed;

		int ticks = 0;
		SpawnsetBinary spawnset = replay.Header.Spawnset;
		PlayerContext playerContext = new(spawnset.ArenaTiles[SpawnsetBinary.ArenaDimensionMax / 2, SpawnsetBinary.ArenaDimensionMax / 2]);

		List<PlayerMovementSnapshot> playerMovementSnapshots = [new(playerContext.Rotation, playerContext.Position, true)];
		List<PlayerInputSnapshot> playerInputSnapshots = [];
		List<SoundSnapshot> soundSnapshots = [];

		foreach (ReplayEvent e in replay.Events)
		{
			switch (e.Data)
			{
				case EntityPositionEventData { EntityId: 0 } entityPositionEvent:
				{
					const float divisor = 16f;
					playerContext.Position = new()
					{
						X = entityPositionEvent.Position.X / divisor,
						Y = entityPositionEvent.Position.Y / divisor,
						Z = entityPositionEvent.Position.Z / divisor,
					};
					break;
				}

				case InputsEventData or InitialInputsEventData:
				{
					PlayerInputSnapshot inputSnapshot = e.Data switch
					{
						InputsEventData ie => new(ie.Left, ie.Right, ie.Forward, ie.Backward, ie.Jump, ie.Shoot, ie.ShootHoming, ie.MouseX, ie.MouseY),
						InitialInputsEventData iie => new(iie.Left, iie.Right, iie.Forward, iie.Backward, iie.Jump, iie.Shoot, iie.ShootHoming, iie.MouseX, iie.MouseY),
						_ => throw new UnreachableException(),
					};
					ProcessInputs(spawnset, lookSpeed, inputSnapshot, playerContext, ticks);
					playerInputSnapshots.Add(inputSnapshot);
					ticks++;
					break;
				}

				default: continue;
			}

			playerMovementSnapshots.Add(new(playerContext.Rotation, playerContext.Position, playerContext.IsOnGround));
		}

		return new(playerMovementSnapshots, playerInputSnapshots, soundSnapshots);
	}

	private static void ProcessInputs(SpawnsetBinary spawnset, float lookSpeed, PlayerInputSnapshot inputSnapshot, PlayerContext playerContext, int ticks)
	{
		// Player movement constants
		const float velocityEpsilon = 0.01f;
		const float moveSpeed = 11.676f / 60f;
		const float gravityForce = 0.016f / 60f;

		// Orientation
		float yaw = lookSpeed * -inputSnapshot.MouseX;
		float pitch = lookSpeed * inputSnapshot.MouseY;
		pitch = Math.Clamp(pitch, ToRadians(-89.999f), ToRadians(89.999f));

		playerContext.Rotation *= Quaternion.CreateFromYawPitchRoll(yaw, -pitch, 0);

		// Position

		// Jumping

		// TODO: Dagger-jumping.

		// Find the highest of all 4 tiles.
		const float playerSize = 1f; // Guess
		float topLeft = GetTileHeightAtWorldPosition(playerContext.Position.X - playerSize, playerContext.Position.Z - playerSize);
		float topRight = GetTileHeightAtWorldPosition(playerContext.Position.X + playerSize, playerContext.Position.Z - playerSize);
		float bottomLeft = GetTileHeightAtWorldPosition(playerContext.Position.X - playerSize, playerContext.Position.Z + playerSize);
		float bottomRight = GetTileHeightAtWorldPosition(playerContext.Position.X + playerSize, playerContext.Position.Z + playerSize);

		float GetTileHeightAtWorldPosition(float positionX, float positionZ)
		{
			int arenaX = spawnset.WorldToTileCoordinate(positionX);
			int arenaZ = spawnset.WorldToTileCoordinate(positionZ);
			return arenaX is < 0 or > SpawnsetBinary.ArenaDimensionMax - 1 || arenaZ is < 0 or > SpawnsetBinary.ArenaDimensionMax - 1 ? -1000 : spawnset.GetActualTileHeight(arenaX, arenaZ, ticks / 60f);
		}

		float currentTileHeight = Math.Max(Math.Max(topLeft, topRight), Math.Max(bottomLeft, bottomRight));
		playerContext.IsOnGround = playerContext.Position.Y <= currentTileHeight;

		if (playerContext.IsOnGround)
		{
			playerContext.Position = playerContext.Position with { Y = currentTileHeight };

			playerContext.Gravity = 0;
			playerContext.VelocityY = 0;
			playerContext.Velocity *= 57 * (1 / 60f);

			if (playerContext.JumpCooldown <= 0 && inputSnapshot.Jump is JumpType.StartedPress or JumpType.Hold)
			{
				playerContext.JumpCooldown = 10; // Guess
				playerContext.VelocityY = 0.1f; // Guess
				playerContext.SpeedBoost = 1.5f; // Guess

				// TODO: Use Jump2 when jump was not precise.
				// ReplaySound replaySound = ReplaySound.Jump3;
				// soundSnapshots.Add(new(ticks, replaySound, position));
			}
		}
		else
		{
			playerContext.Gravity -= gravityForce;
			playerContext.VelocityY += playerContext.Gravity;
		}

		playerContext.SpeedBoost += (1 - playerContext.SpeedBoost) / 10f; // Guess
		playerContext.JumpCooldown--;

		// WASD movement
		Vector2 GetWishDirection()
		{
			Vector3 axisAlignedWishDirection = new(Convert.ToInt32(inputSnapshot.Left) - Convert.ToInt32(inputSnapshot.Right), 0, Convert.ToInt32(inputSnapshot.Forward) - Convert.ToInt32(inputSnapshot.Backward));
			Vector3 wishDirection3d = Vector3.Transform(axisAlignedWishDirection, Matrix4x4.CreateFromQuaternion(playerContext.Rotation));
			Vector2 wishDirection = new(wishDirection3d.X, wishDirection3d.Z);

			return axisAlignedWishDirection.Length() < velocityEpsilon ? Vector2.Zero : Vector2.Normalize(wishDirection);
		}

		Vector2 wishDirection = GetWishDirection();

		float horizontalSpeed = playerContext.Velocity.Length();

		// TODO: When switching directions quickly, decrease accelerationAir by a lot. Air control should be controllable but this control should be lost when changing direction quickly.
		bool onlyLeft = inputSnapshot is { Left: true, Right: false };
		bool onlyRight = inputSnapshot is { Right: true, Left: false };
		bool onlyForward = inputSnapshot is { Forward: true, Backward: false };
		bool onlyBackward = inputSnapshot is { Backward: true, Forward: false };
		bool isStrafing = onlyLeft && onlyForward || onlyLeft && onlyBackward || onlyRight && onlyForward || onlyRight && onlyBackward;
		float actualMoveSpeed = isStrafing ? moveSpeed * 1.41f : moveSpeed;
		float addSpeed = Math.Clamp(actualMoveSpeed - horizontalSpeed, 0, 1 / 60f);
		playerContext.Velocity += addSpeed * wishDirection;
		playerContext.Position += new Vector3(playerContext.Velocity.X, playerContext.VelocityY, playerContext.Velocity.Y);
	}

	private static float ToRadians(float degrees)
	{
		return degrees * (MathF.PI / 180f);
	}

	private sealed class PlayerContext
	{
		public PlayerContext(float spawnHeight)
		{
			Position = new(0, spawnHeight, 0);
			Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathF.PI);
			IsOnGround = false;
			VelocityY = 0;
			Velocity = default;
			Gravity = 0;
			SpeedBoost = 1;
			JumpCooldown = 0;
		}

		public Quaternion Rotation { get; set; }
		public Vector3 Position { get; set; }
		public bool IsOnGround { get; set; }
		public float VelocityY { get; set; }
		public Vector2 Velocity { get; set; }
		public float Gravity { get; set; }
		public float SpeedBoost { get; set; }
		public int JumpCooldown { get; set; }
	}
}
