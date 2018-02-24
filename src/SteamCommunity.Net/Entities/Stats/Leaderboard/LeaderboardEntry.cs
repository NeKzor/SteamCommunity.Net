using Model = SteamCommunity.API.LeaderboardEntryModel;

namespace SteamCommunity
{
	public class LeaderboardEntry : ILeaderboardEntry
	{
		public ulong Id { get; private set; }
		public int Score { get; private set; }
		public int Rank { get; private set; }
		public string UgcId { get; private set; }
		public string Details { get; private set; }

		internal static LeaderboardEntry Create(Model model)
		{
			return new LeaderboardEntry()
			{
				Id = model.SteamId,
				Score = model.Score,
				Rank = model.Rank,
				UgcId = model.UgcId,
				Details = model.Details
			};
		}
	}
}