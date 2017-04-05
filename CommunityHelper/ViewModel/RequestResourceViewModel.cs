using System;
using System.Windows.Input;
using RepositoryCommunityHelper;
using RepositoryCommunityHelper.Repository;
using ViewCommunityHelper.View;

namespace CommunityHelper.ViewModel
{
    public class RequestResourceViewModel : BaseMagic
    {


        public int Id { get; set; }
        public string Name { get; set; }
        public string PlayerNick { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSelected { get; set; }
        public string TypeAmount { get; set; }
        public string MaxSended { get; set; }
        public int Village { get; set; }

        public RequestResourceViewModel()
        {
        }

        public RequestResourceViewModel(int id, string name, string playerNick, DateTime timestamp, bool isSelected, string typeAmount, string maxSended, int village)
        {
            Id = id;
            Name = name;
            PlayerNick = playerNick;
            Timestamp = timestamp;
            IsSelected = isSelected;
            TypeAmount = typeAmount;
            MaxSended = maxSended;
            Village = village;
        }

        

        //public RequestResource requestResource { get; set; }  //  временно так. Потом переделать под селектед. Пока не понимаю как это работает.
    }
}
