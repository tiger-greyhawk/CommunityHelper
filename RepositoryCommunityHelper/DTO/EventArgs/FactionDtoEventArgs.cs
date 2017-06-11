namespace RepositoryCommunityHelper.DTO.EventArgs
{
    public class FactionDtoEventArgs : System.EventArgs
    {
        public FactionDto FactionDto { get; set; }
        public FactionDtoEventArgs(FactionDto factionDto)
        {
            this.FactionDto = factionDto;
        }
    }
}