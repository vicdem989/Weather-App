using System.Text.Json;
using System.Text.Json.Serialization;

public class Program {

    static void Main(string[] args) {
        List<Data> _data = new List<Data>();
        _data.Add(new Data()
        {
            id = 1,
            SSN = 2,
            Message = "A Message"
        });

        _data.Add(new Data()
        {
            id = 13,
            SSN = 22,
            Message = "A Messdsadasdasdasdage"
        });

        _data.Add(new Data()
        {
            id = 121321,
            SSN = 12,
            Message = "A DASDADASD"
        });

        string json = JsonSerializer.Serialize(_data);
        File.WriteAllText("config.json", json);
    
    }
}

public class Data {
    public int id {get; set;}
    public int SSN {get; set;}
    public string Message {get; set;}
}