namespace program;
using apiconnection;
using userHandling;
using jsonhandling;

using System.Text.Json;
using System.Text.Json.Serialization;
using reports;
using System.Runtime.InteropServices.Marshalling;
using helperFunctions;

public class Program {
    public static List<UserHandling.Day> weatherLog = new List<UserHandling.Day>();
    public static List<UserHandling.Day> lastLogInput = new List<UserHandling.Day>();
    

    static void Main(string[] args) {

        MainAsync();

        MainAsync().GetAwaiter().GetResult();

        JsonHandling.ReadJson();
        
        Console.WriteLine("Do you want to add a new entry to the log? yes/no");
        if(Console.ReadLine().ToLower() == "yes") { 
            JsonHandling.CheckIfJSONExists();
            RunComparison(lastLogInput);
            Console.WriteLine("");
            JsonHandling.PushListToJSON(lastLogInput, weatherLog);
            
        } 

        Console.Clear();

        Console.WriteLine("Do you want a day report? yes/no");
        if(Console.ReadLine().ToLower() == "yes") { 
            Console.WriteLine("What day do you want to see the report of? dd/mm/yyyy");
            string dayToReport = Console.ReadLine();
            while(!HelperFunctions.VerifyDateFormat(dayToReport)) {
                Console.WriteLine("Enter correct format!");
            }
            Console.Clear();
            Reports.reportDay(weatherLog, dayToReport);
        }


        Console.WriteLine("Do you want a week report? yes/no");
        if(Console.ReadLine().ToLower() == "yes") { 
            Reports.reportPastWeek(weatherLog);
        }


        Console.WriteLine("Do you want a report for more than a week? yes/no");
        if(Console.ReadLine().ToLower() == "yes") { 
            Console.WriteLine("How many days?");
            int amountDays = 0;
            string inputDays = Console.ReadLine();
            while (!int.TryParse(inputDays, out amountDays)) {
                Console.WriteLine("Input integer please.");
                inputDays = Console.ReadLine();
            }
            Reports.reportPastXDays(weatherLog, amountDays);
            }

                
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

