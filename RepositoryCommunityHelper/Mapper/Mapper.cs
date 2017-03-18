using System;
using System.Collections.Generic;
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

        public RequestResourceDto Map(RequestResource requestResource)
        {
            if (requestResource == null)
            {
                throw new ArgumentNullException(nameof(requestResource));
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
                requestResource.PlayerId,
                Map(requestResource.Timestamp),
                Map(requestResource.CurrentTimestamp)
                );
            
            //dto.PlayerNick = this.Map(player).Nick;
            
            return dto;
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

        public IEnumerable<RequestResourceDto> Map(IEnumerable<RequestResource> requestsResources)//, IEnumerable<Player> players)
        {
            if (requestsResources == null)
            {
                throw new ArgumentNullException("requestResources");
            }
            /*
            if (players == null)
            {
                throw new ArgumentNullException("players");
            }
            */

            foreach (var reqRes in requestsResources)
            {

                //yield return this.Map(reqRes, players.FirstOrDefault(player => player.Id == reqRes.PlayerId));
                yield return this.Map(reqRes);
            }
        }


        public IEnumerable<PlayerDto> Map(IEnumerable<Player> players)
        {
            if (players == null)
            {
                throw new ArgumentNullException("players");
            }

            foreach (var player in players)
            {
                yield return this.Map(player);
            }
        }
    }
}
