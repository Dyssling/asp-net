using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SiliconApp.Entities;

namespace SiliconApp.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllCategoriesAsync()
        {
            try
            {
                var result = await _http.GetAsync("https://localhost:7231/api/Category/GetAll?api-key=OWNmNDZhNmYtZDZiNS00NTViLTg4NzQtZWM4NjIxZjUwNGQ2");

                if (result.IsSuccessStatusCode)
                {
                    var list = JsonConvert.DeserializeObject<IEnumerable<CategoryEntity>>(await result.Content.ReadAsStringAsync());

                    if (!list.IsNullOrEmpty())
                    {
                        return list!;
                    }
                }
            }

            catch { }

            return new List<CategoryEntity>();
        }
    }
}
