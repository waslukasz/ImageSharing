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

			ConfigurationExecuter configurationExecuter = new ConfigurationExecuter(builder);
			
			configurationExecuter.AddConfiguration(new AlbumEntityConfiguration());
			configurationExecuter.AddConfiguration(new CommentEntityConfiguration());
			configurationExecuter.AddConfiguration(new ImageEntityConfiguration());
			configurationExecuter.AddConfiguration(new PostEntityConfiguration());
			configurationExecuter.AddConfiguration(new StatusEntityConfiguration());
			configurationExecuter.AddConfiguration(new ReactionEntityConfiguration());
			
			configurationExecuter.Execute();

		}
	}
}
