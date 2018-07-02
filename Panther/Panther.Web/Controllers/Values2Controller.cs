using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Panther.Clients.Steam;

namespace Panther.Web.Controllers
{
    [Route("api/[controller]")]
    public class Values2Controller : Controller
    {
        private readonly ISteamClient _steamClient;

        public Values2Controller(ISteamClient steamClient)
        {
            _steamClient = steamClient;
        }

        // GET api/values
        [HttpGet]
        [ProducesResponseType(typeof(GetOwnedGamesResponse), 200)]
        public async Task<IActionResult> GetOwnedGamesForSteamIdAsync(string steamId)
        {
            var steamClient = _steamClient;

            GetOwnedGamesResponse response = await _steamClient.GetOwnedGamesForSteamdIdAsync(steamId, true, true);
            response.Games = response.Games.OrderByDescending(game => game.Name).ToList();

            return Ok(response);
        }
    }
}
