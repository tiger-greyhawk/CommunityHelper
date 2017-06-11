

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RepositoryCommunityHelper.DAO;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class PlayerService : BaseMagic
    {
        private readonly PlayerDao _playerDao;
        private ObservableCollection<PlayerDto> _players;
        private IMapper _mapper;


        public PlayerService(PlayerDao playerDao, IMapper mapper)//, ObservableCollection<PlayerDto> playerDtos )
        {
            if (playerDao == null)
                throw new ArgumentNullException(nameof(playerDao));
            _playerDao = playerDao;
            _mapper = mapper;
            //_players = playerDtos;
        }

        public ObservableCollection<PlayerDto> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public void UpdatePlayersCache()
        {
            Players = _mapper.Map(_playerDao.GetPlayers());
        }

        /*private IEnumerable<Player> GetPlayers()
        {
            //Players = _playerDao.GetPlayers();
            return _playerDao.GetPlayers();
        }*/

        public IEnumerable<Player> GetPlayers(List<int> ids)
        {
            return _playerDao.GetPlayers(ids);
        }

        public PlayerDto GetPlayer(int id)
        {
            return Players.FirstOrDefault(p => p.Id == id);
            //return _mapper.Map(_playerDao.GetPlayer(id));
        }

        public IEnumerable<PlayerDto> GetPlayersInFaction(int factionId)
        {
            List<PlayerDto> players = new List<PlayerDto>();
            foreach (PlayerDto player in Players)
            {
                if (player.FactionId == factionId)
                    players.Add(player);
            }
            return players;
            //return _playerDao.GetFactionPlayers(factionId);
        }

        public Player SavePlayer(Player player)
        {
            return _playerDao.SavePlayer(player);
        }

        public Player DeletePlayer(Player player)
        {
            return _playerDao.DeletePlayer(player);
        }

        public IEnumerable<Player> SetActivePlayer(Player player)
        {
            return _playerDao.SetActivePlayer(player);
        }

        public IEnumerable<Player> GetMyPlayers()
        {
            return _playerDao.GetMyPlayers();
        }

    }
}