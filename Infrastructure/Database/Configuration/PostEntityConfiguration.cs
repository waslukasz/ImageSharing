using Application_Core.Model;
using Infrastructure.Database.Converter;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .HasOne(p => p.Status)
            .WithMany(s => s.Posts);
        builder
            .HasOne(p => p.Image)
            .WithOne(i => i.Post)
            .HasForeignKey<Post>(p => p.ImageId)
            .IsRequired();
        builder
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post);
        builder
            .HasMany(p => p.Reactions)
            .WithOne(r => r.Post);
        builder
            .HasOne(p => (User)p.User)
            .WithMany(u => u.Posts);
        builder.Property(c => c.Tags).HasConversion<TagConverter>();
        builder.ToTable("Posts");
        
    }
}