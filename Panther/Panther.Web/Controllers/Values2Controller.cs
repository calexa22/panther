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
        public GetOwnedGamesResponse Get(string steamId)
        {
            var steamClient = _steamClient;

            Task<GetOwnedGamesResponse> responseTask = _steamClient.GetOwnedGamesForSteamdId(steamId, true, true);

            Task.WaitAll(responseTask);

            GetOwnedGamesResponse response = responseTask.Result;

            response.Games = response.Games.OrderByDescending(game => game.Name).ToList();
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
