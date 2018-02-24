using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.FriendsListModel;

namespace SteamCommunity
{
	public class Friends : IFriends
	{
		public ulong Id { get; private set; }
		public string Name { get; private set; }
		public IEnumerable<ulong> List { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IPublicProfile> GetProfileAsync(bool ignoreCache = false)
			=> await Client.GetProfileAsync(Id, ignoreCache)
				.ConfigureAwait(false);
		public async Task<IPublicProfile> GetProfileAsync(ulong steamId64, bool ignoreCache = false)
			=> await Client.GetProfileAsync(steamId64, ignoreCache)
				.ConfigureAwait(false);

		internal static Friends Create(SteamCommunityClient client, Model model)
		{
			return new Friends()
			{
				Id = model.SteamId64,
				Name = model.SteamId,
				List = model.Friends,
				Client = client
			};
		}
	}
}