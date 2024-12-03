using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Interfaces;

public interface ILoanService
{
    Task<JsonResult> CreateLoan([FromBody] LoanViewModel loanViewModel);
    Task<JsonResult> UpdateLoanById([FromBody] LoanViewModel? loanViewModel);
    Task<JsonResult> DeleteLoanById([FromQuery] String loanId);
    Task<JsonResult> GetLoanById([FromQuery] String loanId);
    Task<JsonResult> GetAllLoans();
    
    Task<List<Loan>> GetLoanList();
    
    Task<JsonResult> GetLoanByUserId([FromQuery] String userId);
}
