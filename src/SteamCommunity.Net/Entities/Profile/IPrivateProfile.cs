using System;
using System.Collections.Generic;

namespace SteamCommunity
{
	public interface IPrivateProfile
	{
		string CustomUrl { get; }
		string MemberSince { get; }
		float HoursPlayedPastWeeks { get; }
		string Headline { get; }
		string Location { get; }
		string Realname { get; }
		string Summary { get; }
		IEnumerable<IMostPlayedGame> MostPlayedGames { get; }
		IEnumerable<IGroupDetails> Groups { get; }
	}
}