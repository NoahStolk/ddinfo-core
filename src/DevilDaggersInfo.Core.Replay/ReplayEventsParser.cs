using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.IO.Compression;

namespace DevilDaggersInfo.Core.Replay;

public static class ReplayEventsParser
{
	public static ReplayEventsData Parse(byte[] compressedEvents)
	{
		ReplayEventsData eventsData = new();

		using MemoryStream ms = new(compressedEvents[2..]); // Skip ZLIB header.
		using DeflateStream deflateStream = new(ms, CompressionMode.Decompress, true);
		using BinaryReader br = new(deflateStream);

		bool parsedInitialInput = false;

		while (true)
		{
			byte eventType = br.ReadByte();
			IEventData e = eventType switch
			{
				0x00 => ParseEntitySpawnEvent(br),
				0x01 => ParseEntityPositionEvent(br),
				0x02 => ParseEntityOrientationEvent(br),
				0x04 => ParseEntityTargetEvent(br),
				0x05 => ParseHitEvent(br),
				0x06 => new GemEvent(),
				0x07 => ParseTransmuteEvent(br),
				0x09 => parsedInitialInput ? ParseInputsEvent(br) : ParseInitialInputsEvent(br),
				0x0b => new EndEvent(),
				_ => throw new InvalidReplayBinaryException($"Invalid event type '{eventType}'."),
			};
			eventsData.AddEvent(e);

			if (e is InitialInputsEvent)
				parsedInitialInput = true;

			if (e is EndEvent)
				break;
		}

		return eventsData;
	}

	private static ISpawnEventData ParseEntitySpawnEvent(BinaryReader br)
	{
		byte entityType = br.ReadByte();
		return entityType switch
		{
			0x01 => ParseDaggerSpawnEvent(br),
			0x03 => ParseSquidSpawnEvent(br, SquidType.Squid1),
			0x04 => ParseSquidSpawnEvent(br, SquidType.Squid2),
			0x05 => ParseSquidSpawnEvent(br, SquidType.Squid3),
			0x06 => ParseBoidSpawnEvent(br),
			0x07 => ParsePedeSpawnEvent(br, PedeType.Centipede),
			0x0c => ParsePedeSpawnEvent(br, PedeType.Gigapede),
			0x0f => ParsePedeSpawnEvent(br, PedeType.Ghostpede),
			0x08 => ParseSpiderSpawnEvent(br, SpiderType.Spider1),
			0x09 => ParseSpiderSpawnEvent(br, SpiderType.Spider2),
			0x0a => ParseSpiderEggSpawnEvent(br),
			0x0b => ParseLeviathanSpawnEvent(br),
			0x0d => ParseThornSpawnEvent(br),
			_ => throw new InvalidReplayBinaryException($"Invalid entity type '{entityType}'."),
		};
	}

	private static EntityPositionEvent ParseEntityPositionEvent(BinaryReader br)
	{
		int entityId = br.ReadInt32();
		Int16Vec3 position = br.ReadInt16Vec3();
		return new(entityId, position);
	}

	private static EntityOrientationEvent ParseEntityOrientationEvent(BinaryReader br)
	{
		int entityId = br.ReadInt32();
		Int16Mat3x3 orientation = br.ReadInt16Mat3x3();
		return new(entityId, orientation);
	}

	private static EntityTargetEvent ParseEntityTargetEvent(BinaryReader br)
	{
		int entityId = br.ReadInt32();
		Int16Vec3 targetPosition = br.ReadInt16Vec3();
		return new(entityId, targetPosition);
	}

	private static IEventData ParseHitEvent(BinaryReader br)
	{
		int entityIdA = br.ReadInt32();
		if (entityIdA == 0)
			return ParseDeathEvent(br);

		int entityIdB = br.ReadInt32();
		int userData = br.ReadInt32();
		return new HitEvent(entityIdA, entityIdB, userData);
	}

	private static DeathEvent ParseDeathEvent(BinaryReader br)
	{
		int deathType = br.ReadInt32();
		_ = br.ReadInt32();
		return new(deathType);
	}

	private static TransmuteEvent ParseTransmuteEvent(BinaryReader br)
	{
		int entityId = br.ReadInt32();
		return new(entityId, br.ReadInt16Vec3(), br.ReadInt16Vec3(), br.ReadInt16Vec3(), br.ReadInt16Vec3());
	}

