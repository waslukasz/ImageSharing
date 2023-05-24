using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.CommentRepository
{
    public class CommentRepository : BaseRepository<Comment,int>, ICommentRepository
    {
        public CommentRepository(ImageSharingDbContext context) : base(context)
        {
            
        }

        public async Task EditCommentAsync(Comment comment)
        {
            Context.Update(comment);
            await Context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync(int postId)
        {
            return await Context.Comments
                .Where(i=>i.PostId==postId)
                .Include(p=>p.Post)
                .Include(u=>u.User)
                .ToListAsync();
        }

        public IQueryable<Comment> GetAllCommentsQueryAsync(int postId)
        {
            return Context.Comments
                .Where(i => i.PostId == postId)
                .Include(p => p.Post)
                .Include(u => u.User);
        }
    }
}
