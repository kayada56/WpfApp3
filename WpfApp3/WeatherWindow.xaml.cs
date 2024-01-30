using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3
{
    public partial class WeatherWindow : Window
    {
        public WeatherWindow()
        {
            InitializeComponent();
        }




        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string city = cityTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(city))
            {
                WeatherService weatherService = new WeatherService();
                try
                {
                    string weatherJson = await weatherService.GetWeatherAsync(city);

                    if (weatherJson != null)
                    {
                        UpdateWeatherUI(weatherJson);
                    }
                    else
                    {
                        weatherResultText.Text = "Ошибка при получении данных о погоде.";
                    }
                }
                catch (Exception ex)
                {
                    weatherResultText.Text = $"Произошла ошибка: {ex.Message}";
                }
            }
            else
            {
                weatherResultText.Text = "Введите название города.";
            }
        }

        private void UpdateWeatherUI(string weatherJson)
        {
            dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(weatherJson);

            // Вывести полученный JSON в консоль (для отладки)
            Console.WriteLine(weatherJson);

            if (weatherData != null)
            {
                // Если удалось распарсить JSON, пробуем получить данные
                try
                {
                    string cityName = weatherData.name;
                    string temperature = weatherData.main.temp;
                    string description = weatherData.weather[0].description;

                    weatherResultText.Text = $"Город: {cityName}\nТемпература: {temperature}°C\nОписание: {description}";
                }
                catch (Exception ex)
                {
                    weatherResultText.Text = $"Ошибка при обработке данных: {ex.Message}";
                }
            }
            else
            {
                weatherResultText.Text = "Не удалось получить данные о погоде.";
            }
        }

        private void back_to_profile_Click(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }
    }
}