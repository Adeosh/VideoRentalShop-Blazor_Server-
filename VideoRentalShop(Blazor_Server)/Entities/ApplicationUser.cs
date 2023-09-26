using Microsoft.AspNetCore.Identity;

namespace VideoRentalShop_Blazor_Server_.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public Cart Cart { get; set; }
    }
}
