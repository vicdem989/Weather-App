using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json;
using program;
using userHandling;
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
            HttpResponseMessage response = await client.GetAsync("complete?lat=" + grimstadCoordLat + "&lon=" + grimstadCoordLon);

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

    public static async Task GetTemperatureForSpecificTime(List<UserHandling.Day> lastLogInput) {
        
        string dateFromUser = string.Empty;
        string timeFromUser = string.Empty;

        /*
        
        string airTempFromUser = string.Empty;
        string rainfallFromUser = string.Empty;
        string windFromUser = string.Empty;
        string rainfall = string.Empty;
        string sunnyFromUser = string.Empty;
        string cloudyFromUser = string.Empty;
        
        */

        foreach(UserHandling.Day day in lastLogInput) {
            dateFromUser = day.date;
            timeFromUser = day.time;
        }

        DateTime originalDate;
        if (DateTime.TryParseExact(dateFromUser, "dd/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out originalDate)) {
            dateFromUser = originalDate.ToString("yyyy-MM-dd");
        }
        else {
            Console.WriteLine("Failed to parse the date.");
        }

        using (JsonDocument doc = JsonDocument.Parse(responseBody)) {
            JsonElement root = doc.RootElement;
            JsonElement timeseriesArray = root.GetProperty("properties").GetProperty("timeseries");

            JsonElement entry = default;
            using (var enumerator = timeseriesArray.EnumerateArray()) {
                while (enumerator.MoveNext()) {
                    var currentEntry = enumerator.Current;
                    if (currentEntry.GetProperty("time").GetString() == dateFromUser + "T" + timeFromUser + "Z") {
                        entry = currentEntry;
                        break;
                    }
                }
            }

            if (!entry.Equals(default)) {
                double temperature = entry.GetProperty("data").GetProperty("instant").GetProperty("details").GetProperty("air_temperature").GetDouble();
                Console.WriteLine($"Temperature at 12:00:00: {temperature}°C on {dateFromUser}");
            } else {
                Console.WriteLine("Temperature data not available for 20:00:00.");
            }
        }
    }




}