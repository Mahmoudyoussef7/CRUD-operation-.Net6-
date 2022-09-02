using Microsoft.EntityFrameworkCore;

namespace Movies_Api.Service
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            _ = _context.SaveChangesAsync();
            return movie;
        }

        public async Task<Movie> DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            var movies = await _context.Movies
                .OrderByDescending(x => x.Rate)
                .Include(d => d.Genre)
                .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetByIdAsync(byte id)
        {
            var mov = await _context.Movies.Include(m => m.Genre).FirstOrDefaultAsync(m => m.Id == id);
            return mov;
        }
        
        public async Task<IEnumerable<Movie>> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _context.Movies.Include(m => m.Genre).Where(m => m.GenreId == genreId).ToListAsync();
            return movies;
        }

        public Movie Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
