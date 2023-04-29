using Application_Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public class StatusEntityConfiguration : IEntityConfiguration
{
    public void Configure(ModelBuilder builder)
    {
        builder.Entity<Status>().HasKey(s => s.Name);
        builder.Entity<Status>()
            .HasMany(s => s.Posts)
            .WithOne(p => p.Status);
        builder.Entity<Status>().ToTable("Status");
    }
}