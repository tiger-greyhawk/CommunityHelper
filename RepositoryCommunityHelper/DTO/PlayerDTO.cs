using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryCommunityHelper.DTO.EventArgs;

namespace RepositoryCommunityHelper.DTO
{
    [Magic]
    public class PlayerDto : BaseMagic
    {

        event EventHandler<PlayerDtoEventArgs> PlayerDtoUpdated;

        public int Id { get; set; }
        public int UserId { get; set; }
        //public static readonly DependencyProperty NickProperty;
        public string Nick { get; set; }
        public int Invite { get; set; }
        public string Motivater { get; set; }
        public DateTime LastAccess { get; set; }
        public int FactionId { get; set; }
        public string Avatar { get; set; }
        public string Faction { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                //RaisePropertyChanged("IsSelected");
            }
        }

        public string NickWorld
        {
            get { return Nick + " (" + Motivater+ ")"; }
        }

        private bool _isCurrentPlayer;

        public bool IsCurrentPlayer
        {
            get { return _isCurrentPlayer; }
            set
            {
                if (value == null || value == _isCurrentPlayer) return;
                _isCurrentPlayer = value;
            }
        }

        public PlayerDto()
        {
            
        }

        public PlayerDto(int id, int userId, string nick, int invite, string motivater, DateTime lastAccess, int factionId, string avatar)
        {
            Id = id;
            UserId = userId;
            Nick = nick;
            Invite = invite;
            Motivater = motivater;
            LastAccess = lastAccess;
            FactionId = factionId;
            Avatar = avatar;
            //Update(this);
        }

        public PlayerDto(PlayerDto playerDto)
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

        
    }
}
