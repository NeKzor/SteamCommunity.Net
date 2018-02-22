using System.Collections.Generic;
using Model = SteamCommunity.API.FriendsListModel;

namespace SteamCommunity
{
	public class Friends : IFriends
	{
		public ulong SteamId64 { get; private set; }
		public string SteamId { get; private set; }
		public IEnumerable<ulong> Items { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		internal static Friends Create(SteamCommunityClient client, Model model)
		{
			return new Friends()
			{
				SteamId64 = model.SteamId64,
				SteamId = model.SteamId,
				Items = model.Friends,
				Client = client
			};
		}
	}
}