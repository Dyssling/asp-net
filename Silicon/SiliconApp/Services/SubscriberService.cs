using Newtonsoft.Json;
using SiliconApp.Models;
using System.Text;

namespace SiliconApp.Services
{
    public class SubscriberService
    {
        private readonly HttpClient _http;

        public SubscriberService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> Subscribe(HomeNewsletterModel model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model); //För att kunna skicka med modellen i en POST måste den först omvandlas till en sträng
                var content = new StringContent(json, Encoding.UTF8, "application/json"); //Sedan formateras den till HTTP innehåll, så att den sedan kan kommunicera i "HTTP språk"

                var result = await _http.PostAsync($"https://localhost:7231/api/Subscribers/Register/?api-key=OWNmNDZhNmYtZDZiNS00NTViLTg4NzQtZWM4NjIxZjUwNGQ2", content);

                return result.StatusCode.ToString();
            }

            catch { }

            return "Error"; //Ett fel har skett, och man får en error status tillbaka
        }
    }
}
