using System;
using System.Linq;
using Xunit;

namespace SteamCommunity.Net.Tests
{
    public class UnitTest
    {
        [Fact]
        public async void WannaBeFriendsQuestionmark()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var friends = await client.GetFriendsAsync(76561198049848090u);
			
			Assert.NotNull(friends);
			Assert.True(friends.SteamId64 == 76561198049848090u);
			Assert.True(friends.SteamId == "NeKz");
			Assert.True(friends.Items.Count() > 0);
        }

		[Fact]
        public async void WasteMoneyOnVideoGames()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var games = await client.GetGamesAsync(76561198049848090u);
			
			Assert.NotNull(games);
			Assert.True(games.Id == 76561198049848090u);
			Assert.True(games.ProfileName == "NeKz");
			Assert.True(games.Games.Count() > 0);
        }

		[Fact]
        public async void TopTenCheatedScores()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var board = await client.GetLeaderboardAsync("Portal 2", 47458, 0, 10);
			
			Assert.NotNull(board);
			Assert.True(board.AppId == 620);
			Assert.True(board.AppFriendlyName == "Portal2");
			Assert.True(board.TotalLeaderboardEntries > 0);
			Assert.True(board.EntryStart == 0);
			Assert.True(board.EntryEnd == 10);
			Assert.True(board.NextRequestUrl.EndsWith("11"));
			Assert.True(board.ResultCount == 10);
			Assert.True(board.Entries.Count() == 10);
        }

		[Fact]
        public async void FriendsBeatMeAtAVideoGame()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var board = await client.GetLeaderboardAsync("Portal 2", 47458, 76561198049848090u);
			
			Assert.NotNull(board);
			Assert.True(board.AppId == 620);
			Assert.True(board.AppFriendlyName == "Portal2");
			Assert.True(board.TotalLeaderboardEntries > 0);
			Assert.True(board.ResultCount > 0);
			Assert.True(board.Entries.Count() > 0);
        }

		[Fact]
        public async void UninstallGame()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var stats = await client.GetLeaderboardsAsync("Portal 2");
			
			Assert.NotNull(stats);
			Assert.True(stats.AppId == 620);
			Assert.True(stats.AppFriendlyName == "Portal2");
			Assert.True(stats.LeaderboardCount > 0);
			Assert.True(stats.Entries.Count() > 0);
        }

		[Fact]
        public async void D()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var members = await client.GetMemberListAsync(103582791431034943u);
			
			Assert.NotNull(members);
			Assert.True(members.Id == 103582791431034943u);
			Assert.True(members.Details.Url == "sourceruns");
			Assert.True(members.Details.MemberCount > 0);
			Assert.True(members.MemberCount > 0);
			Assert.True(members.Details.MemberCount > 0);
			Assert.True(members.Members.Count() > 0);
        }

		[Fact]
        public async void HeyThatIsMe()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var profile = await client.GetProfileAsync(76561198049848090u);
			
			Assert.NotNull(profile);
			Assert.True(profile.Id == 76561198049848090u);
			Assert.True(profile.IsVacBanned == false);
			Assert.True(profile.IsLimitedAccount == false);
			Assert.True(profile.Privacy == PrivacyState.Public);
			Assert.True(profile.NameId == "NeKz");
        }

		[Fact]
        public async void AlwaysPerfectGame()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var stats = await client.GetStatsAsync(76561198049848090u, 620);
			
			Assert.NotNull(stats);
        }

		[Fact]
        public async void DunnoWhatThisEvenIs()
        {
			var client = new SteamCommunityClient(autoCache: false);
			var feed = await client.GetStatsFeedAsync(76561198049848090u, 620);
			
			Assert.NotNull(feed);
        }
    }
}