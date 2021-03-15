using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octoller.PinBook.Web.Data.Model;

namespace Octoller.PinBook.Web.Data.Configurations
{
    public class PublishingHouseModelConfiguration : IEntityTypeConfiguration<PublishingHouse>
    {
        public void Configure(EntityTypeBuilder<PublishingHouse> builder)
        {
            builder.ToTable("PublishingHouses");
            builder.HasKey(ph => ph.Id);

            builder.Property(ph => ph.Id).ValueGeneratedOnAdd();
            builder.Property(ph => ph.Name).IsRequired().HasMaxLength(250);
            builder.Property(ph => ph.Description).HasMaxLength(1000);

            builder.Property(ph => ph.CreatedAt).IsRequired();
            builder.Property(ph => ph.CreatedBy).IsRequired().HasMaxLength(100);
            builder.Property(ph => ph.UpdatedAt);
            builder.Property(ph => ph.UpdatedBy).HasMaxLength(100);

            builder.HasMany(ph => ph.Books).WithOne(b => b.PublishingHouse);
        }
    }
}
