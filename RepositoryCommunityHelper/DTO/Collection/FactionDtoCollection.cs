using System;
using System.Collections.ObjectModel;
using System.Linq;
using RepositoryCommunityHelper.DTO.EventArgs;
using RepositoryCommunityHelper.Service;

namespace RepositoryCommunityHelper.DTO.Collection
{
    public class FactionDtoCollection :BaseMagic
    {
        
        public event EventHandler<FactionDtoEventArgs> FactionDtoUpdated = delegate { };
        private readonly FactionService _factionService;

        public FactionDtoCollection(FactionService factionService)
        {
            _factionService = factionService;
            //FactionDtos = new ObservableCollection<FactionDto>();
            //GetFactions();
        }
        /*
        public void UpdateFaction(FactionDto updatedFactionDto)
        {
            GetFaction(updatedFactionDto.Id).Update(updatedFactionDto);
            FactionDtoUpdated(this,
                new FactionDtoEventArgs(updatedFactionDto));
        }
        */
        /*
        public ObservableCollection<FactionDto> FactionDtos
        {
            get
            {
                Mapper.Mapper mapper = new Mapper.Mapper();
                return mapper.Map(_factionService.GetFactions());
            }
            //set;
        }*/

        /*public void GetFactions()
        {
            Mapper.Mapper mapper = new Mapper.Mapper();
            foreach (FactionDto factionDto in mapper.Map(_factionService.GetFactions()))
            {
                FactionDtos.Add(factionDto);
                //UpdateFaction(factionDto);
            }
            
        }*/
        /*
        private FactionDto GetFaction(int factionDtoId)
        {
            return FactionDtos.FirstOrDefault(
                factionDto => factionDto.Id == factionDtoId);
        }
        */
    }
}