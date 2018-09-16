using System;
using System.Threading.Tasks;
using Portal2Boards;
using SteamCommunity;

namespace Seum
{
    internal static class Logger
    {
        public static Task LogSteamCommunityClient(object _, SteamCommunity.LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        public static Task LogPortal2BoardsClient(object _, Portal2Boards.LogMessage message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
