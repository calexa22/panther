using System;
namespace Panther.Clients.Steam
{
    public static class SteamParams
    {
        public static class Shared
        {
            public const string APP_ID = "appid";
            public const string KEY = "key";
            public const string LANGUAGE = "l";
            public const string STEAM_ID = "steamid";
            public const string STEAM_IDS = "steamids";
        }

        public static class PlayerService
        {
            public const string INCLUDE_APPINFO = "include_appinfo";
            public const string INCLUDE_PAYED_FREE_GAMES = "include_played_free_games";
        }
    }
}
