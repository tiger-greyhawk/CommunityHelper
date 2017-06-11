using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.DAO
{
    public class FactionDao : IFactionDao
    {
        private readonly IService _restClient;
        private readonly ConverterJson _converterJson;

        public FactionDao(IService restClient, ConverterJson converterJson)
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));
            if (converterJson == null)
                throw new ArgumentNullException(nameof(converterJson));
            _restClient = restClient;
            _converterJson = converterJson;
        }

        public IEnumerable<Faction> GetFactions()
        {
            
                using (var restClient = this._restClient.CreateRequest())
                {

                    return
                        _converterJson.ConvertJsonToFactionsCollection(
                            restClient.CreateRequest().DoGetAsync("faction"));
                }
            
        }

        public IEnumerable<Faction> GetFactions(List<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public Faction GetFaction(int id)
        {
            return _converterJson.ConvertJsonToFaction(_restClient.CreateRequest().DoGetAsync("faction/"+id));
        }

        public Faction SaveFaction(Faction faction)
        {
            
            return _converterJson.ConvertJsonToFaction(_restClient.CreateRequest().DoPostAsync(_converterJson.ConvertFactionToJson(faction), "faction/"));

        }

        public Player JoinFaction(int factionId, Player player)
        {
            player.Nick = _restClient.Create().Login;
            string json = _restClient.CreateRequest()
                .DoPostAsync(_converterJson.ConvertPlayerToJson(player), "faction/join/" + factionId);
            if (json.Length > 0 && json != "304")
                return _converterJson.ConvertJsonToPlayer(json);
            else if (json == "304") return null;
            else return null;
        }

        public Player EliminateFromFaction(int factionId, Player player)
        {
            //player.Nick = _restClient.Create().Login;
            string json = _restClient.CreateRequest()
                .DoDeleteAsync(_converterJson.ConvertPlayerToJson(player), "faction/invite/" + factionId);
            if (json.Length > 0 && json != "304")
                return _converterJson.ConvertJsonToPlayer(json);
            else if (json == "304") return null;
            else return null;
        }

        public Faction DeleteFaction(Faction faction)
        {

            return _converterJson.ConvertJsonToFaction(_restClient.CreateRequest().DoDeleteAsync(_converterJson.ConvertFactionToJson(faction), "faction/"+faction.Id));

        }
    }
}