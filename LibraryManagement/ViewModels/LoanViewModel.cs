using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class LoanViewModel
{
    public int LoanId { get; set; }
    
    // Foreign Key and Navigation Property for User
    public string UserId { get; set; }
    [ForeignKey("Id")]
    public User User { get; set; }                   // Each loan is associated with one user

    // Foreign Key and Navigation Property for Book
    public int BookId { get; set; }
    [ForeignKey("BookId")]
    public Book Book { get; set; }                   // Each loan is associated with one book

    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    [Range(0, 2, ErrorMessage = "Status must be 0 (Active), 1 (Returned), or 2 (Overdue).")]
    public int Status { get; set; }
    
    
    public Loan ToLoan()
    {
        return new Loan
        {
            LoanId = LoanId,
            UserId = UserId,
            BookId = BookId,
            LoanDate = LoanDate,
            DueDate = DueDate,
            ReturnDate = ReturnDate,
            Status = Status
        };
    }
    
    // Update the Loan object with the values from the ViewModel
    public Loan UpdateLoan(Loan loan, LoanViewModel loanViewModel)
    {
        loan.LoanId = loanViewModel.LoanId;
        loan.UserId = loanViewModel.UserId;
        loan.BookId = loanViewModel.BookId;
        loan.LoanDate = loanViewModel.LoanDate;
        loan.DueDate = loanViewModel.DueDate;
        loan.ReturnDate = loanViewModel.ReturnDate;
        loan.Status = loanViewModel.Status;
        
        return loan;
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
