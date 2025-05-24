namespace PSA_1uzd.Persistence.Models
{
    public class BookResult
    {
        public required int ID { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string Genre { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
