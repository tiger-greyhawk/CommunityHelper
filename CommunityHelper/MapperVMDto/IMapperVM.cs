using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunityHelper.ViewModel;
using RepositoryCommunityHelper.DTO;

namespace CommunityHelper.MapperVMDto
{
    interface IMapperVM
    {
        RequestResourceViewModel MapToVM(RequestResourceDto requestResourceDto, PlayerDto playerDto);
        PlayerViewModel MapToVM(PlayerDto playerDto);
    }
}
