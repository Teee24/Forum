using AutoMapper;
using Forum.API.Domain.Entity;
using Forum.API.Domain.Request.Get;
using Forum.API.Domain.Request.Post;
using Forum.API.Domain.Request.Put;

namespace Forum.API.Domain.AutoMapperProfile
{
    public class ForumProfile : Profile
    {
        public ForumProfile()
        {
            CreateMap<QueryPostRequest, PostEntity>();
            CreateMap<PutPostRequest, PostEntity>()
                .AfterMap((src, dest) => dest.PostDate = DateTime.Now);
            CreateMap<PostPostRequest, PostEntity>()
                 .AfterMap((src, dest) => dest.PostDate = DateTime.Now);
            CreateMap<QueryCommentRequest, CommentEntity>();
            CreateMap<PostCommentRequest, CommentEntity>();
            CreateMap<PutCommentRequest, CommentEntity>();
        }
    }
}
