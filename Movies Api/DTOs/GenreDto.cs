namespace Movies_Api.DTOs
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
