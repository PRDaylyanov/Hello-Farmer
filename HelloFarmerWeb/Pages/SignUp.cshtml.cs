using HelloFarmerWeb.BLL;
using HelloFarmerWeb.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HelloFarmerWeb.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly ILogger<SignUpModel> _logger;
        private readonly AccountService accountService = new AccountService();

        public SignUpModel(ILogger<SignUpModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public Account NewAccount { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    accountService.CreateAccount(NewAccount);

                    return RedirectToPage("Index");
                }
                catch (InvalidOperationException ex)
                {
                    Message = ex.Message;
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return Page();
        }
    }
}
