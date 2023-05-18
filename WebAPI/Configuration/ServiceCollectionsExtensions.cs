using Infrastructure.Database;
using Infrastructure.EF.Repository.PostRepository;
using Microsoft.EntityFrameworkCore;
using WebAPI.Managers;

namespace WebAPI.Configuration
{
	public static class ServiceCollectionsExtensions
	{
		public static void AddInfrastructures(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddDbContext<ImageSharingDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);
            services.AddScoped<IPostRepository, PostRepository>();
		}
	}
}
