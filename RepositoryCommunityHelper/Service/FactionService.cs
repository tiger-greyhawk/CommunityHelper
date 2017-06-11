using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using RepositoryCommunityHelper.DAO;
using RepositoryCommunityHelper.DTO;
using RepositoryCommunityHelper.Mapper;
using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.Service
{
    public class FactionService : BaseMagic
    {
        private readonly FactionDao _factionDao;
        private ObservableCollection<FactionDto> _factionsObservableCollection;
        private readonly IMapper _mapper;

        public FactionService(FactionDao factionDao, IMapper mapper)
        {
            if (factionDao == null)
                throw new ArgumentNullException(nameof(factionDao));
            _factionDao = factionDao;
            //_factionsObservableCollection = new ObservableCollection<FactionDto>();
            _factionsObservableCollection = new ObservableCollection<FactionDto>();
            _mapper = mapper;

        }

        public ObservableCollection<FactionDto> FactionsCollection
        {
            get { return _factionsObservableCollection;}
            set
            {
                if (value == _factionsObservableCollection) return;
                _factionsObservableCollection = value;
            }
        }

        public void UpdateFactions()
        {
            
            FactionsCollection = _mapper.Map(_factionDao.GetFactions());
            //FactionsCollection = _factionDao.GetFactions();
        }

        public ObservableCollection<FactionDto> GetFactions()
        {
            return FactionsCollection;
            //return _factionDao.GetFactions();
        }

        public Faction GetFaction(int id)
        {
            return _factionDao.GetFaction(id);
        }

        public Faction SaveFaction(Faction faction)
        {
            return _factionDao.SaveFaction(faction);
        }

        public Faction DeleteFaction(Faction faction)
        {
            return _factionDao.DeleteFaction(faction);
        }

        public Player JoinFaction(int factionId, Player player)
        {
            return _factionDao.JoinFaction(factionId, player);
        }

        public Player EliminateFromFaction(int factionId, Player player)
        {
            return _factionDao.EliminateFromFaction(factionId, player);
        }

        public FactionDto GetFactionById(int id)
        {
            return FactionsCollection.FirstOrDefault(f => f.Id == id);
        }
    }
}