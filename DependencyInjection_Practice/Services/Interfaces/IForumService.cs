using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;

namespace Forum.API.Services.Interfaces
{
    public interface IForumService
    {
        public Task<IActionResult> GetAllPost(QueryPostRequest request);
        public Task<IActionResult> InsertPost(PostPostRequest request);
        public Task<IActionResult> UpdatePost(PutPostRequest request);
        public Task<IActionResult> DeletePost(Guid postid);
        //依照分類搜尋貼文
        public Task<IActionResult> GetPost(string category);
    }
}
