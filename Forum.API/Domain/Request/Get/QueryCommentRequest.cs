namespace Forum.API.Domain.Request.Get;

public class QueryCommentRequest
{
    ///<summary>
    ///針對哪篇貼文的留言
    /// </summary>
    public Guid FromId { get; set; }
}
