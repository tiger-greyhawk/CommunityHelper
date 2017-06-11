using System;
using System.Runtime.Serialization;

namespace RepositoryCommunityHelper.Entity
{
    [DataContract]
    public class User 
    {

        [DataMember]
        private int id;
        [DataMember]
        private String login;
        [DataMember]
        private String password;
        [DataMember]
        private int activePlayerId;


        public User()
        {
        }

        public User(int id, string username, string password, int activePlayerId)
        {
            this.id = id;
            this.login = username;
            this.password = password;
            this.activePlayerId = activePlayerId;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return this.login; }
            set { this.login = value; }
        }

        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public int ActivePlayerId
        {
            get { return activePlayerId; }
            set { activePlayerId = value; }
        }
    }
}