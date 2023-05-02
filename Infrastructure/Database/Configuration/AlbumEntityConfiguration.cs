using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class AlbumEntityConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);
        builder
            .HasOne(a => (User)a.User)
            .WithMany(u => u.Albums);
        builder
            .HasMany(a => a.Images)
            .WithMany(i => i.Albums);
        builder.ToTable("Albums");
    }
}