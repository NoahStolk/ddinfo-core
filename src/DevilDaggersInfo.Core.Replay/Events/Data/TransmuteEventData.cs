namespace DevilDaggersInfo.Core.Replay.Events.Data;

// TODO: One of these vectors is likely the skull position when it is transmuted.
public record TransmuteEventData(int EntityId, Int16Vec3 A, Int16Vec3 B, Int16Vec3 C, Int16Vec3 D) : IEventData
{
#pragma warning disable CA1051 // Visible instance fields.
	public int EntityId = EntityId;
	public Int16Vec3 A = A;
	public Int16Vec3 B = B;
	public Int16Vec3 C = C;
	public Int16Vec3 D = D;
#pragma warning restore CA1051

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x07);
		bw.Write(EntityId);
		bw.Write(A);
		bw.Write(B);
		bw.Write(C);
		bw.Write(D);
	}

	public static TransmuteEventData CreateDefault()
	{
		return new(IEventData.DefaultEntityId, Int16Vec3.Zero, Int16Vec3.Zero, Int16Vec3.Zero, Int16Vec3.Zero);
	}
}
