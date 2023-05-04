using Application_Core.Model;
using Infrastructure.Database;
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

			services.AddDbContext<ImageSharingDbContext>(options => options.UseSqlServer(
				   Configuration.GetConnectionString("DefaultConnection")));
			//TODO Identity
		}

	}
}
