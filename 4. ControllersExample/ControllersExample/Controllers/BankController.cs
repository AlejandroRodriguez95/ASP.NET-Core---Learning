using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Controllers;

public class BankController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private int _accNumber = 1001;
    private string _accHolderName = "Example Name";
    private long _accBalance = 5000;

    public BankController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    [Route("/account-details")]
    public IActionResult AccountDetails()
    {
        return Ok(new
        {
            AccountNumber = _accNumber,
            AccountHolderName = _accHolderName,
            AccountBalance = _accBalance
        });
    }

    [Route("/account-statement")]
    public IActionResult PrintPdf()
    {
        const string pdfVirtualPath = "pdfs/account-statement.pdf";
        var pdfFile = _environment.WebRootFileProvider.GetFileInfo(pdfVirtualPath);

        if (!pdfFile.Exists)
        {
            return new ContentResult
            {
                Content = "<h1>File not found</h1>",
                ContentType = "text/html",
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        return File($"/{pdfVirtualPath}", "application/pdf");
    }

    [Route("/get-current-balance/{accountNumber:int:min(1):max(10000)}")]
    public IActionResult GetCurrentBalance(int accountNumber)
    {
        if(accountNumber != _accNumber)
        {
            return NotFound(new { Message = "Account number not found." });
        }
        return Ok(new { Balance = _accBalance });
    }
}
