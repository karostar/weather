using System.Text.Json;
using System.Linq;
using Weather;

var builder = WebApplication.CreateBuilder();
//builder.Services.AddScoped<>();
var app = builder.Build();

Random rand = new Random();

List<WeatherMeasurements> measurements = new List<WeatherMeasurements>
{
    new WeatherMeasurements{date=new DateTime(2022,1,1), temperatureC=rand.Next(-20,50)},
    new WeatherMeasurements{date=new DateTime(2021,10,2), temperatureC=rand.Next(-20,50)},
    new WeatherMeasurements{date=new DateTime(2021,10,2), temperatureC=rand.Next(-20,50)},
    new WeatherMeasurements{date=new DateTime(2021,10,3), temperatureC=rand.Next(-20,50)}
};

app.MapGet("/weather", () => measurements); //returns all entries

app.MapGet("/weather/{year}/{month}/{day}", (int year, int month, int day) => { //returns entries by full date
    DateTime dateTime = new DateTime(year, month, day);
    var dateOnly=dateTime.ToShortDateString();
    List<WeatherMeasurements> tempMeasurements = new List<WeatherMeasurements> { };

    foreach (var item in measurements)
    {
        if (item.date.ToShortDateString() == dateOnly)
            tempMeasurements.Add(item);
    }
    return tempMeasurements;
});

app.MapGet("/weather/{year}/{month}", (int year, int month) => { //returns entries by month
    List<WeatherMeasurements> tempMeasurements = new List<WeatherMeasurements> { };

    foreach (var item in measurements)
    {
        if (item.date.Year == year && item.date.Month==month)
            tempMeasurements.Add(item);
    }
    return tempMeasurements;
});

app.MapGet("/weather/{year}", (int year) => { //returns entries by year
    List<WeatherMeasurements> tempMeasurements = new List<WeatherMeasurements> { };

    foreach (var item in measurements)
    {
        if (item.date.Year == year)
            tempMeasurements.Add(item);
    }
    return tempMeasurements;
});

app.MapGet("/weather/current", () => {  //creates a new entry
    DateTime dateTime = DateTime.Now;
    measurements.Add(new WeatherMeasurements{date=dateTime, temperatureC=rand.Next(-20,50)});
    return measurements[measurements.Count-1]; //returns current entry
});

app.Run();
