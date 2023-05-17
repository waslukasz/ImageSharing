using Application_Core.Model;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.EF.Repository.AlbumRepository;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.Manager;
using Infrastructure.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                cfg.AddProfile(new ImageSharingMappingProfile());
            }).CreateMapper()
            );

        }

    }
}
