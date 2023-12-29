using PowerPipe.Visualization.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<VisualizationMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
