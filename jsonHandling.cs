namespace jsonhandling;

using System.Text.Json;
using program;
using userHandling;

public class JsonHandling {
    
    public static async void ReadJson() {
            string jsonFilePath = "WeatherLog.json";
            try {
                string jsonString = File.ReadAllText(jsonFilePath);

                UserHandling.Day[] days = JsonSerializer.Deserialize<UserHandling.Day[]>(jsonString);

                foreach (UserHandling.Day day in days) {
                    Console.WriteLine($"Date: {day.date}, Day: {day.day}, Higest temperature: {day.highestTemp}, Lowest temperature: {day.lowestTemp}, Rainfall: {day.rainfall}mm, Wind: {day.highestTemp}m/s, Sunny: {day.sunny}, Cloudy: {day.cloudy}");

                }
            } catch (Exception e) {
                Console.WriteLine($"An error occurred while reading or deserializing the JSON file: {e.Message}");
            }
    }

    public static void OutputDataToJson(List<UserHandling.Day> listToBeOutputted) {
        try {
        string fileName = "WeatherLog.json"; 
        string jsonString = JsonSerializer.Serialize(listToBeOutputted);
        File.WriteAllText(fileName, jsonString);
        
        Console.WriteLine("Log has been addewd to json!");
        } catch (Exception e) {
            Console.WriteLine("Error when outputting to JSON!", e);
        }
    }

}