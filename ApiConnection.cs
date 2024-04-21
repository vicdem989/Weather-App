using System.ComponentModel.DataAnnotations.Schema;
using program;
namespace apiconnection;

public class ApiConnection {
    
    public static async Task TestApi() {

        using (HttpClient client = new HttpClient()) {
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");
        
        //Need to add access header thingy

        try {
            HttpResponseMessage response = await client.GetAsync("status");

            if (response.IsSuccessStatusCode) {
                string responseBody = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(responseBody);
            } else {
                Console.WriteLine("Error if fail to retrieve data: " + response.StatusCode);
            }
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }

        }

          
    }


}