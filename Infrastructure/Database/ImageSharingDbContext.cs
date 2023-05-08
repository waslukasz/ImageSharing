using Application_Core.Model;
using Infrastructure.Database.Configuration;
using Infrastructure.Database.Seed.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Database.Seed;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database
{

	public class ImageSharingDbContext : IdentityDbContext<User,IdentityRole<int>,int>
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Post> Posts { get; set; }	
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public ImageSharingDbContext(DbContextOptions options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			//optionsBuilder.UseSqlite("Data Source=app.db");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new AlbumEntityConfiguration());
			builder.ApplyConfiguration(new CommentEntityConfiguration());
			builder.ApplyConfiguration(new ImageEntityConfiguration());
			builder.ApplyConfiguration(new PostEntityConfiguration());
			builder.ApplyConfiguration(new ReactionEntityConfiguration());
			builder.ApplyConfiguration(new StatusEntityConfiguration());

			ISeed seed = new Seed.Seed();
			seed.SeedData(builder);
		}
	}
}
