using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CommunityHelper.ViewModel;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.WebService;
using ViewCommunityHelper.View;
using ViewCommunityHelper.View.WindowXaml;

namespace CommunityHelper.Container
{
    class MainContainer : IContainer
    {
        private IWindow window;
        //public Window main { get; private set; }

        public IWindow ResolveWindow()
        {
            ConnectionProperties connectionPropeties = new ConnectionProperties();
            Auth auth = new Auth(connectionPropeties);
            //auth.DoAuth();
            /*TODO Авторизация
            *  Перенес авторизацию в MainViewModel. Теперь инжектю объект auth в "IRepository" и вызываю метод из MainViewModel
            */
            IService restClient = new RestClient(connectionPropeties);
            IMapper mapper = new Mapper();
            IRepository repository = new Repository(restClient, mapper, auth);
            IViewModelFactory vmFactory = new ViewModelFactory(repository);


            Window main = new Main();
            
            window = new MainWindowAdapter(main, vmFactory);
            PrepareViewModels();

            return window;
        }

        private void PrepareViewModels()
        {
            // здесь создаем все вьюМодели и передаем их в MainWindowAdapter(?)
            //throw new NotImplementedException("сделать подготовку вьюМоделей и никаких нью в дальнейшем.");
        }
    }
}
