using RepositoryCommunityHelper.Entity;

namespace RepositoryCommunityHelper.DTO
{
    public class PlayerInFactionDto
    {
        private readonly Player _player;
        private readonly Faction _faction;

        public PlayerInFactionDto(Player player, Faction faction)
        {
            _player = player;
            _faction = faction;
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public Faction GetFaction()
        {
            return _faction;
        }
    }
}