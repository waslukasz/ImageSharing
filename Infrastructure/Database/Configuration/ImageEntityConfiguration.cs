using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class ImageEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Image>().HasKey(i => i.Id);
        builder.Entity<Image>()
            .HasOne(i => (User)i.User)
            .WithMany(u => u.Images);
        builder.Entity<Image>()
            .HasOne(i => i.Post)
            .WithOne(p => p.Image);
        builder.Entity<Image>()
            .HasMany(i => i.Albums)
            .WithMany(a => a.Images);
        builder.Entity<Image>().ToTable("Images");
    }
}