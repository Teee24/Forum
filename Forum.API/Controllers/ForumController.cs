using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;
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
        public async Task<IResult> Insert([FromBody] PostPostRequest request)
        {
          var result = await _forumService.InsertPost(request);

            return Results.Ok(result);
        }


        /// <summary>
        /// 刪除Post
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpDelete("{postId}")]
        public async Task<IResult> Delete([FromRoute] Guid postId)
        {
            var result = await _forumService.DeletePost(postId);

            return Results.Ok(result);
        }

        [HttpPatch]
        public async Task<IResult> Update([FromBody] PutPostRequest request,[FromRoute] Guid postId)
        {
            var result = await _forumService.UpdatePost(request);

            return Results.Ok(result);
        }
    }
}
