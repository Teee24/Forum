namespace HttpClient_Practice.Request
{
    public class ActityRootObject
    {
        public int Page { get; set; }
        public bool LastPage { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Acticitiy> Acticities { get; set; }

    }
}
