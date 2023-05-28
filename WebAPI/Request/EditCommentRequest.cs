using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request
{
    public class EditCommentRequest
    {
        [Required]
        public string Text { get; set; }
    }
}
