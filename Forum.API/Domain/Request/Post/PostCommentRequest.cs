﻿namespace Forum.API.Domain.Request.Post
{
    public class PostCommentRequest
    {
        ///<summary>
        ///留言內容
        /// </summary>
        public string Comment { get; set; }

        ///<summary>
        ///發佈時間
        /// </summary>
        public DateTime PostDate { get; set; }

        ///<summary>
        ///發佈人
        /// </summary>
        public string Publisher { get; set; }

        ///<summary>
        ///第幾層 (0-留言;1-回復)
        /// </summary>
        public bool Layer { get; set; }

        ///<summary>
        ///回復哪則留言的Id
        /// </summary>
        public Guid? ToId { get; set; }

        ///<summary>
        ///針對哪篇貼文的留言
        /// </summary>
        public Guid FromId { get; set; }
    }
}