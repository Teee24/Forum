namespace HttpClient_Practice.Request
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public int price { get; set; }
        public string description { get; set; }
        public Category category { get; set; }
        public List<string> images { get; set; }
    }
}
