using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SteamCommunity.API
{
	internal sealed class ApiClient : IDisposable
	{
		private readonly HttpClient _client;

		public ApiClient(string userAgent = default)
		{
			_client = new HttpClient();
			_client.DefaultRequestHeaders.UserAgent.ParseAdd
			(
				"SteamCommunity.Net/1.0" +
				((!string.IsNullOrEmpty(userAgent)) ? $" {userAgent}" : string.Empty)
			);
		}

		public async Task<T> GetXmlObjectAsync<T>(string url)
		{
			var get = new HttpRequestMessage(HttpMethod.Get, url);
			var response = await _client.SendAsync(get, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
			response.EnsureSuccessStatusCode();
			var xml = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			return await Task.Run(() => (T)new XmlSerializer(typeof(T)).Deserialize(new StringReader(xml) as TextReader));
		}

		public void Dispose()
		{
			_client.Dispose();
		}
	}
}