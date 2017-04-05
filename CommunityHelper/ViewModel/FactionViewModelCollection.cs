using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommunityHelper.MapperVMDto;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Repository;
using ViewCommunityHelper.View;

namespace CommunityHelper.ViewModel
{
    public class FactionViewModelCollection : BaseMagic
    {
        private readonly IWindow _window;
        private readonly IRepository _repository;

        private readonly ObservableCollection<FactionViewModel> _factionVMs;

        public FactionViewModelCollection(IWindow window, IRepository repository)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            _window = window;
            _repository = repository;

            _factionVMs = new ObservableCollection<FactionViewModel>();
            //Refresh(null);
        }

        private ObservableCollection<FactionDto> _factionDtos;
        public ObservableCollection<FactionDto> FactionDtos
        {
            get { return _factionDtos; }
            set
            {
                _factionDtos = value;
                RaisePropertyChanged(nameof(FactionDtos));
            }
        }

        /*
        public ObservableCollection<FactionViewModel> FactionVMs
        {
            get { return _factionVMs; }
        }

        public void Refresh(object parameter)
        {
            //_repository.RefreshRequestsResources();
            //_repository.RefreshPlayers();
            _repository.RefreshFactions();
            //foreach (var rR in this.repository.SelectAllRequestsResources())
            foreach (var f in _repository.GetFactionDtos())
            {
                MapperToFromVM mapper = new MapperToFromVM();
                FactionViewModel fvm = mapper.MapToVM(_repository.FindFactionDtoById(f.id));
                //RequestResourceViewModel rrvm = new RequestResourceViewModel();
                //rrvm.Timestamp = rR.Timestamp;
                //rrvm.Id = rR.Id;
                FactionVMs.Add(fvm);
            }
        }
        */

        public void CreateFaction(object parameter)
        {
            var editor = new FactionViewModel();
            
            if (_window.CreateChild(editor).ShowDialog() ?? false)
            {
                FactionViewModel fVM = new FactionViewModel();
                fVM.name = editor.name;
                //rRVM.Timestamp = new DateTime();
                //FactionVMs.Add(fVM);
            }
            else
            {

            }

        }
    }
}
