using Application_Core.Model;
using Application_Core.Model.Interface;
using Bogus;
using Infrastructure.EF.Entity;

namespace Infrastructure.Database.Seed.Generator;

public class DataGenerator
{
    private static int 
        _statusId = 1,
        _albumId = 1,
        _postId = 1, 
        _imageId = 1;

    public static Faker<Status> GenerateStatusData()
    {
        return new Faker<Status>()
            .RuleFor(s => s.Id, f => _statusId++)
            .RuleFor(s => s.Name, f => f.PickRandom<StatusEnum>().ToString());
    }

    public static Faker<Album> GenerateAlbumData(IUser user)
    {
        return new Faker<Album>()
            .RuleFor(a => a.Id, f => _albumId++)
            .RuleFor(a => a.Guid, Guid.NewGuid)
            .RuleFor(a => a.Description, f => f.Lorem.Lines(3))
            .RuleFor(a => a.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(a => a.User, user);
    }

    public static Faker<Post> GeneratePostData(IUser<int> user)
    {
        Image image = GenerateImageData(user).Generate();
        
        return new Faker<Post>()
            .RuleFor(p => p.Id, f => _postId++)
            .RuleFor(p => p.Guid, Guid.NewGuid)
            .RuleFor(p => p.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(p => p.Tags, f => f.Random.WordsArray(3))
            .RuleFor(p => p.ImageId, image.Id)
            .RuleFor(p => p.Image, image)
            .RuleFor(p => p.Status, GenerateStatusData().Generate())
            .RuleFor(p => p.User, user);
    }

    public static Faker<Image> GenerateImageData(IUser<int> user)
    {
        return new Faker<Image>()
            .RuleFor(i => i.Id, f => _imageId++)
            .RuleFor(i => i.Guid, Guid.NewGuid)
            .RuleFor(i => i.Title, f => string.Join(' ', f.Lorem.Words()))
            .RuleFor(i => i.Slug, string.Empty)
            .RuleFor(i => i.User, user);
    }
}