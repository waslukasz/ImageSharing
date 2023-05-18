using Infrastructure.Database;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension
{
	public static class ServiceCollectionsExtensions
	{
		public static void AddInfrastructures(this IServiceCollection services, IConfiguration Configuration)
		{
			services.AddDbContext<ImageSharingDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);
            services.AddScoped<PostManager>();
            services.AddScoped<IPostRepository, PostRepository>();
		}
	}
}
