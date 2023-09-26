namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}
