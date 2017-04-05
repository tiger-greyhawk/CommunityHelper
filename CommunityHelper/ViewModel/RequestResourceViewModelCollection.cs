using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using CommunityHelper.MapperVMDto;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Repository;
using ViewCommunityHelper.View;


namespace CommunityHelper.ViewModel
{
    public class RequestResourceViewModelCollection : BaseMagic
    {
        private readonly IWindow _window;
        private readonly IRepository _repository;
        //MapperToFromVM mapper = new MapperToFromVM();
        //public RequestResourceEditorViewModel editor;

        //private  ObservableCollection<RequestResourceViewModel> _requestResourceVM;
        

        private readonly RelayCommand _addRequestResourceCommand;
        

        

        public RequestResourceViewModelCollection(IWindow window, IRepository repository)//, ObservableCollection<RequestResourceDto> requestResourceDtos)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            _window = window;
            _repository = repository;
            _addRequestResourceCommand = new RelayCommand(AddRequestResource);

            RequestResourceDtos = ((Repository)_repository).RequestResourceDtos;

            
        }


        
        private ObservableCollection<RequestResourceDto> _requestResourceDtos;
        public ObservableCollection<RequestResourceDto> RequestResourceDtos {
            get { return _requestResourceDtos; }
            set
            {
                _requestResourceDtos = value; 
                RaisePropertyChanged("RequestResourceDtos");
            } }
        /*
        public ObservableCollection<RequestResourceViewModel> RequestResourceVM
        {
            get
            {
                return _requestResourceVM;
            }

            set { _requestResourceVM = value; }
        }
        */
        /*TODO mapping dtos into vm
             * пример маппинга из Dto в ViewModel
             * Так ли делать? Или и этот маппер нельзя создавать каждый раз, а лучше в Composition Root его инжектить во ViewModel?
             * 
            foreach (var p in this._repository.GetPlayerDtos())
            {
                MapperRequestResourceVM mapper = new MapperRequestResourceVM();
                PlayerViewModel pvm = mapper.MapToVm(_repository.FindPlayerDtoById(p.Id));
                
            }
            */
        /*
        public void Refresh(object parameter)
        {
            
            _repository.RefreshPlayers();
            Application.Current.Dispatcher.BeginInvoke(new Action(
                    delegate ()
                    {
                        //_requestResourceVM.Clear();

                        foreach (var rR in _repository.GetRequestResourceDtos())
                        {
                            MapperToFromVM mapper = new MapperToFromVM();
                            RequestResourceViewModel rrvm = mapper.MapToVM(_repository.FindRequestResourceDtoById(rR.Id), _repository.FindPlayerDtoByNick(rR.PlayerNick));
                            RequestResourceVM.Add(rrvm);
                        }
                    }));

            
        }*/

        public void AddRequestResource(object parameter)
        {
            var editor = new RequestResourceEditorViewModel();

            if (_window.CreateChild(editor).ShowDialog() ?? false)
            {
                RequestResourceDto rRVM = new RequestResourceDto();
                rRVM.Name = editor.Name;
                rRVM.PlayerNick = editor.PlayerNick;
                rRVM.Timestamp = new DateTime();
                RequestResourceDtos.Add(rRVM);
                //RequestResourceVM.Add(rRVM);
                //_repository.AddRequestResource(editor);
            }
            else
            {
                
            }
            
        }

        public ICommand AddRequestResourceCommand
        {
            get { return _addRequestResourceCommand; }
        }
    }
}
