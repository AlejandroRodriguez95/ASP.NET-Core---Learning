using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace MiddlewareExample.Middleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post && context.Request.Path == "/login")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();

            var parsed = QueryHelpers.ParseQuery(body);

            string? email = parsed.TryGetValue("email", out StringValues emailValue)
                ? emailValue.ToString()
                : null;

            string? password = parsed.TryGetValue("password", out StringValues passwordValue)
                ? passwordValue.ToString()
                : null;

            if (email == "admin@example.com" && password == "admin1234")
            {
                await context.Response.WriteAsync($"Successful login on email {email}");
                return;
            }

            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid login");
            return;
        }

        await _next(context);
    }
}