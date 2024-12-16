using HelloFarmerWeb.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HelloFarmerWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly AccountService accountService = new AccountService();

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var account = accountService.ValidateLogin(Email, Password);

                if (account != null)
                {
                    accountService.SetCurrentAccount(account);
                    return RedirectToPage("Index");
                }
                else
                {
                    Message = "Invalid email or password.";
                }
            }

            return Page();
        }
    }
}
