using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MemeGenerator.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public async Task LogIn(IdentityUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}