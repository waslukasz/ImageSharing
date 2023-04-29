using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Configuration;

public interface IEntityConfiguration
{
    void Configure(ModelBuilder builder);
}