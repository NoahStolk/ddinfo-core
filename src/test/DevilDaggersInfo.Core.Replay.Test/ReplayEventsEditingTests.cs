using DevilDaggersInfo.Core.Replay.Events.Data;
using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Numerics;
using System.Numerics;

namespace DevilDaggersInfo.Core.Replay.Test;

[TestClass]
public class ReplayEventsEditingTests
{
	private const int _eventCount = 72;
	private const int _entityCount = 5;
	private const int _tickCount = 67;

	private readonly ReplayBinary<LocalReplayBinaryHeader> _replay;

	public ReplayEventsEditingTests()
	{
		string replayFilePath = Path.Combine("Resources", "SkullTest.ddreplay");
		byte[] replayBuffer = File.ReadAllBytes(replayFilePath);
		_replay = new(replayBuffer);

		// Check initial events.
		Assert.AreEqual(_eventCount, _replay.EventsData.Events.Count);

		// Check initial entity types.
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);
		ValidateOriginalEntityTypes();

		// Check initial event offsets per tick.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		ValidateOriginalTicks(_tickCount);

		// Check initial entity IDs.
		ValidateOriginalEntityIds();
	}

	private static void AssertEntityId<TEvent>(ReplayEvent e, int expectedEntityId)
		where TEvent : ISpawnEventData
	{
		Assert.IsInstanceOfType<EntitySpawnReplayEvent>(e);
		Assert.IsInstanceOfType<TEvent>(e.Data);
		Assert.AreEqual(expectedEntityId, ((EntitySpawnReplayEvent)e).EntityId);
	}

	private static void AssertSpawnerEntityId(ReplayEvent e, int expectedSpawnerEntityId)
	{
		Assert.IsInstanceOfType<BoidSpawnEventData>(e.Data);
		Assert.AreEqual(expectedSpawnerEntityId, ((BoidSpawnEventData)e.Data).SpawnerEntityId);
	}

	private void ValidateOriginalEntityTypes()
	{
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.GetEntityType(1));
		for (int i = 2; i < _entityCount; i++)
			Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(i));
	}

	private void ValidateOriginalTicks(int count)
	{
		int expectedOffset = 0;
		for (int i = 0; i < count; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	private void ValidateOriginalEntityIds()
	{
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[24], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[45], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[66], 5);

		// All Skulls should be spawned by the Squid.
		AssertSpawnerEntityId(_replay.EventsData.Events[3], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[24], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[45], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[66], 1);
	}

	[TestMethod]
	public void AddGemEvent()
	{
		_replay.EventsData.AddEvent(new GemEventData());

		// There should be one new event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);

		// There shouldn't be any new ticks or entities.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
		ValidateOriginalEntityIds();
		ValidateOriginalTicks(_tickCount - 1); // Except for the last tick, which now has an extra event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.EventOffsetsPerTick[^1]);
	}

	[TestMethod]
	public void AddSpawnEvent()
	{
		_replay.EventsData.AddEvent(new ThornSpawnEventData(-1, default, 0));

		// There should be one new event and one new entity.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount + 1, _replay.EventsData.SpawnEventCount);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// The new entity should be a Thorn.
		Assert.AreEqual(EntityType.Thorn, _replay.EventsData.GetEntityType(6));

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
		ValidateOriginalEntityIds();
		ValidateOriginalTicks(_tickCount - 1); // Except for the last tick, which now has an extra event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.EventOffsetsPerTick[^1]);
	}

	[TestMethod]
	public void AddInputsEvent()
	{
		_replay.EventsData.AddEvent(new InputsEventData(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 0, 0));

		// There should be one new event and one new tick.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_tickCount + 1, _replay.EventsData.EventOffsetsPerTick.Count);

		// There shouldn't be any new entities.
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
		ValidateOriginalEntityIds();
		ValidateOriginalTicks(_tickCount - 1); // Except for the last tick, which now has an extra event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.EventOffsetsPerTick[^1]);
	}

	[TestMethod]
	public void RemoveHitEvent()
	{
		_replay.EventsData.RemoveEvent(0);

		// There should be one less event.
		Assert.AreEqual(_eventCount - 1, _replay.EventsData.Events.Count);

		// There shouldn't be any new ticks or entities.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be decremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[1], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[2], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[23], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[44], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[65], 5);

		// Offsets should be changed.
		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void RemoveSquidSpawnEvent()
	{
		_replay.EventsData.RemoveEvent(2); // Remove the Squid spawn.

		// There should be one less event and one less entity.
		Assert.AreEqual(_eventCount - 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount - 1, _replay.EventsData.SpawnEventCount);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// There should be one less entity.
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		for (int i = 1; i < _entityCount - 1; i++)
			Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(i));

		// Entity IDs should be changed.
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[23], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[44], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[65], 4);

		// All Skulls should be now refer to -1 because the Squid is gone.
		AssertSpawnerEntityId(_replay.EventsData.Events[2], -1);
		AssertSpawnerEntityId(_replay.EventsData.Events[23], -1);
		AssertSpawnerEntityId(_replay.EventsData.Events[44], -1);
		AssertSpawnerEntityId(_replay.EventsData.Events[65], -1);

		// Offsets should be changed.
		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i is 1 or 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void RemoveSkullSpawnEvent()
	{
		_replay.EventsData.RemoveEvent(3); // Remove the first Skull spawn.

		// There should be one less event and one less entity.
		Assert.AreEqual(_eventCount - 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount - 1, _replay.EventsData.SpawnEventCount);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// There should be one less entity.
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.GetEntityType(1));
		for (int i = 2; i < _entityCount - 1; i++)
			Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(i));

		// Entity IDs should be changed.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[23], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[44], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[65], 4);

		// Offsets should be changed.
		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset++; // Squid spawn event.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[DataTestMethod]
	[DataRow(4)] // Inputs event after the Squid and Skull spawns.
	[DataRow(5)] // Inputs event without any additional events.
	[DataRow(6)] // Inputs event without any additional events.
	public void RemoveInputsEvent(int eventIndex)
	{
		_replay.EventsData.RemoveEvent(eventIndex);

		// There should be one less event and one less tick.
		Assert.AreEqual(_eventCount - 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_tickCount - 1, _replay.EventsData.EventOffsetsPerTick.Count);

		// There shouldn't be any new entities.
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be decremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[23], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[44], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[65], 5);

		// Offsets should be changed.
		int expectedOffset = 0;
		for (int i = 0; i < _tickCount - 1; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 20 or 40 or 60)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void RemoveAllEvents()
	{
		for (int i = 0; i < _eventCount - 2; i++)
			_replay.EventsData.RemoveEvent(2); // Do not remove initial hits and initial inputs event.

		Assert.AreEqual(2, _replay.EventsData.Events.Count);
		Assert.AreEqual(2, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(0, _replay.EventsData.SpawnEventCount);

		Assert.IsInstanceOfType<HitEventData>(_replay.EventsData.Events[0].Data);
		Assert.IsInstanceOfType<InitialInputsEventData>(_replay.EventsData.Events[1].Data);

		Assert.AreEqual(0, _replay.EventsData.EventOffsetsPerTick[0]);
		Assert.AreEqual(2, _replay.EventsData.EventOffsetsPerTick[1]);

		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
	}

	[TestMethod]
	public void RemoveAllEventsReverse()
	{
		for (int i = _eventCount - 1; i >= 2; i--)
			_replay.EventsData.RemoveEvent(i); // Do not remove initial hits and initial inputs event.

		Assert.AreEqual(2, _replay.EventsData.Events.Count);
		Assert.AreEqual(2, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(0, _replay.EventsData.SpawnEventCount);

		Assert.IsInstanceOfType<HitEventData>(_replay.EventsData.Events[0].Data);
		Assert.IsInstanceOfType<InitialInputsEventData>(_replay.EventsData.Events[1].Data);

		Assert.AreEqual(0, _replay.EventsData.EventOffsetsPerTick[0]);
		Assert.AreEqual(2, _replay.EventsData.EventOffsetsPerTick[1]);

		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
	}

	[TestMethod]
	public void InsertGemEventAtStart()
	{
		_replay.EventsData.InsertEvent(0, new GemEventData());

		// There should be one new event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);

		// There shouldn't be any new ticks or entities.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be incremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[3], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[4], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 5);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset += 2; // Hit event 53333... and Gem event.
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void InsertGemEvent()
	{
		_replay.EventsData.InsertEvent(10, new GemEventData());

		// There should be one new event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);

		// There shouldn't be any new ticks or entities.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be incremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 5);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i == 7)
				expectedOffset++; // Gem event.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void InsertSpawnEventAtStart()
	{
		_replay.EventsData.InsertEvent(0, new ThornSpawnEventData(-1, default, 0));

		// There should be one new event and one new entity.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount + 1, _replay.EventsData.SpawnEventCount);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// The new entity should be a Thorn.
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		Assert.AreEqual(EntityType.Thorn, _replay.EventsData.GetEntityType(1));
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.GetEntityType(2));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(3));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(4));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(5));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(6));

		AssertEntityId<ThornSpawnEventData>(_replay.EventsData.Events[0], 1);
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[4], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 5);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 6);

		// All Skulls should still be spawned by the Squid, so the SpawnerEntityId should be updated to 2.
		AssertSpawnerEntityId(_replay.EventsData.Events[4], 2);
		AssertSpawnerEntityId(_replay.EventsData.Events[25], 2);
		AssertSpawnerEntityId(_replay.EventsData.Events[46], 2);
		AssertSpawnerEntityId(_replay.EventsData.Events[67], 2);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset += 2; // Hit event 53333... and Thorn spawn event.
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void InsertSpawnEvent()
	{
		_replay.EventsData.InsertEvent(10, new ThornSpawnEventData(-1, default, 0));

		// There should be one new event and one new entity.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount + 1, _replay.EventsData.SpawnEventCount);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// The new entity should be a Thorn.
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.GetEntityType(1));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(2));
		Assert.AreEqual(EntityType.Thorn, _replay.EventsData.GetEntityType(3));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(4));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(5));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(6));

		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<ThornSpawnEventData>(_replay.EventsData.Events[10], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 5);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 6);

		// All Skulls should still be spawned by the Squid.
		AssertSpawnerEntityId(_replay.EventsData.Events[3], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[25], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[46], 1);
		AssertSpawnerEntityId(_replay.EventsData.Events[67], 1);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i == 7)
				expectedOffset++; // Thorn spawn event.
			else if (i is 21 or 41 or 61)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void InsertInputsEventAtStart()
	{
		_replay.EventsData.InsertEvent(0, new InputsEventData(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 0, 0));

		// There should be one new event and one new tick.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_tickCount + 1, _replay.EventsData.EventOffsetsPerTick.Count);

		// There shouldn't be any new entities.
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be incremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[3], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[4], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 5);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount + 1; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 1)
				expectedOffset++; // Hit event 53333...
			else if (i == 2)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 22 or 42 or 62)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void InsertInputsEvent()
	{
		_replay.EventsData.InsertEvent(10, new InputsEventData(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 0, 0));

		// There should be one new event and one new tick.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_tickCount + 1, _replay.EventsData.EventOffsetsPerTick.Count);

		// There shouldn't be any new entities.
		Assert.AreEqual(_entityCount, _replay.EventsData.SpawnEventCount);
		ValidateOriginalEntityTypes();

		// Entity IDs should be unchanged, but their indexes should be incremented.
		AssertEntityId<SquidSpawnEventData>(_replay.EventsData.Events[2], 1);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[3], 2);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[25], 3);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[46], 4);
		AssertEntityId<BoidSpawnEventData>(_replay.EventsData.Events[67], 5);

		int expectedOffset = 0;
		for (int i = 0; i < _tickCount + 1; i++)
		{
			int offset = _replay.EventsData.EventOffsetsPerTick[i];
			Assert.AreEqual(expectedOffset, offset);

			expectedOffset++; // Inputs event.

			if (i == 0)
				expectedOffset++; // Hit event 53333...
			else if (i == 1)
				expectedOffset += 2; // Squid and Skull spawn events.
			else if (i is 22 or 42 or 62)
				expectedOffset++; // Skull spawn event.
		}
	}

	[TestMethod]
	public void ChangeEntityType()
	{
		Assert.IsInstanceOfType<BoidSpawnEventData>(_replay.EventsData.Events[3].Data);
		BoidSpawnEventData boidSpawnEventData = (BoidSpawnEventData)_replay.EventsData.Events[3].Data;
		boidSpawnEventData.BoidType = BoidType.Skull2;

		Assert.AreEqual(EntityType.Zero, _replay.EventsData.GetEntityType(0));
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.GetEntityType(1));
		Assert.AreEqual(EntityType.Skull2, _replay.EventsData.GetEntityType(2));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(3));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(4));
		Assert.AreEqual(EntityType.Skull1, _replay.EventsData.GetEntityType(5));
	}

	[TestMethod]
	public void TestReferringEntityIds_InsertThornBeforeSquidAndSkulls()
	{
		ReplayBinary<LocalReplayBinaryHeader> replay = ReplayBinary<LocalReplayBinaryHeader>.CreateDefault();

		// Remove initial inputs and end events for ease of testing.
		replay.EventsData.RemoveEvent(0);
		replay.EventsData.RemoveEvent(0);

		int squid2EntityId = 1;

		// Add a Squid2 that spawns 3 skulls.
		replay.EventsData.AddEvent(new SquidSpawnEventData(SquidType.Squid2, -1, Vector3.Zero, Vector3.Zero, 0f));
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		List<BoidSpawnEventData> boids = new();
		for (int i = 0; i < 3; i++)
		{
			BoidSpawnEventData boid = new(squid2EntityId, BoidType.Skull1, Int16Vec3.Zero, Int16Mat3x3.Identity, Vector3.Zero, 0f);
			boids.Add(boid);
			replay.EventsData.AddEvent(boid);
			Assert.AreEqual(EntityType.Skull1, replay.EventsData.GetEntityType(squid2EntityId + i + 1));
		}

		// Add a Thorn in front of everything.
		replay.EventsData.InsertEvent(0, new ThornSpawnEventData(-1, Vector3.Zero, 0f));
		Assert.AreEqual(EntityType.Thorn, replay.EventsData.GetEntityType(1));

		// Test if all skulls are still referring to the Squid2.
		squid2EntityId = 2;
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		for (int i = 0; i < boids.Count; i++)
		{
			BoidSpawnEventData boid = boids[i];
			Assert.AreEqual(EntityType.Skull1, replay.EventsData.GetEntityType(squid2EntityId + i + 1));
			Assert.AreEqual(squid2EntityId, boid.SpawnerEntityId);
		}

		// Remove the Thorn.
		replay.EventsData.RemoveEvent(0);

		// Test if all skulls are still referring to the Squid2.
		squid2EntityId = 1;
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		for (int i = 0; i < boids.Count; i++)
		{
			BoidSpawnEventData boid = boids[i];
			Assert.AreEqual(EntityType.Skull1, replay.EventsData.GetEntityType(squid2EntityId + i + 1));
			Assert.AreEqual(squid2EntityId, boid.SpawnerEntityId);
		}

		// Remove the Squid2.
		replay.EventsData.RemoveEvent(0);

		// Test if all skulls are now referring to -1.
		for (int i = 0; i < boids.Count; i++)
		{
			BoidSpawnEventData boid = boids[i];
			Assert.AreEqual(EntityType.Skull1, replay.EventsData.GetEntityType(i + 1));
			Assert.AreEqual(-1, boid.SpawnerEntityId);
		}
	}

	// TODO: Also test EntityOrientationEventData and EntityTargetEventData.
	// TODO: Also test EntityPositionEventData with multiple entities.
	// TODO: Also test HitEventData, SpiderEggSpawnEventData, and TransmuteEventData.
	[TestMethod]
	public void TestReferringEntityIds_InsertThornBetweenEntityPositionEvents()
	{
		ReplayBinary<LocalReplayBinaryHeader> replay = ReplayBinary<LocalReplayBinaryHeader>.CreateDefault();

		// Remove initial inputs and end events for ease of testing.
		replay.EventsData.RemoveEvent(0);
		replay.EventsData.RemoveEvent(0);

		int squid2EntityId = 1;

		// Add a Squid2.
		replay.EventsData.AddEvent(new SquidSpawnEventData(SquidType.Squid2, -1, Vector3.Zero, Vector3.Zero, 0f));
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));

		// Add two entity position events.
		EntityPositionEventData firstUpdate = new(squid2EntityId, Int16Vec3.Zero);
		EntityPositionEventData secondUpdate = new(squid2EntityId, Int16Vec3.Zero);
		replay.EventsData.AddEvent(firstUpdate);
		replay.EventsData.AddEvent(secondUpdate);

		// Add a Thorn in front of everything.
		replay.EventsData.InsertEvent(0, new ThornSpawnEventData(-1, Vector3.Zero, 0f));
		Assert.AreEqual(EntityType.Thorn, replay.EventsData.GetEntityType(1));

		// Test if all entity position events are still referring to the Squid2.
		squid2EntityId = 2;
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		Assert.AreEqual(squid2EntityId, firstUpdate.EntityId);
		Assert.AreEqual(squid2EntityId, secondUpdate.EntityId);

		// Remove the Thorn.
		replay.EventsData.RemoveEvent(0);

		// Test if all entity position events are still referring to the Squid2.
		squid2EntityId = 1;
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		Assert.AreEqual(squid2EntityId, firstUpdate.EntityId);
		Assert.AreEqual(squid2EntityId, secondUpdate.EntityId);

		// Add another Thorn in between 2 entity position events.
		replay.EventsData.InsertEvent(2, new ThornSpawnEventData(-1, Vector3.Zero, 0f));

		// Test if all entity position events are still referring to the Squid2.
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		Assert.AreEqual(squid2EntityId, firstUpdate.EntityId);
		Assert.AreEqual(squid2EntityId, secondUpdate.EntityId);

		// Remove the Thorn.
		replay.EventsData.RemoveEvent(2);

		// Test if all entity position events are still referring to the Squid2.
		Assert.AreEqual(EntityType.Squid2, replay.EventsData.GetEntityType(squid2EntityId));
		Assert.AreEqual(squid2EntityId, firstUpdate.EntityId);
		Assert.AreEqual(squid2EntityId, secondUpdate.EntityId);

		// Remove the Squid2.
		replay.EventsData.RemoveEvent(0);

		// Test if all entity position events are now referring to -1.
		Assert.IsNull(replay.EventsData.GetEntityType(1));
		Assert.AreEqual(-1, firstUpdate.EntityId);
		Assert.AreEqual(-1, secondUpdate.EntityId);
	}
}
