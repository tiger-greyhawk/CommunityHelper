using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Mapper
{
    public class ConverterJson
    {
        public RequestResource ConvertJsonToRequestResource(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(RequestResource));
            RequestResource clear = (RequestResource)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public IEnumerable<RequestResource> ConvertJsonToRequestResourcesCollection(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<RequestResource>));
            List<RequestResource> clear = (List<RequestResource>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public Player ConvertJsonToPlayer(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Player));
            Player clear;
            //dataToSerialize = "";
            try
            {
                clear = (Player)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                clear = new Player();
            }
            //Player clear = (Player)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public string ConvertPlayerToJson(Player player)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Player));
            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, player);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();
            return json;
        }

        public User ConvertJsonToUser(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(User));
            User clear = (User)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public string ConvertUserToJson(User user)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(User));
            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, user);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();
            return json;
        }

        public IEnumerable<Player> ConvertJsonToPlayersCollection(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Player>));
            List<Player> clear = (List<Player>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public IEnumerable<FactionPlayer> ConvertJsonToFactionPlayersCollection(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<FactionPlayer>));
            List<FactionPlayer> clear = (List<FactionPlayer>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public Faction ConvertJsonToFaction(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(Faction));
            Faction clear = (Faction)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        public string ConvertFactionToJson(Faction faction)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Faction));
            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, faction);
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            string json = sr.ReadToEnd();
            return json;
            //byte[] body = Encoding.UTF8.GetBytes(json);
            //Faction clear = (Faction)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            //return clear;
        }



        



        public IEnumerable<Faction> ConvertJsonToFactionsCollection(string dataToSerialize)
        {
            //ObservableCollection<RequestResource> reqsRes = new ObservableCollection<RequestResource>();
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(List<Faction>));
            List<Faction> clear = (List<Faction>)json.ReadObject(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(dataToSerialize)));
            return clear;
        }

        
    }
}
