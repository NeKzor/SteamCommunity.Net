using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IStatsFeed
	{
		ulong Id { get; }
		IEnumerable<IStatsFeedEntry> Stats { get; }
		IEnumerable<IStatsFeedEntry> Achievements { get; }
	}
}