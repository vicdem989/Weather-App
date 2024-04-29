using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using apiconnection;
using jsonhandling;
using helperFunctions;
using program;

namespace userHandling;

public class UserHandling {

    #region Lists

    private static List<string> daysOfTheWeek = new List<string>() {"monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"};
    private static List<string> decisionForDays = new List<string>() {"today", "tomorrow", "yesterday"};

    #endregion

    #region Variables for day data type
    public static string dayToList = string.Empty;
    public static string dateToList = string.Empty;
    public static string timeToList = string.Empty;
    public static int airTempToList;
    public static double rainfallToList;
    public static int windToList;
    public static bool sunnyToList;
    public static bool cloudyToList;
    #endregion

    #region Helper variables

    public static bool specificDayChoice = false;

    #endregion

    #region Functions for entry of data

    public static List<Day> AddEntryToLog() {
        List<Day> weatherLog = new List<Day>();
        try {
            Console.WriteLine("What day do you want to add to the log?");
            string decision = Console.ReadLine().ToLower();
            while(!daysOfTheWeek.Contains(decision) && !decisionForDays.Contains(decision)) { 
                Console.WriteLine("That is not a valid day?? Input a valid one!");
                decision = Console.ReadLine().ToLower();
            }

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

                while(!HelperFunctions.VerifyDateFormat(dateInput)) {
                    Console.WriteLine("Input correct format for date, dd/mm/yyyy");
                    dateInput = Console.ReadLine().ToLower();
                }

                dateToList = dateInput;

                if(DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date)) {
                    dayToList = date.DayOfWeek.ToString();
                } 
            }

            #region Date user input
        
            Console.WriteLine("What is the air temp(c) currently");
            string airTempInput = Console.ReadLine();
            while(!HelperFunctions.VerifyIntInput(airTempInput)) {
                Console.WriteLine("Not a valid int input!");
                airTempInput = Console.ReadLine(); 
            } 
            airTempToList = int.Parse(airTempInput);

            Console.WriteLine("How much rainfall(mm) was it today?");
            string rainfallInput = Console.ReadLine();
            while(!HelperFunctions.VerifyIntInput(rainfallInput)) {
                Console.WriteLine("Not a valid double input!");
                rainfallInput = Console.ReadLine(); 
            } 
            rainfallToList = int.Parse(rainfallInput);

            Console.WriteLine("How much wind(m/s) was it today?");
            string windInput = Console.ReadLine();
            while(!HelperFunctions.VerifyIntInput(windInput)) {
                Console.WriteLine("Not a valid double input!");
                windInput = Console.ReadLine(); 
            } 
            windToList = int.Parse(windInput);

            Console.WriteLine("Was it sunny? true/false");
            string sunnyInput = Console.ReadLine();
            while(!HelperFunctions.VerifyBoolInput(sunnyInput)) {
                Console.WriteLine("Not a valid boolean input!");
                sunnyInput = Console.ReadLine(); 
            } 
            sunnyToList = bool.Parse(sunnyInput);

            Console.WriteLine("Was it cloudy? true/false");
            string cloudyInput = Console.ReadLine();
            while(!HelperFunctions.VerifyBoolInput(cloudyInput)) {
                Console.WriteLine("Not a valid boolean input!");
                cloudyInput = Console.ReadLine(); 
            } 
            cloudyToList = bool.Parse(cloudyInput);

            #endregion

        } catch (Exception e) {
            Console.WriteLine("Error in user input: " + e);
        }

        DateTime currentTime = DateTime.Now;
        DateTime nextHour = currentTime.AddHours(1);
        nextHour = new DateTime(nextHour.Year, nextHour.Month, nextHour.Day, nextHour.Hour, 0, 0);
        timeToList = nextHour.ToString("HH:mm:ss");
        Console.Clear();
        weatherLog.Add(new Day(){
                date = dateToList,
                day = dayToList,
                time = timeToList,
                airTemp = airTempToList,
                rainfall = rainfallToList, 
                wind = windToList,
                sunny = sunnyToList,
                cloudy = cloudyToList
            });    
        return weatherLog;
    
    }

    #endregion

    #region Add days to log 
    

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
        public string date {get; set;} = string.Empty;
        public string day {get; set;} = string.Empty;
        public string time {get; set;} = string.Empty;
        public int airTemp {get; set;}
        public double rainfall {get; set;}
        public double wind {get; set;}
        public bool sunny {get; set;}
        public bool cloudy {get; set;}
    }

    

}