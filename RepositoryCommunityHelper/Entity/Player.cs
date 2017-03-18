using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RepositoryCommunityHelper.Entity
{


    [DataContract]
    public class Player : INotifyPropertyChanged
    {
        [DataMember]
        //public static readonly DependencyProperty IdProperty;
        private int id;

        [DataMember] private int userId;

        [DataMember]
        //public static readonly DependencyProperty NickProperty;
        private string nick;

        [DataMember] private int invite;
        [DataMember] private string motivater;
        [DataMember] private long lastAccess;
        [DataMember] private int factionId;
        private string avatar;

        //public SolidColorBrush online { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                // setter
                id = value;

                OnPropertyChanged("Id");
            }
        }

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                // setter
                userId = value;

                OnPropertyChanged("UserId");
            }
        }

        public string Nick
        {
            get
            {
                return nick;
            }
            set
            {
                // setter
                nick = value;

                OnPropertyChanged("Nick");
            }
        }

        public int Invite
        {
            get
            {
                return invite;
            }
            set
            {
                // setter
                invite = value;

                OnPropertyChanged("Invite");
            }
        }

        public string Motivater
        {
            get
            {
                return motivater;
            }
            set
            {
                // setter
                motivater = value;

                OnPropertyChanged("Motivater");
            }
        }
        public long LastAccess
        {
            get
            {
                //DateTime resultDate = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(lastAccess).AddHours(2);
                return lastAccess;
            }
            set
            {
                // setter
                //long resultDate = (value.Subtract(new DateTime()).Milliseconds);
                long resultDate = (long) new DateTime().Millisecond;
                //long resultDate = (value.Subtract(new DateTime()).Ticks);
                lastAccess = resultDate;

                OnPropertyChanged("LastAccess");
            }
        }

        public int FactionId
        {
            get
            {
                return factionId;
                //return 1;
            }
            set
            {
                // setter
                factionId = value;

                OnPropertyChanged("FactionId");
            }
        }

        public string Avatar
        {
            get
            {
                if (avatar == null) Avatar = "G:\\_Foto\\gerka_ava2.jpg";
                if (nick == "Tiger_Greyhawk") Avatar = "G:\\_Foto\\gerka.jpg";
                return avatar;
                //return 1;
            }
            set
            {
                // setter
                avatar = value;

                //OnPropertyChanged("Avatar");
            }
        }
    }
}
