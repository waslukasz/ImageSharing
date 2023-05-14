using Application_Core.Model;
using Application_Core.Model.Interface;
using Bogus;
using Infrastructure.Database.Entity;
using Infrastructure.Database.Seed.Generator;
using Infrastructure.Database.Seed.Interface;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed;

public class Seed : ISeed
{
    private ICollection<Post> AssignImagesToPosts(ICollection<Post> posts, ICollection<Image> images)
    {
        if (posts.Count != images.Count)
            throw new ArgumentException();
        
        return posts.Select((p, current) => {
            p.ImageId = images.ElementAt(current).Id;
            return p;
        }).ToList() ;
    }

    private ICollection<AlbumImage> PopulateAlbumImagesJoinTable(ICollection<Album> albums, ICollection<Image> images)
    {
        Random random = new Random();

        return albums.SelectMany(a =>
            Enumerable.Range(0, random.Next(0, images.Count - 1))
                .Select(i => new AlbumImage() { AlbumId = a.Id, ImageId = images.ElementAt(i).Id })).ToList();
    }

    public void SeedData(ModelBuilder builder)
    {
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

        int postsCount = 20;

        ICollection<User> users = DataGenerator.GenerateDummyUserData().Generate(5);
        ICollection<IUser<int>> iUsers = users.Cast<IUser<int>>().ToList();
        ICollection<Status> status = DataGenerator.GenerateStatusData().Generate(3);
        ICollection<Post> posts = DataGenerator.GeneratePostData(users.First(), status).Generate(postsCount);
        ICollection<Image> images = DataGenerator.GenerateImageData(users.First()).Generate(postsCount);
        ICollection<Album> albums = DataGenerator.GenerateAlbumData(users.First()).Generate(5);
        
        Faker.GlobalUniqueIndex = 0; // IMPORTANT !
        ICollection<Reaction> reactions = posts.SelectMany(p => DataGenerator.GenerateReactionData(iUsers, p).Generate(5)).ToList();
        
        Faker.GlobalUniqueIndex = 0; // IMPORTANT !
        ICollection<Comment> comments = posts.SelectMany(p => DataGenerator.GenerateCommentData(iUsers, p).Generate(10)).ToList();

        ICollection<Post> postsWithImages = this.AssignImagesToPosts(posts, images);
        ICollection<AlbumImage> albumImagesJoinTable = this.PopulateAlbumImagesJoinTable(albums, images);

        builder.Entity<Status>().HasData(status);
        builder.Entity<User>().HasData(users);
        builder.Entity<Image>().HasData(images);
        builder.Entity<Post>().HasData(postsWithImages);
        builder.Entity<Album>().HasData(albums);
        builder.Entity<AlbumImage>().HasData(albumImagesJoinTable);
        builder.Entity<Reaction>().HasData(reactions);
        builder.Entity<Comment>().HasData(comments);

    }
}