	private static InputsEvent ParseInputsEvent(BinaryReader br)
	{
		bool left = br.ReadBoolean();
		bool right = br.ReadBoolean();
		bool forward = br.ReadBoolean();
		bool backward = br.ReadBoolean();

		byte jumpTypeByte = br.ReadByte();
		JumpType jumpType = jumpTypeByte.ToJumpType();

		byte shootTypeByte = br.ReadByte();
		ShootType shootType = shootTypeByte.ToShootType();

		byte shootTypeByteHoming = br.ReadByte();
		ShootType shootTypeHoming = shootTypeByteHoming.ToShootType();

		short mouseX = br.ReadInt16();
		short mouseY = br.ReadInt16();

		byte end = br.ReadByte();
		const byte expectedEnd = 0x0a;
		if (end != expectedEnd)
			throw new InvalidReplayBinaryException($"Invalid end of inputs event. Should be {expectedEnd} but got {end}.");

		return new(left, right, forward, backward, jumpType, shootType, shootTypeHoming, mouseX, mouseY);
	}

	private static InitialInputsEvent ParseInitialInputsEvent(BinaryReader br)
	{
		bool left = br.ReadBoolean();
		bool right = br.ReadBoolean();
		bool forward = br.ReadBoolean();
		bool backward = br.ReadBoolean();

		byte jumpTypeByte = br.ReadByte();
		JumpType jumpType = jumpTypeByte.ToJumpType();

		byte shootTypeByte = br.ReadByte();
		ShootType shootType = shootTypeByte.ToShootType();

		byte shootTypeByteHoming = br.ReadByte();
		ShootType shootTypeHoming = shootTypeByteHoming.ToShootType();

		short mouseX = br.ReadInt16();
		short mouseY = br.ReadInt16();
		float lookSpeed = br.ReadSingle();

		byte end = br.ReadByte();
		const byte expectedEnd = 0x0a;
		if (end != expectedEnd)
			throw new InvalidReplayBinaryException($"Invalid end of inputs event. Should be {expectedEnd} but got {end}.");

		return new(left, right, forward, backward, jumpType, shootType, shootTypeHoming, mouseX, mouseY, lookSpeed);
	}

	private static DaggerSpawnEvent ParseDaggerSpawnEvent(BinaryReader br)
	{
		int a = br.ReadInt32(); // Always 0
		Int16Vec3 position = br.ReadInt16Vec3();
		Int16Mat3x3 orientation = br.ReadInt16Mat3x3();
		bool isShot = br.ReadBoolean();
		byte daggerTypeByte = br.ReadByte();
		DaggerType daggerType = daggerTypeByte.ToDaggerType();

		return new(a, position, orientation, isShot, daggerType);
	}

	private static SquidSpawnEvent ParseSquidSpawnEvent(BinaryReader br, SquidType squidType)
	{
		int a = br.ReadInt32();
		Vector3 position = br.ReadVector3();
		Vector3 direction = br.ReadVector3();
		float rotationInRadians = br.ReadSingle();

		return new(squidType, a, position, direction, rotationInRadians);
	}

	private static BoidSpawnEvent ParseBoidSpawnEvent(BinaryReader br)
	{
		int spawner = br.ReadInt32();
		byte boidTypeByte = br.ReadByte();
		Int16Vec3 position = br.ReadInt16Vec3();
		Int16Mat3x3 orientation = br.ReadInt16Mat3x3();
		Vector3 velocity = br.ReadVector3();
		float speed = br.ReadSingle();
		BoidType boidType = boidTypeByte.ToBoidType();

		return new(spawner, boidType, position, orientation, velocity, speed);
	}

	private static PedeSpawnEvent ParsePedeSpawnEvent(BinaryReader br, PedeType pedeType)
	{
		int a = br.ReadInt32();
		Vector3 position = br.ReadVector3();
		Vector3 b = br.ReadVector3();
		Matrix3x3 orientation = br.ReadMatrix3x3();

		return new(pedeType, a, position, b, orientation);
	}

	private static SpiderSpawnEvent ParseSpiderSpawnEvent(BinaryReader br, SpiderType spiderType)
	{
		int a = br.ReadInt32();
		Vector3 position = br.ReadVector3();

		return new(spiderType, a, position);
	}

	private static SpiderEggSpawnEvent ParseSpiderEggSpawnEvent(BinaryReader br)
	{
		int spawnerEntityId = br.ReadInt32();
		Vector3 position = br.ReadVector3(); // Not sure
		Vector3 targetPosition = br.ReadVector3(); // Not sure

		return new(spawnerEntityId, position, targetPosition);
	}

	private static LeviathanSpawnEvent ParseLeviathanSpawnEvent(BinaryReader br)
	{
		int a = br.ReadInt32();
		return new(a);
	}

	private static ThornSpawnEvent ParseThornSpawnEvent(BinaryReader br)
	{
		int a = br.ReadInt32();
		Vector3 position = br.ReadVector3(); // Not sure
		float rotationInRadians = br.ReadSingle(); // Not sure
		return new(a, position, rotationInRadians);
	}
}
