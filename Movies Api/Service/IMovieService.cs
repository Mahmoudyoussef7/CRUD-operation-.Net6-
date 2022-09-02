namespace Movies_Api.Service
{
    public interface IMovieService
    {
        Task<Movie> AddAsync(Movie movie);
        Movie Update(Movie movie);
        Task<Movie> DeleteAsync(Movie genre);
        Task<Movie> GetByIdAsync(byte id);
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<IEnumerable<Movie>> GetByGenreIdAsync(byte genreId);
    }
}
