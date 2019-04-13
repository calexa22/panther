using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Panther.Clients;
using Panther.Clients.Steam;

namespace Panther.Web.Controllers
{
    [Route("api/[controller]")]
    public class PantherController : Controller
    {
        ISteamClient _steamClient;

        public PantherController(ISteamClient steamClient)
        {
            _steamClient = steamClient;
        }

        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(typeof(PantherProfileResponse), 200)]
        public async Task<ActionResult> Profile([FromBody] PantherProfileRequest request)
        {
            var steamUserTask = _steamClient.GetPlayerSummariesAsync(request.SteamId);
            var ownedGamesTask = _steamClient.GetOwnedGamesForSteamdIdAsync(request.SteamId, true, true);

            await Task.WhenAll(steamUserTask, ownedGamesTask);
            if (steamUserTask.Result.Players.Count == 0)
            {
                return NotFound($"User not found for {request.SteamId}");
            }
            else if (steamUserTask.Result.Players.Count > 1)
            {
                return NotFound($"Multiple users found for {request.SteamId}");
            }

            var profile = new PantherProfileResponse
            {
                User = steamUserTask.Result.Players.Single(),
                OwnedGames = ownedGamesTask.Result
            };

            return Ok(profile);
        }

        // GET api/<controller>/5
        [HttpPost]
        [Route("gameinfo")]
        [ProducesResponseType(typeof(PantherGameInfo), 200)]
        public async Task<ActionResult> GameInfo([FromBody] PantherGameInfoRequest request)
        {
            //var steamUserTask = _steamClient.GetPlayerSummariesAsync(request.SteamId);
            var achievements = await _steamClient.GetPlayerAchievementsAsync(request.SteamId, request.AppId);

            if (achievements == null)
            {
                return NotFound($"Game {request.AppId} does not exist or is not owned by user {request.SteamId}");
            }

            var gameInfo = new PantherGameInfo
            {
                TotalAchievements = achievements.Achievements.Count,
                TotalUnlockedAchievements = achievements.Achievements.Where(a => a.Achieved).ToList().Count,
                AchievementList = achievements
            };

            gameInfo.UnlockedAchievementsPercentage = Math.Round((Convert.ToDecimal(gameInfo.TotalUnlockedAchievements) / gameInfo.TotalAchievements), 2, MidpointRounding.AwayFromZero);
            
            return Ok(gameInfo);
        }
    }
}
