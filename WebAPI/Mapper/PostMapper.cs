using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using WebAPI.Request;
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

    public static FileDto FromCreatePostRequestToFileDto(CreatePostRequest request)
    {
        return new FileDto()
        {
            Length = request.Image.Length,
            Name = request.Image.FileName,
            Stream = request.Image.OpenReadStream(),
            Title = request.Title
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