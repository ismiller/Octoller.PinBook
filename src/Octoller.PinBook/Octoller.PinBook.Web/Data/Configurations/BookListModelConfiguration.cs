using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data.Configurations
{
    public class BookListModelConfiguration : IEntityTypeConfiguration<BookList>
    {
        public void Configure(EntityTypeBuilder<BookList> builder)
        {
            builder.ToTable("BookLists");
            builder.HasKey(bl => bl.Id);
            builder.HasAlternateKey(bl => bl.UserId);

            builder.Property(bl => bl.Id).ValueGeneratedOnAdd();
            builder.Property(bl => bl.Name).IsRequired().HasMaxLength(250);
            builder.Property(bl => bl.Description).HasMaxLength(1000).HasDefaultValue("<none>");

            builder.Property(bl => bl.CreatedAt).IsRequired();
            builder.Property(bl => bl.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(bl => bl.UpdatedAt);
            builder.Property(bl => bl.UpdatedBy).HasMaxLength(100);

            builder.HasMany(bl => bl.Books).WithMany(b => b.BookLists);
            builder.HasOne(bl => bl.User).WithMany(u => u.BookLists);
        }
    }
}
