using Application_Core.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database
{

	public class ImageSharingDbContext : IdentityDbContext<IdentityUser>
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Post> Posts { get; set; }	
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Status>Statuses { get; set; }

		public ImageSharingDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

		}
	}
}
