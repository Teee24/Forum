using Dapper;
using Forum.API.Domain.Entity;
using Forum.API.Infrastructures.Database;
using Forum.API.Repositories.Interfaces;
using Microsoft.Extensions.Hosting;
using static Dapper.SqlMapper;

namespace Forum.API.Repositories.Implements;


public class ForumRepository : IForumRepository
{

    private readonly DatabaseConnHelper _databaseConnHelper;
    public ForumRepository(DatabaseConnHelper databaseConnHelper)
    {
        this._databaseConnHelper = databaseConnHelper;
    }
    public async Task<bool> DeleteByPostIdAsync(Guid PostId)
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

    public async Task<PostEntity?> GetByPostIdAsync(Guid PostId)
    {
        string sql = @"SELECT * FROM [dbo].[Posts]
                        WHERE PostId = @PostId";
        using var conn = _databaseConnHelper.ForumConnection();
        var posts = await conn.QuerySingleOrDefaultAsync<PostEntity>(sql, new { PostId = PostId });
        return posts;
    }

    public async Task<bool> InsertAsync(PostEntity entity)
    {
        string sql = @"INSERT INTO [dbo].[Posts]
                        ([Category]
                        ,[Title]
                        ,[Detail]
                        ,[PostDate]
                        ,[Publisher])
                    VALUES
                        (@Category
                        ,@Title
                        ,@Detail
                        ,@PostDate
                        ,@Publisher)";

        using var conn = _databaseConnHelper.ForumConnection();
        var count = await conn.ExecuteAsync(sql, entity);
        if (count != 1) return false;
        return true;
    }

    public async Task<bool> UpdatePostAsync(PostEntity entity)
    {
        string sql = @"UPDATE [dbo].[Posts]
                        SET
                            [Title] = @Title,
                            [Detail] = @Detail,
                            [PostDate] = @PostDate     
                        WHERE PostId = @PostId";
        using var conn = _databaseConnHelper.ForumConnection();
        var count = await conn.ExecuteAsync(sql, entity);
        if (count != 1) return false;
        return true;
    }

    public async Task<IEnumerable<CommentEntity>> GetCommentByPostIdAsync(CommentEntity? entity = null)
    {
        string sql = @"SELECT *  FROM [dbo].[Comments]
                         WHERE  1=1 AND FromId = @FromId";
        using var conn = _databaseConnHelper.ForumConnection();
        var comments = await conn.QueryAsync<CommentEntity>(sql, entity);
        return comments;
    }

    public async Task<bool> PostCommentAsync(CommentEntity entity)
    {
        string sql = @"INSERT INTO [dbo].[Comments]
                       ([Comment]
                       ,[PostDate]
                       ,[Publisher]
                       ,[FromId])
                 VALUES
                       (@Comment
                       ,@PostDate
                       ,@Publisher
                       ,@FromId)";
        var conn = _databaseConnHelper.ForumConnection();
        var count = await conn.ExecuteAsync(sql, entity);
        if (count != 1) return false;
        return true;
    }
}
