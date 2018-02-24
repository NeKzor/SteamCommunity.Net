using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IGameList
	{
		ulong Id { get; }
		string Name { get; }
		IEnumerable<IGame> Games { get; }
	}
}