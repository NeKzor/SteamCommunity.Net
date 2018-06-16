using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LeastPortals
{
	internal class Statistics
	{
		private Dictionary<ulong, int> _tiedRecords { get; set; }

		public Statistics() => _tiedRecords = new Dictionary<ulong, int>();

		public int GetRecordCount(ulong id)
			=> _tiedRecords.GetValueOrDefault(id);
		public void SetRecordCount(ulong id, int count)
			=> _tiedRecords.Add(id, count);

		public async Task Export(string file)
		{
			if (File.Exists(file)) File.Delete(file);
			await File.WriteAllTextAsync(file, JsonConvert.SerializeObject(_tiedRecords));
		}
		public async Task Import(string file)
		{
			if (!File.Exists(file)) return;
			_tiedRecords = JsonConvert.DeserializeObject<Dictionary<ulong, int>>(await File.ReadAllTextAsync(file));
		}
	}
}