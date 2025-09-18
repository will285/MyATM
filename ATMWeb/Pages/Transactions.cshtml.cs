using ATMData;
using ATMData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ATMWeb.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly ATMContext _context;

        public List<Transaction> Transactions { get; set; } = new();

        public TransactionsModel(ATMContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var accountNumber = HttpContext.Session.GetInt32("AccountNumber");
            if (accountNumber == null)
                return RedirectToPage("/Index");

            Transactions = _context.Transactions
                .Where(t => t.FromAccountNumber == accountNumber || t.ToAccountNumber == accountNumber)
                .OrderByDescending(t => t.TransactionId)
                .Take(20)
                .ToList();

            return Page();
        }
    }
}