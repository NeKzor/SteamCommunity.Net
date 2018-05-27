using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.ProfileModel;

namespace SteamCommunity
{
	public class Profile : IPublicProfile, IPrivateProfile
	{
		public ulong Id { get; private set; }
		public string Name { get; private set; }
		public OnlineState State { get; private set; }
		public string StateMessage { get; private set; }
		public PrivacyState Privacy { get; private set; }
		public int VisibilityState { get; private set; }
		public string AvatarIcon { get; private set; }
		public string AvatarMedium { get; private set; }
		public string AvatarFull { get; private set; }
		public bool IsVacBanned { get; private set; }
		public TradeBanState TradeBan { get; private set; }
		public bool IsLimitedAccount { get; private set; }

		public string CustomUrl { get; private set; }
		public string MemberSince { get; private set; }
		public float HoursPlayedPastWeeks { get; private set; }
		public string Headline { get; private set; }
		public string Location { get; private set; }
		public string Realname { get; private set; }
		public string Summary { get; private set; }
		public IEnumerable<IMostPlayedGame> MostPlayedGames { get; private set; }
		public IEnumerable<IGroupDetails> Groups { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IFriends> GetFriendsAsync(bool ignoreCache = false)
			=> await Client.GetFriendsAsync(Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IGameList> GetGamesAsync(bool ignoreCache = false)
			=> await Client.GetGamesAsync(Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IFriendsLeaderboard> GetLeaderboardAsync(string appName, int leaderboardId, bool ignoreCache = false)
			=> await Client.GetLeaderboardAsync(appName, leaderboardId, Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IStats> GetStatsAsync(string appName, bool ignoreCache = false)
			=> await Client.GetStatsAsync(Id, appName, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IStatsFeed> GetStatsFeedAsync(int appId, bool ignoreCache = false)
			=> await Client.GetStatsFeedAsync(Id, appId, ignoreCache)
				.ConfigureAwait(false);

		internal static Profile Create(SteamCommunityClient client, Model model)
		{
			var games = new List<IMostPlayedGame>();
			var groups = new List<IGroupDetails>();
			foreach (var item in model.MostPlayedGames)
				games.Add(MostPlayedGame.Create(item));
			foreach (var item in model.Groups)
				groups.Add(GroupDetails.Create(client, item));
			
			return new Profile()
			{
				Id = model.SteamId64,
				Name = model.SteamId,
				State = (OnlineState)Enum.Parse(typeof(OnlineState), model.OnlineState.Replace("-", string.Empty), true),
				StateMessage = model.StateMessage,
				Privacy = (PrivacyState)Enum.Parse(typeof(PrivacyState), model.PrivacyState, true),
				VisibilityState = model.VisibilityState,
				AvatarIcon = model.AvatarIcon,
				AvatarMedium = model.AvatarMedium,
				AvatarFull = model.AvatarFull,
				IsVacBanned = model.VacBanned != 0,
				TradeBan = (TradeBanState)Enum.Parse(typeof(TradeBanState), model.TradeBanState, true),
				IsLimitedAccount = model.IsLimitedAccount != 0,
				CustomUrl = model.CustomUrl,
				MemberSince = model.MemberSince,
				HoursPlayedPastWeeks = (!string.IsNullOrEmpty(model.HoursPlayed2Wk))
					? float.Parse(model.HoursPlayed2Wk)
					: 0f,
				Headline = model.Headline,
				Location = model.Location,
				Realname = model.Realname,
				Summary = model.Summary,
				MostPlayedGames = games,
				Groups = groups,
				Client = client
			};
		}
	}
}