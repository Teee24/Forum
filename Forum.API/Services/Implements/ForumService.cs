using AutoMapper;
using Azure;
using Forum.API.Domain.Entity;
using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;
using Forum.API.Domain.Response;
using Forum.API.Repositories.Interfaces;
using Forum.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Forum.API.Services.Implements;


public class ForumService : IForumService
{
    private readonly IMapper _mapper;
    private readonly IForumRepository _forumRepository;
    public ForumService(IForumRepository forumRepository, IMapper mapper)
    {
        this._forumRepository = forumRepository;
        this._mapper = mapper;
    }

    public async Task<ResultResponse> DeletePost(Guid postid)
    {
        var post = await this._forumRepository.GetByPostIdAsync(postid);
        if (post is null) { return new ResultResponse() { ReturnMessage = "查無該筆資料", ReturnData = "" }; }
        var result = await this._forumRepository.DeleteByPostIdAsync(postid);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "刪除成功", ReturnData = result };
        return resultResponse;
    }

    public async Task<ResultResponse> GetAllPost(QueryPostRequest request)
    {
        var entity = this._mapper.Map<PostEntity>(request);
        var result = await this._forumRepository.GetAsync(entity);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "查詢成功", ReturnData = result };
        return resultResponse;
    }

    public async Task<ResultResponse> InsertPost(PostPostRequest request)
    {
        var entity = this._mapper.Map<PostEntity>(request);
        var result = await this._forumRepository.InsertAsync(entity);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "新增成功", ReturnData = result };
        return resultResponse;
    }

    public async Task<ResultResponse> UpdatePost(PutPostRequest request)
    {
        var post = await this._forumRepository.GetByPostIdAsync(request.PostId);
        if (post is null) { return new ResultResponse() { ReturnMessage = "查無該筆資料", ReturnData = "" }; }
        var entity = this._mapper.Map<PostEntity>(request);
        var result = await this._forumRepository.UpdatePostAsync(entity);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "更新成功", ReturnData = result };
        return resultResponse;
    }
    public async Task<ResultResponse> GetComment(QueryCommentRequest request)
    {
        var post = await this._forumRepository.GetByPostIdAsync(request.FromId);
        if (post is null) { return new ResultResponse() { ReturnMessage = "該筆貼文不存在", ReturnData = null }; }
        var entity = this._mapper.Map<CommentEntity>(request);
        var result = await this._forumRepository.GetCommentByPostIdAsync(entity);
        if (result.Count() == 0) { return new ResultResponse() { ReturnMessage = "該筆貼文尚未有人留言", ReturnData = null }; }
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "查詢成功", ReturnData = result };
        return resultResponse;
    }

    public async Task<ResultResponse> PostComment(PostCommentRequest request)
    {
        var entity = this._mapper.Map<CommentEntity>(request);
        entity.PostDate = DateTime.Now;
        var result = await this._forumRepository.PostCommentAsync(entity);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "新增成功", ReturnData = result };
        return resultResponse;
    }

    public async Task<ResultResponse> UpdateComment(PutCommentRequest request)
    {
        var entity = this._mapper.Map<CommentEntity>(request);
        entity.PostDate = DateTime.Now;
        var result = await this._forumRepository.PutByCommentIdAsync(entity);
        ResultResponse resultResponse = result ? new ResultResponse() { ReturnMessage = "修改成功", ReturnData = result } : new ResultResponse() { ReturnMessage = "修改失敗", ReturnData = null };
        return resultResponse;
    }

    public async Task<ResultResponse> DeleteComment(Guid commentid)
    {
        var post = await this._forumRepository.GetByCommentIdAsync(commentid);
        if (post is null) { return new ResultResponse() { ReturnMessage = "查無該筆資料", ReturnData = "" }; }
        var result = await this._forumRepository.DeleteByCommentIdAsync(commentid);
        ResultResponse resultResponse = new ResultResponse() { ReturnMessage = "刪除成功", ReturnData = result };
        return resultResponse;
    }
}
