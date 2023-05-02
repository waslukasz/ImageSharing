using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed.Interface;

public interface ISeed
{
    void SeedData(ModelBuilder builder);
}