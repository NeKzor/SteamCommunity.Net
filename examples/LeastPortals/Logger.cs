using System;
using System.Threading.Tasks;
using SteamCommunity;

namespace LeastPortals
{
    internal static class Logger
    {
        public static Task LogSteamCommunityClient(object _, LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
