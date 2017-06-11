using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityHelper.ViewModel.Internal;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.DTO.Collection;
using RepositoryCommunityHelper.DTO.EventArgs;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Service;
using RepositoryCommunityHelper.WebService;

namespace CommunityHelper.ViewModel
{
    public class PlayerViewModelCollection : BaseMagic
    {
        public const string SELECTED_PROJECT_PROPERRTY_NAME = "SelectedPlayer";

        private Auth _auth;
        //private readonly PlayerDtoCollection _playerDtoCollection;
        private ObservableCollection<PlayerDto> _playerProxyCollection;
        private ObservableCollection<PlayerDto> _players;
        private ObservableCollection<PlayerDto> _myPlayers;
        private readonly ServiceCollection _serviceCollection;
        private readonly PlayerService _playerService;
        //private PlayerViewModel _selectedPlayer;
        private PlayerDto _selectedPlayer;
        private PlayerDto _activePlayer;
        //private Status _detailsEstimateStatus = Status.None;
        private bool _detailsEnabled;
        private readonly ICommand _updateCommand;

        private Mapper mapper = new Mapper();

        private RelayCommand _addPlayerCommand;
        private RelayCommand _deletePlayerCommand;

        public Auth Auth { get { return _auth; } }

        public PlayerViewModelCollection(ServiceCollection serviceCollection, Auth auth)//, ObservableCollection<PlayerDto> players, ObservableCollection<PlayerDto> myPlayers)
        {
            //_playerDtoCollection = playerDtoCollection;
            //_playerDtoCollection.PlayerDtoUpdated += model_PlayerDtoUpdated;
            _auth = auth;
            _serviceCollection = serviceCollection;
            _playerService = serviceCollection.PlayerService;
            //Mapper mapper = new Mapper();
            //_players = mapper.Map(_playerService.GetPlayers());
            _updateCommand = new UpdateCommand(this);
            _playerProxyCollection = new ObservableCollection<PlayerDto>();
            
            _players = new ObservableCollection<PlayerDto>();
            _myPlayers = new ObservableCollection<PlayerDto>();

            //_players = players;
            //_myPlayers = myPlayers;
            
            _addPlayerCommand = new RelayCommand(AddPlayer);
            _deletePlayerCommand = new RelayCommand(DeletePlayer);
            NewPlayerDto = new PlayerDto();
        }

        public ObservableCollection<PlayerDto> PlayerProxyCollection
        {
            get { return _playerProxyCollection; }
            /*set
            {
                if (value == null || value == _playerProxyCollection) return;
                _playerProxyCollection = value;
            }*/
        }

        public void PlayerProxyCollectionUpdate()
        {
            _playerProxyCollection.Clear();
            
            //_myPlayers = new ObservableCollection<PlayerDto>();
            //Mapper mapper = new Mapper();
            //IEnumerable<Player> players = _playerService.GetPlayers();
            //ObservableCollection<PlayerDto> players = _serviceCollection.PlayerDtos;
            foreach (PlayerDto player in _serviceCollection.PlayerDtos)
            {
                _playerProxyCollection.Add(player);
            }
        }

        public ObservableCollection<PlayerDto> MyPlayers
        {
            get
            {
                return _myPlayers;
            }
            //set { }
        }

        public void MyPlayersUpdate()
        {
            _myPlayers.Clear();
            //_myPlayers = new ObservableCollection<PlayerDto>();
            //Mapper mapper = new Mapper();
            IEnumerable<Player> players = _playerService.GetMyPlayers();
            //IEnumerable<Player> players = PlayerProxyCollection;
            foreach (Player myPlayer in players)
            {
                if (myPlayer.UserId == Auth.AuthenticatedUser.Id)
                    _myPlayers.Add(mapper.Map(myPlayer));
            }
            _activePlayer = (from p in _myPlayers
                             where p.Id == Auth.AuthenticatedUser.ActivePlayerId
                             select p).FirstOrDefault();
        }

        public ObservableCollection<PlayerDto> Players
        {
            get
            {
                //return mapper.Map(_playerService.GetPlayers());
                
                return _serviceCollection.PlayerDtos;
                //return _players;
                //return _playerDtoCollection.PlayerDtos;
            } 
            set { _players = value; }
        }

