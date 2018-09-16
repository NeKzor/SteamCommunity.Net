using System;
using System.Threading.Tasks;

namespace Seum
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new WebPageBuilder("Seum/1.0");

            await builder.Fetch(30);
            await builder.Build("gh-pages/seum.html", 30);
        }
    }
}
