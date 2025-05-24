namespace SearchService.Persistence.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;
    public string Genre { get; set; } = null!;
}
