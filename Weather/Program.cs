using System.Text.Json;
using System.Linq;
using Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

//configuration
var builder = WebApplication.CreateBuilder();
builder.Services.AddDbContext<WeatherMeasurementDb>(options => 
options.UseSqlServer("Server=.\\SQLEXPRESS;Database=WeatherDatabase;Trusted_Connection=True;"));

builder.Services.AddSingleton<IMeasurementSource, MeasurementSource>();

var app = builder.Build();
Random rand = new Random();

//returns an entry by id
app.MapGet("/measurements/{id}", async (WeatherMeasurementDb db, int id) =>
{
    var data = from WeatherMeasurementsDb
               in db.WeatherMeasurements
               where WeatherMeasurementsDb.Id == id
               select WeatherMeasurementsDb;
    return data;
});

//returns historic entries starting with chosen date
app.MapGet("/measurements/from", async (WeatherMeasurementDb db, [FromQuery] DateTime? from) => 
{ 
    var data = from WeatherMeasurementsDb 
               in db.WeatherMeasurements 
               where !@from.HasValue || WeatherMeasurementsDb.Date > @from
               select WeatherMeasurementsDb;
    return data;
});

//returns historic entries between two dates
app.MapGet("/measurements/timeperiod", async (WeatherMeasurementDb db, [FromQuery] DateTime? from, 
    DateTime? to) =>
{
    var data = from WeatherMeasurementsDb
               in db.WeatherMeasurements
               where !@from.HasValue || !to.HasValue 
               || WeatherMeasurementsDb.Date > @from && WeatherMeasurementsDb.Date < to
               select WeatherMeasurementsDb;
    return data;
});

//returns historic entries before chosen date
app.MapGet("/measurements/to", async (WeatherMeasurementDb db, [FromQuery] DateTime? to) =>
{
    var data = from WeatherMeasurementsDb
               in db.WeatherMeasurements
               where !to.HasValue || WeatherMeasurementsDb.Date < to
               select WeatherMeasurementsDb;
    return data;
});

//creates a new entry
app.MapGet("/measurements/current", async (IMeasurementSource source, WeatherMeasurementDb db) => 
{
    var w = source.GetCurrentMeasurement();
    await db.WeatherMeasurements.AddAsync(w);
    await db.SaveChangesAsync();
    return Results.Created($"/weather/{w.Id}", w);
});

app.Run();
