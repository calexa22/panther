using System;
namespace Panther.Clients.Steam
{
    public static class SteamRoutes
    {
        public const string GetOwnedGames = "IPlayerService/GetOwnedGames/v0001";
        public const string GetPlayerAchievements = "ISteamUserStats/GetPlayerAchievements/v0001";
        public const string GetPlayerSummariesV2 = "ISteamUser/GetPlayerSummaries/v0002";
    }
}
