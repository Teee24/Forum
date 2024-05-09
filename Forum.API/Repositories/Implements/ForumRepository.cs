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

    public async Task<IEnumerable<PostEntity>> GetAsync(PostEntity? entity = null)
    {
        string sql = @"SELECT * FROM [dbo].[Posts]
                        WHERE 1=1";

        if (entity is not null)
        {
            if (!String.IsNullOrEmpty(entity.Category))
            {
                sql += " AND Category = @Category ";
            }
        }
        using var conn = _databaseConnHelper.ForumConnection();
        var posts = await conn.QueryAsync<PostEntity>(sql, entity);
        return posts;
    }



    public Task<PostEntity?> GetByPostIdAsync(Guid PostId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> InsertAsync(PostEntity entity)
    {
        string sql = @"INSERT INTO [dbo].[Posts]
           ([PostId]
           ,[Category]
           ,[Title]
           ,[Detail]
           ,[PostDate]
           ,[Publisher])
     VALUES
           (@PostId
           ,@Category
           ,@Title
           ,@Detail
           ,@PostDate
           ,@Publisher)";

        using var conn = _databaseConnHelper.ForumConnection();
        var count = await conn.ExecuteAsync(sql, entity);
        if (count != 1) return false;
        return true;


    }

    public Task<bool> UpdatePostAsync(PostEntity entity)
    {
        throw new NotImplementedException();
    }
}
