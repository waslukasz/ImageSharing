using Application_Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration;

public class StatusEntityConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(s => s.Id);
        builder
            .HasMany(s => s.Posts)
            .WithOne(p => p.Status)
            .OnDelete(DeleteBehavior.ClientCascade);
        builder.ToTable("Status");
    }
}