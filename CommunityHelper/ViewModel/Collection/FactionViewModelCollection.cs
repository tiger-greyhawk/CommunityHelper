using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.DTO.Collection;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.Service;
using ViewCommunityHelper.View;
using ViewCommunityHelper.View.WindowXaml;

namespace CommunityHelper.ViewModel.Collection
{
    public class FactionViewModelCollection : BaseMagic
    {
        //private FactionDtoCollection _factionDtoCollection;
        //private ObservableCollection<FactionDto> _factions;
        private ObservableCollection<FactionDto> _factionsView;
        private readonly ServiceCollection _serviceCollection;
        private ObservableCollection<PlayerDto> _playerDtos;
        //_window, _repository, _serviceCollection, factionDtoCollection
        private readonly IWindow _window;
        //private readonly IRepository _repository;

        private RelayCommand _createFactionCommand;
        private RelayCommand _joinFactionCommand;
        private RelayCommand _deleteFactionCommand;
        private RelayCommand _eliminateCommand;
        

        private FactionDto _selectedFaction;

        public Cursor Cursor1 { get; set; }

        Mapper mapper = new Mapper();


        //public FactionViewModelCollection(IWindow window, IRepository repository, ServiceCollection serviceCollection, FactionDtoCollection factionDtoCollection )
        public FactionViewModelCollection(IWindow window, ServiceCollection serviceCollection)
        {
            _window = window;
            //_repository = repository;
            _serviceCollection = serviceCollection;
            //_factionDtoCollection = factionDtoCollection;
            //Mapper mapper = new Mapper();
            //_factions = mapper.Map(_serviceCollection.FactionService.GetFactions());
            //_factions = new ObservableCollection<FactionDto>();
            _factionsView = new ObservableCollection<FactionDto>();
            _playerDtos = new ObservableCollection<PlayerDto>();

            //_selectedFaction = new FactionDto();
            _createFactionCommand = new RelayCommand(CreateFaction);
            _joinFactionCommand = new RelayCommand(JoinFaction);
            _deleteFactionCommand = new RelayCommand(DeleteFaction);
            _eliminateCommand = new RelayCommand(Eliminate);
            //_filterFactionsByName = new FactionDto();

        }


        public ObservableCollection<FactionDto> FactionsView
        {
            get
            {
                //Update();
                //return mapper.Map(_serviceCollection.FactionService.GetFactions());
                
                return _factionsView;
                //return _factionDtoCollection.FactionDtos;
            }

            set
            {
                if (_factionsView == value) return;
                _factionsView = value;
            }
        }

        public ObservableCollection<FactionDto> Factions
        {
            get
            {
                return _serviceCollection.FactionDtos;
            }

            /*set
            {
                if (_factions == value) return;
                _factions = value;
            }*/
        }

        /*
        public void Update()
        {

//            ObservableCollection<FactionDto> factionDtosForUpdate = new ObservableCollection<FactionDto>();
//            List<Faction> factionsNew = new List<Faction>(_serviceCollection.FactionService.GetFactions());
//            foreach (Faction faction in factionsNew)
//            {
//                FactionDto factionDtoNew = mapper.Map(faction);
//                if (Factions.FirstOrDefault(f => f.Id == factionDtoNew.Id) != null)
//                {
//                    FactionDto factionDtoOld = Factions.Single(f => f.Id == factionDtoNew.Id);
//                    if (factionDtoOld.IsSelected) factionDtoNew.IsSelected = true;
//                    
//                }
//                factionDtosForUpdate.Add(factionDtoNew);
//                //SelectedFaction = factionDtoNew;
//                //Factions.IndexOf(factionDtoOld) = factionDtoNew
//            }
//            Factions = factionDtosForUpdate;
//            
//            Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
//            {
//                Mouse.OverrideCursor = Cursors.Wait;
//            }));
            //Task.Factory.StartNew(() =>
            {
                ObservableCollection<FactionDto> factionDtosForUpdate = new ObservableCollection<FactionDto>();
                List<Faction> factionsNew = new List<Faction>(_serviceCollection.FactionService.GetFactions());
                //IEnumerable<Player> players = _serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id);
                //Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    foreach (Faction faction in factionsNew)
                    {
                        FactionDto factionDtoNew = mapper.Map(faction);
                        if (Factions.FirstOrDefault(f => f.Id == factionDtoNew.Id) != null)
                        {
                            FactionDto factionDtoOld = Factions.Single(f => f.Id == factionDtoNew.Id);
                            if (factionDtoOld.IsSelected) factionDtoNew.IsSelected = true;

                        }
                        factionDtosForUpdate.Add(factionDtoNew);
                        //SelectedFaction = factionDtoNew;
                        //Factions.IndexOf(factionDtoOld) = factionDtoNew
                    }
                    Factions = factionDtosForUpdate;
                    //FactionsView = Factions;
                    Mouse.OverrideCursor = Cursors.Arrow;
                    UpdateFactionsView();
                }
                //));
                //_playerDtos = mapper.Map(_serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id));
            }//);
            
        }
        */

