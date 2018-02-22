using System;
using System.Net.Http;
using System.Xml;

namespace SteamCommunity
{
	public class LogMessage
	{
		public Exception Error { get; }
		public string Message { get; }

		public LogMessage(Type type, Exception error)
		{
			Error = error;
			if (Error is XmlException)
				Message = $"[LeastPortals.Core] Failed to parse {type} object.";
			else if (Error is HttpRequestException)
				Message = $"[LeastPortals.Core] Failed to fetch {type} object.";
			else
				Message = $"[LeastPortals.Core] Failed to create {type} object.";
		}

		public override string ToString()
		{
			return Message +
				"\n --- Stack Trace ---" +
				$"\n{Error}" +
				"\n -------------------";
		}
	}
}