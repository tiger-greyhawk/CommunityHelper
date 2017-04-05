using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunityHelper.ViewModel
{
    public class PlayerViewModel
    {
        public int Id { get; set; }
        public string Nick { get; set; }
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
    }
}
