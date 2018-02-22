using System.Threading.Tasks;
using Model = SteamCommunity.API.StatsLeaderboardItemModel;

namespace SteamCommunity
{
	public class StatsLeaderboardEntry : IStatsLeaderboardEntry
	{
		public int Id { get; private set; }
		public string Url { get; private set; }
		public string Name { get; private set; }
		public string DisplayName { get; private set; }
		public int Entries { get; private set; }
		public int SortMethod { get; private set; }
		public int DisplayType { get; private set; }
		
		internal static StatsLeaderboardEntry Create(Model model)
		{
			return new StatsLeaderboardEntry()
			{
				Id = model.LbId,
				Url = model.Url,
				Name = model.Name,
				DisplayName = model.DisplayName,
				Entries = model.Entries,
				SortMethod = model.SortMethod,
				DisplayType = model.DisplayType
			};
		}
	}
}