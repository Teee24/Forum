using Forum.API.Domain.Request.Get;
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
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IResult> Get([FromQuery] QueryPostRequest request)
        {
            var result = await this._forumService.GetAllPost(request);
            return Results.Ok();
        }
    }
}
