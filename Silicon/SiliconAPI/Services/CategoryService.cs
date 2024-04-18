using Microsoft.AspNetCore.Components.Forms;
using SiliconAPI.Entitites;
using SiliconAPI.Repositories;

namespace SiliconAPI.Services
{
    public class CategoryService
    {
        public readonly CategoryRepository _repo;

        public CategoryService(CategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync()
        {
            try
            {
                var list = await _repo.GetAllAsync();

                if (list != null)
                {
                    return list.OrderBy(x => x.CategoryName);
                }
            }

            catch { }

            return null!;

        }
    }
}
