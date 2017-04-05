using ViewCommunityHelper.View;
using System;
using RepositoryCommunityHelper.Repository;

namespace CommunityHelper.ViewModel
{
    class ViewModelFactory : IViewModelFactory
    {
        private readonly IRepository _repository;

        public ViewModelFactory(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }
            _repository = repository;
        }

        public MainViewModel Create(IWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            return new MainViewModel(_repository, window);
        }
    }
}
