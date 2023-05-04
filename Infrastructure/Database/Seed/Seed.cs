using Application_Core.Model;
using Application_Core.Model.Interface;
using Bogus;
using Infrastructure.Database.Seed.Generator;
using Infrastructure.Database.Seed.Interface;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seed;

public class Seed : ISeed
{
    public ICollection<Post> AssignImagesToPosts(ICollection<Post> posts, ICollection<Image> images)
    {
        if (posts.Count != images.Count)
            throw new ArgumentException();

        for (int i = 0; i < posts.Count; i++)
        {
            posts.ElementAt(i).ImageId = images.ElementAt(i).Id;
        }

        return posts;
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

        List<IUser<int>> users = new List<IUser<int>>();
        users.Add(user);
        
        ICollection<Status> status = DataGenerator.GenerateStatusData().Generate(3);
        ICollection<Post> posts = DataGenerator.GeneratePostData(user, status).Generate(20);
        ICollection<Image> images = DataGenerator.GenerateImageData(user).Generate(20);
        //ICollection<Album> albums = DataGenerator.GenerateAlbumData(user, images).Generate(5);
        ICollection<Reaction> reactions = posts.SelectMany(p => DataGenerator.GenerateReactionData(users, p).Generate(5)).ToList();
        ICollection<Comment> comments = posts.SelectMany(p => DataGenerator.GenerateCommentData(users, p).Generate(10)).ToList();

        ICollection<Post> postsWithImages = AssignImagesToPosts(posts, images);

        builder.Entity<Status>().HasData(status);
        builder.Entity<User>().HasData(users);
        builder.Entity<Image>().HasData(images);
        builder.Entity<Post>().HasData(postsWithImages);
        //builder.Entity<Album>().HasData(albums);
        builder.Entity<Reaction>().HasData(reactions);
        builder.Entity<Comment>().HasData(comments);

    }
}