using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
            
namespace Panther.Clients.Steam
{
    public interface ISteamClient
    {
        Task<GetOwnedGamesResponse> GetOwnedGamesForSteamdId(string steamdId, bool includeAppInfo, bool includedPlayedFreeGames);
    }

    public class SteamClient : HttpClientBase, ISteamClient
    {
        public SteamClient(string baseUri, string token)
            : base(baseUri, token)
        {
            // because I'm the steam api and I'm dumb asf
            ApiKey = token;
        }

        public string ApiKey { get; private set;}

        public async Task<GetOwnedGamesResponse> GetOwnedGamesForSteamdId(string steamId, bool includeAppInfo, bool includedPlayedFreeGames)
        {
            Dictionary<string, string> query = GetDefaultSteamQueryParams();

            query[SteamParams.Shared.STEAM_ID] = ToQueryParamStringValue(steamId);
            query[SteamParams.PlayerService.INCLUDE_APPINFO] = ToQueryParamStringValue(includeAppInfo);
            query[SteamParams.PlayerService.INCLUDE_PAYED_FREE_GAMES] = ToQueryParamStringValue(includedPlayedFreeGames);

            //Thanks Steam!!!
            Dictionary<string, GetOwnedGamesResponse> thanksSteam = 
                await GetAsync<Dictionary<string, GetOwnedGamesResponse>>(SteamRoutes.GetOwnedGames, query);

            if (thanksSteam.Any())
            {
                return thanksSteam.Single().Value;
            }

            return new GetOwnedGamesResponse();
        }

        private Dictionary<string, string> GetDefaultSteamQueryParams()
        {
            //SIGH
            return new Dictionary<string, string>()
            {
                {SteamParams.Shared.KEY, ApiKey}
            };
        }

        private static string ToQueryParamStringValue(object arg)
        {
            if (arg is string)
            {
                return arg as string;
            }
            else if (arg is bool)
            {
                return ((bool)arg) ? "1" : "0";
            }

            throw new NotImplementedException($"{arg.GetType()} not yet implemented for ToQueryParamStringValue.");
        }
    }
}
