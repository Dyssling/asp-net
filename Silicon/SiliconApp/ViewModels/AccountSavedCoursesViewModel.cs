﻿using SiliconApp.Entities;

namespace SiliconApp.ViewModels
{
    public class AccountSavedCoursesViewModel
    {
        public UserEntity? UserEntity { get; set; }

        public IEnumerable<CourseEntity>? CourseList { get; set; }
    }
}
