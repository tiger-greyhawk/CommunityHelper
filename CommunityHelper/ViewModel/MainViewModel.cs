using ViewCommunityHelper.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using CommunityHelper.Container;
using CommunityHelper.ViewModel.Collection;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.DTO.Collection;
using RepositoryCommunityHelper.Entity;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.Service;
using RepositoryCommunityHelper.WebService;
using ViewCommunityHelper.View.WindowXaml;

namespace CommunityHelper.ViewModel
{
    public class MainViewModel : BaseMagic
    {
        //delegate void AuthDo();


        //private readonly IRepository _repository;
        private readonly IWindow _window;
        //private readonly PlayerInFactionListService _playerInFactionListService;
        //private readonly PlayerService _playerService;
        private readonly ServiceCollection _serviceCollection;
        private readonly Auth _auth;
        private readonly MainViewModelContainer _mainViewModelContainer;

        private readonly RequestResourceViewModelCollection _requestResourceViewModelCollection;

        private PlayerViewModelCollection _playerViewModelCollection;
        private ObservableCollection<PlayerDto> _players;
        private ObservableCollection<PlayerDto> _myPlayers;


        private FactionViewModelCollection _factionViewModelCollection;

        

        private TimerCallback timeCB;
        private Timer Timer1;
        //        private readonly RelayCommand _addRRCommand;
        public bool initialized = false;
        
        private Image _connectImage = new Image();
        public SolidColorBrush ConnectedStatusColor { get; set; }
        
        //private List<MenuItem>
        private ObservableCollection<MenuItem> _menuLanguage = new ObservableCollection<MenuItem>();

        // окна программы (дети)
        private GameFunctionalWindow _gameFunctionalWindow = new GameFunctionalWindow();
        private FactionWindow _factionWindow = new FactionWindow();
        private PlayersWindow _playersWindow = new PlayersWindow();
        
        // Команды
        private RelayCommand _doConnectCommand;
        private RelayCommand _showPlayerWindowCommand;
        private RelayCommand _showOptionsWindowCommand;
        private RelayCommand _showRegisterNewUserWindowCommand;
        private RelayCommand _showSettingMyPlayersWindowCommand;
        

        //public MainViewModel(IRepository repository, IWindow window, ServiceCollection serviceCollection)
        public MainViewModel(IWindow window, ServiceCollection serviceCollection, Auth auth, MainViewModelContainer mainViewModelContainer)
        {
            //if (repository == null)
            //    throw new ArgumentNullException(nameof(repository));
            
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            
            //_repository = repository;
            //_repository.Auth();
            _window = window;
            _serviceCollection = serviceCollection;
            _auth = auth;
            _mainViewModelContainer = mainViewModelContainer;
            ConnectedStatusColor = new SolidColorBrush(Colors.Red);
            //ConnectedStatusColor.Color = Color.FromRgb(255,0,0);
            //_playerInFactionListService = _layerInFactionListService;
            //_playerService = playerService;

            //            _requestResourceViewModelCollection = new RequestResourceViewModelCollection(_window, _repository);//, ((Repository)_repository).RequestResourceDtos);

            //_playerViewModelCollection = new PlayerViewModelCollection(_window, _repository, _serviceCollection.PlayerService);
            //PlayerDtoCollection playerDtoCollection = new PlayerDtoCollection(_serviceCollection.PlayerService);
            //_playerViewModelCollection = new PlayerViewModelCollection(playerDtoCollection);

            //FactionDtoCollection factionDtoCollection = new FactionDtoCollection(_serviceCollection.FactionService);
            //_factionViewModelCollection = new FactionViewModelCollection(_window, _repository, _serviceCollection, factionDtoCollection);

            _players = new ObservableCollection<PlayerDto>();
            _myPlayers = new ObservableCollection<PlayerDto>();
            //_playerViewModelCollection = new PlayerViewModelCollection(_serviceCollection.PlayerService, _auth, _players, _myPlayers);
            _playerViewModelCollection = mainViewModelContainer.PlayerViewModelCollection;
            //_factionViewModelCollection = new FactionViewModelCollection(_window, _serviceCollection);
            _factionViewModelCollection = mainViewModelContainer.FactionViewModelCollection;


            ConnectImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/disconnect.png"));

            _doConnectCommand = new RelayCommand(Connect);
            _showPlayerWindowCommand = new RelayCommand(ShowPlayerWindow);
            _showOptionsWindowCommand = new RelayCommand(ShowOptionsWindow);
            _showRegisterNewUserWindowCommand = new RelayCommand(ShowRegisterNewUserWindow);
            _showSettingMyPlayersWindowCommand = new RelayCommand(ShowSettingMyPlayersWindow);
            

            timeCB = new TimerCallback(TimeTick);
            Timer1 = new Timer(timeCB, null, 10000, 10000);
            PrepareLangSupport();
        }

