namespace jsonhandling;

using System.Text.Json;
using program;
using userHandling;

public class JsonHandling {

    /// How to not fuck up past JSON files
    /// check if JSON file exists
    ///     if not, create a new one 
    /// if it does, pull data from JSON into List<day>
    /// put new day into said List<day> 
    /// output new List to json
    /// </unfucked done


    public static string jsonFilePath = "WeatherLog.json";
    public static async void ReadJson() {
            try {
                string jsonString = File.ReadAllText(jsonFilePath);

                UserHandling.Day[] days = JsonSerializer.Deserialize<UserHandling.Day[]>(jsonString);

                foreach (UserHandling.Day day in days) {
                    Console.WriteLine($"Date: {day.date}, Day: {day.day}, Time: {day.time}, Air temperature: {day.airTemp}, Rainfall: {day.rainfall}mm, Wind: {day.wind}m/s, Sunny: {day.sunny}, Cloudy: {day.cloudy}");

                }
            } catch (Exception e) {
                Console.WriteLine($"An error occurred while reading or deserializing the JSON file: {e.Message}");
            }
    }

    public static void OutputDataToJson(List<UserHandling.Day> listToBeOutputted) {
        try {
        string jsonString = JsonSerializer.Serialize(listToBeOutputted);
        File.WriteAllText(jsonFilePath, jsonString);
        
        Console.WriteLine("Log has been added to json!");
        } catch (Exception e) {
            Console.WriteLine("Error when outputting to JSON!", e);
        }
    }

}