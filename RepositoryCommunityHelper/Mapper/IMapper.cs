using System.Collections.Generic;
using System.Collections.ObjectModel;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Mapper
{
    public interface IMapper
    {
        ObservableCollection<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources, IEnumerable<Player> players);
        //ObservableCollection<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources);
        ObservableCollection<PlayerDto> Map(IEnumerable<Player> players);
        ObservableCollection<FactionDto> Map(IEnumerable<Faction> factions);
    }
}
