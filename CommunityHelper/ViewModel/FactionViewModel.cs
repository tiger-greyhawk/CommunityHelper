using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryCommunityHelper;

namespace CommunityHelper.ViewModel
{
    public class FactionViewModel : BaseMagic
    {
        public int id { get; set; }
        public int houseId { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string officer1 { get; set; }
        public string officer2 { get; set; }
        public string officer3 { get; set; }
        public string officer4 { get; set; }
        public string officer5 { get; set; }
        public string officerChat { get; set; }
        public string basicChat { get; set; }

        public FactionViewModel()
        {
        }

        public FactionViewModel(int id, int houseId, string name, string owner, string officer1, string officer2, string officer3, string officer4, string officer5, string officerChat, string basicChat)
        {
            this.id = id;
            this.houseId = houseId;
            this.name = name;
            this.owner = owner;
            this.officer1 = officer1;
            this.officer2 = officer2;
            this.officer3 = officer3;
            this.officer4 = officer4;
            this.officer5 = officer5;
            this.officerChat = officerChat;
            this.basicChat = basicChat;
        }
    }
}
