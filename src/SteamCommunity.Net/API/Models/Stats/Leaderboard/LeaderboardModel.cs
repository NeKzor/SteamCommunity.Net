using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("response")]
	public class LeaderboardModel
	{
		[XmlElement("appID")]
		public int AppId { get; set; }
		[XmlElement("appFriendlyName")]
		public string AppFriendlyName { get; set; }
		[XmlElement("leaderboardID")]
		public int LeaderboardId { get; set; }
		[XmlElement("totalLeaderboardEntries")]
		public int TotalLeaderboardEntries { get; set; }
		[XmlElement("steamID64")]
		public ulong? SteamId64 { get; set; }
		[XmlElement("entryStart")]
		public int? EntryStart { get; set; }
		[XmlElement("entryEnd")]
		public int? EntryEnd { get; set; }
		[XmlElement("nextRequestURL")]
		public string NextRequestUrl { get; set; }
		[XmlElement("resultCount")]
		public int ResultCount { get; set; }
		[XmlArray("entries")]
		public List<LeaderboardEntryModel> Entries { get; set; }
	}
}