using Application_Core.Model;
using Infrastructure.Dto;

namespace WebAPI.Request;

public class UploadImageRequest : UploadFileRequest
{
    public string Title { get; set; }
}