using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IGameList
	{
		ulong Id { get; }
		string ProfileName { get; }
		IEnumerable<IGame> Games { get; }
	}
}