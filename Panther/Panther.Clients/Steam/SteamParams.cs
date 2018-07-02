using System;
namespace Panther.Clients.Steam
{
    public static class SteamParams
    {
        public static class Shared
        {
            public const string KEY = "key";
            public const string STEAM_ID = "steamid";
        }

        public static class PlayerService
        {
            public const string INCLUDE_APPINFO = "include_appinfo";
            public const string INCLUDE_PAYED_FREE_GAMES = "include_played_free_games";
        }
    }
}
