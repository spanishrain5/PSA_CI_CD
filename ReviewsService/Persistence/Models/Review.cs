namespace ReviewsService.Persistence.Models;

public partial class Review
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string ReviewerName { get; set; } = null!;

    public string? Comment { get; set; }

    public int Rating { get; set; }
}
