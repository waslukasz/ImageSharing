using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request
{
    public class EditCommentRequest
    {
        [Required]
        public Guid CommentGuId { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
