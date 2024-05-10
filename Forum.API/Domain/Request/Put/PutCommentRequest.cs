using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Request.Put;

public class PutCommentRequest
{
    ///<summary>
    ///PK
    /// </summary>
    public Guid CommentId { get; set; }
    ///<summary>
    ///留言內容
    /// </summary>
    [MaxLength(50, ErrorMessage = "欄位長度為50")]
    [Display(Name = "留言")]
    [Required(ErrorMessage = "{0}為必填欄位")]
    public string Comment { get; set; }
}
