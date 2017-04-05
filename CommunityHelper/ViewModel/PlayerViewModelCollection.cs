using System;
using System.Collections.ObjectModel;
using CommunityHelper.MapperVMDto;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Repository;
using ViewCommunityHelper.View;


namespace CommunityHelper.ViewModel
{
    public class PlayerViewModelCollection :BaseMagic
    {
        private readonly ObservableCollection<PlayerViewModel> _playerViewModels;

        private readonly IWindow _window;
        private readonly IRepository _repository;




        public PlayerViewModelCollection(IWindow window, IRepository repository)
        {
            if (window == null) throw new ArgumentNullException(nameof(window));
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            _window = window;
            _repository = repository;
            _playerViewModels = new ObservableCollection<PlayerViewModel>();
            //Refresh(null);
        }


        public ObservableCollection<PlayerViewModel> PlayerVMs
        {
            get
            {
                //Refresh(null);
                return _playerViewModels;
            }
        }

        public void Refresh(object parameter)
        {
            /*TODO маппер Dto -> VM
             * реализовать маппер из DTO во ViewModel
             */

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
                PlayerVMs.Add(pvm);
            }

        }
    }
}
