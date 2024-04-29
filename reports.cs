namespace reports;
using apiconnection;
using userHandling;
using jsonhandling;
using System.Runtime;

public class Reports {

    public static void reportDay(List<UserHandling.Day> weatherLog, string dateToBeReported) {
        foreach(UserHandling.Day day in weatherLog) {
            if(day.date == dateToBeReported) {
                Console.WriteLine($"Report for {day.date}:");
                Console.WriteLine($"Day: {day.day}, Time: {day.time}, Air temperature: {day.airTemp}c, Rainfall: {day.rainfall}mm, Wind: {day.wind}m/s, Sunny: {day.sunny}, Cloudy: {day.cloudy}");
                return;
            }
        }
        Console.WriteLine($"{dateToBeReported} does not exist in the log?!");
        Console.WriteLine("Do you want to add a new date to the log? yes/no");
        if(Console.ReadLine().ToLower() == "yes") {
            UserHandling.AddEntryToLog();
        }
    }

    public static void reportPastWeek(List<UserHandling.Day> weatherLog) { 
        int daysToReport = 7;
        if(weatherLog.Count < daysToReport) {
            Console.WriteLine($"There are not seven entries, only {weatherLog.Count}.");
            Console.WriteLine("Do you want the report of those days? yes/no");
            string userResponse = Console.ReadLine().ToLower();
            if(userResponse == "yes") {
                daysToReport = weatherLog.Count;
            }
        }
        
        Console.Clear();
        Console.WriteLine($"Report for the past {daysToReport} days:");
        for (int i = 0; i < daysToReport; i++) {
            Console.WriteLine($"Date: {weatherLog[i].date}, Day: {weatherLog[i].day}, Time: {weatherLog[i].time}, Air temperature: {weatherLog[i].airTemp}, Rainfall: {weatherLog[i].rainfall}mm, Wind: {weatherLog[i].wind}m/s, Sunny: {weatherLog[i].sunny}, Cloudy: {weatherLog[i].cloudy}");
        }
    }

    public static void reportPastXDays(List<UserHandling.Day> weatherLog, int amountDays) {
        int amountOfDaysToBeReported = amountDays;
        if(weatherLog.Count < amountDays) {
            Console.WriteLine($"There is not {amountDays} days worth of entry, only {weatherLog.Count}!");
            Console.WriteLine($"Do you want a report for the past {weatherLog.Count} days? yes/no");
            if(Console.ReadLine().ToLower() == "no") {
                return;
            } else {
                amountOfDaysToBeReported = weatherLog.Count;
            }
        }
        Console.WriteLine($"Report for the past {amountOfDaysToBeReported} days:");
        for (int i = 0; i < amountOfDaysToBeReported; i++) {
            Console.WriteLine($"Date: {weatherLog[i].date}, Day: {weatherLog[i].day}, Time: {weatherLog[i].time}, Air temperature: {weatherLog[i].airTemp}, Rainfall: {weatherLog[i].rainfall}mm, Wind: {weatherLog[i].wind}m/s, Sunny: {weatherLog[i].sunny}, Cloudy: {weatherLog[i].cloudy}");
        }
    }

}