using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class PostEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Post>().HasKey(p => p.Id);
        builder.Entity<Post>()
            .HasOne(p => p.Status)
            .WithMany(s => s.Posts);
        builder.Entity<Post>()
            .HasOne(p => p.Image)
            .WithOne(i => i.Post)
            .HasForeignKey<Post>(p => p.ImageId)
            .IsRequired();
        builder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post);
        builder.Entity<Post>()
            .HasMany(p => p.Reactions)
            .WithOne(r => r.Post);
        builder.Entity<Post>()
            .HasOne(p => (User)p.User)
            .WithMany(u => u.Posts);
        builder.Entity<Post>().ToTable("Posts");
    }
}