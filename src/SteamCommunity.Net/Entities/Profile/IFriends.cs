using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IFriends
	{
		ulong SteamId64 { get; }
		string SteamId { get; }
		IEnumerable<ulong> Items { get; }
	}
}