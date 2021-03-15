using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data.Configurations
{
    public class AuthorModelConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Name).IsRequired().HasMaxLength(250);
            builder.Property(a => a.Description).HasMaxLength(1000).HasDefaultValue("<none>");

            builder.Property(a => a.CreatedAt).IsRequired();
            builder.Property(a => a.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(a => a.UpdatedAt).IsRequired();
            builder.Property(a => a.UpdatedBy).IsRequired().HasMaxLength(100);

            builder.HasMany(a => a.Books).WithMany(b => b.Authors);
        }
    }
}
