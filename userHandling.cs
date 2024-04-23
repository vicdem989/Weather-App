using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using jsonhandling;

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
    public static string timeToList = string.Empty;
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

    public static void AddSingularEntry() {
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

                while(!VerifyDateFormat(dateInput)) {
                    Console.WriteLine("Input correct format for date, dd/mm/yyyy");
                    dateInput = Console.ReadLine().ToLower();
                }

                dateToList = dateInput;

                if(DateTime.TryParseExact(dateInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date)) {
                    dayToList = date.DayOfWeek.ToString();
                } 
            }

            #region Date user input
        
            Console.WriteLine("What is the higest temp(c) of the day?");
            string highestTempInput = Console.ReadLine();
            while(!VerifyIntInput(highestTempInput)) {
                Console.WriteLine("Not a valid int input!");
                highestTempInput = Console.ReadLine(); 
            } 
            highestTempToList = int.Parse(highestTempInput);

            Console.WriteLine("What is the lowest temp(c) of the day?");
            string lowestTempInput = Console.ReadLine();
            while(!VerifyIntInput(lowestTempInput)) {
                Console.WriteLine("Not a valid int input!");
                lowestTempInput = Console.ReadLine(); 
            } 
            lowestTempToList = int.Parse(lowestTempInput);

            Console.WriteLine("How much rainfall(mm) was it today?");
            string rainfallInput = Console.ReadLine();
            while(!VerifyIntInput(rainfallInput)) {
                Console.WriteLine("Not a valid double input!");
                rainfallInput = Console.ReadLine(); 
            } 
            rainfallToList = int.Parse(rainfallInput);

            Console.WriteLine("How much wind(m/s) was it today?");
            string windInput = Console.ReadLine();
            while(!VerifyIntInput(windInput)) {
                Console.WriteLine("Not a valid double input!");
                windInput = Console.ReadLine(); 
            } 
            windToList = int.Parse(windInput);

            Console.WriteLine("Was it sunny? true/false");
            string sunnyInput = Console.ReadLine();
            while(!VerifyBoolInput(sunnyInput)) {
                Console.WriteLine("Not a valid boolean input!");
                sunnyInput = Console.ReadLine(); 
            } 
            sunnyToList = bool.Parse(sunnyInput);

            Console.WriteLine("Was it cloudy? true/false");
            string cloudyInput = Console.ReadLine();
            while(!VerifyBoolInput(cloudyInput)) {
                Console.WriteLine("Not a valid boolean input!");
                cloudyInput = Console.ReadLine(); 
            } 
            cloudyToList = bool.Parse(cloudyInput);

            #endregion

        } catch (Exception e) {
            Console.WriteLine("Error in user input: " + e);
        }

        /// When adding new entry to log
        /// Include time of entry
        /// Get time from API and compare JSON data with inputted data from that time (hour)

        //TimeSpan currentTime = DateTime.Now.TimeOfDay;
        //timeToList = currentTime.ToString();
        timeToList = string.Empty;


        OutputDataToJson(dateToList, dayToList, timeToList, highestTempToList, lowestTempToList, rainfallToList, windToList, sunnyToList, cloudyToList);
    }

    public static void OutputDataToJson(string dateInput, string dayInput, string timeInput, int higestTempInput, int lowerTempInput, double rainfallInput, double windInput, bool sunnyInput, bool cloudyInput) {
        try {
            weatherLog.Add(new Day(){
                date = dateInput,
                day = dayInput,
                time = timeInput,
                highestTemp = higestTempInput,
                lowestTemp = lowerTempInput,
                rainfall = rainfallInput, 
                wind = windInput,
                sunny = sunnyInput,
                cloudy = cloudyInput
            });

            JsonHandling.OutputDataToJson(weatherLog);

        } catch (Exception e) {
            Console.WriteLine("Error when outputting to json file", e);
        }

        
    }

    #endregion

    #region Helper functions
    public static bool VerifyDateFormat(string date) {
        return date.Length == 10 && date[2] == '/' && date[5] == '/';
    }

    public static bool VerifyIntInput(string dataToBeVerified) {
        if(int.TryParse(dataToBeVerified, out int value)) {
            return true;
        }
        return false;
    }

    public static bool VerifyBoolInput(string dataToBeVerified) {
        if(bool.TryParse(dataToBeVerified, out bool value)) {
            return true;
        }
        return false;
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
        public string date {get; set;} = string.Empty;
        public string day {get; set;} = string.Empty;
        public string time {get; set;} = string.Empty;
        public int highestTemp {get; set;}
        public int lowestTemp {get; set;}
        public double rainfall {get; set;}
        public double wind {get; set;}
        public bool sunny {get; set;}
        public bool cloudy {get; set;}
    }

    

}