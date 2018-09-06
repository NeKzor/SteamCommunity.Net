using System;
using System.Threading.Tasks;

namespace SkillPoints
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new WebPageBuilder("SkillPoints/2.0");

            await builder.Fetch(30);
            await builder.Build("gh-pages/skill.html", 30);
        }
    }
}
