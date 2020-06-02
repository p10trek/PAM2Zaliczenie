using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PAM2Zaliczenie.DAL;
using PAM2Zaliczenie.Models;

namespace PAM2Zaliczenie.Controllers
{
    public class LoginController : Controller
    {

        private readonly PAM_KillersDBContext _context;

        public LoginController(PAM_KillersDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserLogin([Bind] Users user)
        {

            var users = _context.Users.ToList();
            var allUsers = users.FirstOrDefault();
            //tworzy ciasteczko logowania w roli admina
            if (users.Any(u => u.Login == user.Login))
            {
                var userClaims = new List<Claim>()
                {
                    //dodaje wpisy do ciasteczka
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.Email, "zleceniamafia2.0@wp.pl"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var grandmaIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                //HttpContext.SignInAsync(userPrincipal);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}