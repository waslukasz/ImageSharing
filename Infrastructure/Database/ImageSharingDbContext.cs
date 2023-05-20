using Application_Core.Model;
using Infrastructure.Database.Configuration;
using Infrastructure.Database.Seed.Interface;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EF.Entity;
using Infrastructure.EventListener;

namespace Infrastructure.Database
{

	public class ImageSharingDbContext : IdentityDbContext<UserEntity, RoleEntity, int>
	{
		public DbSet<Album> Albums { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Image> Images { get; set; }
		public DbSet<Thumbnail> Thumbnails { get; set; }
		public DbSet<Post> Posts { get; set; }	
		public DbSet<Reaction> Reactions { get; set; }
		public DbSet<Status> Statuses { get; set; }
		
		// uncomment when manipulating data from infrastructure project !
		public ImageSharingDbContext()
		{
			
		}

		public ImageSharingDbContext(DbContextOptions options, ImageEntityEventListener listener) : base(options)
		{
			ChangeTracker.Tracked += listener.OnImageCreate;
			ChangeTracker.StateChanged += listener.OnImageDelete;
			ChangeTracker.StateChanged += listener.OnImageUpdate;
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			
			// MOJE BAGNO !!!111 proszę nie ruszać a jak już to odkomentować. Dziękuje ~ Michaś
			optionsBuilder.UseSqlServer(
				"Server=DESKTOP-7J9U791;Database=ImageSharing;TrustServerCertificate=true;Integrated Security=true"
			);

			 // optionsBuilder.UseSqlServer(
			 // 	"Server=(localdb)\\MSSQLLocalDB;Database=ImageSharing;TrustServerCertificate=true;Integrated Security=true"
			 // );
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
			builder.ApplyConfiguration(new ThumbnailEntityConfiguration());

			ISeed seed = new Seed.Seed();
			seed.SeedData(builder);
		}
	}
}