        public int? SelectedValue
        {
            set
            {
                if (value == null)
                    return;
                FactionDto factionDto = GetFactionDto((int)value);
                if (SelectedFaction == null)
                {
                    SelectedFaction
                        = new FactionDto(factionDto);
                }
                else
                {
                    //SelectedFaction = new FactionDto(factionDto);
                    //SelectedFaction = GetFactionDto((int) value);
                    SelectedFaction = factionDto;
                    SelectedFaction.Update(factionDto);
                    //RaisePropertyChanged(nameof(SelectedFaction));
                }
                //DetailsEstimateStatus = SelectedPlayer.EstimateStatus;
            }
        }

        public FactionDto SelectedFaction
        {
            get { return _selectedFaction; }
            set
            {
                if (value == null)
                {
                    _selectedFaction = value;
                    //DetailsEnabled = false;
                }
                else
                {
                    if (_selectedFaction == null)
                    {
                        _selectedFaction =
                            new FactionDto(value);
                    }
                    _selectedFaction = value;
                    _selectedFaction.Update(value);
                    //DetailsEstimateStatus = _selectedPlayer.EstimateStatus;
                    //DetailsEnabled = true;
                    //NotifyPropertyChanged(SELECTED_PROJECT_PROPERRTY_NAME);
                    //RaisePropertyChanged(nameof(PlayerDtos));
                }
                //Update();
                //RaisePropertyChanged(nameof(PlayerDtos));
                UpdatePlayersInFaction();
                RaisePropertyChanged(nameof(DeleteButtonEnabled));
            }
        }

