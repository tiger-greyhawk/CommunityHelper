using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CommunityHelper.ViewModel;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace CommunityHelper.MapperVMDto
{
    public class MapperToFromVM : IMapperVM
    {
        public ObservableCollection<RequestResourceViewModel> MapToVM(IEnumerable<RequestResourceDto> rRDtos, IEnumerable<PlayerDto> playerDtos)
        {
            ObservableCollection<RequestResourceViewModel> resultCollection = new ObservableCollection<RequestResourceViewModel>();
            if (rRDtos != null)
            foreach (var rRD in rRDtos)
            {
                 resultCollection.Add(this.MapToVM(rRD, playerDtos.FirstOrDefault(p => p.Nick == rRD.PlayerNick)));
            }
            return resultCollection;
        }

        public RequestResourceViewModel MapToVM(RequestResourceDto requestResourceDto, PlayerDto playerDto)
        {
            RequestResourceViewModel requestResourceViewModel = new RequestResourceViewModel(
                requestResourceDto.Id,
                requestResourceDto.Name,
                playerDto.Nick,
                requestResourceDto.Timestamp,
                false,
                requestResourceDto.Type+"/"+requestResourceDto.Amount,
                requestResourceDto.MaxQuantum+"/"+requestResourceDto.OnWay,
                requestResourceDto.VillageId
                );

            return requestResourceViewModel;
        }

        public PlayerViewModel MapToVM(PlayerDto playerDto)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel(
                playerDto.Id,
                playerDto.Nick,
                playerDto.LastAccess,
                false);
            return playerViewModel;
        }

        public FactionViewModel MapToVM(FactionDto factionDto)
        {
            FactionViewModel factionViewModel = new FactionViewModel(
                factionDto.Id,
                factionDto.HouseId,
                factionDto.Name,
                factionDto.Owner,
                factionDto.officer1,
                factionDto.officer2,
                factionDto.officer3,
                factionDto.officer4,
                factionDto.officer5,
                factionDto.officerChat, 
                factionDto.basicChat
               );
            return factionViewModel;
        }
    }
}
