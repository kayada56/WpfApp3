using System.Net.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public class WeatherService
    {
        private const string ApiKey = "909f21b227ad4577959112c5374809c8";
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather";

        public async Task<string> GetWeatherAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrlWithParams = $"{ApiUrl}?q={city}&appid={ApiKey}&units=metric";
                HttpResponseMessage response = await client.GetAsync(apiUrlWithParams);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}