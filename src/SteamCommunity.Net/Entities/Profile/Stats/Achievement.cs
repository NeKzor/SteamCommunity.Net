using Model = SteamCommunity.API.AchievementModel;

namespace SteamCommunity
{
	public class Achievement : IAchievement
	{
		public string IconClosed { get; private set; }
		public string IconOpen { get; private set; }
		public string Name { get; private set; }
		public string ApiName { get; private set; }
		public string Description { get; private set; }
		public ulong UnlockTimestamp { get; private set; }

		internal static Achievement Create(Model model)
		{
			return new Achievement()
			{
				IconClosed = model.IconClosed,
				IconOpen = model.IconOpen,
				Name = model.Name,
				ApiName = model.ApiName,
				Description = model.Description,
				UnlockTimestamp = model.UnlockTimestamp
			};
		}
	}
}