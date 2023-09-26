namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int RepertoireId { get; set; }
        public Repertoire Repertoire { get; set; }
        public int Quantity { get; set; }
    }
}
