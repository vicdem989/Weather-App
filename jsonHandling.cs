namespace jsonhandling;

using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json;
using program;
using userHandling;

public class JsonHandling {
    public static string jsonFilePath = "WeatherLog.json";

    public static void CheckIfJSONExists() {
            if (File.Exists(jsonFilePath)) {
                Program.lastLogInput = UserHandling.AddEntryToLog();
            } 
        }

    public static void PushListToJSON(List<UserHandling.Day> dataToBeAdded, List<UserHandling.Day> listToBeOutputted) {
        try {    
            if(dataToBeAdded.Count != 0) {
                foreach(UserHandling.Day day in dataToBeAdded) {
                    listToBeOutputted.Add(new UserHandling.Day(){
                    date = day.date,
                    day = day.day,
                    time = day.time,
                    airTemp = day.airTemp,
                    rainfall = day.rainfall, 
                    wind = day.wind,
                    sunny = day.sunny,
                    cloudy = day.cloudy
                }); 
                }
            }
            
            string jsonString = JsonSerializer.Serialize(listToBeOutputted);
            File.WriteAllText(jsonFilePath, jsonString);
            
            Console.WriteLine("Log has been added to json!");
        } catch (Exception e) {
            Console.WriteLine("Error when outputting to JSON!", e);
        }
    }

    public static async void ReadJson() {
            try {
                string jsonString = File.ReadAllText(jsonFilePath);

                UserHandling.Day[] days = JsonSerializer.Deserialize<UserHandling.Day[]>(jsonString);
                Program.weatherLog = days.ToList();
            } catch (Exception e) {
                Console.WriteLine($"An error occurred while reading or deserializing the JSON file: {e.Message}");
            }
    }

   

}