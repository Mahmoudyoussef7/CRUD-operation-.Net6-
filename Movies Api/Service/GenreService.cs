using Microsoft.EntityFrameworkCore;

namespace Movies_Api.Service
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> AddAsync(Genre genre)
        {     
            await _context.Genres.AddAsync(genre);
            _ = _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> DeleteAsync(Genre genre)
        {
            _context.Remove(genre);
             await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.OrderBy(gen => gen.Name).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(byte id)
        {
            var gen = _context.Genres.FirstOrDefault(gen => gen.Id == id);
            return gen;
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
