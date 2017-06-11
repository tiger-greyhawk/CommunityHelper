using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RepositoryCommunityHelper.Entity
{
    [DataContract]
    public class Faction// : BaseMagic
    {
        [DataMember] private int id;
        [DataMember] private int houseId;
        [DataMember] private string name;
        [DataMember] private string owner;
        [DataMember] private string officer1;
        [DataMember] private string officer2;
        [DataMember] private string officer3;
        [DataMember] private string officer4;
        [DataMember] private string officer5;
        [DataMember] private string officerChat;
        [DataMember] private string basicChat;

        public Faction()
        {
        }

        public Faction(int id, int houseId, string name, string owner, string officer1, string officer2, string officer3, string officer4, string officer5, string officerChat, string basicChat)
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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int HouseId
        {
            get { return houseId; }
            set { houseId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string Officer1
        {
            get { return officer1; }
            set { officer1 = value; }
        }

        public string Officer2
        {
            get { return officer2; }
            set { officer2 = value; }
        }

        public string Officer3
        {
            get { return officer3; }
            set { officer3 = value; }
        }

        public string Officer4
        {
            get { return officer4; }
            set { officer4 = value; }
        }

        public string Officer5
        {
            get { return officer5; }
            set { officer5 = value; }
        }

        public string OfficerChat
        {
            get { return officerChat; }
            set { officerChat = value; }
        }

        public string BasicChat
        {
            get { return basicChat; }
            set { basicChat = value; }
        }
    }
}