        public void PlayersUpdate()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate()
            {
                Mouse.OverrideCursor = Cursors.Wait;
            }));
                //Task.Factory.StartNew(() =>
            {
                ObservableCollection<PlayerDto> playerDtosForUpdate = new ObservableCollection<PlayerDto>();
                //List<Player> playersNew = new List<Player>(_playerService.GetPlayers());
                ObservableCollection<PlayerDto> playersNew = PlayerProxyCollection;
                //IEnumerable<Player> players = _serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id);
                
                    foreach (PlayerDto player in playersNew)
                    {
                        PlayerDto playerDtoNew = (player);
                        if (Players.FirstOrDefault(p => p.Id == playerDtoNew.Id) != null)
                        {
                            PlayerDto playerDtoOld = Players.Single(p => p.Id == playerDtoNew.Id);
                            if (playerDtoOld.IsSelected) playerDtoNew.IsSelected = true;
                            /*if (playerDtoNew.FactionId != 0)
                                playerDtoNew.Faction = factionsList.Single(f => f.Id == playerDtoNew.FactionId).Name;
                            else
                            {
                                playerDtoNew.Faction = "";
                            }*/

                        }
                        playerDtosForUpdate.Add(playerDtoNew);
                    //SelectedFaction = factionDtoNew;
                    //Factions.IndexOf(factionDtoOld) = factionDtoNew
                    }
                Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()

                {
                    Players = playerDtosForUpdate;
                    Mouse.OverrideCursor = Cursors.Arrow;
                //RaisePropertyChanged(nameof(ActivePlayer));
                }
                ));
                //_playerDtos = mapper.Map(_serviceCollection.PlayerService.GetFactionPlayers(_selectedFaction.Id));
            }//);
        }

        public void UpdatePlayersView()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Players.Clear();
                for (int i = 0; i < PlayerProxyCollection.Count; i++)
                {
                    Players.Add(PlayerProxyCollection[i]);
                }
            }));
        }

        static string _filterPlayersByNick;
        public string FilterPlayersByNick
        {
            get { return _filterPlayersByNick; }
            set
            {
                _filterPlayersByNick = value;
                if (_filterPlayersByNick.Length < 1)
                {
                    UpdatePlayersView();
                    return;
                }

                Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
                {
                    Players.Clear();
                    for (int i = 0; i < PlayerProxyCollection.Count; i++)
                    {
                        if (PlayerProxyCollection[i].Nick.Contains(_filterPlayersByNick))
                        {

                            //Factions.RemoveAt(i);
                            Players.Add(PlayerProxyCollection[i]);
                            //Factions.
                            //FactionsView.Add(Factions[i]);
                            //Factions.Add(Factions[i]);
                        }
                    }
                }
                ));
            }
        }

        public int? SelectedValue
        {
            set
            {
                if (value == null)
                    return;
                PlayerDto playerDto = GetPlayerDto((int)value);
                if (SelectedPlayer == null)
                {
                    SelectedPlayer
                        //= new PlayerViewModel(playerDto);
                        = new PlayerDto(playerDto);
                    playerDto.IsSelected = true;
                }
                else
                {
                    playerDto.IsSelected = true;
                    SelectedPlayer = playerDto;
                    //SelectedPlayer = new PlayerDto(playerDto);
                    //SelectedPlayer.Update(playerDto);
                    //RaisePropertyChanged(nameof(SelectedPlayer));
                }
                //DetailsEstimateStatus = SelectedPlayer.EstimateStatus;
                //RaisePropertyChanged(nameof(SelectedPlayer));
                //RaisePropertyChanged(nameof(Players));
            }
        }

        public int? SelectedMyPlayer
        {
            set
            {
                if (value == null)
                    return;
                PlayerDto playerDto = (from p in _myPlayers
                                                    where p.Id == (int)value
                                                    select p).FirstOrDefault();
                if (SelectedPlayer == null)
                {
                    SelectedPlayer
                        //= new PlayerViewModel(playerDto);
                        = new PlayerDto(playerDto);
                    //playerDto.IsSelected = true;
                }
                else
                {
                    //playerDto.IsSelected = true;
                    SelectedPlayer = playerDto;
                    //SelectedPlayer = new PlayerDto(playerDto);
                    //SelectedPlayer.Update(playerDto);
                    //RaisePropertyChanged(nameof(SelectedPlayer));
                }
                //DetailsEstimateStatus = SelectedPlayer.EstimateStatus;
                //RaisePropertyChanged(nameof(SelectedPlayer));
                //RaisePropertyChanged(nameof(Players));
            }
        }

        public PlayerDto SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                //IEnumerable<PlayerDto> playersTest = new PlayerDtoCollection(_playerService).GetMyPlayers();
                if (_selectedPlayer != null) _selectedPlayer.IsSelected = false;
                if (value == null)
                {
                    //_selectedPlayer.IsSelected = false;
                    _selectedPlayer = value;
                    DetailsEnabled = false;
                    return;
                }
                else
                {
                    if (_selectedPlayer == null)
                    {
                        _selectedPlayer = 
                            //TODO переделать. Сейчас передается не все?
                            //new PlayerViewModel(value);
                            new PlayerDto(value);
                            //GetPlayerDto(value.Id);
                    }
                    value.IsSelected = true;
                    _selectedPlayer = value;
                    //_selectedPlayer.Update(value);
                    //DetailsEstimateStatus = _selectedPlayer.EstimateStatus;
                    DetailsEnabled = true;
                    //_selectedPlayer.IsSelected = true;
                    _selectedPlayer.Update(value);
                    //NotifyPropertyChanged(SELECTED_PROJECT_PROPERRTY_NAME);
                }
                _selectedPlayer.IsSelected = true;
                //RaisePropertyChanged(nameof(SelectedPlayer.IsSelected));
                //RaisePropertyChanged(nameof(MyPlayers));
            }
        }

        public PlayerDto ActivePlayer
        {
            get
            {
                /*_activePlayer = (from p in MyPlayers
                 where p.Id == Auth.AuthenticatedUser.ActivePlayerId
                 select p).FirstOrDefault();*/
                //_activePlayer = GetPlayerDto(Auth.AuthenticatedUser.ActivePlayerId);
                return _activePlayer;
            }
            set
            {
                if (value == null) return;
                _activePlayer = value;
                if (_playerService.SetActivePlayer(mapper.Map(_activePlayer)).Count() > 0)
                    Auth.AuthenticatedUser.ActivePlayerId = _activePlayer.Id;
            }
        }

        public bool DetailsEnabled
        {
            get { return _detailsEnabled; }
            set
            {
                _detailsEnabled = value;
                //NotifyPropertyChanged("DetailsEnabled");
            }
        }

        public ICommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        
        
        /*
        public void UpdatePlayer()
        {
            //DetailsEstimateStatus = SelectedPlayer.EstimateStatus;
            PlayerDto playerDto = new PlayerDto(
                SelectedPlayer.Id, 
                SelectedPlayer.UserId,
                SelectedPlayer.Nick,
                SelectedPlayer.Invite,
                SelectedPlayer.Motivater,
                SelectedPlayer.LastAccess,
                SelectedPlayer.FactionId,
                SelectedPlayer.Avatar
                );
                
            _playerDtoCollection.UpdatePlayer(playerDto);
        }
        */
        
        private void model_PlayerDtoUpdated(object sender, PlayerDtoEventArgs e)
        {
            //GetPlayerDto(e.PlayerDto.Id).Update(e.PlayerDto);
            if (SelectedPlayer != null
                && e.PlayerDto.Id == SelectedPlayer.Id)
            {
                SelectedPlayer.Update(e.PlayerDto);
                //DetailsEstimateStatus = SelectedPlayer.EstimateStatus;
            }
        }

        private PlayerDto GetPlayerDto(int playerDtoId)
        {
            return (from p in Players
                    where p.Id == playerDtoId
                    select p).FirstOrDefault();
        }

        public PlayerDto NewPlayerDto { get; set; }

        public ICommand AddPlayerCommand
        {
            get { return _addPlayerCommand; }
        }

        
        public void AddPlayer(object parameter)
        {
            
            if (parameter.ToString() == "") return;
            //NewPlayerDto = new PlayerDto();
            //newPlayer.Nick = NewPlayerDto.Nick;
            //if (NewPlayerDto == null) return;
            //NewPlayerDto.UserId = Auth.AuthenticatedUser.Id;
            //Mapper mapper = new Mapper();
            _playerService.SavePlayer(mapper.Map(NewPlayerDto));
            RaisePropertyChanged(nameof(MyPlayers));
        }

        public ICommand DeletePlayerCommand
        {
            get { return _deletePlayerCommand; }
        }

        public void DeletePlayer(object parameter)
        {
            if (parameter == null) return;
            //Mapper mapper = new Mapper();
            PlayerDto playerToDeleteDto = parameter as PlayerDto;
            if (_playerService.DeletePlayer(mapper.Map(playerToDeleteDto)).Id == playerToDeleteDto.Id)
                RaisePropertyChanged(nameof(MyPlayers));
                //MessageBox.Show("player deleted", "success");
        }
    }

}