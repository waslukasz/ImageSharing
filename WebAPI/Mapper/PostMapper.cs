using Application_Core.Model;
using Infrastructure.EF.Entity;
using WebAPI.Response;

namespace WebAPI.Mapper;

public static class PostMapper
{
    public static PostResponse FromPostToPostResponse(Post post)
    {
        return new PostResponse()
        {
            Id = post.Guid,
            Title = post.Title,
            Image = ImageMapper.FromImageToImageResponse(post.Image),
            Thumbnail = new ThumbnailResponse(),
            User = UserMapper.FromUserToUserResponse((UserEntity)post.User),
            Tags = post.Tags.ToList(),
            Status = post.Status.Name
        };
    }

    public static PostResponseWithDetails FromPostToPostResponseWithDetails(Post post)
    {
        return new PostResponseWithDetails()
        {
            Id = post.Guid,
            Title = post.Title,
            Image = ImageMapper.FromImageToImageResponse(post.Image),
            Thumbnail = new ThumbnailResponse(),
            User = UserMapper.FromUserToUserResponse((UserEntity)post.User),
            Tags = post.Tags.ToList(),
            Status = post.Status.Name,
            CommentCount = post.Comments.Count(),
            ReactionCount = post.Reactions.Count()
        };
    }
}