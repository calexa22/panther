using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Panther.Clients.Steam
{
    public class OwnedGame
    {
        public int AppId { get; set; }
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
}
