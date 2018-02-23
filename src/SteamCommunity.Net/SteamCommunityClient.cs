using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SteamCommunity.API;

namespace SteamCommunity
{
	public sealed class SteamCommunityClient : IDisposable
	{
		public string BaseApiUrl => "http://steamcommunity.com";
		public event Func<object, LogMessage, Task> Log;

		public bool AutoCache
		{
			get => _autoCache;
			set
			{
				if ((_autoCache != value) && (_autoCache))
				{
					_timer = null;
					_cache = null;
				}
				_autoCache = value;
			}
		}
		public uint CacheResetTime
		{
			get => _cacheResetTime / 60 / 1000;
			set => _cacheResetTime = ((value == 0) ? 5 : value) * 60 * 1000;
		}

		private ApiClient _client;
		private Cache _cache;
		private Timer _timer;
		private bool _autoCache;
		private uint _cacheResetTime;

		public SteamCommunityClient(string userAgent = "", bool autoCache = true, uint? cacheResetTime = default)
		{
			_client = new ApiClient(userAgent);
			_autoCache = autoCache;
			CacheResetTime = cacheResetTime ?? 5;
		}

		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(string appName, bool ignoreCache = false)
		{
			var result = default(IStatsLeaderboard);
			try
			{
				var get = $"/stats/{appName.Replace(" ", string.Empty)}/leaderboards?xml=1";
				var model = await GetCacheOrFetch<StatsLeaderboardModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => StatsLeaderboard.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStatsLeaderboard), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IStatsLeaderboard> GetLeaderboardsAsync(int appId, bool ignoreCache = false)
		{
			var result = default(IStatsLeaderboard);
			try
			{
				var get = $"/stats/{appId}/leaderboards?xml=1";
				var model = await GetCacheOrFetch<StatsLeaderboardModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => StatsLeaderboard.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStatsLeaderboard), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IGlobalLeaderboard> GetLeaderboardAsync(
			string appName,
			int leaderboardId,
			uint entryStart = 0,
			uint entryEnd = 5000,
			bool ignoreCache = false)
		{
			if (entryEnd - entryStart  > 5000)
				throw new InvalidOperationException("Cannot fetch more than 5000 entries.");

			var result = default(IGlobalLeaderboard);
			try
			{
				var get = $"/stats/{appName.Replace(" ", string.Empty)}/leaderboards/{leaderboardId}?xml=1" +
					$"&start={entryStart}" +
					$"&end={entryEnd}";
				var model = await GetCacheOrFetch<LeaderboardModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Leaderboard.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IGlobalLeaderboard), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IFriendsLeaderboard> GetLeaderboardAsync(
			string appName,
			int leaderboardId,
			ulong steamId,
			bool ignoreCache = false)
		{
			var result = default(IFriendsLeaderboard);
			try
			{
				var get = $"/stats/{appName.Replace(" ", string.Empty)}/leaderboards/{leaderboardId}?xml=1" +
					$"&steamid={steamId}";
				var model = await GetCacheOrFetch<LeaderboardModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Leaderboard.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IFriendsLeaderboard), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IPublicProfile> GetProfileAsync(ulong steamId64, bool ignoreCache = false)
		{
			var result = default(IPublicProfile);
			try
			{
				var get = $"/profiles/{steamId64}?xml=1";
				var model = await GetCacheOrFetch<ProfileModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Profile.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IPublicProfile), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IPublicProfile> GetProfileAsync(string steamId, bool ignoreCache = false)
		{
			var result = default(IPublicProfile);
			try
			{
				var get = $"/id/{steamId}?xml=1";
				var model = await GetCacheOrFetch<ProfileModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Profile.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IPublicProfile), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IFriends> GetFriendsAsync(ulong steamId64, bool ignoreCache = false)
		{
			var result = default(IFriends);
			try
			{
				var get = $"/profiles/{steamId64}/friends?xml=1";
				var model = await GetCacheOrFetch<FriendsListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Friends.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IFriends), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IFriends> GetFriendsAsync(string steamId, bool ignoreCache = false)
		{
			var result = default(IFriends);
			try
			{
				var get = $"/id/{steamId}/friends?xml=1";
				var model = await GetCacheOrFetch<FriendsListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Friends.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IFriends), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IGameList> GetGamesAsync(ulong steamId64, bool ignoreCache = false)
		{
			var result = default(IGameList);
			try
			{
				var get = $"/profiles/{steamId64}/games?xml=1";
				var model = await GetCacheOrFetch<GamesListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => GameList.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IGameList), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IGameList> GetGamesAsync(string steamId, bool ignoreCache = false)
		{
			var result = default(IGameList);
			try
			{
				var get = $"/id/{steamId}/games?xml=1";
				var model = await GetCacheOrFetch<GamesListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => GameList.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IGameList), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IMemberList> GetMemberListAsync(ulong groupId, uint page = 1, bool ignoreCache = false)
		{
			var result = default(IMemberList);
			try
			{
				var get = $"/gid/{groupId}/memberslistxml?xml=1&p={page}";
				var model = await GetCacheOrFetch<MemberListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => MemberList.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IMemberList), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IMemberList> GetMemberListAsync(string gropName, bool ignoreCache = false)
		{
			var result = default(IMemberList);
			try
			{
				var get = $"/groups/{gropName}/memberslistxml?xml=1";
				var model = await GetCacheOrFetch<MemberListModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => MemberList.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IMemberList), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IStats> GetStatsAsync(ulong steamId64, int appId, bool ignoreCache = false)
		{
			var result = default(IStats);
			try
			{
				var get = $"/profiles/{steamId64}/stats/{appId}?xml=1";
				var model = await GetCacheOrFetch<PlayerStatsModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Stats.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStats), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IStats> GetStatsAsync(string steamId, int appId, bool ignoreCache = false)
		{
			var result = default(IStats);
			try
			{
				var get = $"/id/{steamId}/stats/{appId}?xml=1";
				var model = await GetCacheOrFetch<PlayerStatsModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => Stats.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStats), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IStatsFeed> GetStatsFeedAsync(ulong steamId64, int appId, bool ignoreCache = false)
		{
			var result = default(IStatsFeed);
			try
			{
				var get = $"/profiles/{steamId64}/statsfeed/{appId}?xml=1";
				var model = await GetCacheOrFetch<StatsFeedModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => StatsFeed.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStatsFeed), ex)).ConfigureAwait(false);
			}
			return result;
		}
		public async Task<IStatsFeed> GetStatsFeedAsync(string steamId, int appId, bool ignoreCache = false)
		{
			var result = default(IStatsFeed);
			try
			{
				var get = $"/id/{steamId}/statsfeed/{appId}?xml=1";
				var model = await GetCacheOrFetch<StatsFeedModel>(get, ignoreCache).ConfigureAwait(false);
				result = await Task.Run(() => StatsFeed.Create(this, model)).ConfigureAwait(false);
			}
			catch (Exception ex)
			{
				if (Log != null)
					await Log.Invoke(this, new LogMessage(typeof(IStatsFeed), ex)).ConfigureAwait(false);
			}
			return result;
		}

		public Task<bool> ResetCacheTimer()
		{
			if (_autoCache)
				_timer = new Timer(TimerCallback, _autoCache, (int)_cacheResetTime, (int)_cacheResetTime);
			return Task.FromResult(_autoCache);
		}
		public Task ClearCache()
		{
			_cache = new Cache();
			return Task.FromResult(false);
		}

		internal async Task<T> GetCacheOrFetch<T>(string url, bool ignoreCache)
			where T : class
		{
			if (!_autoCache || ignoreCache)
				return await _client.GetXmlObjectAsync<T>(BaseApiUrl + url).ConfigureAwait(false);
			
			_timer = _timer ?? new Timer(TimerCallback, _autoCache, (int)_cacheResetTime, (int)_cacheResetTime);
			_cache = _cache ?? new Cache();

			var model = await _cache.Get<T>(url).ConfigureAwait(false)
				?? await _client.GetXmlObjectAsync<T>(BaseApiUrl + url).ConfigureAwait(false);
			
			await _cache.AddOrUpdate(url, model).ConfigureAwait(false);
			return model;
		}

		internal void TimerCallback(Object stateInfo)
		{
			if ((bool)stateInfo)
				_cache = new Cache();
		}

		public void Dispose()
		{
			_client.Dispose();
			_autoCache = default;
			_timer?.Dispose();
			_cache = null;
			Log = null;
		}
	}
}