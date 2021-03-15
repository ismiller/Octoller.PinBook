using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data.Configurations
{
    public class BookModelConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.Id);
            builder.HasAlternateKey(b => b.ISBN);

            builder.Property(b => b.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Name).IsRequired().HasMaxLength(250);
            builder.Property(b => b.Description).HasMaxLength(1000).HasDefaultValue("<none>");
            builder.Property(b => b.ISBN).IsRequired().HasMaxLength(250);
            builder.Property(b => b.NumberOfPages).IsRequired();
            builder.Property(b => b.Image).HasColumnType("varbinary(max)");

            builder.Property(b => b.CreatedAt).IsRequired();
            builder.Property(b => b.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(b => b.UpdatedAt);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);

            builder.HasMany(b => b.Authors).WithMany(a => a.Books);
            builder.HasMany(b => b.BookLists).WithMany(bl => bl.Books);
            builder.HasMany(b => b.Categories).WithMany(c => c.Books);
        }
    }
}
