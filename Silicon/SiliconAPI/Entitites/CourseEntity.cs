using SiliconAPI.Entitites;
using System.ComponentModel.DataAnnotations;

namespace SiliconAPI.Entities
{
    public class CourseEntity
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int? Rating { get; set; }
        public int? LikesInPercent { get; set; }
        public int? LikesInNumbers { get; set; }
        public int Hours { get; set; }
        public string Author { get; set; } = null!;
        public bool IsBestSeller { get; set; } = false;
        public string ImageName { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }

        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
    }
}
