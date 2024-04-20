namespace jsonhandling;

using System.Text.Json;
using program;
using userHandling;

public class JsonHandling {
    
    public static async void ReadJson() {
            string jsonFilePath = "WeatherForecast.json";
            try {
                string jsonString = File.ReadAllText(jsonFilePath);

                UserHandling.Day[] days = JsonSerializer.Deserialize<UserHandling.Day[]>(jsonString);

                foreach (UserHandling.Day day in days) {
                    //Console.WriteLine($"Date: {day.date}, Type: {day.day}, Temperature: {day.}");

                }
            } catch (Exception e) {
                Console.WriteLine($"An error occurred while reading or deserializing the JSON file: {e.Message}");
            }
    }

}