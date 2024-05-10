using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Request.Post;

public class PostPostRequest
{
    ///<summary>
    ///分類
    /// </summary>
    [MaxLength(4, ErrorMessage = "欄位長度為4")]
    [Display(Name = "分類")]
    [Required(ErrorMessage = "{0}為必填欄位")]
    public string Category { get; set; }
    ///<summary>
    ///標題
    /// </summary>
    [MaxLength(20, ErrorMessage = "欄位長度為20")]
    [Display(Name = "標題")]
    [Required(ErrorMessage = "{0}為必填欄位")]
    public string Title { get; set; }
    ///<summary>
    ///內容
    /// </summary>
    [MaxLength(100, ErrorMessage = "欄位長度為100")]
    [Display(Name = "內容")]
    [Required(ErrorMessage = "{0}為必填欄位")]
    public string Detail { get; set; }
    ///<summary>
    ///發佈者
    /// </summary>
    [MaxLength(10, ErrorMessage = "欄位長度為10")]
    [Display(Name = "發佈者")]
    [Required(ErrorMessage = "{0}為必填欄位")]
    public string Publisher { get; set; }

}
