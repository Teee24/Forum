namespace Forum.API.Domain.Entity;

public class CommentEntity
{
    ///<summary>
    ///PK
    /// </summary>
    public Guid CommentId { get; set; }
    ///<summary>
    ///留言內容
    /// </summary>
    public string Comment { get; set; }
    ///<summary>
    ///發佈時間
    /// </summary>
    public DateTime PostDate { get; set; }
    ///<summary>
    ///發佈者
    /// </summary>
    public string Publisher { get; set; }
    ///<summary>
    ///針對哪篇貼文的留言
    /// </summary>
    public Guid FromId { get; set; }
}
