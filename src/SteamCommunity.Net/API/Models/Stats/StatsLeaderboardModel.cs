using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("response")]
	public class StatsLeaderboardModel
	{
		[XmlElement("appID")]
		public int AppId { get; set; }
		[XmlElement("appFriendlyName")]
		public string AppFriendlyName { get; set; }
		[XmlElement("leaderboardCount")]
		public int LeaderboardCount { get; set; }
		[XmlElement("leaderboard")]
		public List<StatsLeaderboardItemModel> Leaderboard { get; set; }
	}
}