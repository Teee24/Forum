using Azure;
using Forum.API.Domain.Entity;
using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;
using Forum.API.Domain.Response;
using Forum.API.Repositories.Interfaces;
using Forum.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Forum.API.Services.Implements
{

    public class ForumService : IForumService
    {

        private readonly IForumRepository _forumRepository;
        public  ForumService(IForumRepository forumRepository)
        {
           this. _forumRepository = forumRepository;
        }

        public Task<IActionResult> DeletePost(Guid postid)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> GetAllPost(QueryPostRequest request)
        {
            //var entity = this._mapper.Map<TodoEntity>(request);
            var result = await this._forumRepository.GetAsync();
            ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "", ReturnData = result };
            return resultResponse;


        }

        public Task<IActionResult> GetPost(Guid postid)
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
