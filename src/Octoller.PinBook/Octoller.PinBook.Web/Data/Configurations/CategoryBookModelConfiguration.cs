using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data.Configurations
{
    public class CategoryBookModelConfiguration : IEntityTypeConfiguration<CategoryBook>
    {
        public void Configure(EntityTypeBuilder<CategoryBook> builder)
        {
            builder.ToTable("CategoryBooks");
            builder.HasKey(cb => cb.Id);

            builder.Property(cb => cb.Id).ValueGeneratedOnAdd();
            builder.Property(cb => cb.Name).IsRequired().HasMaxLength(250);
            builder.Property(cb => cb.Description).HasMaxLength(1000);

            builder.Property(cb => cb.CreatedAt).IsRequired();
            builder.Property(cb => cb.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(cb => cb.UpdatedAt);
            builder.Property(cb => cb.UpdatedBy).HasMaxLength(100);

            builder.HasMany(cb => cb.Books).WithMany(b => b.Categories);
        }
    }
}
