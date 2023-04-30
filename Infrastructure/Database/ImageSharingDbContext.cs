using Application_Core.Model;
using Infrastructure.Database.Configuration;
using Infrastructure.Database.Configuration.Executer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EF.Entity;

namespace Infrastructure.Database
{

	public class ImageSharingDbContext : IdentityDbContext<User>
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Post> Posts { get; set; }	
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Status> Statuses { get; set; }
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=app.db");
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			EntityConfigurator entityConfigurator = new EntityConfigurator(builder);
			
			entityConfigurator.AddConfiguration(new AlbumEntityConfiguration());
			entityConfigurator.AddConfiguration(new CommentEntityConfiguration());
			entityConfigurator.AddConfiguration(new ImageEntityConfiguration());
			entityConfigurator.AddConfiguration(new PostEntityConfiguration());
			entityConfigurator.AddConfiguration(new StatusEntityConfiguration());
			entityConfigurator.AddConfiguration(new ReactionEntityConfiguration());
			
			entityConfigurator.Configure();

		}
	}
}
