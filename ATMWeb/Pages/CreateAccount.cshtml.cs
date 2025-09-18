using ATMData;
using ATMData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ATMWeb.Pages
{
    public class CreateAccountModel : PageModel
    {
        private readonly ATMContext _context;

        [BindProperty]
        public Account NewAccount { get; set; }

        public string? ErrorMessage { get; set; }

        public CreateAccountModel(ATMContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please correct the errors and try again.";
                return Page();
            }

            _context.Accounts.Add(NewAccount);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}