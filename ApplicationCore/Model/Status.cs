using System.ComponentModel.DataAnnotations;

namespace Application_Core.Model;

public class Status
{
    [Key]
    public string Name { get; set; }
    public Post Post { get; set; }
}