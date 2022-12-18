using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using DevilDaggersInfo.Core.Spawnset;
using DevilDaggersInfo.Types.Core.Replays;

namespace DevilDaggersInfo.Core.Replay.PostProcessing.ReplaySimulation;

public static class ReplaySimulationBuilder
{
	public static ReplaySimulation Build(SpawnsetBinary spawnset, ReplayBinary<LocalReplayBinaryHeader> replay)
	{
		// Player movement constants
		const float moveSpeed = 11.676f / 60f;
		const float gravityForce = 0.16f / 60f;

		float spawnTileHeight = spawnset.ArenaTiles[SpawnsetBinary.ArenaDimensionMax / 2, SpawnsetBinary.ArenaDimensionMax / 2];

		Quaternion rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathF.PI);
		Vector3 position = new(0, spawnTileHeight, 0);

		int ticks = 0;
		float velocityY = 0;
		float velocityX = 0;
		float velocityZ = 0;
		float gravity = 0;
		float speedBoost = 1;
		int jumpCooldown = 0;

		InitialInputsEvent initialInputsEvent = (InitialInputsEvent?)replay.EventsData.Events.FirstOrDefault(e => e is InitialInputsEvent) ?? throw new InvalidOperationException("Replay does not contain an initial inputs event.");
		float lookSpeed = initialInputsEvent.LookSpeed;

		List<PlayerMovementSnapshot> playerMovementSnapshots = new() { new(rotation, position, true) };
		List<SoundSnapshot> soundSnapshots = new();

		foreach (IEvent e in replay.EventsData.Events)
		{
			bool isOnGround = false;
			switch (e)
			{
				case EntityPositionEvent { EntityId: 0 } entityPositionEvent:
				{
					const float divisor = 16f;
					position = new()
					{
						X = entityPositionEvent.Position.X / divisor,
						Y = entityPositionEvent.Position.Y / divisor,
						Z = entityPositionEvent.Position.Z / divisor,
					};
					break;
				}

				case IInputsEvent inputs:
				{
					// Orientation
					float yaw = lookSpeed * -inputs.MouseX;
					float pitch = lookSpeed * inputs.MouseY;
					pitch = Math.Clamp(pitch, ToRadians(-89.999f), ToRadians(89.999f));

					rotation *= Quaternion.CreateFromYawPitchRoll(yaw, -pitch, 0);

					// Position

					// Jumping

					// TODO: Dagger-jumping.

					// Find the highest of all 4 tiles.
					const float playerSize = 1f; // Guess
					float topLeft = GetTileHeightAtWorldPosition(position.X - playerSize, position.Z - playerSize);
					float topRight = GetTileHeightAtWorldPosition(position.X + playerSize, position.Z - playerSize);
					float bottomLeft = GetTileHeightAtWorldPosition(position.X - playerSize, position.Z + playerSize);
					float bottomRight = GetTileHeightAtWorldPosition(position.X + playerSize, position.Z + playerSize);

					float GetTileHeightAtWorldPosition(float positionX, float positionZ)
					{
						int arenaX = spawnset.WorldToTileCoordinate(positionX);
						int arenaZ = spawnset.WorldToTileCoordinate(positionZ);
						return arenaX is < 0 or > SpawnsetBinary.ArenaDimensionMax - 1 || arenaZ is < 0 or > SpawnsetBinary.ArenaDimensionMax - 1 ? -1000 : spawnset.GetActualTileHeight(arenaX, arenaZ, ticks / 60f);
					}

					float currentTileHeight = Math.Max(Math.Max(topLeft, topRight), Math.Max(bottomLeft, bottomRight));
					isOnGround = position.Y <= currentTileHeight;

					if (isOnGround)
					{
						position.Y = currentTileHeight;

						gravity = 0;
						velocityY = 0;

						if (jumpCooldown <= 0 && inputs.Jump is JumpType.StartedPress or JumpType.Hold)
						{
							// TODO: Use Jump2 when jump was not precise.
							jumpCooldown = 10; // Guess
							velocityY = 0.35f; // Guess
							speedBoost = 1.5f; // Guess
							// ReplaySound replaySound = ReplaySound.Jump3;
							// soundSnapshots.Add(new(ticks, replaySound, position));
						}
					}
					else
					{
						gravity -= gravityForce;
						velocityY += gravity;
					}

					speedBoost += (1 - speedBoost) / 10f; // Guess
					jumpCooldown--;

					// WASD movement
					const float acceleration = 0.1f; // Guess
					const float friction = 10f; // Guess
					const float airAcceleration = 0.01f; // Guess
					const float airFriction = 100f; // Guess
					if (inputs.Right)
						velocityX -= isOnGround ? acceleration : airAcceleration;
					else if (inputs.Left)
						velocityX += isOnGround ? acceleration : airAcceleration;
					else
						velocityX -= velocityX / (isOnGround ? friction : airFriction);

					if (inputs.Forward)
						velocityZ += isOnGround ? acceleration : airAcceleration;
					else if (inputs.Backward)
						velocityZ -= isOnGround ? acceleration : airAcceleration;
					else
						velocityZ -= velocityZ / (isOnGround ? friction : airFriction);

					velocityX = Math.Clamp(velocityX, -speedBoost, speedBoost);
					velocityZ = Math.Clamp(velocityZ, -speedBoost, speedBoost);

					// Update state
					Vector3 axisAlignedMovement = new(velocityX, velocityY, velocityZ);
					Matrix4x4 rotMat = Matrix4x4.CreateFromQuaternion(rotation);
					Vector3 transformed = RotateVector(axisAlignedMovement, rotMat) + new Vector3(0, axisAlignedMovement.Y, 0);
					position += transformed * moveSpeed;

					static Vector3 RotateVector(Vector3 vector, Matrix4x4 rotationMatrix)
					{
						Vector3 right = new(rotationMatrix.M11, rotationMatrix.M12, rotationMatrix.M13);
						Vector3 forward = -Vector3.Cross(Vector3.UnitY, right);
						return right * vector.X + forward * vector.Z;
					}

					ticks++;
					break;
				}

				default: continue;
			}

			playerMovementSnapshots.Add(new(rotation, position, isOnGround));
		}

		return new(playerMovementSnapshots, soundSnapshots);
	}

	private static float ToRadians(float degrees) => degrees * (MathF.PI / 180f);
}