using Microsoft.AspNetCore.Builder;
using MiddlewareExample.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseMiddleware<LoginMiddleware>();

app.Map("/", () => "Welcome. Use Postman to send a POST request to /login, with a the following credentials: email = admin@example.com, password = admin1234");

app.Run();
