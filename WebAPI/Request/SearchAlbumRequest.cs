using System.ComponentModel.DataAnnotations;
using Application_Core.Common.Specification;
using Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Request;

public class SearchAlbumRequest : PaginationRequest, IValidatableObject
{
    public string? AlbumTitle { get; set; }
    
    public int? MaxImages { get; set; }
    
    public int? MinImages { get; set; }
    
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