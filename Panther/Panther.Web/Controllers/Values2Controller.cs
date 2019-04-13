using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Panther.Clients.Steam;

namespace Panther.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Values2Controller : Controller
    {
        private readonly ISteamClient _steamClient;

        public Values2Controller(ISteamClient steamClient)
        {
            _steamClient = steamClient;
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOwnedGamesResponse), 200)]
        [Route("users/{steamId}/games")]
        public async Task<IActionResult> GetOwnedGamesForSteamIdAsync(string steamId)
        {
            GetOwnedGamesResponse response = await _steamClient.GetOwnedGamesForSteamdIdAsync(steamId, true, true);
            response.Games = response.Games.OrderBy(game => game.Name).ToList();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetOwnedGamesResponse), 200)]
        [Route("users/{steamId}/games/{appId}/achievements")]
        public async Task<IActionResult> GetPlayerAchievementsForGameAsync(string steamId, string appId)
        {
            GetPlayerAchievementsResponse response = await _steamClient.GetPlayerAchievementsAsync(steamId, appId);

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SteamUser), 200)]
        [Route("users/{steamId}")]
        public async Task<IActionResult> GetPlayerSummary(string steamId)
        {
            GetSteamUsersResponse response = await _steamClient.GetPlayerSummariesAsync(steamId);

            return Ok(response.Players.First());
        }
    }
}
