using WeatherApp2.Services;

namespace WeatherApp2;

public partial class WeatherPage : ContentPage
{
	public List<Models.List> WeatherList;
	private double latitude;
    private double longitude;

    public WeatherPage()
	{
		InitializeComponent();
		WeatherList = new List<Models.List>();
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await GetLocation();
		await GetWeatherDataByLocation(latitude, longitude);
    }

	public async Task GetLocation()
	{
		var location = await Geolocation.GetLocationAsync();
		latitude = location.Longitude;
		longitude = location.Latitude;
	}

	private async void TapLocation_Tapped(object sender, EventArgs e)
	{
		await GetLocation();
		await GetWeatherDataByLocation(latitude, longitude);
	}

	public async Task GetWeatherDataByLocation(double latitude, double longitude)
	{
        var result = await ApiServices.GetWeather(latitude, longitude);
        UpdateUI(result);

    }

	private async void ImageButton_Clicked(object sender, EventArgs e)
	{
		var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search city", accept:"Search", cancel:"Cancel");
		if (response!=null)
		{
			await GetWeatherDataByCity(response);
		}
	}

    public async Task GetWeatherDataByCity(string city)
    {
        
        var result = await ApiServices.GetWeatherByCityName(city);
        UpdateUI(result);

    }

    public void UpdateUI(dynamic result)
    {
        CvWeather.ItemsSource = null;
        WeatherList.Clear();
        foreach (var item in result.list)
        {
			
            WeatherList.Add(item);
        }
        CvWeather.ItemsSource = WeatherList;
		

        LblCity.Text = result.city.name;
        LblWeatherDescription.Text = result.list[0].weather[0].description;
        LblTemperature.Text = result.list[0].main.temperature + "°C";
        LblHumidity.Text = result.list[0].main.humidity + "%";
        LblWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].customIcon;
    }
}