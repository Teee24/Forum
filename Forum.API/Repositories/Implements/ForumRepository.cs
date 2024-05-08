using Dapper;
using Forum.API.Domain.Entity;
using Forum.API.Infrastructures.Database;
using Forum.API.Repositories.Interfaces;

namespace Forum.API.Repositories.Implements;


public class ForumRepository : IForumRepository
{

    private readonly DatabaseConnHelper _databaseConnHelper;
    public ForumRepository(DatabaseConnHelper databaseConnHelper)
    {
        this._databaseConnHelper = databaseConnHelper;
    }
    public async  Task<bool> DeleteByPostIdAsync(Guid PostId)
    {
        string sql = @" DELETE FROM [dbo].[Posts]
                        WHERE  PostId = @PostId ";

        using var conn = _databaseConnHelper.ForumConnection();
        var count = await conn.ExecuteAsync(sql, new { PostId = PostId });
        if (count != 1) return false;
        return true;
    }

    public Task<IEnumerable<PostEntity>> GetAsync(PostEntity? entity = null)
    {
        string sql = @"SELECT FROM [dbo].[Posts]
                        WHERE 1=1";

        if (entity is not null)
        {
            if (!String.IsNullOrEmpty(entity.Category))
            {
                sql += " And Category = @Category ";
            }
        }

        throw new NotImplementedException();
    }



    public Task<PostEntity?> GetByPostIdAsync(Guid PostId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(PostEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdatePostAsync(PostEntity entity)
    {
        throw new NotImplementedException();
    }
}
