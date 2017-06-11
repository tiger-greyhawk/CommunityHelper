using System.Collections.Generic;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.DAO
{
    public class FactionPlayerDao
    {
        private readonly IService _restClient;
        private readonly ConverterJson _converterJson;

        public FactionPlayerDao(IService restClient, ConverterJson converterJson)
        {
            _restClient = restClient;
            _converterJson = converterJson;
        }

        public IEnumerable<FactionPlayer> GetFactionPlayers()
        {
            return _converterJson.ConvertJsonToFactionPlayersCollection(_restClient.CreateRequest().DoGetAsync("faction-player/all"));
        }
    }
}