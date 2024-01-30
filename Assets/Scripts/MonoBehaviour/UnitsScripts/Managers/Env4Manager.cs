using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class Env4Manager : MonoBehaviour, IShowable
{
	public static Env4Manager instance;

	public float Temperature { get; private set; }
    public float Pressure { get; private set; }
    public float Humidity { get; private set; }


    private void Awake() => instance = this;

    private async void Start()
	{
		string publicIP = await GetPublicIPAddressAsync();
		EarthCoordinates coordinates = await GetLatitudeLongitude(publicIP);
		string url = "https://api.open-meteo.com/v1/forecast?latitude=" + coordinates.lat + "&longitude=" + coordinates.lon + "&current=temperature_2m&current=surface_pressure&current=relative_humidity_2m";

		WeatherData weatherdata = await GetWeatherData(url);
		Temperature = weatherdata.current.temperature_2m;
		Pressure = weatherdata.current.surface_pressure;
		Humidity = weatherdata.current.relative_humidity_2m;
	}

    private async Task<WeatherData> GetWeatherData(string url)
	{
        using (HttpClient client = new HttpClient())
		{
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
			{
                string responseString = await response.Content.ReadAsStringAsync();
                WeatherData weatherData = JsonUtility.FromJson<WeatherData>(responseString);
                return weatherData;
            }
            else
			{
                Debug.Log("Error: " + response.StatusCode);
				return null;
            }
        }
    }

	private async Task<string> GetPublicIPAddressAsync()
	{
		using (HttpClient client = new HttpClient())
		{
			string apiUrl = "https://httpbin.org/ip";
			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				string responseString = await response.Content.ReadAsStringAsync();
				PublicIPResponse ipInfo = JsonUtility.FromJson<PublicIPResponse>(responseString);
				return ipInfo.origin;
			}
			else
			{
                Debug.Log("Error: " + response.StatusCode);
                return null;
            }
		}
	}
    private async Task<EarthCoordinates> GetLatitudeLongitude(string ip)
	{
        using (HttpClient client = new HttpClient())
		{
            HttpResponseMessage response = await client.GetAsync($"http://ip-api.com/json/" + ip);

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                EarthCoordinates coords = JsonUtility.FromJson<EarthCoordinates>(responseString);
                return coords;
            }
            else
            {
                Debug.Log("Error: " + response.StatusCode);
                return null;
            }
        }
    }

    public bool CanShowCategory() => UnitsManager.instance.UnitSlotHasChildren() && UnitsManager.instance.GetUnitImage().name.ToUpper() == GetCategoryName();
    public void ClearGarbage() { /*do nothing*/}
    public string GetCategoryName() => this.name.ToUpper();
}


	[Serializable]
	public class PublicIPResponse
	{
		public string origin;
	}

	[System.Serializable]
	public class WeatherData
	{
		public Current current;
	}

	[System.Serializable]
	public class Current
	{
		public float temperature_2m;
		public float surface_pressure;
		public int relative_humidity_2m;
	}


	[System.Serializable]
	public class EarthCoordinates
	{
		public string lat;
		public string lon;
	}

