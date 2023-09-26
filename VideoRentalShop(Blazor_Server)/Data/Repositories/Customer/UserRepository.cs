using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VideoRentalShop_Blazor_Server_.Entities;

namespace VideoRentalShop_Blazor_Server_.Data.Repositories.Customer
{
    public class UserRepository : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthenticationStateProvider _authenticationState;

        public UserRepository(
            DataContext dataContext, 
            UserManager<ApplicationUser> userManager,
            AuthenticationStateProvider authenticationState)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _authenticationState = authenticationState;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task<ApplicationUser> GetUserAsync()
        {
            var authState = await _authenticationState.GetAuthenticationStateAsync();
            var user = authState.User;
            var applicationUser = new ApplicationUser();
            if (user != null)
            {
                applicationUser = await _userManager.Users
                    .Include(u => u.Cart)
                    .ThenInclude(c => c.CartItems)
                    .ThenInclude(ci => ci.Repertoire)
                    .FirstOrDefaultAsync(u => u.Id == user.Claims.FirstOrDefault().Value);

                if (applicationUser.Cart == null)
                {
                    applicationUser.Cart = new Cart { UserId = applicationUser.Id };
                    _dataContext.Carts.Add(applicationUser.Cart);
                    await _dataContext.SaveChangesAsync();
                }
            }
            return applicationUser;
        }

        public async Task<CartItem> AddToCartAsync(Repertoire repertoire, ApplicationUser user)
        {
            var cart = new Cart();

            if (user.Cart == null)
            {
                cart = new Cart { UserId = user.Id };
                _dataContext.Carts.Add(cart);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                cart = user.Cart;
            }

            if (cart.CartItems.Any(ci => ci.RepertoireId == repertoire.Id))
            {
                var existantCartItem = cart.CartItems.FirstOrDefault(ci => ci.RepertoireId == repertoire.Id);
                existantCartItem.Quantity += 1;
                await _dataContext.SaveChangesAsync();
                return existantCartItem;
            }
            else
            {
                var cartItem = new CartItem()
                {
                    CartId = cart.Id,
                    RepertoireId = repertoire.Id,
                    Quantity = 1
                };
                _dataContext.CartItems.Add(cartItem);
                await _dataContext.SaveChangesAsync();
                return cartItem;
            }
        }

        public async Task RemoveItemFromCart(CartItem item)
        {
            _dataContext.CartItems.Remove(item);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateItem(CartItem updateItem)
        {
            var oldItem = await _dataContext.CartItems.FirstOrDefaultAsync(ci => ci.Id == updateItem.Id);
            oldItem.Quantity = updateItem.Quantity;
            await _dataContext.SaveChangesAsync();
        }
    }
}
