using System;

namespace CommunityHelper.ViewModel
{
    public class RequestResourceViewModel
    {
        public int Id { get; set; }
        public string PlayerNick { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsSelected { get; set; }
        //public RequestResource requestResource { get; set; }  //  временно так. Потом переделать под селектед. Пока не понимаю как это работает.
    }
}
