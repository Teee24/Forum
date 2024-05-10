using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Request.Post
{
    public class PostCommentRequest
    {
        ///<summary>
        ///留言內容
        /// </summary>
        [MaxLength(50, ErrorMessage = "欄位長度為50")]
        [Display(Name = "留言")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Comment { get; set; }

        ///<summary>
        ///發佈人
        /// </summary>
        [MaxLength(10, ErrorMessage = "欄位長度為10")]
        [Display(Name = "發佈者")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Publisher { get; set; }

        ///<summary>
        ///針對哪篇貼文的留言
        /// </summary>
        public Guid FromId { get; set; }
    }
}
