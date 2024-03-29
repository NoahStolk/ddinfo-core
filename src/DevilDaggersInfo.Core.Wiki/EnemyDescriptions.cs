using System.Diagnostics;

namespace DevilDaggersInfo.Core.Wiki;

public static class EnemyDescriptions
{
	private static readonly Dictionary<Enemy, string[]> _enemyDescriptions = new()
	{
		{ EnemiesV1_0.Squid1, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull II every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV1_0.Squid2, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull III every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV1_0.Centipede, ["Emerges approximately 3 seconds after its spawn, starts flying idly for a while, then starts chasing the player", "Regularly dives down and moves underground for a while"] },
		{ EnemiesV1_0.Gigapede, ["Emerges approximately 3 seconds after its spawn, then starts flying around the arena", "Regularly dives down and moves underground for a while"] },
		{ EnemiesV1_0.Leviathan, ["Activates 8.5333 seconds after its initial appearance", "Attracts and transmutes all skulls by beckoning every 20 seconds, starting 13.5333 seconds after its spawn (5 seconds after becoming active)", "Rotates counter-clockwise"] },
		{ EnemiesV1_0.Spider1, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 3 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg I one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head"] },
		{ EnemiesV1_0.Skull1, ["Slowly chases the player"] },
		{ EnemiesV1_0.Skull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV1_0.Skull3, ["Chases the player fast"] },
		{ EnemiesV1_0.TransmutedSkull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV1_0.TransmutedSkull3, ["Chases the player fast"] },
		{ EnemiesV1_0.TransmutedSkull4, ["Chases the player fast"] },
		{ EnemiesV1_0.SpiderEgg1, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV1_0.Spiderling, ["Darts towards the player in bursts with random offsets"] },
		{ EnemiesV2_0.Squid1, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull II every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV2_0.Squid2, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull III every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV2_0.Squid3, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 15 Skull Is and 1 Skull IV every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV2_0.Centipede, ["Emerges approximately 3 seconds after its spawn, starts flying idly for a while, then starts chasing the player", "Regularly dives down and moves underground for a while"] },
		{ EnemiesV2_0.Gigapede, ["Emerges approximately 3 seconds after its spawn, then starts chasing the player immediately"] },
		{ EnemiesV2_0.Leviathan, ["Activates 8.5333 seconds after its initial appearance", "Attracts and transmutes all skulls by beckoning every 20 seconds, starting 13.5333 seconds after its spawn (5 seconds after becoming active)", "Rotates counter-clockwise"] },
		{ EnemiesV2_0.Spider1, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 3 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg I one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head"] },
		{ EnemiesV2_0.Spider2, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 9 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg II one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head (though barely noticeable due to its size)"] },
		{ EnemiesV2_0.Skull1, ["Slowly chases the player"] },
		{ EnemiesV2_0.Skull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV2_0.Skull3, ["Chases the player fast"] },
		{ EnemiesV2_0.Skull4, ["Chases the player fast"] },
		{ EnemiesV2_0.TransmutedSkull1, ["Slowly chases the player"] },
		{ EnemiesV2_0.TransmutedSkull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV2_0.TransmutedSkull3, ["Chases the player fast"] },
		{ EnemiesV2_0.TransmutedSkull4, ["Chases the player fast"] },
		{ EnemiesV2_0.SpiderEgg1, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV2_0.SpiderEgg2, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV2_0.Spiderling, ["Darts towards the player in bursts with random offsets"] },
		{ EnemiesV2_0.Andras, ["Unfinished enemy that was never added to the real game", "Only appears in V2, can only be spawned using an edited spawnset", "Has its own sounds", "Uses the mesh for Skull III, but is smaller in size", "Does nothing but attract and consume all homing daggers like Ghostpede ", "Only takes damage when shot from above, so the player will need to daggerjump", "The player doesn't die when touching it"] },
		{ EnemiesV3_0.Squid1, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull II every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_0.Squid2, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull III every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_0.Squid3, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 15 Skull Is and 1 Skull IV every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_0.Centipede, ["Emerges approximately 3 seconds after its spawn, starts flying idly for a while, then starts chasing the player", "Regularly dives down and moves underground for a while"] },
		{ EnemiesV3_0.Gigapede, ["Emerges approximately 3 seconds after its spawn, then starts chasing the player immediately"] },
		{ EnemiesV3_0.Ghostpede, ["Emerges approximately 3 seconds after its spawn, then starts flying in circles high above the arena", "Attracts and consumes all homing daggers, making them useless"] },
		{ EnemiesV3_0.Leviathan, ["Activates 8.5333 seconds after its initial appearance", "Attracts and transmutes all skulls by beckoning every 20 seconds, starting 13.5333 seconds after its spawn (5 seconds after becoming active)", "Rotates counter-clockwise", "Drops The Orb 3.3167 seconds after dying"] },
		{ EnemiesV3_0.Thorn, ["Emerges approximately 3 seconds after its spawn", "Takes up space"] },
		{ EnemiesV3_0.Spider1, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 3 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg I one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head"] },
		{ EnemiesV3_0.Spider2, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 9 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg II one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head (though barely noticeable due to its size)"] },
		{ EnemiesV3_0.Skull1, ["Slowly chases the player"] },
		{ EnemiesV3_0.Skull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_0.Skull3, ["Chases the player fast"] },
		{ EnemiesV3_0.Skull4, ["Chases the player fast"] },
		{ EnemiesV3_0.TransmutedSkull1, ["Slowly chases the player"] },
		{ EnemiesV3_0.TransmutedSkull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_0.TransmutedSkull3, ["Chases the player fast"] },
		{ EnemiesV3_0.TransmutedSkull4, ["Chases the player fast"] },
		{ EnemiesV3_0.SpiderEgg1, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_0.SpiderEgg2, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_0.Spiderling, ["Darts towards the player in bursts with random offsets"] },
		{ EnemiesV3_0.TheOrb, ["Activates 10 seconds after Leviathan's death", "Behaves like an eyeball, will look at the player, then attract and transmute all skulls by beckoning every 2.5333 seconds", "Becomes stunned under constant fire, cannot look or attract skulls while stunned"] },
		{ EnemiesV3_1.Squid1, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull II every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_1.Squid2, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull III every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_1.Squid3, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 15 Skull Is and 1 Skull IV every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_1.Centipede, ["Emerges approximately 3 seconds after its spawn, starts flying idly for a while, then starts chasing the player", "Burrows after 15 seconds of being emerged, or if the player gets too close to its head"] },
		{ EnemiesV3_1.Gigapede, ["Emerges approximately 3 seconds after its spawn, then starts chasing the player immediately"] },
		{ EnemiesV3_1.Ghostpede, ["Emerges approximately 3 seconds after its spawn, then starts flying in circles high above the arena", "Attracts and consumes all homing daggers, making them useless"] },
		{ EnemiesV3_1.Leviathan, ["Activates 8.5333 seconds after its initial appearance", "Attracts and transmutes all skulls by beckoning every 20 seconds, starting 13.5333 seconds after its spawn (5 seconds after becoming active)", "Rotates counter-clockwise", "Drops The Orb 3.3167 seconds after dying"] },
		{ EnemiesV3_1.Thorn, ["Emerges approximately 3 seconds after its spawn", "Takes up space"] },
		{ EnemiesV3_1.Spider1, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 3 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg I one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head"] },
		{ EnemiesV3_1.Spider2, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 9 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg II one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head (though barely noticeable due to its size)"] },
		{ EnemiesV3_1.Skull1, ["Slowly chases the player"] },
		{ EnemiesV3_1.Skull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_1.Skull3, ["Chases the player fast"] },
		{ EnemiesV3_1.Skull4, ["Chases the player fast"] },
		{ EnemiesV3_1.TransmutedSkull1, ["Slowly chases the player"] },
		{ EnemiesV3_1.TransmutedSkull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_1.TransmutedSkull3, ["Chases the player fast"] },
		{ EnemiesV3_1.TransmutedSkull4, ["Chases the player fast"] },
		{ EnemiesV3_1.SpiderEgg1, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_1.SpiderEgg2, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_1.Spiderling, ["Darts towards the player in bursts with random offsets"] },
		{ EnemiesV3_1.TheOrb, ["Activates 10 seconds after Leviathan's death", "Behaves like an eyeball, will look at the player, then attract and transmute all skulls by beckoning every 2.5333 seconds", "Becomes stunned under constant fire, cannot look or attract skulls while stunned"] },
		{ EnemiesV3_2.Squid1, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull II every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_2.Squid2, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 10 Skull Is and 1 Skull III every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_2.Squid3, ["Spawns at the edge of the arena", "Moves slowly and rotates clockwise", "Spawns 15 Skull Is and 1 Skull IV every 20 seconds (starting 3 seconds after its initial appearance)"] },
		{ EnemiesV3_2.Centipede, ["Emerges approximately 3 seconds after its spawn, starts flying idly for a while, then starts chasing the player", "Burrows after 15 seconds of being emerged, or if the player gets too close to its head"] },
		{ EnemiesV3_2.Gigapede, ["Emerges approximately 3 seconds after its spawn, then starts chasing the player immediately"] },
		{ EnemiesV3_2.Ghostpede, ["Emerges approximately 3 seconds after its spawn, then starts flying in circles high above the arena", "Attracts and consumes all homing daggers, making them useless"] },
		{ EnemiesV3_2.Leviathan, ["Activates 8.5333 seconds after its initial appearance", "Attracts and transmutes all skulls by beckoning every 20 seconds, starting 13.5333 seconds after its spawn (5 seconds after becoming active)", "Rotates counter-clockwise", "Drops The Orb 3.3167 seconds after dying"] },
		{ EnemiesV3_2.Thorn, ["Emerges approximately 3 seconds after its spawn", "Takes up space"] },
		{ EnemiesV3_2.Spider1, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 3 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg I one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head"] },
		{ EnemiesV3_2.Spider2, ["Spawns at the edge of the arena and starts lifting its head, faces the player after 9 seconds", "Attracts and consumes gems when facing the player, ejecting them as Spider Egg II one at a time", "Hides its head when shot and left unharmed for 1 second", "Begins moving randomly in an unpredictable jittery fashion after initially raising its head (though barely noticeable due to its size)"] },
		{ EnemiesV3_2.Skull1, ["Slowly chases the player"] },
		{ EnemiesV3_2.Skull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_2.Skull3, ["Chases the player fast"] },
		{ EnemiesV3_2.Skull4, ["Chases the player fast"] },
		{ EnemiesV3_2.TransmutedSkull1, ["Slowly chases the player"] },
		{ EnemiesV3_2.TransmutedSkull2, ["Repeatedly moves towards a random position for between 1.6 and 2.5 seconds"] },
		{ EnemiesV3_2.TransmutedSkull3, ["Chases the player fast"] },
		{ EnemiesV3_2.TransmutedSkull4, ["Chases the player fast"] },
		{ EnemiesV3_2.SpiderEgg1, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_2.SpiderEgg2, ["Hatches into 5 Spiderlings after 10 seconds"] },
		{ EnemiesV3_2.Spiderling, ["Darts towards the player in bursts with random offsets"] },
		{ EnemiesV3_2.TheOrb, ["Activates 10 seconds after Leviathan's death", "Behaves like an eyeball, will look at the player, then attract and transmute all skulls by beckoning every 2.5333 seconds", "Becomes stunned under constant fire, cannot look or attract skulls while stunned"] },
	};

	public static string[] GetEnemyDescription(Enemy enemy)
	{
		foreach (KeyValuePair<Enemy, string[]> kvp in _enemyDescriptions)
		{
			if (kvp.Key == enemy)
				return kvp.Value;
		}

		throw new UnreachableException($"Could not find enemy description for {nameof(Enemy)} '{enemy}'.");
	}
}
