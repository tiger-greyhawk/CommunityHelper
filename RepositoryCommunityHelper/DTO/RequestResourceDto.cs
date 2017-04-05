using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryCommunityHelper.DTO
{
    public class RequestResourceDto : BaseMagic
    {
        public int Id { get; set; }
        public int WorldId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public int VillageId { get; set; }
        public int Amount { get; set; }
        public int OnWay { get; set; }
        public int MaxQuantum { get; set; }
        public string PlayerNick { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CurrentTimestamp { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }


        public RequestResourceDto()
        {
        }


        public RequestResourceDto(int id, int worldId, string type, string name, int villageId, int amount, int onWay, int maxQuantum, string playerNick, DateTime timestamp, DateTime currentTimestamp)
        {
            Id = id;
            WorldId = worldId;
            Type = type;
            Name = name;
            VillageId = villageId;
            Amount = amount;
            OnWay = onWay;
            MaxQuantum = maxQuantum;
            PlayerNick = playerNick;
            Timestamp = timestamp;
            CurrentTimestamp = currentTimestamp;
        }
    }
}
