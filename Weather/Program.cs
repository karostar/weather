var builder = WebApplication.CreateBuilder();
//builder.Logging.AddJsonConsole();
//builder.Services.AddScoped<>();


var app = builder.Build();

app.MapPost("/", () => "This is a POST");
//app.MapGet("/", () => "Hello World");
app.MapGet("/bye", () => "Goodbye World");
app.Run();
