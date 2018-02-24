using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model = SteamCommunity.API.MemberListModel;

namespace SteamCommunity
{
	public class Group : IGroup
	{
		public ulong Id { get; private set; }
		public IGroupDetails Details { get; private set; }
		public int MemberCount { get; private set; }
		public int TotalPages { get; private set; }
		public int CurrentPage { get; private set; }
		public int StartingMember { get; private set; }
		public string NextPageLink { get; private set; }
		public IEnumerable<ulong> Members { get; private set; }

		internal SteamCommunityClient Client { get; private set; }

		public async Task<IGroup> GetNextAsync(uint jumps = 1, bool ignoreCache = false)
		{
			if (CurrentPage + jumps > TotalPages)
				throw new InvalidOperationException("Invalid page jump.");
			
			return await Client.GetGroup(Id, (uint)CurrentPage + jumps, ignoreCache)
				.ConfigureAwait(false);
		}
		public async Task<IGroup> GetPrevious(uint jumps = 1, bool ignoreCache = false)
		{
			if (CurrentPage - jumps < 1)
				throw new InvalidOperationException("Invalid page jump.");
			
			return await Client.GetGroup(Id, (uint)CurrentPage - jumps, ignoreCache)
				.ConfigureAwait(false);
		}

		internal static Group Create(SteamCommunityClient client, Model model)
		{
			return new Group()
			{
				Id = model.GroupId64,
				Details = GroupDetails.Create(client, model.GroupId64, model.GroupDetails),
				MemberCount = model.MemberCount,
				TotalPages = model.TotalPages,
				CurrentPage = model.CurrentPage,
				StartingMember = model.StartingMember,
				NextPageLink = model.NextPageLink,
				Members = model.Members,
				Client = client
			};
		}
	}
}