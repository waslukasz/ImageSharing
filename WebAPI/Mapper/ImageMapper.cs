using Application_Core.Model;
using Infrastructure.Dto;
using WebAPI.Request;
using WebAPI.Response;

namespace WebAPI.Mapper;

public class ImageMapper
{
    public static FileDto FromRequestToFileDto(UploadImageRequest request)
    {
        return new FileDto()
        {
            Title = request.Title,
            Name = request.File.FileName,
            Length = request.File.Length,
            Stream = request.File.OpenReadStream()
        };
    }

    public static ImageDto FromRequestToImageDto(UpdateImageRequest request)
    {
        return new ImageDto()
        {
            Guid = request.Id,
            Title = request.Title,
            Name = request.File.FileName,
            Stream = request.File.OpenReadStream(),
            Length = request.File.Length
        };
    }

    public static ImageResponse FromImageToImageResponse(Image image)
    {
        return new ImageResponse()
        {
            Id = image.Guid,
            Name = image.Slug,
            Extension = image.Extension,
        };
    }
}