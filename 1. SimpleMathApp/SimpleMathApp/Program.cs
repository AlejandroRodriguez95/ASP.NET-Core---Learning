using Microsoft.AspNetCore.WebUtilities;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async (context) =>
{
    var query = context.Request.QueryString.ToString();
    var dict = QueryHelpers.ParseQuery(query);


    if (dict.ContainsKey("operation") && 
        dict.ContainsKey("firstNumber") &&
        dict.TryGetValue("secondNumber", out var value))
    {
        var operation = dict["operation"][0];

        if (int.TryParse(dict["firstNumber"][0], out int firstNumber) &&
            int.TryParse(value[0], out int secondNumber))
        {
            int result = 0;
            bool success = false;
            switch (operation)
            {
                case "add":
                    result = firstNumber + secondNumber;
                    success = true;
                    break;
                case "multiply":
                    result = firstNumber * secondNumber;
                    success = true;
                    break;
                case "subtract":
                    result = firstNumber - secondNumber;
                    success = true;
                    break;
                case "divide":
                    result = firstNumber / secondNumber;
                    success = true;
                    break;
            }

            if (success)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(result.ToString());
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid operator provided.");
            }
        }
    }
    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid input.");
    }
});

app.Run();