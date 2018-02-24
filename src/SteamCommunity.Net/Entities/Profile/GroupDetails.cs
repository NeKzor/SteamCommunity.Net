using System.Threading.Tasks;
using Model = SteamCommunity.API.GroupModel;
using Model2 = SteamCommunity.API.GroupDetailsModel;

namespace SteamCommunity
{
	public class GroupDetails : IGroupDetails
	{
		public ulong Id { get; private set; }
		public string Name { get; private set; }
		public string Url { get; private set; }
		public string Headline { get; private set; }
		public string Summary { get; private set; }
		public string AvatarIcon { get; private set; }
		public string AvatarMedium { get; private set; }
		public string AvatarFull { get; private set; }
		public int MemberCount { get; private set; }
		public int MembersInChat { get; private set; }
		public int MembersInGame { get; private set; }
		public int MembersOnline { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IGroup> GetGroupAsync(uint page = 1, bool ignoreCache = false)
			=> await Client.GetGroup(Id, page, ignoreCache)
				.ConfigureAwait(false);

		internal static GroupDetails Create(SteamCommunityClient client, Model model)
		{
			return new GroupDetails()
			{
				Id = model.GroupId64,
				Name = model.GroupName,
				Url = model.GroupUrl,
				Headline = model.Headline,
				Summary = model.Summary,
				AvatarIcon = model.AvatarIcon,
				AvatarMedium = model.AvatarMedium,
				AvatarFull = model.AvatarFull,
				MemberCount = model.MemberCount,
				MembersInChat = model.MembersInChat,
				MembersInGame = model.MembersInGame,
				MembersOnline = model.MembersOnline,
				Client = client
			};
		}
		internal static GroupDetails Create(SteamCommunityClient client, ulong groupId64, Model2 model)
		{
			return new GroupDetails()
			{
				Name = model.GroupName,
				Url = model.GroupUrl,
				Headline = model.Headline,
				Summary = model.Summary,
				AvatarIcon = model.AvatarIcon,
				AvatarMedium = model.AvatarMedium,
				AvatarFull = model.AvatarFull,
				MemberCount = model.MemberCount,
				MembersInChat = model.MembersInChat,
				MembersInGame = model.MembersInGame,
				MembersOnline = model.MembersOnline,
				Id = groupId64,
				Client = client
			};
		}
	}
}