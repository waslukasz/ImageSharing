using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Common.Specification;
using Infrastructure.EF.Evaluator;

namespace Infrastructure.EF.Repository.PostRepository
{
    public class PostRepository : BaseRepository<Post,int>, IPostRepository
    {
        
        public PostRepository(ImageSharingDbContext context) : base(context)
        {
            
        }
        
        public IQueryable<Post> GetAllQuery()
        {
            return Context.Posts;
        }

        public async Task UpdateAsync(Post post)
        {
            Context.Posts.Update(post);
            await Context.SaveChangesAsync();
        }

       
    }
}
