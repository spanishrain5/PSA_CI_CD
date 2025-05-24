using Microsoft.EntityFrameworkCore;
using SearchService.Persistence.Models;

namespace SearchService.Persistence.Context;

public partial class BooksSearchDbContext : DbContext
{
    public BooksSearchDbContext()
    {
    }

    public BooksSearchDbContext(DbContextOptions<BooksSearchDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC27632D78AF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(128);
            entity.Property(e => e.Title).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
