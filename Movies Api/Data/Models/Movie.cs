

namespace Movies_Api.Data.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string  StoreLine { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }


        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }
    }
}
