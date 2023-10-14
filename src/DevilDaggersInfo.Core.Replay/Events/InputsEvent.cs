using DevilDaggersInfo.Core.Replay.Events.Enums;
using DevilDaggersInfo.Core.Replay.Events.Interfaces;

namespace DevilDaggersInfo.Core.Replay.Events;

public record InputsEvent(bool Left, bool Right, bool Forward, bool Backward, JumpType Jump, ShootType Shoot, ShootType ShootHoming, short MouseX, short MouseY) : IEvent
{
	public bool Left = Left;
	public bool Right = Right;
	public bool Forward = Forward;
	public bool Backward = Backward;
	public JumpType Jump = Jump;
	public ShootType Shoot = Shoot;
	public ShootType ShootHoming = ShootHoming;
	public short MouseX = MouseX;
	public short MouseY = MouseY;

	public void Write(BinaryWriter bw)
	{
		bw.Write((byte)0x09);
		bw.Write(Left);
		bw.Write(Right);
		bw.Write(Forward);
		bw.Write(Backward);
		bw.Write((byte)Jump);
		bw.Write((byte)Shoot);
		bw.Write((byte)ShootHoming);
		bw.Write(MouseX);
		bw.Write(MouseY);
		bw.Write((byte)0x0a);
	}
}
