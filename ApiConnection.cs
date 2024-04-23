using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using program;
namespace apiconnection;

public class ApiConnection {
    
    public static string responseBody = string.Empty;

    public static async Task TestApi() {

        string grimstadCoordLat = "58.3421";
        string grimstadCoordLon = "8.5945";

        

        using (HttpClient client = new HttpClient()) {
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");

            client.DefaultRequestHeaders.Add("User-Agent", "Weather-App github.com/Weather-App");
        
        //Need to add access header thingy

        try {
            HttpResponseMessage response = await client.GetAsync("compact?lat=" + grimstadCoordLat + "&lon=" + grimstadCoordLon);

            if (response.IsSuccessStatusCode) {
                responseBody = await response.Content.ReadAsStringAsync();
                
                //Console.WriteLine(responseBody);
            } else {
                Console.WriteLine("Error if fail to retrieve data: " + response.StatusCode);
            }
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }

        }
          
    }

    public static async Task GetTemperatureForSpecificTime() {
        using (JsonDocument doc = JsonDocument.Parse(responseBody)) {
            JsonElement root = doc.RootElement;
            JsonElement timeseriesArray = root.GetProperty("properties").GetProperty("timeseries");

            JsonElement entry = default;
            using (var enumerator = timeseriesArray.EnumerateArray()) {
                while (enumerator.MoveNext()) {
                    var currentEntry = enumerator.Current;
                    if (currentEntry.GetProperty("time").GetString() == "2024-04-23T20:00:00Z") {
                        entry = currentEntry;
                        break;
                    }
                }
            }

            if (!entry.Equals(default)) {
                double temperature = entry.GetProperty("data").GetProperty("instant").GetProperty("details").GetProperty("air_temperature").GetDouble();
                Console.WriteLine($"Temperature at 20:00:00: {temperature}°C");
            } else {
                Console.WriteLine("Temperature data not available for 20:00:00.");
            }
        }
    }

    public static async Task GetPropertyForSpecifcTime(string parameter) {
        using (JsonDocument doc = JsonDocument.Parse(responseBody)) {
            JsonElement root = doc.RootElement;
            JsonElement timeseriesArray = root.GetProperty("properties").GetProperty("timeseries");

            JsonElement entry = default;
            using (var enumerator = timeseriesArray.EnumerateArray()) {
                while (enumerator.MoveNext()) {
                    var currentEntry = enumerator.Current;
                    if (currentEntry.GetProperty("time").GetString() == "2024-04-23T20:00:00Z") {
                        entry = currentEntry;
                        break;
                    }
                }
            }

            if (!entry.Equals(default)) {
                double wind = entry.GetProperty("data").GetProperty("instant").GetProperty("details").GetProperty("parameter").GetDouble();
                Console.WriteLine($"Wind at 20:00:00: {wind} m/s");
            } else {
                Console.WriteLine("Temperature data not available for 20:00:00.");
            }
        }
    }


}