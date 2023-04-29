using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class ReactionEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Reaction>().HasKey(r => r.Id);
        builder.Entity<Reaction>()
            .HasOne(r => (User)r.User)
            .WithMany(u => u.Reactions);
        builder.Entity<Reaction>()
            .HasOne(r => r.Post)
            .WithMany(p => p.Reactions);
        builder.Entity<Reaction>().ToTable("Reactions");
    }
}