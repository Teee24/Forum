using Forum.API.Domain.Entity;

namespace Forum.API.Repositories.Interfaces;

public interface IForumRepository
{
    /// <summary>
    /// 多筆取得Post
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<IEnumerable<PostEntity>> GetAsync(PostEntity? entity = null);

    /// <summary>
    /// 單筆新增 Post
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<bool> InsertAsync(PostEntity entity);

    /// <summary>
    /// 單筆修改內容
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<bool> UpdatePostAsync(PostEntity entity);

    /// <summary>
    /// 單筆取得Post
    /// </summary>
    /// <param name="postId">PK</param>
    /// <returns></returns>
    public Task<PostEntity?> GetByPostIdAsync(Guid postId);

    /// <summary>
    /// 單筆刪除Post
    /// </summary>
    /// <param name="PostId">PK</param>
    /// <returns></returns>
    public Task<bool> DeleteByPostIdAsync(Guid PostId);

    /// <summary>
    /// 取得指定Post下面的Comment
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<IEnumerable<CommentEntity>> GetCommentByPostIdAsync(CommentEntity? entity = null);

    /// <summary>
    /// 新增Post下面的Comment
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<bool> PostCommentAsync(CommentEntity entity);

    /// <summary>
    /// 單筆取得Comment
    /// </summary>
    /// <param name="commentId">PK</param>
    /// <returns></returns>
    public Task<CommentEntity?> GetByCommentIdAsync(Guid commentId);

    /// <summary>
    /// 修改Post下面的Comment
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<bool> PutByCommentIdAsync(CommentEntity entity);

    /// <summary>
    /// 單筆刪除Comment
    /// </summary>
    /// <param name="commenttId">PK</param>
    /// <returns></returns>
    public Task<bool> DeleteByCommentIdAsync(Guid commenttId);
}
