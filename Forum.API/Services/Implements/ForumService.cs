using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;
using Forum.API.Services.Interfaces;

namespace Forum.API.Services.Implements
{

    public class ForumService : IForumService
    {
        public Task<IActionResult> DeletePost(Guid postid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetAllPost(QueryPostRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetPost(string category)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> InsertPost(PostPostRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdatePost(PutPostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
