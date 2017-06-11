using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityHelper.ViewModel;
using CommunityHelper.ViewModel.Collection;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.Service;
using ViewCommunityHelper.View;

namespace CommunityHelper.Container
{
    public class MainViewModelContainer: BaseMagic
    {
        public PlayerViewModelCollection PlayerViewModelCollection { get; set; }
        public FactionViewModelCollection FactionViewModelCollection { get; set; }

        public MainViewModelContainer(PlayerViewModelCollection playerViewModelCollection)
        {
            PlayerViewModelCollection = playerViewModelCollection;
            //FactionViewModelCollection = factionViewModelCollection;
        }

        public FactionViewModelCollection CreateFactionViewModelCollection(IWindow window, ServiceCollection serviceCollection)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            //return new MainViewModel(_repository, window, _serviceCollection);
            FactionViewModelCollection = new FactionViewModelCollection(window, serviceCollection);
            return FactionViewModelCollection;
        }
    }
}
