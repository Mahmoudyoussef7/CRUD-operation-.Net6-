namespace Movies_Api.Service
{
    public interface IGenreService
    {
        Task<Genre> AddAsync(Genre genre);
        Genre Update(Genre genre);
        Task<Genre> DeleteAsync(Genre genre);
        Task<Genre> GetByIdAsync(byte id);
        Task<IEnumerable<Genre>> GetAllAsync();
    }
}
