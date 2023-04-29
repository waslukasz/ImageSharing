using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class CommentEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Comment>().HasKey(c => c.Id);
        builder.Entity<Comment>()
            .HasOne(c => (User)c.User)
            .WithMany(u => u.Comments);
        builder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments);
        builder.Entity<Comment>().ToTable("Comments");
    }
}