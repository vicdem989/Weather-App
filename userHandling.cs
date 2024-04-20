using System.Diagnostics.Contracts;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace userHandling;

public class UserHandling {

    #region Variables

    private static List<string> daysOfTheWeek = new List<string>() {"monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"};
    private static List<string> decisionForDays = new List<string>() {"today", "tomorrow"};
    public static List<Day> weatherLog = new List<Day>();
    public static string dayToList = string.Empty;
    public static string dateToList = string.Empty;
    public static int highestTempToList;
    public static int lowestTempToList;

    #endregion

    #region Functions for entry of data

    public static List<Day> AddSingularEntry() {
        
        Console.WriteLine("What day do you want to add to the log?");
        string decision = Console.ReadLine().ToLower();

        if(decisionForDays.Contains(decision)) {
            
        }


        DayOfWeek todaysDay = DateTime.Now.DayOfWeek;
        dayToList = todaysDay.ToString();

        string todaysDate = DateTime.Now.ToString("dd.MM.yyy");
        dateToList = todaysDate;

        
        
        
        



        

        return null;

    }

    public static List<Day> AddMultipleEntries() {
        return null;
    }

    public static void OutputDataToJson() {
        /*try {
            weatherLog.Add(new Day(){
                date = dateInput,
                day = dayInput,
                degrees = day,
                sunny = sunnyChoice
            });
        } catch (Exception e) {
            Console.WriteLine("Error when outputting to json file", e);
        }*/

        string fileName = "WeatherForecast.json"; 
        string jsonString = JsonSerializer.Serialize(weatherLog);
        File.WriteAllText(fileName, jsonString);
    }

    #endregion

    public class Day {
        public string date {get; set;}
        public string day {get; set;}
        public int highestTemp {get; set;}
        public int lowestTemp {get; set;}
        public double rainfall {get; set;}
        public double wind {get; set;}
        public bool sunny {get; set;}
        public bool cloudy {get; set;}
    }

    

}