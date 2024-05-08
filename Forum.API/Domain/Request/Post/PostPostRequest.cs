using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Request.Post
{
    public class PostPostRequest
    {
        ///<summary>
        ///標題
        /// </summary>
        [Display(Name = "標題")]
        [Required(ErrorMessage = "標題為必填欄位")]
        public string Title { get; set; }

        ///<summary>
        ///分類
        /// </summary>
        [Display(Name = "分類")]
        [Required(ErrorMessage = "請選擇分類")]
        public string Category { get; set; }

        ///<summary>
        ///內容
        /// </summary>
        [Display(Name = "標題")]
        [Required(ErrorMessage = "標題為必填欄位")]
        public string Detail { get; set; }

        ///<summary>
        ///發佈時間
        /// </summary>
        /// 
        [Display(Name = "發佈時間")]
        public DateTime PostDate { get; set; }

        ///<summary>
        ///發佈者
        /// </summary>
        /// 
        [Display(Name = "發佈者")]
        [Required(ErrorMessage = "發佈者為必填欄位")]
        public string Publisher { get; set; }

    }
}
