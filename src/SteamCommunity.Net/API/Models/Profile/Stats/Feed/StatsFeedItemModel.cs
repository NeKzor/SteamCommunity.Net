using System.Xml.Serialization;

namespace SteamCommunity.API
{
	[XmlType("item")]
	public class StatsFeedItemModel
	{
		[XmlElement("APIName")]
		public string ApiName { get; set; }
		[XmlElement("value")]
		public double Value { get; set; }
	}
}