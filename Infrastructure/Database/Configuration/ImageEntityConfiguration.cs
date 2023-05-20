using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ImageEntityConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Ignore(i => i.Stream);
        builder
            .HasOne(i => (UserEntity)i.User)
            .WithMany(u => u.Images)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasOne(i => i.Post)
            .WithOne()
            .OnDelete(DeleteBehavior.ClientCascade);
        builder.
            HasOne(p => p.Thumbnail)
            .WithOne(i => i.Image)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasMany(i => i.Albums)
            .WithMany(a => a.Images);
        builder.ToTable("Images");
    }
}

