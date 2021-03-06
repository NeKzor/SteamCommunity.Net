using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("playerstats")]
	public class PlayerStatsModel
	{
		[XmlElement("privacyState")]
		public string PrivacyState { get; set; }
		[XmlElement("visibilityState")]
		public int VisibilityState { get; set; }
		[XmlElement("game")]
		public PlayerStatsGameModel Game { get; set; }
		[XmlElement("player")]
		public PlayerStatsPlayerModel Player { get; set; }
		[XmlElement("stats")]
		public StatsModel Stats { get; set; }
		[XmlArray("achievements")]
		public List<AchievementModel> Achievements { get; set; }
	}
}