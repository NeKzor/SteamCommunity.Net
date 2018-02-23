using System;

namespace LeastPortals
{
    internal class Program
    {
		private static void Main(string[] args)
		{
			var builder = new WebPageBuilder("Portal 2");

			//builder.Initialize().GetAwaiter().GetResult();
			//builder.Export().GetAwaiter().GetResult();

			builder.Import().GetAwaiter().GetResult();
			builder.Build("gh-pages/lp.html").GetAwaiter().GetResult();
		}
    }
}