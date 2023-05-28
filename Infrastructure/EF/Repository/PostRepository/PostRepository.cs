using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Application_Core.Common.Specification;
using Infrastructure.Database.Configuration;
using Infrastructure.EF.Evaluator;
using Microsoft.Data.SqlClient;

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

        public IQueryable<Post> GetByTagsQuery(List<string> tags, ISpecification<Post> specification)
        {
            string parameterName = "tag";
            string columnName = nameof(Post.Tags);
            
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string query = @$"SELECT * FROM [{PostEntityConfiguration.TableName}]";

            for (int i = 0; i < tags.Count; i++) {
                
                if (i == 0) {
                    query += $" WHERE {columnName} LIKE @{parameterName}{i}";
                } else {
                    query += $" OR {columnName} LIKE @{parameterName}{i}";
                }
                
                sqlParameters.Add(
                    new SqlParameter($"{parameterName}{i}",$"%{tags[i]}%")
                    );
            }

            return SpecificationToQueryEvaluator<Post>.ApplySpecification(
                Context.Posts.FromSqlRaw(query,sqlParameters.ToArray()).AsNoTracking(),specification
                );
        }
    }
}
