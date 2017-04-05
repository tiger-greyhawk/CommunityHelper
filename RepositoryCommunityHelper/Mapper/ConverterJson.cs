using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Mapper
{
    public class ConverterJson
    {
        public IEnumerable<RequestResource> ConvertJsonToRequestResources(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestResource>));
            List<RequestResource> clear = (List<RequestResource>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public IEnumerable<Player> ConvertJsonToPlayers(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
            List<Player> clear = (List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public IEnumerable<Faction> ConvertJsonToFactions(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Faction>));
            List<Faction> clear = (List<Faction>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        
    }
}
