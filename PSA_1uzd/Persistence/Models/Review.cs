
namespace PSA_1uzd.Persistence.Models
{
    public class Review
    {
        public required int BookId { get; set; }
        public string ReviewerName { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public string? Keyword { get; set; }
    }
}

//Scaffold-DbContext "Server=db19689.public.databaseasp.net; Database=db19689; User Id=db19689; Password=x@5HL3d?6r#T; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Persistence\Models -Context IdentityDbContext

