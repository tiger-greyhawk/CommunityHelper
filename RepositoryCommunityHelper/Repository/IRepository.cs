using System.Collections.Generic;
using RepositoryCommunityHelper.DTO;

namespace RepositoryCommunityHelper.Repository
{
    public interface IRepository
    {
        //IRestClient Create();
        //void DeleteRequestResource();
        //IEnumerable<RequestResourceViewModel> SelectAllRequestsResources();
        void RefreshRequestsResources();
        void RefreshPlayers();

        IEnumerable<RequestResourceDto> GetRequestResourceDtos();
        IEnumerable<PlayerDto> GetPlayerDtos();
    }
}
