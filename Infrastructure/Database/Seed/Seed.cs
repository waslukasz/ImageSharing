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
        ICollection<Image> images = posts.Select(p => p.Image).ToList();
        ICollection<Album> albums = DataGenerator.GenerateAlbumData(user, images).Generate(5);
        ICollection<Reaction> reactions = posts.SelectMany(p => DataGenerator.GenerateReactionData(users, p).Generate(5)).ToList();
        ICollection<Comment> comments = posts.SelectMany(p => DataGenerator.GenerateCommentData(users, p).Generate(10)).ToList();
        
        builder.Entity<Status>().HasData(status);
        builder.Entity<User>().HasData(users);
        builder.Entity<Post>().HasData(posts);
        //builder.Entity<Album>().HasData(albums);
        builder.Entity<Reaction>().HasData(reactions);
        builder.Entity<Comment>().HasData(comments);

    }
}