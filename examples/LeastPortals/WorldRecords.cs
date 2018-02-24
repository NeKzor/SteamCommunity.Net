using System.Collections.Generic;

namespace LeastPortals
{
	internal static class WorldRecords
	{
		public static readonly Dictionary<string, int> Cooperative = new Dictionary<string, int>()
		{
			["04 Bridge Gels: Portals"] = 2,
			["06 Bridge Catch: Portals"] = 0,
			["05 Catapults: Portals"] = 6,
			["05 Bridge Fling: Portals"] = 5,
			["03 Catapult Catch: Portals"] = 0,
			["03 Fling Block: Portals"] = 2,
			["03 Team Retrieval: Portals"] = 0, // ~1000
			["01 Doors: Portals"] = 0, // ~1300
			["08 Industrial Fan: Portals"] = 0,
			["04 Vertical Flings: Portals"] = 2, // ~300
			["01 Flings: Portals"] = 4, // ~900
			["07 Fling Crushers: Portals"] = 0,
			["02 Infinifling: Portals"] = 0,
			["03 Lasers: Portals"] = 4, // ~1900
			["05 Laser Crusher: Portals"] = 0, // ~1500
			["07 Double Lift: Portals"] = 0,
			["06 Multifling: Portals"] = 2,
			["03 Bridge Repulsion: Portals"] = 2,
			["01 Repulsion Jumps: Portals"] = 0,
			["05 Maintenance: Portals"] = 0,
			["09 Crazier Box: Portals"] = 0,
			["08 Vault Entrance: Portals"] = 0,
			["08 Gel Maze: Portals"] = 0,
			["06 Turret Ninja: Portals"] = 0,
			["02 Double Bounce: Portals"] = 2, // ~400
			["07 Propulsion Retrieval: Portals"] = 0,
			["05 Propulsion Crushers: Portals"] = 0,
			["04 Wall Repulsion: Portals"] = 2,
			["02 Buttons: Portals"] = 2, // ~300
			["04 Rat Maze: Portals"] = 2, // ~400
			["01 Separation: Portals"] = 0,
			["03 Funnel Catch\": Portals"] = 0,
			["02 Funnel Drill: Portals"] = 0,
			["09 Turret Warehouse: Portals"] = 0, // ~200
			["04 Funnel Laser: Portals"] = 0, // ~400
			["08 Funnel Maze: Portals"] = 0,
			["05 Cooperative Polarity: Portals"] = 0, // ~800
			["06 Funnel Hop: Portals"] = 0,
			["07 Advanced Polarity: Portals"] = 0,
			["01 Cooperative Funnels: Portals"] = 0, // ~300
			["06 Behind the Scenes: Portals"] = 0, // ~3000
			["02 Triple Axis: Portals"] = 0, // ~400
			["07 Turret Assassin: Portals"] = 0,
			["06 Turret Walls: Portals"] = 4,
			["02 Bridge Swap: Portals"] = 3,
			["08 Bridge Testing: Portals"] = 0,
			["04 Catapult Block: Portals"] = 4, // ~200
			["01 Cooperative Bridges: Portals"] = 3, // ~500
		};
		public static readonly Dictionary<string, int> SinglePlayer = new Dictionary<string, int>()
		{
			["Portal Gun: Portals"] = 0,
			["Smooth Jazz: Portals"] = 1,
			["Cube Momentum: Portals"] = 1,
			["Future Starter: Portals"] = 3,
			["Bridge Intro: Portals"] = 3,
			["Bridge the Gap: Portals"] = 2,
			//["Jail Break: Portals"] = 2,
			["Escape: Portals"] = 0,
			["Turret Factory: Portals"] = 6,
			["Turret Sabotage: Portals"] = 4,
			//["Neurotoxin Sabotage: Portals"] = 3,
			["Column Blocker: Portals"] = 2,
			//["Dual Lasers: Portals"] = 2,
			//["Fizzler Intro: Portals"] = 2,
			["Incinerator: Portals"] = 0,
			["Laser Chaining: Portals"] = 2,
			["Laser Over Goo: Portals"] = 0,
			//["Laser Relays: Portals"] = 2,
			["Laser Stairs: Portals"] = 2,
			["Laser vs. Turret: Portals"] = 0,
			["Pit Flings: Portals"] = 0,
			["Pull the Rug: Portals"] = 0,
			["Ricochet: Portals"] = 2,
			["Ceiling Catapult: Portals"] = 3,
			["Triple Laser: Portals"] = 0,
			["Trust Fling: Portals"] = 2,
			["Turret Blocker: Portals"] = 0,
			//["Turret Intro: Portals"] = 0,
			["Underground: Portals"] = 4,
			["Cave Johnson: Portals"] = 5,
			["Bomb Flings: Portals"] = 3,
			["Crazy Box: Portals"] = 0,
			["Three Gels: Portals"] = 4,
			["Repulsion Intro: Portals"] = 0,
			["Conversion Intro: Portals"] = 11,
			["Propulsion Flings: Portals"] = 2,
			["Propulsion Intro: Portals"] = 2,
			["PotatOS: Portals"] = 5,
			["Finale 2: Portals"] = 2,
			["Finale 3: Portals"] = 6,
			["Finale 4: Portals"] = 6,
			["Repulsion Polarity: Portals"] = 2,
			["Laser Catapult: Portals"] = 0,
			["Laser Platform: Portals"] = 4,
			["Propulsion Catch: Portals"] = 2,
			["Stop the Box: Portals"] = 0,
			["Funnel Catch: Portals"] = 2,
			["Funnel Intro: Portals"] = 0,
			["Polarity: Portals"] = 0,
			["Ceiling Button: Portals"] = 0,
			["Wall Button: Portals"] = 2
		};
		// About 5k entries tied the record here
		// Exclude them for now because they would
		// need more than one API request
		public static readonly Dictionary<string, int> Excluded = new Dictionary<string, int>()
		{
			["Jail Break: Portals"] = 2,
			["Neurotoxin Sabotage: Portals"] = 3,
			["Dual Lasers: Portals"] = 2,
			["Fizzler Intro: Portals"] = 2,
			["Laser Relays: Portals"] = 2,
			["Turret Intro: Portals"] = 0
		};
	}
}