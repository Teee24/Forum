using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Response;
using Forum.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        //註冊ForumService
        private readonly IForumService _forumService;
        public ForumController(IForumService forumService)
        {
            this._forumService = forumService;
        }

        /// <summary>
        /// 多筆查詢Posts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IResult> Get([FromQuery] QueryPostRequest request)
        {
            var result = await this._forumService.GetAllPost(request);
           // ResultResponse resultResponse = new ResultResponse() { ReturnData = '"' };
            return Results.Ok(result);
        }


        /// <summary>
        /// 新增單筆Posts
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IResult> Insert([FromQuery] PostPostRequest request)
        {
          var result = await _forumService.InsertPost(request);

            return Results.Ok(result);
        }
    }
}
