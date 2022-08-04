using System.Text.Json;

var builder = WebApplication.CreateBuilder();
//builder.Logging.AddJsonConsole();
//builder.Services.AddScoped<>();
var app = builder.Build();

List<Weather.Pogoda> pogoda = new List<Weather.Pogoda>
{
    new Weather.Pogoda{data=DateTime.Now, temperatura=22},
    new Weather.Pogoda{data=DateTime.Now.AddDays(1), temperatura=26},
    new Weather.Pogoda{data=DateTime.Now.AddDays(2), temperatura=21}
};

//string jsonString = JsonSerializer.Serialize(pogoda);

//Weather.Pogoda pogoda1 = JsonSerializer.Deserialize<Weather.Pogoda>(jsonString);

//app.MapPost("/", () => "This is a POST");
app.MapGet("/", () => pogoda);
//app.MapGet("/", () => pogoda[1].data + Environment.NewLine + pogoda[1].temperatura + " stopni Celsjusza");
app.MapGet("/bye", () => "Goodbye World");
app.Run();
