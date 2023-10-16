namespace DevilDaggersInfo.Core.Replay.Events.Data;

public interface IEventData
{
	void Write(BinaryWriter bw);
}
