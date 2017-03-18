using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryCommunityHelper.DTO
{
    [Magic]
    public class PlayerDto : BaseMagic
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        //public static readonly DependencyProperty NickProperty;
        public string Nick { get; set; }
        public int Invite { get; set; }
        public string Motivater { get; set; }
        public DateTime LastAccess { get; set; }
        public int FactionId { get; set; }
        public string Avatar { get; set; }

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
        }
    }
}
