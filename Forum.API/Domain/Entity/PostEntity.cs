using System.ComponentModel.DataAnnotations;

namespace Forum.API.Domain.Entity
{
    public class PostEntity
    {
        public Guid PostId { get; set; }
        public string? Category { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public DateTime PostDate { get; set; }

        public string Publisher { get; set; }


    }
}
