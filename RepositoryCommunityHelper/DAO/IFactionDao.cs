using System.Collections.Generic;
using System.Threading.Tasks;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.DAO
{
    public interface IFactionDao
    {
        IEnumerable<Faction> GetFactions();
        IEnumerable<Faction> GetFactions(List<int> ids);
        Faction GetFaction(int id);
    }
}