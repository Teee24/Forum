namespace HttpClient_Practice.Request
{
    public class RootObject
    {
        public int Page { get; set; }
        public bool LastPage { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
