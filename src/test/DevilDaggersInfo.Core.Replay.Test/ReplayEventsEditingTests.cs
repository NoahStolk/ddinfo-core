using DevilDaggersInfo.Core.Replay.Events.Enums;
using System.Diagnostics.CodeAnalysis;

namespace DevilDaggersInfo.Core.Replay.Test;

[TestClass]
[SuppressMessage("Major Bug", "S2583:Conditionally executed code should be reachable", Justification = "False positive. This analyzer doesn't always work correctly.")]
public class ReplayEventsEditingTests
{
	private const int _eventCount = 72;
	private const int _entityCount = 6;
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
		Assert.AreEqual(_entityCount, _replay.EventsData.EntityTypes.Count);
		ValidateOriginalEntityTypes();

		// Check initial event offsets per tick.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		ValidateOriginalTicks(_tickCount);
	}

	private void ValidateOriginalEntityTypes()
	{
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.EntityTypes[0]);
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.EntityTypes[1]);
		for (int i = 2; i < _entityCount; i++)
			Assert.AreEqual(EntityType.Skull1, _replay.EventsData.EntityTypes[i]);
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

	[TestMethod]
	public void AddGemEvent()
	{
		_replay.EventsData.AddEvent(new GemEvent());

		// There should be one new event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);

		// There shouldn't be any new ticks or entities.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);
		Assert.AreEqual(_entityCount, _replay.EventsData.EntityTypes.Count);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
		ValidateOriginalTicks(_tickCount - 1); // Except for the last tick, which now has an extra event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.EventOffsetsPerTick[^1]);
	}

	[TestMethod]
	public void AddSpawnEvent()
	{
		// TODO: The entity ID should be calculated automatically.
		const int entityId = 6;
		_replay.EventsData.AddEvent(new ThornSpawnEvent(entityId, -1, default, 0));

		// There should be one new event and one new entity.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount + 1, _replay.EventsData.EntityTypes.Count);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// The new entity should be a Thorn.
		Assert.AreEqual(EntityType.Thorn, _replay.EventsData.EntityTypes[entityId]);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
		ValidateOriginalTicks(_tickCount - 1); // Except for the last tick, which now has an extra event.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.EventOffsetsPerTick[^1]);
	}

	[TestMethod]
	public void AddInputsEvent()
	{
		_replay.EventsData.AddEvent(new InputsEvent(true, false, false, false, JumpType.None, ShootType.None, ShootType.None, 0, 0));

		// There should be one new event and one new tick.
		Assert.AreEqual(_eventCount + 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_tickCount + 1, _replay.EventsData.EventOffsetsPerTick.Count);

		// There shouldn't be any new entities.
		Assert.AreEqual(_entityCount, _replay.EventsData.EntityTypes.Count);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();
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
		Assert.AreEqual(_entityCount, _replay.EventsData.EntityTypes.Count);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

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
	public void RemoveSpawnEvent()
	{
		_replay.EventsData.RemoveEvent(3); // Remove the first Skull spawn.

		// There should be one less event and one less entity.
		Assert.AreEqual(_eventCount - 1, _replay.EventsData.Events.Count);
		Assert.AreEqual(_entityCount - 1, _replay.EventsData.EntityTypes.Count);

		// There shouldn't be any new ticks.
		Assert.AreEqual(_tickCount, _replay.EventsData.EventOffsetsPerTick.Count);

		// There should be one less entity.
		Assert.AreEqual(EntityType.Zero, _replay.EventsData.EntityTypes[0]);
		Assert.AreEqual(EntityType.Squid1, _replay.EventsData.EntityTypes[1]);
		for (int i = 2; i < _entityCount - 1; i++)
			Assert.AreEqual(EntityType.Skull1, _replay.EventsData.EntityTypes[i]);

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
		Assert.AreEqual(_entityCount, _replay.EventsData.EntityTypes.Count);

		// Original data should be unchanged.
		ValidateOriginalEntityTypes();

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
}
