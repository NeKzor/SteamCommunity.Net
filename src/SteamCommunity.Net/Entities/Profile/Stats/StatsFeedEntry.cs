using Model = SteamCommunity.API.StatsFeedItemModel;

namespace SteamCommunity
{
	public class StatsFeedEntry : IStatsFeedEntry
	{
		public string ApiName { get; private set; }
		public double Value { get; private set; }

		internal static StatsFeedEntry Create(Model model)
		{
			return new StatsFeedEntry()
			{
				ApiName = model.ApiName,
				Value = model.Value
			};
		}
	}
}