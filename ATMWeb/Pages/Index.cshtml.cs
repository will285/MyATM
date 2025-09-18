using ATMData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ATMContext _context;

        [BindProperty]
        public int AccountNumber { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel(ATMContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            var account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == AccountNumber);
            // Replace this with your actual authentication logic
            if (account != null)
            {
                HttpContext.Session.SetInt32("AccountNumber", account.AccountNumber);
                return RedirectToPage("/AccountHome", new { accountNumber = account.AccountNumber });
            }

            ErrorMessage = "Invalid Account Number.";
            return Page();
        }

        public void OnGet()
        {

        }
    }
}
