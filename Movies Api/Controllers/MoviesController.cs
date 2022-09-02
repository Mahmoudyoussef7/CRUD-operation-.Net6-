using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies_Api.Service;

namespace Movies_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
        private new List<string> _supportedExtensionsImages = new List<string> { ".jpg",".png"};
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMovieService movieService, IGenreService genreService, IMapper mapper)
        {
            _movieService = movieService;
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieService.GetAllAsync();
            var data = _mapper.Map<List<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetByIdAsync(byte id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound("not found movie");
            var data = _mapper.Map<MovieDetailsDto>(movie);

            return Ok(data);
        }

        [HttpGet("genreId")]
        public async Task<IActionResult> GetByGenreId(byte genreId)
        {
            var movies = await _movieService.GetByGenreIdAsync(genreId);
            var data = _mapper.Map<List<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto dto)
        {
            if (dto.Poster == null)
                return BadRequest("Poster is required");
            if (!_supportedExtensionsImages.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("only .jpg or .png");
            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("max size must be in 1MB");
            var isValidGenre =  _genreService.GetByIdAsync( dto.GenreId);
            if (isValidGenre==null)
                return BadRequest("Invalid genre ID!");
            using var dataStream = new MemoryStream();
            await dto.Poster.CopyToAsync(dataStream);
           var data = _mapper.Map<Movie>(dto);
            await _movieService.AddAsync(data);
            return Ok(dto);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAsync([FromForm]MovieDto dto,byte id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound("not found movie");
            var isValidGenre = await _genreService.GetByIdAsync(dto.GenreId);
            if (isValidGenre == null)
                return BadRequest("Invalid genre ID!");

            if (dto.Poster != null)
            {
                if (!_supportedExtensionsImages.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("only .jpg or .png");
                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("max size must be in 1MB");
                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);
              
                movie = _mapper.Map<Movie>(dto);
                movie.Poster = dataStream.ToArray();
            }
            await _movieService.GetByGenreIdAsync(dto.GenreId);
            return Ok(movie);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie == null)
                return NotFound("not found movie");
            await _movieService.DeleteAsync(movie);
            return Ok(movie);
        }

    }
}
