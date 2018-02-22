using Model = SteamCommunity.API.StatsModel;

namespace SteamCommunity
{
	public class StatsBase : IStatsBase
	{
		public float HoursPlayed { get; set; }

		internal static StatsBase Create(Model model)
		{
			return new StatsBase()
			{
				HoursPlayed = model.HoursPlayed
			};
		}
	}
}