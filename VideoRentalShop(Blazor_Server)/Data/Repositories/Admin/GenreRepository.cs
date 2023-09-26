using Microsoft.EntityFrameworkCore;
using VideoRentalShop_Blazor_Server_.Entities;

namespace VideoRentalShop_Blazor_Server_.Data.Repositories.Admin
{
    public class GenreRepository : IDisposable
    {
        private readonly DataContext _dataContext;

        public GenreRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public async Task<Genre> CreateGenreAsync(Genre genre)
        {
            _dataContext.Genres.Add(genre);
            await _dataContext.SaveChangesAsync();
            return genre;
        }

        public async Task<List<Genre>> GetAllGenresAsync()
        {
            var genres = await _dataContext.Genres.ToListAsync();
            return genres;
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _dataContext.Genres.FindAsync(id);
        }

        public async Task DeleteGenreAsync(int id)
        {
            var genre = await GetGenreByIdAsync(id);
            _dataContext.Genres.Remove(genre);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Genre> UpdateGenreAsync(Genre updateGenre)
        {
            var genre = await GetGenreByIdAsync(updateGenre.Id);
            genre.Name = updateGenre.Name;
            genre.Description = updateGenre.Description;

            await _dataContext.SaveChangesAsync();
            return genre;
        }
    }
}
