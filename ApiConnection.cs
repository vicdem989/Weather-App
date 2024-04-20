using System.ComponentModel.DataAnnotations.Schema;
using program;
namespace apiconnection;

public class ApiConnection {
    
    public static async void TestApi() {

        using (HttpClient client = new HttpClient()) {
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/airqualityforecast/0.1/");
        

        try {
            HttpResponseMessage response = await client.GetAsync("?lat=60&lon=10&areaclass=grunnkrets");

            if (response.IsSuccessStatusCode) {
                string responseBody = await response.Content.ReadAsStringAsync();
                
                Console.WriteLine(responseBody);
            } else {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        } catch (Exception e) {
            Console.WriteLine("Exception: " + e.Message);
        }

        }

          
    }


}