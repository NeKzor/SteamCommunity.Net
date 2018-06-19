using System;
using System.Threading.Tasks;

namespace LeastPortals
{
    internal class Program
    {
		private static async Task Main(string[] args)
		{
			var builder = new WebPageBuilder("LeastPortals/2.1");

			//await builder.Initialize();
			//await builder.Export("gh-pages/unfiltered.json");
			//await builder.Filter();
			//await builder.Export("gh-pages/players.json");
			await builder.Import("gh-pages/players.json");
			await builder.Build("gh-pages/lp.html", 20);
		}
    }
}