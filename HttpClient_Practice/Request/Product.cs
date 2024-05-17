namespace HttpClient_Practice.Request
{
    public class Product
    {

        public class Rootobject
        {
            public Class1[] Property1 { get; set; }
        }

        public class Class1
        {
            public int id { get; set; }
            public string title { get; set; }
            public int price { get; set; }
            public string description { get; set; }
            public string[] images { get; set; }
            public DateTime creationAt { get; set; }
            public DateTime updatedAt { get; set; }
            public Category category { get; set; }
        }

        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public DateTime creationAt { get; set; }
            public DateTime updatedAt { get; set; }
        }

    }
}
