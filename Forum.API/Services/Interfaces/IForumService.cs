using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;
using Forum.API.Domain.Response;

namespace Forum.API.Services.Interfaces
{
    public interface IForumService
    {
        public Task<ResultResponse> GetAllPost(QueryPostRequest request);
        public Task<ResultResponse> InsertPost(PostPostRequest request);
        public Task<IActionResult> UpdatePost(PutPostRequest request);
        public Task<IActionResult> DeletePost(Guid postid);
        public Task<IActionResult> GetPost(Guid postid);
    }
}
