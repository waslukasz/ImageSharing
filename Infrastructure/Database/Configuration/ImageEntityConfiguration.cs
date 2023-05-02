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
        builder
            .HasOne(i => (User)i.User)
            .WithMany(u => u.Images);
        builder
            .HasOne(i => i.Post)
            .WithOne()
            .HasForeignKey<Post>(p => p.ImageId);
        builder
            .HasMany(i => i.Albums)
            .WithMany(a => a.Images);
        builder.ToTable("Images");
    }
}

