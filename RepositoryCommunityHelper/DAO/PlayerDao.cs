using System;
using System.Collections.Generic;
using System.Linq;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.WebService;

namespace RepositoryCommunityHelper.DAO
{
    public class PlayerDao : IPlayerDao
    {
        private readonly IService _restClient;
        private readonly ConverterJson _converterJson;

        public PlayerDao(IService restClient, ConverterJson converterJson)
        {
            if (restClient == null)
                throw new ArgumentNullException(nameof(restClient));
            if (converterJson == null)
                throw new ArgumentNullException(nameof(converterJson));
            _restClient = restClient;
            _converterJson = converterJson;
        }

        public IEnumerable<Player> GetPlayersByFactionId(int factionId)
        {
            return GetPlayers().Where(p => p.FactionId == factionId);
            //GetPlayers().Select()
            //return data.stream().filter((p)->p.getFractionId() == fractionId).collect(Collectors.toList());
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _converterJson.ConvertJsonToPlayersCollection(_restClient.CreateRequest().DoGetAsync("player"));
        }

        public IEnumerable<Player> GetPlayers(List<int> ids)
        {
            IEnumerable<Player> players = GetPlayers();
            //players.Select(player => player.Id, ids)
            List<Player> result = new List<Player>();

            
            foreach (int id in ids)
            {
                Player player = (from p in players where p.Id == id select p).First();
                if (player != null)
                result.Add(player);
            }
            IEnumerable<Player> result1 = new List<Player>(result);
            return result1;
        }
    

        public Player GetPlayer(int id)
        {
            return _converterJson.ConvertJsonToPlayer(_restClient.CreateRequest().DoGetAsync("player/"+id));
        }

        public IEnumerable<Player> GetFactionPlayers(int factionId)
        {
            using (var restClient = _restClient)
            {
                return _converterJson.ConvertJsonToPlayersCollection(restClient.CreateRequest().DoGetAsync("faction/players/" + factionId));
            }
            
        }

        public Player SavePlayer(Player player)
        {
            return
                _converterJson.ConvertJsonToPlayer(
                    _restClient.CreateRequest().DoPostAsync(_converterJson.ConvertPlayerToJson(player), "player/"));
        }

        public Player DeletePlayer(Player player)
        {
            /*try
            {
                _converterJson.ConvertJsonToPlayer(_restClient.CreateRequest()
                    .DoDeleteAsync(_converterJson.ConvertPlayerToJson(player), "player/" + player.Id));
            }
            catch (Exception e)
            {
                
            }*/
            //if () return;
            //player.Id = 1000;
            return _converterJson.ConvertJsonToPlayer(_restClient.CreateRequest()
                    .DoDeleteAsync(_converterJson.ConvertPlayerToJson(player), "player/" + player.Id));


        }

        public IEnumerable<Player> SetActivePlayer(Player player)
        {
            using (var restClient = _restClient)
            {
                string temp = restClient.CreateRequest().DoPutAsync(_converterJson.ConvertPlayerToJson(player), "user/players/"+ player.Id);
                IEnumerable<Player> playersTemp = _converterJson.ConvertJsonToPlayersCollection(temp);
                return playersTemp;
            }
        }

        public IEnumerable<Player> GetMyPlayers()
        {
            using (var restClient = _restClient)
            {
                string temp = restClient.CreateRequest().DoGetAsync("user/players");
                IEnumerable<Player> playersTemp = _converterJson.ConvertJsonToPlayersCollection(temp);
                return playersTemp;
            }
        }
    }
}