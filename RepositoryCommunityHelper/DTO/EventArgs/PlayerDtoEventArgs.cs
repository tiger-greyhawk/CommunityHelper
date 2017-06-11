using System;

namespace RepositoryCommunityHelper.DTO.EventArgs
{
    public class PlayerDtoEventArgs : System.EventArgs
    {
        public PlayerDto PlayerDto { get; set; }
        public PlayerDtoEventArgs(PlayerDto playerDto)
        {
            this.PlayerDto = playerDto;
        }
    }
}