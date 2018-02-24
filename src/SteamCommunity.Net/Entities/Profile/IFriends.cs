using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IFriends
	{
		ulong Id { get; }
		string Name { get; }
		IEnumerable<ulong> List { get; }
	}
}