using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> availableCountries = new()
{
    {1, "United States"},
    {2, "Canada"},
    {3, "United Kingdom"},
    {4, "India"},
    {5, "Japan"}
};

app.MapGet("/countries", async (context) =>
{
    foreach (var country in availableCountries)
    {
        await context.Response.Body.WriteAsync(System.Text.Encoding.UTF8.GetBytes($"{country.Key}, {country.Value}\n"));
    }
});

app.MapGet("/countries/{id:int}", async (HttpContext context, int id) =>
{
    if (availableCountries.TryGetValue(id, out var country))
    {
        await context.Response.Body.WriteAsync(System.Text.Encoding.UTF8.GetBytes($"{id}, {country}\n"));
    }
    else
    {
        Results.NotFound($"Country with ID {id} not found.");
        await context.Response.Body.WriteAsync(System.Text.Encoding.UTF8.GetBytes($"Country with ID {id} not found.\n"));
    }
});

app.Run();  