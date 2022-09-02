using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Api.Service;

namespace Movies_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService service)
        {
            _genreService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreService.GetAllAsync();
            return Ok(genres);
        }
        
        [HttpGet("Id")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genre { Name = dto.Name };
            await _genreService.AddAsync(genre);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id,[FromBody]GenreDto dto)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null)
                return NotFound($"No genre was found with ID :{id}");
            genre.Name = dto.Name;
            _genreService.Update(genre);
            return Ok(genre);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null)
                return NotFound($"No genre was found with ID :{id}");
            await _genreService.DeleteAsync(genre);
            return Ok(genre);
        }
    }
}
