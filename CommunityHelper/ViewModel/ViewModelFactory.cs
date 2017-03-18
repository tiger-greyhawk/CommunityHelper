using ViewCommunityHelper.View;
using System;
using RepositoryCommunityHelper.Repository;

namespace CommunityHelper.ViewModel
{
    class ViewModelFactory : IViewModelFactory
    {
        private readonly IRepository repository;

        public ViewModelFactory(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this.repository = repository;
        }

        public MainViewModel Create(IWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }

            return new MainViewModel(repository, window);
        }
    }
}
