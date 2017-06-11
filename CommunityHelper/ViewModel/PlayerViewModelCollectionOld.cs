using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityHelper.MapperVMDto;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.DTO.EventArgs;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.Service;
using ViewCommunityHelper.View;


namespace CommunityHelper.ViewModel
{
    public class PlayerViewModelCollectionOld :BaseMagic
    {
        //private readonly ObservableCollection<PlayerViewModel> _playerViewModels;
        
        public event EventHandler<PlayerDtoEventArgs> PlayerDtoUpdated = delegate { };

        private readonly IWindow _window;
        //private readonly IRepository _repository;

        private readonly PlayerService _playerService;
        private ObservableCollection<PlayerDto> _playerDtos;


        public PlayerViewModelCollectionOld(IWindow window, IRepository repository, PlayerService playerService)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _window = window;
            //_repository = repository;
            _playerService = playerService;
            //_playerViewModels = new ObservableCollection<PlayerViewModel>();
            //Refresh(null);
        }

        public ObservableCollection<PlayerDto> PlayerDtos
        {
            get
            {
                Mapper mapper = new Mapper();
                //_playerDtos = mapper.Map(_playerService.GetPlayers());
                return _playerDtos;
            }
            set { _playerDtos = value; }
        }



        public void UpdatePlayerDto(PlayerDto updatedPlayerDto)
        {
            GetPlayerDto(updatedPlayerDto.Id).Update(updatedPlayerDto);
            PlayerDtoUpdated(this,
                new PlayerDtoEventArgs(updatedPlayerDto));
        }

        private PlayerDto GetPlayerDto(int playerId)
        {
            return PlayerDtos.FirstOrDefault(player => player.Id == playerId);
        }

        /*
        public ObservableCollection<PlayerViewModel> PlayerVMs
        {
            get
            {
                //Refresh(null);
                return _playerViewModels;
            }
        }*/

        public void Refresh(object parameter)
        {
            /*TODO маппер Dto -> VM
             * реализовать маппер из DTO во ViewModel
             */
             /*
            //_requestResourceVM.Clear();
            //repository.GetAllRes();
            //_repository.RefreshRequestsResources();
            _repository.RefreshPlayers();
            //foreach (var rR in this.repository.SelectAllRequestsResources())
            foreach (var rR in _repository.GetPlayerDtos())
            {
                MapperToFromVM mapper = new MapperToFromVM();
                PlayerViewModel pvm = mapper.MapToVM(_repository.FindPlayerDtoById(rR.Id));
                //RequestResourceViewModel rrvm = new RequestResourceViewModel();
                //rrvm.Timestamp = rR.Timestamp;
                //rrvm.Id = rR.Id;
                //PlayerVMs.Add(pvm);
            }
            */
        }
    }
}
