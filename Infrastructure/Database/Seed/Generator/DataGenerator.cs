using System.Collections;
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
        _imageId = 1,
        _reactionId = 1,
        _commentId = 1;

    public static Faker<Status> GenerateStatusData()
    {
        return new Faker<Status>()
            .RuleFor(s => s.Id, f => _statusId++)
            .RuleFor(s => s.Name, f => f.PickRandom<StatusEnum>().ToString());
    }

    public static Faker<Album> GenerateAlbumData(IUser<int> user, ICollection<Image> images)
    {

        return new Faker<Album>()
            .RuleFor(a => a.Id, f => _albumId++)
            .RuleFor(a => a.Guid, Guid.NewGuid)
            .RuleFor(a => a.Description, f => f.Lorem.Lines(3))
            .RuleFor(a => a.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(a => a.Images, GenerateRandomImagesForAlbum(images, new Random().Next(1,10)))
            .RuleFor(a => a.User, user);
    }

    public static Faker<Post> GeneratePostData(IUser<int> user, ICollection<Status> statusCollection)
    {
        Image image = GenerateImageData(user).Generate();
        
        return new Faker<Post>()
            .RuleFor(p => p.Id, f => _postId++)
            .RuleFor(p => p.Guid, Guid.NewGuid)
            .RuleFor(p => p.Title, f => string.Join(' ', f.Lorem.Words(4)))
            .RuleFor(p => p.Tags, f => f.Random.WordsArray(3))
            .RuleFor(p => p.ImageId, image.Id)
            .RuleFor(p => p.Image, image)
            .RuleFor(p => p.Status, f => f.PickRandom(statusCollection))
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

    public static Faker<Reaction> GenerateReactionData(ICollection<IUser<int>> users , Post post)
    {
        return new Faker<Reaction>()
            .RuleFor(r => r.Id, f => _reactionId++)
            .RuleFor(r => r.Guid, Guid.NewGuid)
            .RuleFor(r => r.Post, post)
            .RuleFor(r => r.User, f => f.PickRandom(users));
    }

    public static Faker<Comment> GenerateCommentData(ICollection<IUser<int>> users, Post post)
    {
        return new Faker<Comment>()
            .RuleFor(c => c.Id, f => _commentId++)
            .RuleFor(c => c.Guid, Guid.NewGuid)
            .RuleFor(c => c.Post, post)
            .RuleFor(c => c.User, f => f.PickRandom(users))
            .RuleFor(c => c.Text, f => f.Lorem.Lines(2));
    }

    private static ICollection<Image> GenerateRandomImagesForAlbum(ICollection<Image> images, int number)
    {
        if (number > images.Count() || number < 1)
            number = images.Count();

        Faker faker = new Faker();
        List<Image> imgs = images.ToList();
        List<Image> outImgs = new List<Image>();

        Enumerable.Range(1, number).Select(i =>
        {
            Image img = faker.PickRandom(imgs);
            imgs.Remove(img);
            outImgs.Add(img);
            return i;
        });

        return outImgs;
    }
}