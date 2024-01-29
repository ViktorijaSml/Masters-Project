using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Antonio : MonoBehaviour
{
	private string url;
	private string latitude = "45.804655221950554";
	private string longitude = "15.965269464488433";

	/*
		Prvo ti mora uzeti IP adresu.
		Nakon uzimanja IP adrese ta vrijednost se šalje metodi > GetLatLong
		Nakon što je ta metoda gotova njene vrijednosti > latitude & longitude
		se moraju poslati sljedećoj metodi to jest > GetWeatherData

		Nakon te procedure možeš manipulirati vrijednostima.
		Moja preporuka je napraviti GetWeatherData kao retrun ili postaviti public variable u nju.

		
		public string Temperature = weatherData.current.temperature_2m;
		public string Pressure = weatherData.current.surface_pressure;
		public string Humidity = weatherData.current.relative_humidity_2m;

		Nema na čemu.
	*/


	private async void Start()
	{
		string publicIP = await GetPublicIPAddressAsync();
		StartCoroutine(GetLatLong(publicIP));
	}

	IEnumerator GetWeatherData(string url)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
		{
			yield return webRequest.SendWebRequest();

			if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log("Error: " + webRequest.error);
			}
			else
			{
				string jsonResult = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
				WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonResult);

				Debug.Log("Temperature: " + weatherData.current.temperature_2m);
				Debug.Log("Surface Pressure: " + weatherData.current.surface_pressure);
				Debug.Log("Relative Humidity: " + weatherData.current.relative_humidity_2m);
			}
		}
	}

	IEnumerator GetLatLong(string ip)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get($"http://ip-api.com/json/" + ip))
		{
			yield return webRequest.SendWebRequest();

			if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				Debug.Log("Error: " + webRequest.error);
			}
			else
			{
				string jsonResult = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
				LatLong Latlong = JsonUtility.FromJson<LatLong>(jsonResult);
				latitude = Latlong.lat;
				longitude = Latlong.lon;

				url = "https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude + "&current=temperature_2m&current=surface_pressure&current=relative_humidity_2m";
				StartCoroutine(GetWeatherData(url));
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
				return "en";
			}
		}
	}

	[Serializable]
	private class PublicIPResponse
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
	public class LatLong
	{
		public string lat;
		public string lon;
	}
}
