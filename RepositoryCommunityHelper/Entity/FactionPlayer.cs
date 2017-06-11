using System.Runtime.Serialization;

namespace RepositoryCommunityHelper.Entity
{
    [DataContract]
    public class FactionPlayer
    {
        [DataMember] private int id;
        [DataMember] private int factionId;
        [DataMember] private int playerId;
        [DataMember] private string comment;
        [DataMember] private bool confirm;
        [DataMember] private bool officer;

        public FactionPlayer()
        {
        }

        public FactionPlayer(int id, int factionId, int playerId, string comment, bool confirm, bool officer)
        {
            this.id = id;
            this.factionId = factionId;
            this.playerId = playerId;
            this.comment = comment;
            this.confirm = confirm;
            this.officer = officer;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int FactionId
        {
            get { return factionId; }
            set { factionId = value; }
        }

        public int PlayerId
        {
            get { return playerId; }
            set { playerId = value; }
        }

        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        public bool Confirm
        {
            get { return confirm; }
            set { confirm = value; }
        }

        public bool Officer
        {
            get { return officer; }
            set { officer = value; }
        }
    }
}