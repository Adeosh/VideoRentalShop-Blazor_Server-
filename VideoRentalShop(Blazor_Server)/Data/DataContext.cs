using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VideoRentalShop_Blazor_Server_.Entities;

namespace VideoRentalShop_Blazor_Server_.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Repertoire> Content { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RepertoireGenre>()
                .HasKey(rg => new {rg.RepertoireId, rg.GenreId});

            builder.Entity<RepertoireGenre>()
                .HasOne(rg => rg.Genre)
                .WithMany(g => g.RepertoireGenres)
                .HasForeignKey(rg => rg.GenreId);
        }
    }
}
