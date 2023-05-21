using Application_Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ThumbnailEntityConfiguration : IEntityTypeConfiguration<Thumbnail>
{
    public void Configure(EntityTypeBuilder<Thumbnail> builder)
    {
        builder.HasKey(i => i.Id);
        builder
            .HasOne(i => i.Image)
            .WithOne(i => i.Thumbnail)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder.ToTable("Thumbnails");
    }
}