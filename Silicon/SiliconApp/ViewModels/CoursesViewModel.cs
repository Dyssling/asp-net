using SiliconApp.Entities;

namespace SiliconApp.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<CourseEntity>? Courses { get; set; }
        public IEnumerable<CategoryEntity>? Categories { get; set; }
    }
}
