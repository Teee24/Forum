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
            PostEntity entity = new() { Category = request.Category};
            var result = await this._forumRepository.GetAsync(entity);

            ResultResponse resultResponse= new ResultResponse() { ReturnMessage = "查詢成功", ReturnData = result };
            
            
            return resultResponse;


        }

        public Task<IActionResult> GetPost(Guid postid)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultResponse> InsertPost(PostPostRequest request)
        {
            PostEntity entity = new() { 
            Category= request.Category,
            Title= request.Title,
            Detail =request.Detail,
            Publisher = request.Publisher,
            //發佈時間為現在
            PostDate = DateTime.Now
            };
            var result =  await this._forumRepository.InsertAsync(entity);
            
            ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "新增成功", ReturnData = "" };
            return resultResponse;
        }

        public Task<IActionResult> UpdatePost(PutPostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
