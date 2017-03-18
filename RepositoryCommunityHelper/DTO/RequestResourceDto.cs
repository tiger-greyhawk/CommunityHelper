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
        public int PlayerId { get; set; }
        public DateTime Timestamp { get; set; }
        public DateTime CurrentTimestamp { get; set; }

        public RequestResourceDto()
        {
        }

        public RequestResourceDto(int id, int worldId, string type, string name, int villageId, int amount, int onWay, int maxQuantum, int playerId, DateTime timestamp, DateTime currentTimestamp)
        {
            Id = id;
            WorldId = worldId;
            Type = type;
            Name = name;
            VillageId = villageId;
            Amount = amount;
            OnWay = onWay;
            MaxQuantum = maxQuantum;
            PlayerId = playerId;
            Timestamp = timestamp;
            CurrentTimestamp = currentTimestamp;
        }
    }
}
