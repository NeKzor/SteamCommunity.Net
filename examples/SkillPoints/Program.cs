using System;
using System.Threading.Tasks;

namespace SkillPoints
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new WebPageBuilder("SkillPoints/1.0");

            var top = 20;
            await builder.Fetch(top);
            await builder.Build("gh-pages/skill.html", top);
        }
    }
}
