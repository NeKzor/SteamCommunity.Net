using System.Xml.Serialization;

namespace SteamCommunity.API
{
	public class StatsModel
	{
		[XmlElement("hoursPlayed")]
		public float HoursPlayed { get; set; }
		// Game specific stats here
	}
}