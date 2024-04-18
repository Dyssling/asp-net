using SiliconAPI.Entitites;
using System.ComponentModel.DataAnnotations;

namespace SiliconAPI.Models
{
    public class CourseModel
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
        public int? Rating { get; set; }
        public int? LikesInPercent { get; set; }
        public int? LikesInNumbers { get; set; }

        [Required]
        public int Hours { get; set; }

        [Required]
        public string Author { get; set; } = null!;
        public bool IsBestSeller { get; set; } = false;

        [Required]
        public string ImageName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
