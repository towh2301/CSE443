using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class LoanService(ILoanRepository loanRepository, ApplicationDbContext context) : ILoanService
{
    public async Task<JsonResult> CreateLoan([FromBody] LoanViewModel? loanViewModel)
    {
        if(loanViewModel == null)
        {
            return new JsonResult(new { success = false, message = "LoanViewModel is null" });
        }
        var loan = loanViewModel.ToLoan();
        
        await context.Loan.AddAsync(loan);
        await context.SaveChangesAsync();
        
        return new JsonResult(new { success = true, loan });
    }

    public async Task<JsonResult> UpdateLoanById([FromBody] LoanViewModel? loanViewModel)
    {
        if(loanViewModel == null)
        {
            return new JsonResult(new { success = false, message = "LoanViewModel is null" });
        }
        
        var loan = await context.Loan.FindAsync(loanViewModel.LoanId);
        if(loan == null)
        {
            return new JsonResult(new { success = false, message = "Loan not found" });
        }
        
        loan = loanViewModel.UpdateLoan(loan, loanViewModel);
        
        context.Loan.Update(loan);
        await context.SaveChangesAsync();
        
        return new JsonResult(new { success = true, loan });
    }

    public async Task<JsonResult> DeleteLoanById([FromQuery] string loanId)
    {
        var loan = await context.Loan.FindAsync(int.Parse(loanId));
        if(loan == null)
        {
            return new JsonResult(new { success = false, message = "Loan not found" });
        }
        
        context.Loan.Remove(loan);
        await context.SaveChangesAsync();
        
        return new JsonResult(new { success = true, loan });
    }

    public async Task<JsonResult> GetLoanById([FromQuery]string loanId)
    {
        var loan = await context.Loan.FindAsync(int.Parse(loanId));
        return loan == null ? new JsonResult(new { success = false, message = "Loan not found" }) : new JsonResult(new { success = true, loan });
    }

    public async Task<JsonResult> GetAllLoans()
    {
        var loans = await context.Loan.ToListAsync();
        return new JsonResult(new { success = true, loans });
    }

    public async Task<List<Loan>> GetLoanList()
    {
        return await context.Loan.ToListAsync();
    }

    public async Task<JsonResult> GetLoanByUserId(string userId)
    {
        var loans = await context.Loan.Where(l => l.UserId == userId).ToListAsync();
        return new JsonResult(new { success = true, loans });
    }
}
