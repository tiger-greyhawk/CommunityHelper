using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.DTO;

namespace CommunityHelper.ViewModel
{
    public class PlayerViewModel : BaseMagic
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Nick { get; set; }
        public int Invite { get; set; }
        public string Motivater { get; set; }
        public DateTime LastAccess { get; set; }
        public int FactionId { get; set; }
        public string Avatar { get; set; }
        public DateTime Timestamp { get; set; }
        //public DateTime Timestamp { get; set; }
        public bool IsSelected { get; set; }

        public PlayerViewModel(int id, string nick, DateTime timestamp, bool isSelected)
        {
            Id = id;
            Nick = nick;
            Timestamp = timestamp;
            IsSelected = isSelected;
        }

        public PlayerViewModel(PlayerDto playerDto)
        {
            if (playerDto == null)
                return;
            Id = playerDto.Id;
            Update(playerDto);
        }

        public PlayerViewModel(PlayerViewModel playerViewModel)
        {
            if (playerViewModel == null)
                return;
            Id = playerViewModel.Id;
            Update(playerViewModel);
        }

        public void Update(PlayerDto playerDto)
        {
            Id = playerDto.Id;
            UserId = playerDto.UserId;
            Nick = playerDto.Nick;
            Invite = playerDto.Invite;
            Motivater = playerDto.Motivater;
            LastAccess = playerDto.LastAccess;
            FactionId = playerDto.FactionId;
            Avatar = playerDto.Avatar;
            IsSelected = playerDto.IsSelected;
        }

        public void Update(PlayerViewModel playerDto)
        {
            //Реально не Dto, а ViewModel
            Id = playerDto.Id;
            UserId = playerDto.UserId;
            Nick = playerDto.Nick;
            Invite = playerDto.Invite;
            Motivater = playerDto.Motivater;
            LastAccess = playerDto.LastAccess;
            FactionId = playerDto.FactionId;
            Avatar = playerDto.Avatar;
            IsSelected = playerDto.IsSelected;
        }
    }
}
