using System;
using System.Collections.Generic;
using System.Text;
using Panther.Clients.Steam;

namespace Panther.Clients
{
    public class PantherGameInfoRequest
    {
        public string SteamId { get; set; }
        public string AppId { get; set; }
    }

    public class PantherGameInfo
    {
        public int TotalAchievements { get; set; }
        public int TotalUnlockedAchievements { get; set; }
        public decimal UnlockedAchievementsPercentage { get; set; }
        public GetPlayerAchievementsResponse AchievementList { get; set; }
    }
}
