using System.Collections.Generic;
using RepositoryCommunityHelper.DAO;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class FactionPlayerService : BaseMagic
    {
        private readonly FactionPlayerDao _factionPlayerDao;
        private IEnumerable<FactionPlayer> _factionPlayers;


        public FactionPlayerService(FactionPlayerDao factionPlayerDao)
        {
            _factionPlayerDao = factionPlayerDao;
            _factionPlayers = new List<FactionPlayer>();
        }

        public IEnumerable<FactionPlayer> GetFactionPlayers()
        {
            return _factionPlayerDao.GetFactionPlayers();
        }

        public IEnumerable<FactionPlayer> FactionPlayers
        {
            get { return _factionPlayers; }
        }

        public void UpdateFactionPlayers()
        {
            _factionPlayers = _factionPlayerDao.GetFactionPlayers();
        }
    }
}