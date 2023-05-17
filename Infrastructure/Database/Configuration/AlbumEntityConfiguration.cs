using Application_Core.Model;
using Azure;
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
            .HasOne(a => (UserEntity)a.User)
            .WithMany(u => u.Albums)
            .OnDelete(DeleteBehavior.ClientCascade )
            .HasForeignKey("UserId")
            .IsRequired();
        builder
            .HasMany(a => a.Images)
            .WithMany(i => i.Albums)
            .UsingEntity<AlbumImage>(
                l => l.HasOne<Image>().WithMany().HasForeignKey(i => i.ImageId),
                r => r.HasOne<Album>().WithMany().HasForeignKey(i => i.AlbumId),
                j => j.HasKey(albumImage => new { aid = albumImage.AlbumId, iid = albumImage.ImageId })
                );



        builder.ToTable("Albums");
    }
}