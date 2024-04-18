using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SiliconApp.Entities;
using System.Diagnostics;

namespace SiliconApp.Services
{
    public class CourseService
    {
        private readonly HttpClient _http;

        public CourseService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync(string categoryId, string search)
        {
            try
            {
                //var result = await _http.GetAsync($"https://localhost:7231/api/Courses/GetAll?{(!categoryId.IsNullOrEmpty() ? $"category={categoryId}" : "")}{(!search.IsNullOrEmpty() ? (!categoryId.IsNullOrEmpty() ? "&" : "") : "")}{(!search.IsNullOrEmpty() ? $"search={search}" : "")}{(!search.IsNullOrEmpty() || !categoryId.IsNullOrEmpty() ? "&" : "")}api-key=OWNmNDZhNmYtZDZiNS00NTViLTg4NzQtZWM4NjIxZjUwNGQ2");
                var result = await _http.GetAsync($"https://localhost:7231/api/Courses/GetAll?category={categoryId}&search={search}&api-key=OWNmNDZhNmYtZDZiNS00NTViLTg4NzQtZWM4NjIxZjUwNGQ2");

                if (result.IsSuccessStatusCode)
                {
                    var list = JsonConvert.DeserializeObject<IEnumerable<CourseEntity>>(await result.Content.ReadAsStringAsync());

                    if (!list.IsNullOrEmpty())
                    {
                        return list!;
                    }
                }
            }

            catch (Exception ex) { Debug.WriteLine(ex); }

            return new List<CourseEntity>(); //Man får tillbaka en tom lista om nåt går snett
        }
    }
}
