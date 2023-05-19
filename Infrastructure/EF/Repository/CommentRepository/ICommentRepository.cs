using Application_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.CommentRepository
{
    public interface ICommentRepository
    {
        Task<Guid> AddCommentAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task EditCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsAsync(int postId);
        Task<Comment> GetCommentByGuIdAsync(Guid guId);
    }
}
