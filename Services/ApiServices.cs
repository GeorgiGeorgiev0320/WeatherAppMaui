using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp2.Models;

namespace WeatherApp2.Services
{
    public static class ApiServices
    {
        public static async Task<Root> GetWeather(double Latitude, double Longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={Latitude}&lon={Longitude}&units=metric&appid=ce7fc8562c5ecbf695b07aed131a83d4");
            return JsonConvert.DeserializeObject<Root>(response);
        }

        public static async Task<Root> GetWeatherByCityName(string City)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={City}&units=metric&appid=ce7fc8562c5ecbf695b07aed131a83d4");
            return JsonConvert.DeserializeObject<Root>(response);
        }
    }
}
