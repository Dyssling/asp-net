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

        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync()
        {
            try
            {
                var list = await _repo.GetAllAsync();

                if (list != null)
                {
                    return list;
                }
            }

            catch { }

            return new List<CourseEntity>(); //En tom lista returneras om resultatet blev null av någon anledning
        }

        public async Task<CourseEntity> GetOneCourseAsync(int id)
        {
            try
            {
                var entity = await _repo.GetOneAsync(x => x.Id == id);

                if (entity != null)
                {
                    return entity;
                }
            }

            catch { }

            return null!;
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

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var result = await _repo.DeleteAsync(x => x.Id == id);

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
