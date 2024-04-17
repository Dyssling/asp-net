using System.ComponentModel.DataAnnotations;

namespace SiliconApp.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? Rating { get; set; }
        public decimal? LikesInPercent { get; set; }
        public decimal? LikesInNumbers { get; set; }
        public int Hours { get; set; }
        public string Author { get; set; } = null!;
        public bool IsBestSeller { get; set; } = false;
        public string ImageName { get; set; } = null!;
    }
}
