using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Request.Post
{
    public class PostPostRequest
    {
        ///<summary>
        ///分類
        /// </summary>
        [Display(Name = "分類")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Category { get; set; }
        ///<summary>
        ///標題
        /// </summary>
        [Display(Name = "標題")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Title { get; set; }

        ///<summary>
        ///內容
        /// </summary>
        [Display(Name = "內容")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Detail { get; set; }

        ///<summary>
        ///發佈者
        /// </summary>
        [Display(Name = "發佈者")]
        [Required(ErrorMessage = "{0}為必填欄位")]
        public string Publisher { get; set; }

    }
}
