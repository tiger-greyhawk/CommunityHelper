using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CommunityHelper.ViewModel;
using CommunityHelper.ViewModel.Collection;
using RepositoryCommunityHelper.DAO;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.Service;
using RepositoryCommunityHelper.WebService;
using ViewCommunityHelper.View;
using ViewCommunityHelper.View.WindowXaml;

namespace CommunityHelper.Container
{
    class MainContainer : IContainer
    {
        private IWindow window;
        //public Window main { get; private set; }
        ConnectionProperties connectionPropeties;// = new ConnectionProperties();
        public ConnectionProperties ConnectionProperties
        {
            get
            { return connectionPropeties; }
            set
            { connectionPropeties = value;}
        }

        public void UpdateConnectionProperties(ConnectionProperties con)
        {
            connectionPropeties.Save(con);
            /*
            connectionPropeties.ServerAddres = con.ServerAddres;
            connectionPropeties.ServerPort = con.ServerPort;
            connectionPropeties.SubServer = con.SubServer;
            connectionPropeties.Login = con.Login;
            connectionPropeties.Password = con.Password;
            */
            //connectionPropeties.UrlServer = "http://" + _serverAddres + ":" + _serverPort + "/" + _subServer + "/";
        }

        public IWindow ResolveWindow()
        {
            
            connectionPropeties = new ConnectionProperties();
            Auth auth = new Auth(connectionPropeties);
            //auth.DoAuth();
            /*TODO Авторизация
            *  Перенес авторизацию в MainViewModel. Теперь инжектю объект auth в "IRepository" и вызываю метод из MainViewModel
            */
            //IService restClient = new RestClient(connectionPropeties);
            IService restClient = new RestClient(auth);
            IMapper mapper = new Mapper();
            //IRepository repository = new Repository(restClient, mapper, auth);
            

            ////  ********  по-lemegeton`ски  открыто

            ConverterJson converterJson = new ConverterJson();
            UserDao userDao = new UserDao(restClient, converterJson);
            FactionDao factionDao = new FactionDao(restClient, converterJson);
            PlayerDao playerDao = new PlayerDao(restClient, converterJson);
            FactionPlayerDao factionPlayerDao = new FactionPlayerDao(restClient, converterJson);
            UserService userService = new UserService(userDao);
            FactionService factionService = new FactionService(factionDao, mapper);
            PlayerService playerService = new PlayerService(playerDao, mapper);
            FactionPlayerService factionPlayerService = new FactionPlayerService(factionPlayerDao);
            //PlayerInFactionListService playerInFactionListService = new PlayerInFactionListService(playerService, factionService);
            
            ServiceCollection serviceCollection = new ServiceCollection(userService, playerService, factionService, factionPlayerService);//, playerInFactionListService);

            PlayerViewModelCollection playerViewModelCollection = new PlayerViewModelCollection(serviceCollection, auth);
            
            MainViewModelContainer mainViewModelContainer = new MainViewModelContainer(playerViewModelCollection);

            
            ////  ********  по-lemegeton`ски  закрыто

            //IViewModelFactory vmFactory = new ViewModelFactory(repository, serviceCollection);
            IViewModelFactory vmFactory = new ViewModelFactory(serviceCollection, auth, mainViewModelContainer);

            Window main = new Main();
            
            window = new MainWindowAdapter(main, vmFactory);//, connectionPropeties);
            PrepareViewModels();

            //FactionViewModelCollection factionViewModelCollection =
                mainViewModelContainer.CreateFactionViewModelCollection(window, serviceCollection);

            return window;
        }

        private void PrepareViewModels()
        {
            // здесь создаем все вьюМодели и передаем их в MainWindowAdapter(?)
            //throw new NotImplementedException("сделать подготовку вьюМоделей и никаких нью в дальнейшем.");
        }
    }
}
