namespace HttpClient_Practice.Request
{
    public class Activities
    {

        public class ActivityRootobject
        {
            public int page { get; set; }
            public bool last_page { get; set; }
            public Activity[] activities { get; set; }
        }

        public class Activity
        {
            public int activity_id { get; set; }
            public string owner_type { get; set; }
            public string owner_id { get; set; }
            public string owner_value { get; set; }
            public string action { get; set; }
            public int trackable_id { get; set; }
            public string trackable_type { get; set; }
            public string trackable_value { get; set; }
            public string message { get; set; }
        }

    }
}
