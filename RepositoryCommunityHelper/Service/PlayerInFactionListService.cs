using System.Collections.Generic;
using System.Linq;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class PlayerInFactionListService
    {
        private readonly PlayerService _playerService;
        private readonly FactionService _factionService;

        public PlayerInFactionListService(PlayerService playerService, FactionService factionService)
        {
            _playerService = playerService;
            _factionService = factionService;
        }

        /**
         * Собственно наша бизнес-логика -- формирование подготовленного списка пользователей по их айдишникам.
         * Пример несколько натянут за уши, ибо отсюда не следует, откуда будет браться параметр,
         * но очень уж не хотелось лепить еще друзей сюда.
         * @return
         */
        public IEnumerable<PlayerInFactionDto> GetPlayersDtosByIds(List<int> ids)
        {
            List<PlayerInFactionDto> playerInFactionDtos = new List<PlayerInFactionDto>();
            //List<Player> players = playerService.getPlayers(friendIds);
            //List<int> ids = new List<int>();
            //ids.Add(1);
            //ids.Add(22);
            IEnumerable<Player> players = _playerService.GetPlayers(ids);
            foreach (Player player in players)
            {
                Faction faction = _factionService.GetFaction(player.FactionId);
                PlayerInFactionDto playerInFactionDto = new PlayerInFactionDto(player, faction);
                playerInFactionDtos.Add(playerInFactionDto);
            }
            return playerInFactionDtos;
        }

        /*
        public IEnumerable<PlayerInFactionDto> GetPlayersInFactionDtos(int id)
        {
            List<PlayerInFactionDto> playerInFactionDtos = new List<PlayerInFactionDto>();
            foreach (Player player in _playerService.GetFactionPlayers(id))
            {
                Faction faction = _factionService.GetFaction(id);
                PlayerInFactionDto playerInFactionDto = new PlayerInFactionDto(player, faction);
                playerInFactionDtos.Add(playerInFactionDto);
            }
            return playerInFactionDtos;
        }

        public IEnumerable<Player> GetFactionPlayers(int id)
        {
            return _playerService.GetFactionPlayers(id);
        }
        */
        
    }
}