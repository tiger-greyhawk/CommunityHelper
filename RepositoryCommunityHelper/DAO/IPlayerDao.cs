using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.DAO
{
    public interface IPlayerDao
    {
        IEnumerable<Player> GetPlayers();
        IEnumerable<Player> GetPlayers(List<int> ids);
        Player GetPlayer(int id);
    }
}
