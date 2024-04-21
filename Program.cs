namespace program;
using apiconnection;
using userHandling;
using jsonhandling;

using System.Text.Json;
using System.Text.Json.Serialization;
using jsonhandling;

public class Program {

    static void Main(string[] args) {

        MainAsync();

        MainAsync().GetAwaiter().GetResult();
        /*

        Console.WriteLine("Do you want to add singular or multiple entries to your log?");
        string amountEntriesAnswer = Console.ReadLine().ToLower();
        if(amountEntriesAnswer == "singular") {
            UserHandling.AddSingularEntry();
        }

        JsonHandling.ReadJson();
    
        */
    }

    public static async Task MainAsync() {
        await ApiConnection.TestApi();
    }
}

