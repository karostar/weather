using WeatherSource;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

Random rand = new Random();

app.MapGet("/temperature", () =>
{
    DateTime dateTime = DateTime.Now;
    Temperature t = new Temperature();
    t.Date = dateTime;
    t.TemperatureC = rand.Next(-20, 50);
    return t;
});

app.Run();
