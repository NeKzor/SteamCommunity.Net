using Newtonsoft.Json;

namespace Seum
{
    internal class ScoreItem
    {
        [JsonProperty("ranks")]
        public System.Collections.Generic.List<uint> Ranks { get; set; }
    }
}
