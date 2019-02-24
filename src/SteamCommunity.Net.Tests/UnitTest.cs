using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace SteamCommunity.Net.Tests
{
    public class UnitTest
    {
		private readonly ITestOutputHelper _output;
		private readonly SteamCommunityClient _client;

		public UnitTest(ITestOutputHelper outputHelper)
		{
			_output = outputHelper;
			_client = new SteamCommunityClient(autoCache: false);
			_client.Log += Log;
		}

		private Task Log(object _, LogMessage msg)
		{
			_output.WriteLine($"{msg}");
			return Task.CompletedTask;
		}

        /* [Fact]
        public async void WannaBeFriendsQuestionmark()
        {
			var friends = await _client.GetFriendsAsync(76561198049848090u);

			Assert.NotNull(friends);
			Assert.True(friends.Id == 76561198049848090u);
			Assert.True(friends.Name == "NeKz");
			Assert.True(friends.List.Count() > 0);
        } */

		[Fact]
        public async void WasteMoneyOnVideoGames()
        {
			var games = await _client.GetGamesAsync(76561198049848090u);

			Assert.NotNull(games);
			Assert.True(games.Id == 76561198049848090u);
			Assert.True(games.Name == "NeKz");
			//Assert.True(games.Games.Count() > 0);
        }

		[Fact]
        public async void TopTenCheatedScores()
        {
			var board = await _client.GetLeaderboardAsync("Portal 2", 47458, 0, 10);
			
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
			var board = await _client.GetLeaderboardAsync("Portal 2", 47458, 76561198049848090u);
			
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
			var stats = await _client.GetLeaderboardsAsync("Portal 2");
			
			Assert.NotNull(stats);
			Assert.True(stats.AppId == 620);
			Assert.True(stats.AppFriendlyName == "Portal2");
			Assert.True(stats.LeaderboardCount > 0);
			Assert.True(stats.Entries.Count() > 0);
        }

		[Fact]
        public async void D()
        {
			var members = await _client.GetGroup(103582791431034943u);
			
			Assert.NotNull(members);
			Assert.True(members.Id == 103582791431034943u);
			Assert.True(members.Details.Url == "sourceruns");
			Assert.True(members.Details.MemberCount > 0);
			Assert.True(members.MemberCount > 0);
			Assert.True(members.Details.MemberCount > 0);
			Assert.True(members.Members.Count() > 0);
        }

		[Fact]
        public async void HeyDasMe()
        {
			var profile = await _client.GetProfileAsync(76561198049848090u);
			
			Assert.NotNull(profile);
			Assert.True(profile.Id == 76561198049848090u);
			Assert.True(profile.IsVacBanned == false);
			Assert.True(profile.IsLimitedAccount == false);
			Assert.True(profile.Privacy == PrivacyState.Public);
			Assert.True(profile.Name == "NeKz");
        }

		[Fact]
        public async void AlwaysPerfectGame()
        {
			var stats = await _client.GetStatsAsync(76561198049848090u, "Portal 2");
			
			//Assert.NotNull(stats);
			//Assert.True(stats.Id == 76561198049848090u);
			//Assert.True(stats.PrivacyState == PrivacyState.Public);
			//Assert.True(stats.VisibilityState != 0);
        }

		[Fact]
        public async void DunnoWhatThisEvenIs()
        {
			var feed = await _client.GetStatsFeedAsync(76561198049848090u, 620);
			
			Assert.NotNull(feed);
			Assert.True(feed.Id == 76561198049848090u);
			//Assert.True(feed.Achievements.Count() > 0);
			//Assert.True(feed.Stats.Count() > 0);
        }

        /* [Fact]
        public async void ThankYouValveVeryCool()
        {
            var golden = await _client.GetProfileAsync(76561197978601137u);

            Assert.NotNull(golden);
            Assert.True(golden.State == OnlineState.Golden);
        } */
    }
}