using Newtonsoft.Json;
using SiliconApp.Models;
using System.Text;

namespace SiliconApp.Services
{
    public class ContactService
    {
        private readonly HttpClient _http;

        public ContactService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> CreateRequest(ContactModel model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await _http.PostAsync($"https://localhost:7231/api/Contact/CreateRequest/?api-key=OWNmNDZhNmYtZDZiNS00NTViLTg4NzQtZWM4NjIxZjUwNGQ2", content);

                return result.StatusCode.ToString();
            }

            catch { }

            return "Error";
        }
    }
}
