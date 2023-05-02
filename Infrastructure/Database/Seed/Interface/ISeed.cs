using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed.Interface;

public interface ISeed
{
    void Seed(ModelBuilder builder);
}