        public Auth Auth
        { get { return _auth; } }

        public ObservableCollection<MenuItem> MenuLanguage
        {
            get { return _menuLanguage; }
            set { _menuLanguage = value; }
        }

        private void PrepareLangSupport()
        {
            App.LanguageChanged += LanguageChanged;

            CultureInfo currLang = App.Language;

            //Заполняем меню смены языка:
            _menuLanguage.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuLang = new MenuItem();
                //menuLang.Icon = ConnectImage;
                menuLang.Header = lang.DisplayName;
                menuLang.Tag = lang;
                menuLang.IsChecked = lang.Equals(currLang);
                menuLang.Click += ChangeLanguageClick;
                _menuLanguage.Add(menuLang);
            }
        }

        private void LanguageChanged(Object sender, EventArgs e)
        {
            CultureInfo currLang = App.Language;

            //Отмечаем нужный пункт смены языка как выбранный язык
            foreach (MenuItem i in _menuLanguage)
            {
                CultureInfo ci = i.Tag as CultureInfo;
                i.IsChecked = ci != null && ci.Equals(currLang);
            }
        }

        private void ChangeLanguageClick(Object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi != null)
            {
                CultureInfo lang = mi.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Language = lang;
                }
            }
        }

        void TimeTick(object state)
        {
            //_playerViewModelCollection.PlayerProxyCollection.Add(new Player());
            //_playerViewModelCollection.PlayersUpdate();
            //_factionViewModelCollection.Update();
            /*
            if (((Repository) _repository).Connected)
            {
                //Refresh(null);
                //RequestResourceVM = mapper.MapToVM(((Repository)_repository).RequestResourceDtos, _repository.GetPlayerDtos());
                RequestResourceViewModelCollection.RequestResourceDtos = ((Repository) _repository).RequestResourceDtos;
                //FactionViewModelCollection.FactionDtos = ((Repository) _repository).FactionDtos;

                //List<int> friendIds = new List<int>();
                //friendIds.Add(1);
                //friendIds.Add(22);
                //IEnumerable<PlayerInFactionDto> players = _playerInFactionListService.GetPlayersDtosByIds(friendIds);
                //IEnumerable<Player> players = _playerInFactionListService.GetFactionPlayers(10);
            }
        */}

        public RequestResourceViewModelCollection RequestResourceViewModelCollection
        { 
            get { return _requestResourceViewModelCollection; }
        }

      

        public PlayerViewModelCollection PlayerViewModelCollection
        {
            get
            {
                _playerViewModelCollection.PlayerProxyCollectionUpdate();
                //_playerViewModelCollection.PlayersUpdate();
                _playerViewModelCollection.MyPlayersUpdate();
                //PlayerFillFaction();
                return _playerViewModelCollection;
            }
        }

        /*
        private void PlayerFillFaction()
        {
            _factionViewModelCollection.Update();
            foreach (var player in _players)
            {
                if (player.FactionId != 0)
                    player.Faction =
                        _factionViewModelCollection.FactionsView.Single(f => f.Id == player.FactionId).Name;
                else player.Faction = "";
            }
        }
        */

        public FactionViewModelCollection FactionViewModelCollection
        {
            get
            {
                //_factionViewModelCollection.Update();
                
                return _factionViewModelCollection;
            }
        }

        public Image ConnectImage
        {
            get { return _connectImage; }
            set { _connectImage = value; }
        }



        /// <summary>
        /// Надо переделать. В теории должно выводить существующее окно.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ShowGameFunctionalWindow(object sender, ExecutedRoutedEventArgs e)
        {
            /*TODO переделать
             * Создавать окно заранее и потом только показывать его.
             * Переписать методы Closing() и скрывать вместо закрытия.
             */
            try
            {
                _window.CreateChildByViewModel(this.RequestResourceViewModelCollection, _gameFunctionalWindow).Show();
                //FactionWindow factionWindow = new FactionWindow();
                //_window.CreateChildByViewModel(this, factionWindow).Show();
            }
            catch (System.InvalidOperationException exception)
            {
                _gameFunctionalWindow = new GameFunctionalWindow();
                _window.CreateChildByViewModel(this.RequestResourceViewModelCollection, _gameFunctionalWindow).Show();
                //FactionWindow factionWindow = new FactionWindow();
                //_window.CreateChildByViewModel(this, factionWindow).Show();
                //Console.WriteLine(exception);
                
            }
        }

        public void ShowPlayerWindow(object parameter)
        {
            try
            {
                _window.CreateChildByViewModel(this.PlayerViewModelCollection, _playersWindow).Show();
                //FactionWindow factionWindow = new FactionWindow();
                //_window.CreateChildByViewModel(this, factionWindow).Show();
            }
            catch (System.InvalidOperationException exception)
            {
                
                //_playerViewModelCollection = new PlayerViewModelCollection(_serviceCollection.PlayerService);
                _playersWindow = new PlayersWindow();
                _window.CreateChildByViewModel(this.PlayerViewModelCollection, _playersWindow).Show();
                //FactionWindow factionWindow = new FactionWindow();
                //_window.CreateChildByViewModel(this, factionWindow).Show();
                //Console.WriteLine(exception);

            }
        }



        public void ShowSettingMyPlayersWindow(object parameter)
        {
            SettingMyPlayersWindow settingMyPlayersWindow = new SettingMyPlayersWindow();
            //if (_window.CreateChild(newFaction).ShowDialog() == true)
            //ConnectionProperties con = new ConnectionProperties();
            //ConnectionProperties con = Auth.ConnectionProperties;
            
            //MyPlayers = new ObservableCollection<PlayerDto>();
            //ObservableCollection<PlayerDto> MyPlayers = new ObservableCollection<PlayerDto>();
            
            //MyPlayers = _serviceCollection.PlayerService.GetMyPlayers();
            
            //NewPlayerDto.Nick = "ert";
            if (_window.CreateChildByViewModel(PlayerViewModelCollection, settingMyPlayersWindow).ShowDialog() == true)
            {
                //con.Save(con);
                //App.UpdateConnectionProperties(con);
                //App.MainContainer = con;
                //_window
            }
        }

        public void ShowFactionsWindow(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                _window.CreateChildByViewModel(this.FactionViewModelCollection, _factionWindow).Show();
            }
            catch (System.InvalidOperationException exception)
            {
                //_factionViewModelCollection = new FactionViewModelCollection(_window, _serviceCollection);
                _factionWindow = new FactionWindow();
                _window.CreateChildByViewModel(this.FactionViewModelCollection, _factionWindow).Show();
                //Console.WriteLine(exception);

            }
        }

        public void ShowOptionsWindow(object parameter)
        {
            OptionWindow optionWindow = new OptionWindow();
            //if (_window.CreateChild(newFaction).ShowDialog() == true)
            //ConnectionProperties con = new ConnectionProperties();
            ConnectionProperties con = Auth.ConnectionProperties;


            if (_window.CreateChildByViewModel(con, optionWindow).ShowDialog() == true )
            {
                //con.Save(con);
                App.UpdateConnectionProperties(con);
                //App.MainContainer = con;
                //_window
            }
            /*try
            {
                _window.CreateChildByViewModel(this.FactionViewModelCollection, _factionWindow).Show();
            }
            catch (System.InvalidOperationException exception)
            {
                _factionWindow = new FactionWindow();
                _window.CreateChildByViewModel(this.FactionViewModelCollection, _factionWindow).Show();
                //Console.WriteLine(exception);

            }*/
        }

        public void ShowRegisterNewUserWindow(object parameter)
        {
            RegisterNewUserWindow window = new RegisterNewUserWindow();
            User newUser = new User();

            if (_window.CreateChildByViewModel(newUser, window).ShowDialog() == true)
            {
                if (_serviceCollection.UserService.SaveUser(newUser).Nick == newUser.Username)
                    MessageBox.Show("", "");
                //newUser.Save(newUser);

            }

        }

        public void Exit(object sender, ExecutedRoutedEventArgs e)
        {
            
            _window.Close();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Just emulating connect 
        /// </summary>
        /// 
        
        public void Connect(object parameter)
        {
            ConverterJson converterJson = new ConverterJson();
            Task.Factory.StartNew(() =>
            {
                if (!_auth.ConnectionProperties.Connected)
                    //_auth.DoAuth(); 
                {
                    string jsonUserData = _auth.DoAuthAsyncNew().Result;
                    
                    if (_auth.DoAuthAsyncNew().Result == "401")
                        MessageBox.Show("Check your authdata.", "Bad auth.");
                    else
                    {
                        User authenticatedUser = converterJson.ConvertJsonToUser(jsonUserData);
                        _auth.AuthenticatedUser = authenticatedUser;
                    }
                }
                if (_auth.ConnectionProperties.Connected)
                {
                    
                    //_factionViewModelCollection.Update();
                    
                    Application.Current.Dispatcher.BeginInvoke(new Action(delegate ()
                    {
                        ConnectImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/connect.png"));
                        ConnectedStatusColor.Color = Color.FromRgb(100, 255, 100);
                        //_auth.ConnectionProperties.ActivePlayer = converterJson.ConvertJsonToPlayer(_auth.GetMeBasicAuth());
                        
                        _serviceCollection.PlayerService.UpdatePlayersCache();
                        _serviceCollection.FactionService.UpdateFactions();
                        _factionViewModelCollection.UpdateFactionsView();
                        _serviceCollection.FactionPlayerService.UpdateFactionPlayers();
                        _auth.ConnectionProperties.ActivePlayer = _serviceCollection.PlayerService.GetPlayer(converterJson.ConvertJsonToUser(_auth.GetMeBasicAuth()).ActivePlayerId);
                    }
                    ));
                    //RaisePropertyChanged(nameof(_factionViewModelCollection.Factions));
                    //RaisePropertyChanged(nameof(_playerViewModelCollection.Players));
                }

                /*if (!((Repository)_repository).Connected)
                {
                    if (_repository.Auth() != null)
                    //DoConnectAsync();
                    {
                        ((Repository)_repository).Connected = true;
                        ConnectImage.Source =
                            BitmapFrame.Create(
                                new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/connect.png"));
                        //_playerViewModelCollection.Players = null;
                        //PlayerDtoCollection playerDtoCollection = new PlayerDtoCollection(_serviceCollection.PlayerService);
                        //_playerViewModelCollection = new PlayerViewModelCollection(playerDtoCollection);
                        _playerViewModelCollection = new PlayerViewModelCollection(_serviceCollection.PlayerService);

                        //FactionDtoCollection factionDtoCollection = new FactionDtoCollection(_serviceCollection.FactionService);
                        //_factionViewModelCollection = new FactionViewModelCollection(_window, _repository, _serviceCollection, factionDtoCollection);
                        _factionViewModelCollection = new FactionViewModelCollection(_window, _repository, _serviceCollection);
                    }
                }*/
                //else
                //{
                //((Repository) _repository).UnAuth();
                /*((Repository)_repository).Connected = false;
                ConnectImage.Source =
                    BitmapFrame.Create(
                        new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/disconnect.png"));
                        */
                //}

                //_repository.Auth();
                //RequestResourceViewModelCollection.Refresh(null);
                //PlayerViewModelCollection.Refresh(null);
                //FactionViewModelCollection.Refresh(null);
            });
        }

        public ICommand DoConnectCommand
        {
            get { return _doConnectCommand; }
        }

        public ICommand ShowPlayerWindowCommand
        {
            get { return _showPlayerWindowCommand; }
        }

        public ICommand ShowOptionsWindowCommand
        {
            get { return _showOptionsWindowCommand; }
        }

        public ICommand ShowRegisterNewUserWindowCommand
        {
            get { return _showRegisterNewUserWindowCommand; }
        }

        public ICommand ShowSettingMyPlayersWindowCommand
        {
            get { return _showSettingMyPlayersWindowCommand; }
        }





        /*
        private Task<string> DoConnectAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                //TODO Диспатчер
                // здесь надо делать через диспатчер
                
                Application.Current.Dispatcher.BeginInvoke(new Action(
                    delegate()
                    {
                        //_repository.Auth()
                        if (_repository.Auth() != "")
                        {
                            connected = true;
                            //Thread.Sleep(1000);
                            RequestResourceViewModelCollection.Refresh(null);
                            PlayerViewModelCollection.Refresh(null);
                            FactionViewModelCollection.Refresh(null);
                        }


                    }
                ));
                return "";
            });
            
        }*/

    }
}

/*
 * Взято отсюда: https://toster.ru/q/87390
 * 
 Если поток выполняется где-то в бизнес-слое и не знает про форму, то можно использовать контекст синхронизации. Вот так:

При создании формы:
SynchronizationContext uiContext = SynchronizationContext.Current;
Thread thread = new Thread(Run);
// Запустим поток и установим ему контекст синхронизации,
// таким образом этот поток сможет обновлять UI
thread.Start(uiContext);


Код потока:
private void Run(object state)
    {
        // вытащим контекст синхронизации из state'а
        SynchronizationContext uiContext = state as SynchronizationContext;
         // говорим что в UI потоке нужно выполнить метод UpdateUI 
         // и передать ему в качестве аргумента строку
         uiContext.Post(UpdateUI, "Hello world!");
    }


И код который выполняет действие по изменению UI
/// <summary>
/// Этот метод исполняется в основном UI потоке
/// </summary>
private void UpdateUI(object state)
{
    sampleListBox.Items.Add((string)state);
}


При этом никаких beginInvoke'ов в методе UpdateUI уже не потребуется, т.к. код однозначно исполняется в UI потоке.
     */
