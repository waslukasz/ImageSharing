using Infrastructure.EF.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed.Interface;

public interface ISeed
{
    void SeedData(ModelBuilder builder);
}