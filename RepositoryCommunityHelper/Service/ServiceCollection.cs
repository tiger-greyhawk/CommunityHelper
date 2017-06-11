using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class ServiceCollection : BaseMagic
    {
        private readonly PlayerService _playerService;
        private readonly FactionService _factionService;
        private readonly FactionPlayerService _factionPlayerService;
        private readonly UserService _userService;
        //private readonly PlayerInFactionListService _playerInFactionListService;

        public ServiceCollection(UserService userService, PlayerService playerService, FactionService factionService, FactionPlayerService factionPlayerService)//, PlayerInFactionListService playerInFactionListService)
        {
            _userService = userService;
            _playerService = playerService;
            _factionService = factionService;
            _factionPlayerService = factionPlayerService;
            //_playerInFactionListService = playerInFactionListService;
        }

        public UserService UserService
        {
            get { return _userService; }
        }

        public PlayerService PlayerService
        {
            get
            {
                //FactionService.GetFactions();
                return _playerService;
            }
        }

        public FactionService FactionService
        {
            get { return _factionService; }
        }

        public FactionPlayerService FactionPlayerService
        {
            get { return _factionPlayerService; }
        }

        /*
        public PlayerInFactionListService PlayerInFactionListService
        {
            get { return _playerInFactionListService; }
        }
        */

        public ObservableCollection<PlayerDto> GetPlayersInFaction(int factionId)
        {
            
            ObservableCollection<PlayerDto> players = new ObservableCollection<PlayerDto>();
            //IEnumerable<FactionPlayer> factionPlayer = _factionPlayerService.GetFactionPlayers();
            IEnumerable<FactionPlayer> factionPlayer = _factionPlayerService.FactionPlayers;

            foreach (FactionPlayer player in factionPlayer)
            {
                if (player.FactionId == factionId)
                        players.Add(PlayerService.GetPlayer(player.PlayerId));
            }
            return players;

        }

        public ObservableCollection<PlayerDto> PlayerDtos
        {
            get
            {
                //Mapper.Mapper mapper = new Mapper.Mapper();
                //ObservableCollection<PlayerDto> players = new ObservableCollection<PlayerDto>();
                //players = mapper.Map(PlayerService.Players);
                //throw new Exception("Переделать здесь!!! Надо не новую коллекцию создавать!");
                IEnumerable<FactionPlayer> factionPlayer = _factionPlayerService.FactionPlayers;
                foreach (PlayerDto player in PlayerService.Players)
                {
                    //factionPlayer.FirstOrDefault(p => p.PlayerId == player.Id)
                    //if (player.FactionId != 0)
                    if (factionPlayer.FirstOrDefault(p => p.PlayerId == player.Id) != null)
                        player.Faction = FactionService.GetFactionById(factionPlayer.FirstOrDefault(p => p.PlayerId == player.Id).FactionId).Name;
                }
                return PlayerService.Players;

            }
            
        }

        public ObservableCollection<FactionDto> FactionDtos
        {
            get { return FactionService.FactionsCollection; }

        }
    }
}