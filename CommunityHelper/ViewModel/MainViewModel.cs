using ViewCommunityHelper.View;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Repository;
using ViewCommunityHelper.View.WindowXaml;

namespace CommunityHelper.ViewModel
{
    public class MainViewModel : BaseMagic
    {
        //delegate void AuthDo();


        private readonly IRepository _repository;
        private readonly IWindow _window;

        private readonly RequestResourceViewModelCollection _requestResourceViewModelCollection;
        private readonly PlayerViewModelCollection _playerViewModelCollection;
        private readonly FactionViewModelCollection _factionViewModelCollection;

        private TimerCallback timeCB;
        private Timer Timer1;
        //        private readonly RelayCommand _addRRCommand;
        public bool initialized = false;
        
        private Image _connectImage = new Image();
        

        // окна программы (дети)
        private GameFunctionalWindow _gameFunctionalWindow = new GameFunctionalWindow();
        private FactionWindow _factionWindow = new FactionWindow();
        private PlayersWindow _playersWindow = new PlayersWindow();

        // Команды
        private RelayCommand _doConnectCommand;
        private RelayCommand _showPlayerWindowCommand;
        
        public MainViewModel(IRepository repository, IWindow window)
        {
            if (repository == null)
                throw new ArgumentNullException(nameof(repository));
            
            if (window == null)
                throw new ArgumentNullException(nameof(window));

            _repository = repository;
            //_repository.Auth();
            _window = window;

            _requestResourceViewModelCollection = new RequestResourceViewModelCollection(_window, _repository);//, ((Repository)_repository).RequestResourceDtos);

            _playerViewModelCollection = new PlayerViewModelCollection(_window, _repository);
            _factionViewModelCollection = new FactionViewModelCollection(_window, _repository);

            ConnectImage.Source = BitmapFrame.Create(new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/disconnect.png"));

            _doConnectCommand = new RelayCommand(Connect);
            _showPlayerWindowCommand = new RelayCommand(ShowPlayerWindow);

            timeCB = new TimerCallback(TimeTick);
            Timer1 = new Timer(timeCB, null, 10000, 10000);
        }

        void TimeTick(object state)
        {
            //Refresh(null);
            //RequestResourceVM = mapper.MapToVM(((Repository)_repository).RequestResourceDtos, _repository.GetPlayerDtos());
            RequestResourceViewModelCollection.RequestResourceDtos = ((Repository)_repository).RequestResourceDtos;
            FactionViewModelCollection.FactionDtos = ((Repository) _repository).FactionDtos;
        }

        public RequestResourceViewModelCollection RequestResourceViewModelCollection
        { 
            get { return _requestResourceViewModelCollection; }
        }

      

        public PlayerViewModelCollection PlayerViewModelCollection
        {
            get { return _playerViewModelCollection; }
        }

        public FactionViewModelCollection FactionViewModelCollection
        {
            get { return _factionViewModelCollection; }
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
                _playersWindow = new PlayersWindow();
                _window.CreateChildByViewModel(this.PlayerViewModelCollection, _playersWindow).Show();
                //FactionWindow factionWindow = new FactionWindow();
                //_window.CreateChildByViewModel(this, factionWindow).Show();
                //Console.WriteLine(exception);

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
                _factionWindow = new FactionWindow();
                _window.CreateChildByViewModel(this.FactionViewModelCollection, _factionWindow).Show();
                //Console.WriteLine(exception);

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
            if (!((Repository)_repository).Connected)
            {
                if (_repository.Auth() != null)
                //DoConnectAsync();
                {
                    ((Repository)_repository).Connected = true;
                    ConnectImage.Source =
                        BitmapFrame.Create(
                            new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/connect.png"));
                }
            }
            else
            {
                //((Repository) _repository).UnAuth();
                /*((Repository)_repository).Connected = false;
                ConnectImage.Source =
                    BitmapFrame.Create(
                        new Uri(@"pack://application:,,,/ViewCommunityHelper;component/Icons/disconnect.png"));
                        */
            }
            
            //_repository.Auth();
            //RequestResourceViewModelCollection.Refresh(null);
            //PlayerViewModelCollection.Refresh(null);
            //FactionViewModelCollection.Refresh(null);
    
        }

        public ICommand DoConnectCommand
        {
            get { return _doConnectCommand; }
        }

        public ICommand ShowPlayerWindowCommand
        {
            get { return _showPlayerWindowCommand; }
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
