using Forum.API.Domain.Entity;

namespace Forum.API.Repositories.Interfaces
{
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
        /// <param name="PostId">PK</param>
        /// <returns></returns>
        public Task<PostEntity?> GetByPostIdAsync(Guid PostId);

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
    }
}
