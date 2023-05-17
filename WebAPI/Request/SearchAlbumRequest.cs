using System.ComponentModel.DataAnnotations;
using Application_Core.Common.Specification;
using Infrastructure.Manager.Param;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Request;

public class SearchAlbumRequest : IValidatableObject
{
    [FromQuery(Name = "title")]
    public string? AlbumTitle { get; set; }

    [FromQuery(Name = "maxImages")]
    public int? MaxImages { get; set; }
    
    [FromQuery(Name = "minImages")]
    public int? MinImages { get; set; }

    [FromQuery(Name = "orderBy")]
    public OrderBy OrderBy { get; set; } = OrderBy.Desc;

    public AlbumSearchDto ToParam()
    {
        return new AlbumSearchDto()
        {
            AlbumTitle = AlbumTitle,
            MinImages = MinImages,
            MaxImages = MaxImages,
            OrderBy = OrderBy
        };
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (MaxImages is not null && MinImages is not null)
        {
            if (MaxImages < MinImages)
            {
                yield return new ValidationResult($"{nameof(MaxImages)} should't be smaller than {nameof(MinImages)}");
            }

            if (MinImages > MaxImages)
            {
                yield return new ValidationResult($"{nameof(MinImages)} should't be larger than {nameof(MaxImages)}");
            }
        }

    }
}