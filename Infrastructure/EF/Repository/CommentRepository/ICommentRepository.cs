using Application_Core.Model;
using Infrastructure.EF.Entity;
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
        IQueryable<Comment> GetAllCommentsQueryAsync(int postId);
    }
}