using DevilDaggersInfo.Core.Replay.Events.Data;
using System.Diagnostics;

namespace DevilDaggersInfo.Core.Replay;

public static class EventDataExtensions
{
	public static IEventData CloneEventData(this IEventData eventData)
	{
		return eventData switch
		{
			BoidSpawnEventData boid => boid with { },
			DaggerSpawnEventData dagger => dagger with { },
			EndEventData end => end with { },
			EntityOrientationEventData entityOrientation => entityOrientation with { },
			EntityPositionEventData entityPosition => entityPosition with { },
			EntityTargetEventData entityTarget => entityTarget with { },
			GemEventData gem => gem with { },
			HitEventData hit => hit with { },
			InitialInputsEventData initialInputs => initialInputs with { },
			InputsEventData inputs => inputs with { },
			LeviathanSpawnEventData leviathan => leviathan with { },
			PedeSpawnEventData pede => pede with { },
			SpiderEggSpawnEventData spiderEgg => spiderEgg with { },
			SpiderSpawnEventData spider => spider with { },
			SquidSpawnEventData squid => squid with { },
			ThornSpawnEventData thorn => thorn with { },
			TransmuteEventData transmute => transmute with { },
			_ => throw new UnreachableException($"Event type '{eventData.GetType().Name}' is not supported."),
		};
	}
}
