using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Mapper
{
    public class Mapper : IMapper
    {
        public DateTime Map(long timestamp)
        {
            //if (timestamp > 0)  //*  TODO переделать проверку
            //{
            //    throw new ArgumentNullException("timestamp");
            //}

            DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddMilliseconds(timestamp);
            return time;
        }

        public long Map(DateTime timestamp)
        {
            long unixTime = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).Milliseconds;
            return unixTime;
        }

        public PlayerDto Map(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            var dto = new PlayerDto(
                player.Id, 
                player.UserId, 
                player.Nick, 
                player.Invite, 
                player.Motivater, 
                Map(player.LastAccess), 
                player.FactionId, 
                player.Avatar);
            //vm.Id = player.Id;
            //vm.Nick = player.Nick;
            return dto;
        }

        public Player Map(PlayerDto player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }
            var newPlayer = new Player(
                player.Id,
                player.UserId,
                player.Nick,
                player.Invite,
                player.Motivater,
                Map(player.LastAccess),
                player.FactionId,
                player.Avatar);
            //vm.Id = player.Id;
            //vm.Nick = player.Nick;
            return newPlayer;
        }

        public RequestResourceDto Map(RequestResource requestResource, Player player)
        {
            if (requestResource == null)
            {
                throw new ArgumentNullException(nameof(requestResource));
            }

            if (player == null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            /*if (player == null)
            {
                throw new ArgumentNullException("Player");
            }*/

            var dto = new RequestResourceDto(
                requestResource.Id,
                requestResource.WorldId,
                requestResource.Type,
                requestResource.Name,
                requestResource.VillageId,
                requestResource.Amount,
                requestResource.OnWay,
                requestResource.Max_quantum,
                player.Nick,
                Map(requestResource.Timestamp),
                Map(requestResource.CurrentTimestamp)
                );
            
            //dto.PlayerNick = this.Map(player).Nick;
            
            return dto;
        }

        public FactionDto Map(Faction faction)
        {
            //FactionDto factionDto = new FactionDto(
            return new FactionDto(
                faction.Id,
                faction.HouseId,
                faction.Name,
                faction.Owner,
                faction.Officer1,
                faction.Officer2,
                faction.Officer3,
                faction.Officer4,
                faction.Officer5,
                faction.OfficerChat,
                faction.BasicChat
               );
            //return factionDto;
        }

        public Faction Map(FactionDto faction)
        {
            //FactionDto factionDto = new FactionDto(
            return new Faction(
                faction.Id,
                faction.HouseId,
                faction.Name,
                faction.Owner,
                faction.officer1,
                faction.officer2,
                faction.officer3,
                faction.officer4,
                faction.officer5,
                faction.officerChat,
                faction.basicChat
               );
            //return factionDto;
        }

        /*public RequestResourceDto Map(RequestResource requestResource)
        {
            if (requestResource == null)
            {
                throw new ArgumentNullException("RequestResource");
            }

            var vm = new RequestResourceDto();
            vm.Id = requestResource.Id;
            vm.PlayerNick = "unreadable";
            vm.Timestamp = this.Map(requestResource.Timestamp);
            return vm;
        }*/

        public ObservableCollection<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources, IEnumerable<Player> players)
        {
            if (requestsResources == null)
            {
                throw new ArgumentNullException("requestResources");
            }
            
            if (players == null)
            {
                throw new ArgumentNullException("players");
            }
            
            ObservableCollection<RequestResourceDto> result = new ObservableCollection<RequestResourceDto>();
            foreach (var reqRes in requestsResources)
            {

                //yield return this.Map(reqRes, players.FirstOrDefault(player => player.Id == reqRes.PlayerId));
                result.Add(this.Map(reqRes, players.FirstOrDefault(player => player.Id == reqRes.PlayerId)));
                //yield return this.Map(reqRes);
            }
            return (ObservableCollection<RequestResourceDto>) result;
        }


        public ObservableCollection<PlayerDto> Map(IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException("players");
            }

            ObservableCollection<PlayerDto> result = new ObservableCollection<PlayerDto>();
            foreach (var player in players)
            {
                result.Add(this.Map(player));
            }
            return result;
        }

/*        public ObservableCollection<PlayerDto> Map(IEnumerable<PlayerDto> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException("players");
            }

            ObservableCollection<PlayerDto> result = new ObservableCollection<PlayerDto>();
            foreach (var player in players)
            {
                result.Add(this.Map(player));
            }
            return result;
        }*/

        public ObservableCollection<FactionDto> Map(IEnumerable<Faction> factions)
        {
            if (factions == null)
            {
                throw new ArgumentNullException(nameof(factions));
            }

            ObservableCollection<FactionDto> result = new ObservableCollection<FactionDto>();
            foreach (var faction in factions)
            {
                result.Add(this.Map(faction));
            }
            return result;
        }
    }
}
