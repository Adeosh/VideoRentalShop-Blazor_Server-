namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class RepertoireGenre
    {
        public int RepertoireId {  get; set; }
        public Repertoire Repertoire { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
