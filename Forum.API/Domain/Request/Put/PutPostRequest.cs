using System.ComponentModel.DataAnnotations;

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
    [MaxLength(20, ErrorMessage = "欄位長度為20")]
    [Display(Name = "標題")]
    public string Title { get; set; }

    ///<summary>
    ///內容
    /// </summary>
    [MaxLength(100, ErrorMessage = "欄位長度為100")]
    [Display(Name = "內容")]
    public string Detail { get; set; }

}
