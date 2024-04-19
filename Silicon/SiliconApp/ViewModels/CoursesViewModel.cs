using SiliconApp.Entities;

namespace SiliconApp.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<CourseEntity>? Courses { get; set; }
        public IEnumerable<CategoryEntity>? Categories { get; set; }

        public int NumberOfPages { get; set; }
        public int CurrentPage { get; set; }
        public int AmountPerPage { get; set; }

        public IEnumerable<int>? UserSavedCourses { get; set; }
    }
}
