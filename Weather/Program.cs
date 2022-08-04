using System.Text.Json;
using Weather;

var builder = WebApplication.CreateBuilder();
//builder.Logging.AddJsonConsole();
//builder.Services.AddScoped<>();
var app = builder.Build();

List<Pogoda> pogoda = new List<Pogoda>
{
    new Pogoda{data=DateTime.Now, temperatura=22},
    new Pogoda{data=DateTime.Now.AddDays(1), temperatura=26},
    new Pogoda{data=DateTime.Now.AddDays(2), temperatura=21}
};

//string jsonString = JsonSerializer.Serialize(pogoda);

//Weather.Pogoda pogoda1 = JsonSerializer.Deserialize<Weather.Pogoda>(jsonString);

app.MapGet("/", () => pogoda);
app.MapPost("/add", (Pogoda p) => "$Dodano nową prognozę");
//app.MapGet("/", () => pogoda[1].data + Environment.NewLine + pogoda[1].temperatura + " stopni Celsjusza");
app.MapGet("/bye", () => "Goodbye World");
app.Run();
