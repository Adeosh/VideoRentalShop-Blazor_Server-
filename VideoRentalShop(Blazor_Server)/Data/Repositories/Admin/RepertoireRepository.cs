using Microsoft.EntityFrameworkCore;
using VideoRentalShop_Blazor_Server_.Entities;

namespace VideoRentalShop_Blazor_Server_.Data.Repositories.Admin
{
    public class RepertoireRepository : IDisposable
    {
        private readonly DataContext _dataContext;

        public RepertoireRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task<Repertoire> CreateRepertoireAsync(Repertoire repertoire)
        {
            _dataContext.Content.Add(repertoire);
            await _dataContext.SaveChangesAsync();
            return repertoire;
        }

        public async Task<List<Repertoire>> GetAllRepertoireAsync()
        {
            var content = await _dataContext.Content
                .Include(r => r.RepertoireGenres)
                .ThenInclude(rg => rg.Genre)
                .ToListAsync();

            return content;
        }

        public async Task<Repertoire> GetRepertoireByIdAsync(int id)
        {
            var repertoire = await _dataContext.Content
                .Include(r => r.RepertoireGenres)
                .ThenInclude(rg => rg.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);

            return repertoire;
        }

        public async Task DeleteRepertoireAsync(int id)
        {
            var repertoire = await GetRepertoireByIdAsync(id);
            _dataContext.Content.Remove(repertoire);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Repertoire> UpdateRepertoireAsync(Repertoire updateRepertoire)
        {
            var repertoire = await GetRepertoireByIdAsync(updateRepertoire.Id);

            repertoire.Title = updateRepertoire.Title;
            repertoire.Director = updateRepertoire.Director;
            repertoire.Country = updateRepertoire.Country;
            repertoire.Description = updateRepertoire.Description;
            repertoire.Studio = updateRepertoire.Studio;
            repertoire.ReleaseDate = updateRepertoire.ReleaseDate;
            repertoire.Price = updateRepertoire.Price;
            repertoire.Quantity = updateRepertoire.Quantity;
            repertoire.Image = updateRepertoire.Image;

            repertoire.RepertoireGenres.Clear();
            foreach (var rgenre in updateRepertoire.RepertoireGenres)
            {
                var genre = await _dataContext.Genres.FindAsync(rgenre.GenreId);
                if (genre != null)
                {
                    repertoire.RepertoireGenres.Add(new RepertoireGenre { Repertoire = repertoire, Genre = genre });
                }
            }

            await _dataContext.SaveChangesAsync();
            return repertoire;
        }
    }
}
