using Application_Core.Model;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class ReactionEntityConfiguration : IEntityTypeConfiguration<Reaction>
{
    public void Configure(EntityTypeBuilder<Reaction> builder)
    {
        builder.HasKey(r => r.Id);
        builder
            .HasOne(r => (UserEntity)r.User)
            .WithMany(u => u.Reactions);
        builder
            .HasOne(r => r.Post)
            .WithMany(p => p.Reactions);
        builder.ToTable("Reactions");
    }
    
}