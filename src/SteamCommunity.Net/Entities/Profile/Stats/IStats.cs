using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IStats
	{
		ulong Id { get; }
		PrivacyState PrivacyState { get; }
		int VisibilityState { get; }
		IStatsGame Game { get; }
		string CustomUrl { get; }
		IStatsBase Info { get; }
		IEnumerable<IAchievement> Achievements { get; }
	}
}