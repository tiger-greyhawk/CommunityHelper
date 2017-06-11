using ViewCommunityHelper.View;
using System;
using CommunityHelper.Container;
using RepositoryCommunityHelper.Repository;
using RepositoryCommunityHelper.Service;
using RepositoryCommunityHelper.WebService;

namespace CommunityHelper.ViewModel
{
    class ViewModelFactory : IViewModelFactory
    {
        //private readonly IRepository _repository;
        //private readonly PlayerInFactionListService _playerInFactionListService;
        //private readonly PlayerService _playerService;
        private readonly ServiceCollection _serviceCollection;
        private readonly Auth _auth;
        private readonly MainViewModelContainer _mainViewModelContainer;

        //public ViewModelFactory(IRepository repository, ServiceCollection serviceCollection)
        public ViewModelFactory(ServiceCollection serviceCollection, Auth auth, MainViewModelContainer mainViewModelContainer)
        {
            /*if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            _repository = repository;
            */
            _serviceCollection = serviceCollection;
            _auth = auth;
            _mainViewModelContainer = mainViewModelContainer;
            //_playerInFactionListService = playerInFactionListService;
            //_playerService = playerService;
        }

        public MainViewModel Create(IWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            //return new MainViewModel(_repository, window, _serviceCollection);
            return new MainViewModel(window, _serviceCollection, _auth, _mainViewModelContainer);
        }
    }
}
