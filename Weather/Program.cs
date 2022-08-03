var builder = WebApplication.CreateBuilder();
//builder.Logging.AddJsonConsole();
//builder.Services.AddScoped<>();
var app = builder.Build();

var pogoda = new Weather.Pogoda
{
    data = DateTime.Now,
    temperatura = 29
};

//app.MapPost("/", () => "This is a POST");
//app.MapGet("/", () => "Hello World");
app.MapGet("/", () => pogoda);
app.MapGet("/bye", () => "Goodbye World");
app.Run();
