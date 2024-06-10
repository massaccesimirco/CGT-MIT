using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using targheX.Areas.Identity.Data;

namespace targheX.Areas.Identity.Pages.Account
{
    [Authorize(Roles = "Admin")]
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<DeleteUserModel> _logger;

        public DeleteUserModel(UserManager<User> userManager, ILogger<DeleteUserModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public string UserName { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                ModelState.AddModelError(string.Empty, "Richiesta l'email dell'utente corretto");
                return Page();
            }

            var user = await _userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Utente non trovato");
                return Page();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("L'utente con email '{UserName}' è stato eliminato.", UserName);
                return RedirectToPage("UserList"); // redirect
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}

