using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IStats
	{
		string PrivacyState { get; }
		int VisibilityState { get; }
		IStatsGame Game { get; }
		ulong SteamId64 { get; }
		string CustomUrl { get; }
		IStatsBase Info { get; }
		IEnumerable<IAchievement> Achievements { get; }
	}
}