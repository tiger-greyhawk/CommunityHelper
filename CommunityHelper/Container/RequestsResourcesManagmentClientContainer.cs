using System.Windows;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.WebService;
using ViewCommunityHelper.View;
using CommunityHelper.ViewModel;
using RepositoryCommunityHelper.Mapper;


namespace CommunityHelper.Container
{
    public class RequestsResourcesManagmentClientContainer : IRequestsResourcesManagmentClientContainer
    {
        IWindow window;
        public IWindow ResolveWindow()
        {
            //RestClientConfiguration restClientConfiguration = new RestClientConfiguration();
            ConnectionProperties connectionPropeties = new ConnectionProperties();
            //IRestClient restClient = new BaseRestClient(restClientConfiguration);
            //IRestClient restClient = new BaseRestClient(connectionPropeties);
            Auth auth = new Auth(connectionPropeties);
            auth.DoAuth();
            //IService baseService = new BaseService(connectionPropeties);

            IService restClient = new RestClient(connectionPropeties);
            //ResourceService resourceService = new ResourceService(connectionPropeties, restClient);
            //BaseService baseService = new BaseService(connectionPropeties, restClient);
            //RequestResourceRestClientFactory requestResourceRestClientFactory = new RequestResourceRestClientFactory(restClientFactory);
            IMapper mapper = new Mapper();
            //IRepository repository = new RequestResourceRepository(restClient, mapper);
            //IRepository repository = new RequestResourceRepository(baseService, mapper);
            IRepository repository = new Repository(restClient, mapper);
            //IRepository repository = new RequestResourceRepository(restClient, mapper);
            IViewModelFactory vmFactory = new ViewModelFactory(repository);


            Window mainWindow = new MainWindow();
            //IWindow w = new MainWindowAdapter(mainWindow, vmFactory);
            window = new MainWindowAdapter(mainWindow, vmFactory);
            return window;
        }

        //public void Close()
        //{
        //    window.Close();
        //}
    }
}
