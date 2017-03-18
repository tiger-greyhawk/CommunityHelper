using System.Collections.Generic;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Mapper
{
    public interface IMapper
    {
        //IEnumerable<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources, IEnumerable<Player> players);
        IEnumerable<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources);
        IEnumerable<PlayerDto> Map(IEnumerable<Player> players);
    }
}
