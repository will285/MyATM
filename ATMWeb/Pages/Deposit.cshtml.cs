using ATMData;
using ATMData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMWeb.Pages
{
    public class DepositModel : PageModel
    {
        private readonly ATMContext _context;

        [BindProperty]
        public double Amount { get; set; }

        public Account Account { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public DepositModel(ATMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? accountNumber)
        {
            if (accountNumber == null)
                accountNumber = HttpContext.Session.GetInt32("AccountNumber");

            if (accountNumber == null)
                return RedirectToPage("/Index");

            Account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (Account == null)
                return RedirectToPage("/Index");

            return Page();
        }

        public IActionResult OnPost(int? accountNumber)
        {
            if (accountNumber == null)
                accountNumber = HttpContext.Session.GetInt32("AccountNumber");

            if (accountNumber == null)
            {
                ErrorMessage = "Account not found.";
                return Page();
            }

            Account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (Account == null)
            {
                ErrorMessage = "Account not found.";
                return Page();
            }

            if (Amount <= 0)
            {
                ErrorMessage = "Deposit amount must be greater than zero.";
                return Page();
            }

            Account.Balance += Amount;
            _context.Transactions.Add(new Transaction
            {
                IsDeposit = true,
                IsTransfer = false,
                Amount = Amount,
                FromAccountNumber = Account.AccountNumber
            });
            _context.SaveChanges();

            SuccessMessage = $"Successfully deposited ${Amount}!";
            return Page();
        }
    }
}