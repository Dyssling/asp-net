using SiliconApp.Entities;

namespace SiliconApp.ViewModels
{
    public class SingleCourseViewModel
    {
        public CourseEntity? CourseEntity { get; set; }

        public IEnumerable<int>? UserSavedCourses { get; set; }
    }
}
