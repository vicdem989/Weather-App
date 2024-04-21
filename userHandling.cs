using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace userHandling;

public class UserHandling {

    #region Lists

    private static List<string> daysOfTheWeek = new List<string>() {"monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"};
    private static List<string> decisionForDays = new List<string>() {"today", "tomorrow", "yesterday"};
    public static List<Day> weatherLog = new List<Day>();

    #endregion

    #region Variables for day data type
    public static string dayToList = string.Empty;
    public static string dateToList = string.Empty;
    public static int highestTempToList;
    public static int lowestTempToList;
    public static double rainfallToList;
    public static int windToList;
    public static bool sunnyToList;
    public static bool cloudyToList;


    #endregion

    #region Helper variables

    public static bool specificDayChoice = false;

    
    
    #endregion

    #region Functions for entry of data

    public static List<Day> AddSingularEntry() {
        
        Console.WriteLine("What day do you want to add to the log?");
        string decision = Console.ReadLine().ToLower();

        if(decisionForDays.Contains(decision)) {
            specificDayChoice = true;
        }

        if(decision == "today") {
            AddTodayToLog();
        } else if (decision == "tomorrow") {
            AddTomorrowToLog();
        } else if (decision == "yesterday") {
            AddYesterdayToLog();
        }

        if(!specificDayChoice) {
            Console.WriteLine("What is the date of the day you want to add? dd/mm/yyyy");
            string dateInput = Console.ReadLine().ToLower();

            while(!VerifyDateFormat(dateInput)) {
                Console.WriteLine("Input correct format for date, dd/mm/yyyy");
                dateInput = Console.ReadLine().ToLower();
            }

            dateToList = dateInput;

            if(DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date)) {
                dayToList = date.DayOfWeek.ToString();
            } 
        }
    
        Console.WriteLine("The day is " + dayToList + " and the date is " + dateToList);

        Console.WriteLine("What is the higest temp(c) of the day?");
        highestTempToList = int.Parse(Console.ReadLine());

        Console.WriteLine("What is the lowest temp(c) of the day?");
        lowestTempToList = int.Parse(Console.ReadLine());

        Console.WriteLine("How much rainfall(mm) was it today");
        rainfallToList = int.Parse(Console.ReadLine());

        Console.WriteLine("how much wind(m/s) was it today?");
        windToList = int.Parse(Console.ReadLine());

        Console.WriteLine("Was it sunny? true/false");
        sunnyToList = bool.Parse(Console.ReadLine().ToLower());

        Console.WriteLine("Was it cloudy? true/false");
        cloudyToList = bool.Parse(Console.ReadLine().ToLower());

        
        OutputDataToJson(dateToList, dayToList, highestTempToList, lowestTempToList, rainfallToList, windToList, sunnyToList, cloudyToList);

        return null;

    }

    public static List<Day> AddMultipleEntries() {
        return null;
    }

    public static void OutputDataToJson(string dateInput, string dayInput, int higestTempInput, int lowerTempInput, double rainfallInput, double windInput, bool sunnyInput, bool cloudyInput) {
        try {
            weatherLog.Add(new Day(){
                date = dateInput,
                day = dayInput,
                highestTemp = higestTempInput,
                lowestTemp = lowerTempInput,
                rainfall = rainfallInput, 
                wind = windInput,
                sunny = sunnyInput,
                cloudy = cloudyInput
            });
        } catch (Exception e) {
            Console.WriteLine("Error when outputting to json file", e);
        }

        string fileName = "WeatherForecast.json"; 
        string jsonString = JsonSerializer.Serialize(weatherLog);
        File.WriteAllText(fileName, jsonString);
        
        Console.WriteLine("Log has been addewd to json!");
    }

    #endregion

    #region Helper functions
    public static bool VerifyDateFormat(string date) {
        return date.Length == 10 && date[2] == '/' && date[5] == '/';
    }

    public static void AddTodayToLog() {
        DayOfWeek todaysDay = DateTime.Now.DayOfWeek;
        dayToList = todaysDay.ToString();

        string todaysDate = DateTime.Now.ToString("dd/MM/yyy");
        dateToList = todaysDate;
    }

    public static void AddTomorrowToLog() {
        DateTime tomorrowsDate = DateTime.Today.AddDays(1);
        dateToList = tomorrowsDate.ToString("dd/MM/yyyy");
        dayToList =  tomorrowsDate.DayOfWeek.ToString();
    }

    public static void AddYesterdayToLog() {
        DateTime yesterdaysDate = DateTime.Today.AddDays(-1);
        dateToList = yesterdaysDate.ToString("dd/MM/yyyy");
        dayToList =  yesterdaysDate.DayOfWeek.ToString();
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