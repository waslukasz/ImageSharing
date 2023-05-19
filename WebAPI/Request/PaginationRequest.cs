using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Request;

public class PaginationRequest
{
    [Range(1,Int32.MaxValue)]
    public int Page { get; set; } = 1;
    
    [Range(1,50)]
    public int ItemNumber { get; set; } = 5;
}
