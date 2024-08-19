namespace HangfirePractice.Model
{
    public class ETFInfo
    {
        /// <summary>
        /// 標的物
        /// </summary>
        public string TargetName { get; set; }
        /// <summary>
        /// 標的物代號
        /// </summary>
        public string TargetCode { get; set; }
        /// <summary>
        /// 成交
        /// </summary>
        public string ClosingPrice { get; set; }
        /// <summary>
        /// 開盤
        /// </summary>
        public string OpeningPrice { get; set; }
        /// <summary>
        /// 最高
        /// </summary>
        public string HighestPrice { get; set; }
        /// <summary>
        /// 最低
        /// </summary>
        public string LowestPrice { get; set; }
        /// <summary>
        /// 均價
        /// </summary>
        public string AveragePrice { get; set; }
        /// <summary>
        /// 成交金額(億)
        /// </summary>
        public string TotalTradingValueBillion { get; set; }
        /// <summary>
        /// 昨收
        /// </summary>
        public string PreviousClosingPrice { get; set; }
        /// <summary>
        /// 漲跌幅
        /// </summary>
        public string PriceChangePercentage { get; set; }
        /// <summary>
        /// 漲跌
        /// </summary>
        public string PriceChange { get; set; }
        /// <summary>
        /// 總量
        /// </summary>
        public string TotalVolume { get; set; }
        /// <summary>
        /// 昨量
        /// </summary>
        public string PreviousVolume { get; set; }
        /// <summary>
        /// 振幅
        /// </summary>
        public string PriceFluctuationPercentage { get; set; }
    }
}
