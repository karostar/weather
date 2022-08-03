using System.Text.Json;

var builder = WebApplication.CreateBuilder();
//builder.Logging.AddJsonConsole();
//builder.Services.AddScoped<>();
var app = builder.Build();

var pogoda = new Weather.Pogoda
{
    data = DateTime.Now,
    temperatura = 29
};

string jsonString = JsonSerializer.Serialize(pogoda);

Weather.Pogoda pogoda1 =
                JsonSerializer.Deserialize<Weather.Pogoda>(jsonString);

//Console.WriteLine($"Data: {pogoda1.data}");
//Console.WriteLine($"Temperatura: {pogoda1.temperatura}");

//app.MapPost("/", () => "This is a POST");
//app.MapGet("/", () => "Hello World");
app.MapGet("/", () => pogoda1.data+ Environment.NewLine + pogoda1.temperatura+" stopni Celsjusza");
app.MapGet("/bye", () => "Goodbye World");
app.Run();
