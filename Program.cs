namespace program;
using apiconnection;
using userHandling;
using jsonhandling;

using System.Text.Json;
using System.Text.Json.Serialization;

public class Program {

    public static List<UserHandling.Day> lastLogInput = new List<UserHandling.Day>();

    static void Main(string[] args) {

        MainAsync();

        MainAsync().GetAwaiter().GetResult();
        

        Console.WriteLine("Do you want to add singular or multiple entries to your log?");
        string amountEntriesAnswer = Console.ReadLine().ToLower();
        if(amountEntriesAnswer == "singular") {
            lastLogInput = UserHandling.AddSingularEntry();
        }

        RunComparison(lastLogInput);

        JsonHandling.ReadJson();
    
        
    }

    public static async Task MainAsync() {
        await ApiConnection.TestApi();
    }

    public static async Task RunComparison(List<UserHandling.Day> lastLog) {
        await ApiConnection.GetTemperatureForSpecificTime(lastLog);
    }
}

