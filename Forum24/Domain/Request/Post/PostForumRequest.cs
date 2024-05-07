namespace Forum.API.Domain.Request.Post
{
    public class PostForumRequest
    {
        ///<summary>
        ///標題
        /// </summary>
        public string Title { get; set; }

        ///<summary>
        ///分類
        /// </summary>
        public string Category { get; set; }

        ///<summary>
        ///內容
        /// </summary>
        public string Detail { get; set; }

        ///<summary>
        ///發布日期
        /// </summary>
        public DateTime PostDate { get; set; }

        ///<summary>
        ///發佈人
        /// </summary>
        public DateTime Publisher { get; set; }

    }
}
