using Application_Core.Model;
using Application_Core.Model.Interface;
using Application_Core.Tool;
using Infrastructure.Database.Configuration;
using Infrastructure.Database.Seed.Generator;
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

			builder.ApplyConfiguration(new AlbumEntityConfiguration());
			builder.ApplyConfiguration(new CommentEntityConfiguration());
			builder.ApplyConfiguration(new ImageEntityConfiguration());
			builder.ApplyConfiguration(new PostEntityConfiguration());
			builder.ApplyConfiguration(new ReactionEntityConfiguration());
			builder.ApplyConfiguration(new StatusEntityConfiguration());

			IUser<int> user = new User()
			{
				Id = 1,
				Guid = Guid.NewGuid(),
				UserName = "TEST",
				AccessFailedCount = 0,
				LockoutEnabled = false,
				EmailConfirmed = true,
				PhoneNumberConfirmed = true,
				TwoFactorEnabled = false,
			};

			List<Post> postList = DataGenerator.GeneratePostData(user).Generate(5);
			
			Console.WriteLine("-------------------------------");
			Console.WriteLine(ObjectDumper.Dump(postList.First()));
			Console.WriteLine("-------------------------------");
			
			builder.Entity<User>().HasData(
				user
				);

			builder.Entity<Post>().HasData(
				postList
				);

		}
	}
}