        private void UpdatePlayersInFaction()
        {
            PlayerDtos.Clear();
            Mouse.OverrideCursor = Cursors.Wait;
            //Cursor1 = Cursor.AppStarting;
            Mapper mapper = new Mapper();
            if (_selectedFaction != null)
                Task.Factory.StartNew(() =>
                {
                    //IEnumerable<PlayerDto> players = _serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id);

                    
                    IEnumerable<PlayerDto> players = _serviceCollection.GetPlayersInFaction(_selectedFaction.Id);
                    Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
                        {
                            foreach (PlayerDto player in players)
                            {
                                PlayerDtos.Add(player);
                            }
                            //PlayerDtos = (players);
                            Mouse.OverrideCursor = Cursors.Arrow;
                        }
                    ));
                    //_playerDtos = mapper.Map(_serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id));
                });
        }

        public ObservableCollection<PlayerDto> PlayerDtos
        {
            get
            {
                //if ()
                { 
/*                Mapper mapper = new Mapper();
                    if (_selectedFaction != null)
                        Task.Factory.StartNew(() =>
                        {
                            IEnumerable<Player> players = _serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id);
                            Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
                                {
                                    _playerDtos = mapper.Map(players);
                                    return _playerDtos;
                                }
                            ));
                            //_playerDtos = mapper.Map(_serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id));
                        });
*/
                }
                return _playerDtos;
            }

            set { _playerDtos = value; }
        }

        private FactionDto GetFactionDto(int factionDtoId)
        {
            return (from f in Factions
                    where f.Id == factionDtoId
                    select f).FirstOrDefault();
        }

        public ICommand CreateFactionCommand
        {
            get { return _createFactionCommand; }
        }

        /// <summary>
        /// Сервер не создаст фракцию игроку, который уже владеет фракцией. Надо делать проверку и делать кнопку неактивной. (на данный момент создаст)
        /// </summary>
        /// <param name="parameter"></param>
        public void CreateFaction(object parameter)
        {
            
            //FactionDto newFaction = new FactionDto();
            FactionDto newFaction = new FactionDto();
            FactionEditorWindow factionEditorWindow = new FactionEditorWindow();
            newFaction.Name = "newFactionTest2";
            if (_window.CreateChildByViewModel(newFaction, factionEditorWindow).ShowDialog() == true)
            {
                //newFaction.Save(newFaction);
                if (Factions.FirstOrDefault(f => f.Name == newFaction.Name) == null)
                    //FactionDto savedFactionDto = mapper.Map(_serviceCollection.FactionService.SaveFaction(newFaction));
                {
                    Mouse.OverrideCursor = Cursors.Wait;
                    Task.Factory.StartNew(() =>
                    {
                        Faction savedFaction = _serviceCollection.FactionService.SaveFaction(mapper.Map(newFaction));
                        if (savedFaction != null)
                        {
                            
                            Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
                                {
                                    _serviceCollection.FactionService.UpdateFactions();
                                    UpdateFactionsView();
                                    Mouse.OverrideCursor = Cursors.Arrow;
                                }
                            ));
                        }
                    });
                }
                else MessageBox.Show("Name is busy.", "Name is busy.");

            }
                
            //MessageBox.Show("", "");
        }

        

        public bool DeleteButtonEnabled
        {
            get
            {
                if (SelectedFaction != null)
                {
                    string playerNick = App.GetActivePlayer().Nick;
                    if (SelectedFaction.Owner == playerNick)
                    {
                        //Exception exception = new Exception("переделать на проверку имени.");
                        //throw exception;
                        //MessageBox.Show("переделать на проверку имени.");
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            //return false;
        }

        public ICommand DeleteFactionCommand
        {
            get { return _deleteFactionCommand; }
        }

        public void DeleteFaction(object parameter)
        {
            //FactionDto newFaction = new FactionDto();
            if (parameter == null) return;
            FactionDto factionDtoToDelete = Factions.FirstOrDefault(f => f.Id == (int)parameter);
            if (factionDtoToDelete != null)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Task.Factory.StartNew(() =>
                {
                    
                    Faction factionToDelete = mapper.Map(factionDtoToDelete);
                    Faction result =_serviceCollection.FactionService.DeleteFaction(factionToDelete);
                    if (result != null)
                    //if(savedFactionDto.name == newFaction.name)
                    {
                        Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
                        {
                            _serviceCollection.FactionService.UpdateFactions();
                            UpdateFactionsView();
                            SelectedFaction = null;
                            //Update();
                            //Factions.Remove(factionDtoToDelete);
                            Mouse.OverrideCursor = Cursors.Arrow;
                        }
                        ));
                    }
                });
                
                
            }
            //RaisePropertyChanged(nameof(Factions));
        }

        public ICommand JoinFactionCommand
        {
            get { return _joinFactionCommand; }
        }

        public void JoinFaction(object parameter)
        {
            //FactionDto newFaction = new FactionDto();
            //FactionEditorWindow factionEditorWindow = new FactionEditorWindow();
            //newFaction.Name = "newFactionTest2";
            //if (_window.CreateChildByViewModel(newFaction, factionEditorWindow).ShowDialog() == true)
            {
                //newFaction.Save(newFaction);
                FactionDto joinFaction = Factions.FirstOrDefault(f => f.Id == (int) parameter);
                if ( joinFaction != null)
                //FactionDto savedFactionDto = mapper.Map(_serviceCollection.FactionService.SaveFaction(newFaction));
                {
                    PlayerDto playerDto = new PlayerDto(1, 0, "Nick will rename in factionDao to login", 1, "t", DateTime.Now, 0, "" );
                    Player player = new Player();
                    player = mapper.Map(playerDto);
                    if (_serviceCollection.FactionService.JoinFaction(joinFaction.Id, player) != null)
                    //if(savedFactionDto.name == newFaction.name)
                    {
                        UpdatePlayersInFaction();
                    }
                }
                else MessageBox.Show("Faction is wrong.", "Faction is wrong.");

            }

            //MessageBox.Show("", "");
        }

        public ICommand EliminateCommand
        {
            get { return _eliminateCommand; }
        }

        public void Eliminate(object parameter)
        {
            if (parameter == null) return;
            PlayerDto selectedPlayer = PlayerDtos.FirstOrDefault(p => p.Id == (int) parameter);
            if (selectedPlayer != null)
            {
                if (
                    _serviceCollection.FactionService.EliminateFromFaction(SelectedFaction.Id,
                        mapper.Map(selectedPlayer)) != null)
                {
                    UpdatePlayersInFaction();
                }
            }
        }

        ///* Filter. Try
        /// 
        /// 

        public void UpdateFactionsView()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                FactionsView.Clear();
                for (int i = 0; i < Factions.Count; i++)
                {
                    FactionsView.Add(Factions[i]);
                }
            }));
        }

        static string _filterFactionsByName;
        public string FilterFactionsByName
        {
            get { return _filterFactionsByName; }
            set
            {
                _filterFactionsByName = value;
                if (_filterFactionsByName.Length < 1)
                {
                    //if (FactionsView.Count == Factions.Count)
                        //Update();
                    /*Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        FactionsView.Clear();
                        for (int i = 0; i < Factions.Count; i++)
                        {
                            FactionsView.Add(Factions[i]);
                        }
                    }));*/
                    UpdateFactionsView();
                    return;
                }
                //ObservableCollection<FactionDto> FactionsView = new ObservableCollection<FactionDto>();
                //IEnumerable<FactionDto> FactionsView = new List<FactionDto>();
                //FactionsView = Factions;
                
                Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    FactionsView.Clear();
                    for (int i = 0; i < Factions.Count; i++)
                    {
                        if (Factions[i].Name.Contains(_filterFactionsByName))
                        {
                        
                            //Factions.RemoveAt(i);
                            FactionsView.Add(Factions[i]);
                            //Factions.
                            //FactionsView.Add(Factions[i]);
                            //Factions.Add(Factions[i]);
                        }
                    
                    }
                
                    /*Factions.Clear();
                    for (int i = 0; i < FactionsView.Count; i++)
                    {
                        Factions.Add(FactionsView[i]);
                    }*/
                    //Factions = FactionsView;
                    //RaisePropertyChanged(nameof(Factions));
                }
                ));
                //if (FactionsView.Count > 0) Factions = FactionsView;
                //if (FactionsView.Count > 0) Factions = FactionsView;
                //SelectedFaction = Factions.FirstOrDefault(s => s.Name.Contains(_filterFactionsByName));
                /*if (SelectedFaction != null)
                    SelectedFaction.IsSelected = false;
                _filterFactionsByName = value;
                //RaisePropertyChanged(nameof(FilterFactionsByName));
                SelectedFaction = Factions.FirstOrDefault(s => s.Name.Contains(_filterFactionsByName));
                if (SelectedFaction != null)
                    SelectedFaction.IsSelected = true;
                RaisePropertyChanged(nameof(SelectedFaction));
                //SelectedValue = Factions.FirstOrDefault(s => s.Name.StartsWith(FilterFactionsByName)).Id;
                */
            }
        }

/*        string _selected;
        public string Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }*/
    }
}