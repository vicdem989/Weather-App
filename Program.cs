namespace program;
using apiconnection;
using userHandling;
using jsonhandling;

using System.Text.Json;
using System.Text.Json.Serialization;
using reports;

public class Program {
    public static List<UserHandling.Day> weatherLog = new List<UserHandling.Day>();
    public static List<UserHandling.Day> lastLogInput = new List<UserHandling.Day>();
    

    static void Main(string[] args) {

        MainAsync();

        MainAsync().GetAwaiter().GetResult();
        

        
        JsonHandling.CheckIfJSONExists();


        RunComparison(lastLogInput);

        JsonHandling.ReadJson();

        JsonHandling.PushListToJSON(lastLogInput, weatherLog);

        Reports.reportDay(weatherLog, "31/04/2024");
        Reports.reportPastWeek(weatherLog);
        Reports.reportPastXDays(weatherLog, 30);
    
        
    }

    public static async Task MainAsync() {
        await ApiConnection.GetAPIData();
    }

    public static async Task RunComparison(List<UserHandling.Day> lastLogInput) {
        Console.WriteLine("Data outputted to log:");
        foreach(UserHandling.Day day in lastLogInput) {
            Console.WriteLine($"Date: {day.date}, Day: {day.day}, Time: {day.time}, Air temperature: {day.airTemp}, Rainfall: {day.rainfall}mm, Wind: {day.wind}m/s, Sunny: {day.sunny}, Cloudy: {day.cloudy}");
        }
        Console.WriteLine("Compared to API:");
        await ApiConnection.GetDataFromAPI(lastLogInput);
    }
}

