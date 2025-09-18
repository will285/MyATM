using ATMData.Data;
using ATMData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMWeb.Pages
{
    public class AccountHomeModel : PageModel
    {
        private readonly ATMContext _context;

        public Account Account { get; set; }
        public List<Transaction> Transactions { get; set; } = new();

        public AccountHomeModel(ATMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? accountNumber)
        {
            // Try to get account number from query or session
            if (accountNumber == null)
            {
                accountNumber = HttpContext.Session.GetInt32("AccountNumber");
            }
            if (accountNumber == null)
            {
                return RedirectToPage("/Login");
            }

            Account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (Account == null)
            {
                return RedirectToPage("/Login");
            }

            Transactions = _context.Transactions
                .Where(t => t.FromAccountNumber == accountNumber || t.ToAccountNumber == accountNumber)
                .OrderByDescending(t => t.TransactionId)
                .Take(10)
                .ToList();

            return Page();
        }
    }
}
