namespace DevilDaggersInfo.Core.Replay.Events.Data;

public interface IEventData
{
	internal static int DefaultEntityId => 1;

	void Write(BinaryWriter bw);
}
