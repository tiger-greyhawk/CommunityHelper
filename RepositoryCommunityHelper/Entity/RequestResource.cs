using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RepositoryCommunityHelper.Entity
{
    class RequestSResources
    {

        public RequestResource[] Property1 { get; set; }
    }
    [DataContract]
    public class RequestResource : INotifyPropertyChanged
    {
        [DataMember]
        private int id;
        [DataMember]
        private int worldId;
        [DataMember]
        private string type;
        [DataMember]
        private string name;
        [DataMember]
        private int villageId;
        [DataMember]
        private int amount;
        [DataMember]
        private int onWay;
        [DataMember]
        private int max_quantum;
        [DataMember]
        private int playerId;
        [DataMember]
        private long timestamp;
        [DataMember]
        private long currentTimestamp;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public int WorldId
        {
            get
            {
                return worldId;
            }

            set
            {
                worldId = value;
                OnPropertyChanged("WorldId");
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int VillageId
        {
            get
            {
                return villageId;
            }

            set
            {
                villageId = value;
                OnPropertyChanged("VillageId");
            }
        }

        public int Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public int OnWay
        {
            get
            {
                return onWay;
            }

            set
            {
                onWay = value;
                OnPropertyChanged("OnWay");
            }
        }

        public int Max_quantum
        {
            get
            {
                return max_quantum;
            }

            set
            {
                max_quantum = value;
                OnPropertyChanged("Max_quantum");
            }
        }

        public int PlayerId
        {
            get
            {
                return playerId;
            }

            set
            {
                playerId = value;
                OnPropertyChanged("PlayerId");
            }
        }

        public long Timestamp
        {
            get
            {
                return timestamp;
            }

            set
            {
                timestamp = value;
                OnPropertyChanged("Timestamp");
            }
        }

        public long CurrentTimestamp
        {
            get
            {
                return currentTimestamp;
            }

            set
            {
                currentTimestamp = value;
                OnPropertyChanged("CurrentTimestamp");
            }
        }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
