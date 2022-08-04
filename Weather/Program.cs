using System.Text.Json;
using System.Linq;
using Weather;

var builder = WebApplication.CreateBuilder();
//builder.Services.AddScoped<>();
var app = builder.Build();

Random rand = new Random();

List<WeatherMeasurements> measurements = new List<WeatherMeasurements>
{
    new WeatherMeasurements{date=new DateTime(2022,1,1), temperatureC=rand.Next(-20,50)}
};

app.MapGet("/measurements", () => measurements); //returns all entries

app.MapGet("/measurements/{year}/{month}/{day}", (int year, int month, int day) => { //returns entries by date
    
    foreach (var item in measurements)
    {
        
    }
    
    //bool has = measurements.Any(cus => cus.date == 2022);
    //measurements[id];
}); 

app.MapGet("/measurements/current", () => {  //creates a new entry
    DateTime dateTime = DateTime.Now;
    measurements.Add(new WeatherMeasurements{date=dateTime, temperatureC=rand.Next(-20,50)});
    return measurements[measurements.Count-1]; //returns current entry
});

app.Run();
