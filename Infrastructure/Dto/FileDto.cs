using Application_Core.Model;
using Application_Core.Model.Interface;
using Infrastructure.Utility;

namespace Infrastructure.Dto;

public class FileDto
{
    public long Length { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }
    
    public Stream Stream { get; set; }

    public Image ToImage(ISlugCreator creator)
    {
        return new Image()
        {
            Extension = Path.GetExtension(Name),
            Stream = Stream,
            Title = Title,
            Slug = creator.CreateSlug(Title,Name)
        };
    }
}