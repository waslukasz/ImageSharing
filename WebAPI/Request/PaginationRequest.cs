using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Request;

public class PaginationRequest
{
    [FromQuery(Name = "page")] 
    [Range(1,Int32.MaxValue)]
    public int page { get; set; } = 1;

    [FromQuery(Name = "itemNumber")]
    [Range(1,50)]
    public int itemNumber { get; set; } = 5;
}
