using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RepositoryCommunityHelper.DTO.EventArgs;
using RepositoryCommunityHelper.Service;


namespace RepositoryCommunityHelper.DTO.Collection
{
    public class PlayerDtoCollection : BaseMagic
    {
        
        public event EventHandler<PlayerDtoEventArgs> PlayerDtoUpdated = delegate { };
        private readonly PlayerService _playerService;

        public PlayerDtoCollection(PlayerService playerService)
        {
            _playerService = playerService;
            PlayerDtos = new ObservableCollection<PlayerDto>();
            //GetPlayers();
            
        }

        public ObservableCollection<PlayerDto> PlayerDtos { get;
            //{
            //Mapper.Mapper mapper = new Mapper.Mapper();
            //return mapper.Map(_playerService.GetPlayers());
            //}
            set;
        }

        /*
        public void UpdatePlayer(PlayerDto updatedPlayerDto)
        {
            GetPlayer(updatedPlayerDto.Id).Update(updatedPlayerDto);
            PlayerDtoUpdated(this,
                new PlayerDtoEventArgs(updatedPlayerDto));
        }
        */
        
        /*public void GetPlayers()
        {
            Mapper.Mapper mapper = new Mapper.Mapper();
            foreach (PlayerDto playerDto in mapper.Map(_playerService.GetPlayers()))
            {
                PlayerDtos.Add(playerDto);
                //UpdatePlayer(playerDto);
            }
        }*/

        public IEnumerable<PlayerDto> GetMyPlayers()
        {
            Mapper.Mapper mapper = new Mapper.Mapper();
            foreach (PlayerDto playerDto in mapper.Map(_playerService.GetMyPlayers()))
            {
                PlayerDtos.Add(playerDto);
                //UpdatePlayer(playerDto);
            }
            return PlayerDtos;
        }

        private PlayerDto GetPlayer(int playerDtoId)
        {
            return PlayerDtos.FirstOrDefault(
                playerDto => playerDto.Id == playerDtoId);
        }
    }
}