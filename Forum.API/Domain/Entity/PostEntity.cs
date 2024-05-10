using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Entity
{
    public class PostEntity
    {
        ///<summary>
        ///流水號
        /// </summary>
        public Guid PostId { get; set; }
        ///<summary>
        ///分類
        /// </summary>
        public string? Category { get; set; }
        ///<summary>
        ///標題
        /// </summary>
        public string Title { get; set; }
        ///<summary>
        ///內容
        /// </summary>
        public string Detail { get; set; }
        ///<summary>
        ///發佈時間
        /// </summary>
        public DateTime PostDate { get; set; }
        ///<summary>
        ///發佈者
        /// </summary>
        public string Publisher { get; set; }


    }
}
