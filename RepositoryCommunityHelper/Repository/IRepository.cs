using System.Collections.Generic;
using RepositoryCommunityHelper.DTO;

namespace RepositoryCommunityHelper.Repository
{
    public interface IRepository
    {
        //IRestClient Create();
        //void DeleteRequestResource();
        //IEnumerable<RequestResourceViewModel> SelectAllRequestsResources();
        //void RefreshRequestsResources();
        void RefreshPlayers();
        void RefreshFactions();
        string Auth();

        //IEnumerable<RequestResourceDto> RequestResourceDtos();
        IEnumerable<RequestResourceDto> GetRequestResourceDtos();
        IEnumerable<PlayerDto> GetPlayerDtos();
        IEnumerable<FactionDto> GetFactionDtos();
        PlayerDto FindPlayerDtoById(int id);
        PlayerDto FindPlayerDtoByNick(string playerNick);
        RequestResourceDto FindRequestResourceDtoById(int id);
        FactionDto FindFactionDtoById(int id);
    }
}
