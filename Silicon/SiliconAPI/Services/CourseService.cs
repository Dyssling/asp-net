using Microsoft.AspNetCore.Identity;
using SiliconAPI.Entities;
using SiliconAPI.Models;
using SiliconAPI.Repositories;

namespace SiliconAPI.Services
{
    public class CourseService
    {
        private readonly CourseRepository _repo;

        public CourseService(CourseRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateNewCourseAsync(CourseModel model)
        {
            try
            {
                var entity = new CourseEntity()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Rating = model.Rating,
                    LikesInPercent = model.LikesInPercent,
                    LikesInNumbers = model.LikesInNumbers,
                    Hours = model.Hours,
                    Author = model.Author,
                    IsBestSeller = model.IsBestSeller
                };

                var result = await _repo.CreateAsync(entity);

                if (result)
                {
                    return true;
                }
            }

            catch { }

            return false;
        }

        public async Task<bool> UpdateCourseAsync(CourseModel model, int id)
        {
            try
            {
                var entity = new CourseEntity()
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                    Rating = model.Rating,
                    LikesInPercent = model.LikesInPercent,
                    LikesInNumbers = model.LikesInNumbers,
                    Hours = model.Hours,
                    Author = model.Author,
                    IsBestSeller = model.IsBestSeller
                };

                var result = await _repo.UpdateAsync(x => x.Id == id, entity);

                if (result)
                {
                    return true;
                }
            }

            catch { }

            return false;
        }
    }
}
