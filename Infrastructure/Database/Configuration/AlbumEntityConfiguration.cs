using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class AlbumEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Album>().HasKey(a => a.Id);
        builder.Entity<Album>()
            .HasOne(a => (User)a.User)
            .WithMany(u => u.Albums);
        builder.Entity<Album>()
            .HasMany(a => a.Images)
            .WithMany(i => i.Albums);
        builder.Entity<Album>().ToTable("Albums");
    }
}