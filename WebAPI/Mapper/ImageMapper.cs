using Infrastructure.Dto;
using WebAPI.Request;

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
}