var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.MapGet("/", () => "Hello World");
app.MapGet("/bye", () => "Goodbye World");
app.Run();
