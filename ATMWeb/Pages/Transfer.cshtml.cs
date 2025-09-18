using ATMData;
using ATMData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMWeb.Pages
{
    public class TransferModel : PageModel
    {
        private readonly ATMContext _context;

        [BindProperty]
        public int ToAccountNumber { get; set; }

        [BindProperty]
        public double Amount { get; set; }

        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public TransferModel(ATMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var accountNumber = HttpContext.Session.GetInt32("AccountNumber");
            if (accountNumber == null)
                return RedirectToPage("/Index");

            FromAccount = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (FromAccount == null)
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost()
        {
            var accountNumber = HttpContext.Session.GetInt32("AccountNumber");
            if (accountNumber == null)
            {
                ErrorMessage = "Session expired. Please log in again.";
                return Page();
            }

            FromAccount = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (FromAccount == null)
            {
                ErrorMessage = "Source account not found.";
                return Page();
            }

            ToAccount = _context.Accounts.FirstOrDefault(a => a.AccountNumber == ToAccountNumber);
            if (ToAccount == null)
            {
                ErrorMessage = "Destination account not found.";
                return Page();
            }

            if (Amount <= 0)
            {
                ErrorMessage = "Transfer amount must be greater than zero.";
                return Page();
            }

            if (FromAccount.Balance < Amount)
            {
                ErrorMessage = "Insufficient funds.";
                return Page();
            }

            // Perform transfer
            FromAccount.Balance -= Amount;
            ToAccount.Balance += Amount;

            _context.Transactions.Add(new Transaction
            {
                IsDeposit = false,
                IsTransfer = true,
                Amount = Amount,
                FromAccountNumber = FromAccount.AccountNumber,
                ToAccountNumber = ToAccount.AccountNumber
            });

            _context.SaveChanges();

            SuccessMessage = $"Successfully transferred ${Amount} to account {ToAccountNumber}.";
            return Page();
        }
    }
}