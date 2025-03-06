using AsteroidsNasaDataAccess;
using Microsoft.AspNetCore.Mvc;
using NasaAplicationMappers.Services.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IAccessNasa, AccessNasa>();
builder.Services.AddHttpClient("Asteroids", c =>
{
    c.BaseAddress = new Uri("https://api.nasa.gov/");
});


builder.Services.AddOpenApi();
builder.Services.AddScoped<AccessNasa>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
    app.MapGet("/",() => Results.Redirect("/swagger"));
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapGet("/asteroids", async (int days,  IAccessNasa accessNasa) =>
{
    if (days < 1 || days > 7)
    {
        return Results.BadRequest("El parámetro 'days' debe estar entre 1 y 7.");
    }

    var asteroids = await accessNasa.GetAsteroids(days);

    if (asteroids == null || !asteroids.Any())
    {
        return Results.NotFound("No se encontraron asteroides peligrosos.");
    }
    var response = ModelModelMapper.Map(asteroids);
    return Results.Ok(response);
});
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
