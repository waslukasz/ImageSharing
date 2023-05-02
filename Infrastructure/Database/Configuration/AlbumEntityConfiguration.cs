using Application_Core.Model;
using Infrastructure.Database.Entity;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class AlbumEntityConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property<int>("UserId");
        builder
            .HasOne(a => (User)a.User)
            .WithMany(u => u.Albums)
            .HasForeignKey("UserId")
            .IsRequired();
        builder
            .HasMany(a => a.Images)
            .WithMany(i => i.Albums)
            .UsingEntity<AlbumImage>().ToTable("AlbumImage");
        builder.ToTable("Albums");
    }
}