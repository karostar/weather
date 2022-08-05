using System.Text.Json;
using System.Linq;
using Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<WeatherMeasurementsDb>(options => options.UseInMemoryDatabase("results"));
var app = builder.Build();

Random rand = new Random();

// Test
//app.MapGet("/weather", async (WeatherMeasurementsDb db) => await db.weatherMeasurements.Select(x => new WeatherMeasurementsDTO(x)).ToListAsync()); //returns all entries

app.MapGet("/weather", async (WeatherMeasurementsDb db, [FromQuery] DateTime? from) => { //returns entries by full date
    var data = from WeatherMeasurementsDb in db.weatherMeasurements
               where !@from.HasValue || WeatherMeasurementsDb.date > @from select WeatherMeasurementsDb;
    return data;
});

app.MapGet("/weather/current", async (WeatherMeasurementsDb db) => {  //creates a new entry
    DateTime dateTime = DateTime.Now;
    WeatherMeasurements w = new WeatherMeasurements();
    w.date = dateTime;
    w.temperatureC = rand.Next(-20, 50);

    await db.weatherMeasurements.AddAsync(w);
    await db.SaveChangesAsync();
    return Results.Created($"/weather/{w.Id}", w);
});

app.MapGet("/weather/add", async (WeatherMeasurementsDb db) => {  //creates a new entry
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
