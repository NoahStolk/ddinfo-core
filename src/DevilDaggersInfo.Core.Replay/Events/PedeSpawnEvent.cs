using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DevilDaggersInfo.Core.Replay.Events;

[StructLayout(LayoutKind.Sequential)]
public record struct PedeSpawnEvent(int EntityId, PedeType PedeType, int A, Vector3 Position, Vector3 B, Matrix3x3 Orientation) : IEntitySpawnEvent
{
	public PedeType PedeType = PedeType;
	public int A = A;
	public Vector3 Position = Position;
	public Vector3 B = B;
	public Matrix3x3 Orientation = Orientation;

	public EntityType EntityType => PedeType switch
	{
		PedeType.Centipede => EntityType.Centipede,
		PedeType.Gigapede => EntityType.Gigapede,
		PedeType.Ghostpede => EntityType.Ghostpede,
		_ => throw new UnreachableException(),
	};

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x00);
		bw.Write((byte)(PedeType switch
		{
			PedeType.Centipede => 0x07,
			PedeType.Gigapede => 0x0c,
			PedeType.Ghostpede => 0x0f,
			_ => throw new UnreachableException(),
		}));

		bw.Write(A);
		bw.Write(Position);
		bw.Write(B);
		bw.Write(Orientation);
	}
}
