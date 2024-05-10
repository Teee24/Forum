namespace Forum.API.Domain.Request.Put;

public class PutPostRequest
{
    ///<summary>
    ///流水號
    /// </summary>
    public Guid PostId { get; set; }

    ///<summary>
    ///標題
    /// </summary>
    public string Title { get; set; }

    ///<summary>
    ///內容
    /// </summary>
    public string Detail { get; set; }

}
