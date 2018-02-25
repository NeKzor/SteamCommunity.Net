using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IStats
	{
		ulong Id { get; }
		string CustomUrl { get; }
		PrivacyState PrivacyState { get; }
		int VisibilityState { get; }
		IStatsGame Game { get; }
		IStatsBase Info { get; }
		IEnumerable<IAchievement> Achievements { get; }
	}
}