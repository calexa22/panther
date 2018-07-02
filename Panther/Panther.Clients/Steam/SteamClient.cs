using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
            
namespace Panther.Clients.Steam
{
    public interface ISteamClient
    {
        Task<GetOwnedGamesResponse> GetOwnedGamesForSteamdIdAsync(string steamId, bool includeAppInfo, bool includedPlayedFreeGames);
        Task<GetPlayerAchievementsResponse> GetPlayerAchievementsAsync(string steam, string appId);
        Task<GetSteamUsersResponse> GetPlayerSummariesAsync(ICollection<string> steamIds);
    }

    public class SteamClient : HttpClientBase, ISteamClient
    {
        private const string ENGLISH = "english";

        public SteamClient(string baseUri, string token)
            : base(baseUri, token)
        {
            // because I'm the steam api and I'm dumb asf
            ApiKey = token;
        }

        public string ApiKey { get; private set;}

        public async Task<GetOwnedGamesResponse> GetOwnedGamesForSteamdIdAsync(string steamId, bool includeAppInfo, bool includedPlayedFreeGames)
        {
            Dictionary<string, string> query = GetDefaultSteamQueryParams();

            query[SteamParams.Shared.STEAM_ID] = ToQueryParamStringValue(steamId);
            query[SteamParams.PlayerService.INCLUDE_APPINFO] = ToQueryParamStringValue(includeAppInfo);
            query[SteamParams.PlayerService.INCLUDE_PAYED_FREE_GAMES] = ToQueryParamStringValue(includedPlayedFreeGames);

            return await PraiseGabenAsync<GetOwnedGamesResponse>(SteamRoutes.GetOwnedGames, query);
        }

        public async Task<GetPlayerAchievementsResponse> GetPlayerAchievementsAsync(string steamId, string appId)
        {
            Dictionary<string, string> query = GetDefaultSteamQueryParams();

            query[SteamParams.Shared.STEAM_ID] = ToQueryParamStringValue(steamId);
            query[SteamParams.Shared.APP_ID] = ToQueryParamStringValue(appId);
            query[SteamParams.Shared.LANGUAGE] = ToQueryParamStringValue(ENGLISH);

            return await PraiseGabenAsync<GetPlayerAchievementsResponse>(SteamRoutes.GetPlayerAchievements, query);
        }

        public async Task<GetSteamUsersResponse> GetPlayerSummariesAsync(ICollection<string> steamIds)
        {
            Dictionary<string, string> query = GetDefaultSteamQueryParams();

            query[SteamParams.Shared.STEAM_IDS] = ToQueryParamStringValue(steamIds);

            return await PraiseGabenAsync<GetSteamUsersResponse>(SteamRoutes.GetPlayerSummaries, query);
        }
        
        private async Task<T> PraiseGabenAsync<T>(string url, Dictionary<string, string> query) where T : new()
        {
            Dictionary<string, T> responseDictionary =
                await GetAsync<Dictionary<string, T>>(url, query);

            if (responseDictionary.Any())
            {
                return responseDictionary.Single().Value;
            }

            return new T();
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
            else if (arg is ICollection<string>)
            {
                return string.Join(',', (ICollection<string>)arg);
            }

            throw new NotImplementedException($"{arg.GetType()} not yet implemented for ToQueryParamStringValue.");
        }
    }
}
