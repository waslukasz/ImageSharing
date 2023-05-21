using Application_Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.CommentRepository
{
    public interface ICommentRepository : IRepositoryBase<Comment>
    {
        Task EditCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsAsync(int postId);
        Task<Comment?> GetCommentByGuIdAsync(Guid guId);
    }
}
