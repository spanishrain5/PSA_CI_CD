using Microsoft.EntityFrameworkCore;
using ReviewsService.Persistence.Models;

namespace ReviewsService.Persistence.Context;

public partial class BooksReviewsDbContext : DbContext
{
    public BooksReviewsDbContext(DbContextOptions<BooksReviewsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Review> Reviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC27442D8B54");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("Book_ID");
            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.ReviewerName).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
