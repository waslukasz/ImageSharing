using System.Collections;
using Application_Core.Model;
using Application_Core.Model.Interface;
using Bogus;
using Infrastructure.Database.Entity;
using Infrastructure.EF.Entity;

namespace Infrastructure.Database.Seed.Generator;

public class DataGenerator
{
    public static Faker<UserEntity> GenerateDummyUserData()
    {
        int id = 1;
        
        return new Faker<UserEntity>()
            .RuleFor(u => u.Id, f => (f.IndexFaker)+1)
            .RuleFor(u => u.Guid, Guid.NewGuid)
            .RuleFor(u => u.UserName, f => f.Name.FirstName())
            .RuleFor(u => u.AccessFailedCount, 0)
            .RuleFor(u => u.LockoutEnabled, false)
            .RuleFor(u => u.EmailConfirmed, true)
            .RuleFor(u => u.PhoneNumberConfirmed, true)
            .RuleFor(u => u.TwoFactorEnabled, false);
    }
    
    /*public static Faker<Status> GenerateStatusData()
    {
        int id = 1;
        
        return new Faker<Status>()
            .RuleFor(s => s.Id, f => (f.IndexFaker)+1)
            .RuleFor(s => s.Name, f => f.PickRandom<StatusEnum>().ToString());
    }*/

    public static Faker<Album> GenerateAlbumData(IUser<int> user)
    {
        int id = 1;
        
        return new Faker<Album>()
            .RuleFor(a => a.Id, f => (f.IndexFaker)+1)
            .RuleFor(a => a.Guid, Guid.NewGuid)
            .RuleFor(a => a.Description, f => f.Lorem.Lines(3))
            .RuleFor(a => a.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(a => a.UserId, user.Id);
    }

    public static Faker<Post> GeneratePostData(IUser<int> user, ICollection<Status> statusCollection)
    {
        int id = 1;
        
        return new Faker<Post>()
            .RuleFor(p => p.Id, f => (f.IndexFaker)+1)
            .RuleFor(p => p.Guid, Guid.NewGuid)
            .RuleFor(p => p.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(p => p.Tags, f => f.Random.WordsArray(3))
            .RuleFor(p => p.StatusId, f => f.PickRandom(statusCollection).Id)
            .RuleFor(p => p.UserId, user.Id);
    }

    public static Faker<Image> GenerateImageData(IUser<int> user)
    {
        int id = 1;
        
        return new Faker<Image>()
            .RuleFor(i => i.Id, f => (f.IndexFaker)+1)
            .RuleFor(i => i.Guid, Guid.NewGuid)
            .RuleFor(i => i.Extension, ".jpg")
            .RuleFor(i => i.Title, f => string.Join(' ', f.Lorem.Words()))
            .RuleFor(i => i.Slug, string.Empty)
            .RuleFor(i => i.UserId, user.Id);
    }
    
    public static Faker<Thumbnail> GenerateThumbnailData()
    {
        int id = 1;

        return new Faker<Thumbnail>()
            .RuleFor(i => i.Id, f => (f.IndexFaker) + 1)
            .RuleFor(i => i.Guid, Guid.NewGuid)
            .RuleFor(i => i.Name, f => string.Join(' ', f.Lorem.Words()));
    }

    public static Faker<Reaction> GenerateReactionData(ICollection<IUser<int>> users , Post post)
    {
        return new Faker<Reaction>()
            .RuleFor(r => r.Id, f => f.IndexGlobal)
            .RuleFor(r => r.Guid, Guid.NewGuid)
            .RuleFor(r => r.PostId, post.Id)
            .RuleFor(r => r.UserId, f => f.PickRandom(users).Id);
    }

    public static Faker<Comment> GenerateCommentData(ICollection<IUser<int>> users, Post post)
    {
        return new Faker<Comment>()
            .RuleFor(c => c.Id, f => f.IndexGlobal)
            .RuleFor(c => c.Guid, Guid.NewGuid)
            .RuleFor(c => c.PostId, post.Id)
            .RuleFor(c => c.UserId, f => f.PickRandom(users).Id)
            .RuleFor(c => c.Text, f => f.Lorem.Lines(2));
    }
}