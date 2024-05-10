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
        public Task<ResultResponse> UpdatePost(PutPostRequest request);
        public Task<ResultResponse> DeletePost(Guid postid);
        public Task<ResultResponse> GetComment(QueryCommentRequest request);
        public Task<ResultResponse> PostComment(PostCommentRequest request);
    }
}
