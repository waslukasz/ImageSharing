using Application_Core.Model;
using Infrastructure.Database.Converter;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public const string TableName = "Posts";
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);
        builder
            .HasOne(p => p.Status)
            .WithMany(s => s.Posts)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasOne(p => p.Image)
            .WithOne(i => i.Post)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .OnDelete(DeleteBehavior.Cascade)
            .HasForeignKey(c => c.PostId);
        builder
            .HasMany(p => p.Reactions)
            .WithOne(r => r.Post);
        builder
            .HasOne(p => (UserEntity)p.User)
            .WithMany(u => u.Posts)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder.Property(c => c.Tags).HasConversion<TagConverter>();
        builder.ToTable(TableName);
    }
}