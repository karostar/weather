using System.Text.Json;
using System.Linq;
using Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;




// Do poczytania: argumenty generyczne w typach klas, typ List<T>, Dictionary<T,K> i jak ich używamy
var colors = new Dictionary<string, string>();
colors.Add("red", "czerwony");
colors.Add("blue", "niebieski");

if (colors.ContainsKey("red"))
{
    Console.WriteLine(colors["red"]);
}

// Klasa Type
Type mojaKlasa = typeof(Dictionary<string, string>);
Console.WriteLine(mojaKlasa.Name);

var kontenerDI = new Dictionary<Type, object>(); // wartość musi dziedziczyć, implementować lub być typu klucz
kontenerDI.Add(typeof(IMeasurementSource), new RandomMeasurementSource());

var source = (IMeasurementSource) kontenerDI[typeof(IMeasurementSource)];

var numOfColors = new Dictionary<string, int>();
numOfColors["blue"] = 1;

var test = numOfColors["blue"] + 1;


// Do poczytania: interfejsy
var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<WeatherMeasurementsDb>(options => options.UseInMemoryDatabase("results"));
builder.Services.AddSingleton<IMeasurementSource, NullMeasurementSource>();
var app = builder.Build();

Random rand = new Random();

// Fajnie jak adres wskazuje nam konkretny zasób
// Dodajmy filtrowanie w przedziale czasu (from, to)
app.MapGet("/weather/measurements", async (WeatherMeasurementsDb db, [FromQuery] DateTime? from) => { 
    //returns entries by full date
    var data = from WeatherMeasurementsDb in db.weatherMeasurements
               where !@from.HasValue || WeatherMeasurementsDb.date > @from select WeatherMeasurementsDb;
    return data;
});

// Dodajmy adres dla pojedynczego odczytu: /weather/measurements/{id}



app.MapGet("/weather/measurements/current", async (IMeasurementSource source, WeatherMeasurementsDb db) => {  //creates a new entry
    var w = source.GetCurrentMeasurement();
    await db.weatherMeasurements.AddAsync(w);
    await db.SaveChangesAsync();
    return Results.Created($"/weather/{w.Id}", w);
});


app.MapGet("/weather/measurements/add", async (WeatherMeasurementsDb db) => {  //creates a new entry
    int year = rand.Next(2005, 2022);
    int month = rand.Next(1, 12);
    int day = rand.Next(1, 30);
    DateTime dateTime = new DateTime(year, month, day);
    WeatherMeasurements w = new WeatherMeasurements();
    w.date = dateTime;
    w.temperatureC = rand.Next(-20, 50);

    await db.weatherMeasurements.AddAsync(w);
    await db.SaveChangesAsync();
    return Results.Created($"/weather/{w.Id}", w);
});

app.Run();
