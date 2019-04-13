using System;
using System.Collections.Generic;
using System.Text;
using Panther.Clients.Steam;

namespace Panther.Clients
{
    public class PantherProfileRequest
    {
        public string SteamId { get; set; }
    }
    public class PantherProfileResponse
    {
        public SteamUser User { get; set; }
        public GetOwnedGamesResponse OwnedGames { get; set; }
    }
}
