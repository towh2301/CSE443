using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers;

[Authorize]
public class LoanController(ILoanService loanService, IBookService bookService,ILogger<LoanController> logger) : Controller
{
    [HttpGet]
    public async Task<JsonResult> LoanList([FromQuery] string userId)
    {
        return await loanService.GetLoanByUserId(userId);
    }
    
    [HttpGet]
    public async Task<JsonResult> GetLoanDetails([FromQuery] string loanId)
    {
        return await loanService.GetLoanById(loanId);
    }
    
    [HttpPost]
    public async Task<JsonResult> CreateLoan([FromBody] LoanViewModel? loanViewModel)
    {
        if(loanViewModel == null)
        {
            return new JsonResult(new { success = false, message = "LoanViewModel is null in controller" });
        }
        return await loanService.CreateLoan(loanViewModel);
    }
    
    [HttpPost]
    [IgnoreAntiforgeryToken] // This is a public API so we don't need to check for CSRF
    public async Task<JsonResult> UpdateLoan([FromBody] LoanViewModel? loan)
    {
        if(loan == null)
        {
            return new JsonResult(new { success = false, message = "LoanViewModel is null in controller" });
        }
        return await loanService.UpdateLoanById(loan);
    }
    
    [HttpDelete]
    public async Task<JsonResult> DeleteLoan([FromQuery] string loanId)
    {
        return await loanService.DeleteLoanById(loanId);
    }
    
    [HttpGet]
    public async Task<JsonResult> GetAllLoans()
    {
        return await loanService.GetAllLoans();
    }
    
    [HttpGet]
    public async Task<List<Loan>> GetLoanList()
    {
        return await loanService.GetLoanList();
    }
    
}
