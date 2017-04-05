using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RepositoryCommunityHelper.Entity
{
//    [DataContract]
    public class Faction : BaseMagic
    {
//        [DataMember]
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

        public Faction()
        {
        }
    }
}
