using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Panther.Clients.Steam
{
    public class OwnedGame
    {
        [JsonProperty("appid")]
        public int AppId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("playtime_forever")]
        public int PlayTimeForeverMinutes { get; set; }
        [JsonProperty("img_logo_url")]
        public string ImagegLogoUrl { get; set; }
        [JsonProperty("img_icon_url")]
        public string ImageIconUrl { get; set; }
        [JsonProperty("has_community_visible_stats")]
        public bool HasCommunityVisibleStats { get; set; }
    }

    public class GetOwnedGamesResponse
    {
        public GetOwnedGamesResponse()
        {
            Games = new List<OwnedGame>();
        }

        [JsonProperty("game_count")]
        public int GameCount { get; set; }
        public ICollection<OwnedGame> Games { get; set; }
    }

    public class Achievement
    {
        public string ApiName { get; set; }
        public string Name { get; set; }
        public bool Achieved { get; set; }
        [JsonProperty("unlocktime")]
        public string UnlockedAtUnixStamp { get; set; }
        public string Description { get; set; }
    }

    public class GetPlayerAchievementsResponse
    {
        public GetPlayerAchievementsResponse()
        {
            Achievements = new List<Achievement>();
        }

        public string SteamId { get; set; }
        public string GameName { get; set; }

        public ICollection<Achievement> Achievements;
    }

    public enum PersonaStateEnum
    {
        Offline = 1,
        Online = 2,
        Busy = 3,
        Away = 4,
        Snooze = 5,
        LookingToPlay = 6
    }

    public class SteamUser
    {
        [JsonProperty("steamid")]
        public string SteamId { get; set; }
        [JsonProperty("personaname")]
        public string DisplayName { get; set; }
        [JsonProperty("profileurl")]
        public string ProfileUrl { get; set; }

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }
        [JsonProperty("avatarmedium")]
        public string MediumAvatarUrl { get; set; }
        [JsonProperty("avatarfull")]
        public string FullAvatarUrl { get; set; }

        // This will always return 0 if profile is considered
        // "private." Private data will not be shown
        // if profile is marked as 'Friends Only' or 'Private'

        [JsonProperty("personastate")]
        public int PersonaState { get; set; }

        [JsonProperty("lastlogoff")]
        public string LastLogOffUnixStamp { get; set; }

        // The following are private fields according to
        // Steam API docs. Private data will not be shown
        // if profile is marked as 'Friends Only' or 'Private'

        [JsonProperty("realname")]
        public string RealName { get; set; }
        [JsonProperty("timecreated")]
        public string AccountCreatedAtUnixStamp { get; set; }
    }

    public class GetSteamUsersResponse
    {
        public GetSteamUsersResponse()
        {
            Players = new List<SteamUser>();
        }

        public ICollection<SteamUser> Players { get; set; }
    }
}
