using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
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

            //tworzy ciasteczko logowania i jego role
            Users currentUser = _context.Users.FirstOrDefault(u => u.Login == user.Login);
            if (currentUser != null)
            {
                Debug.WriteLine(currentUser.Id);
                Debug.WriteLine(currentUser.Login);
                Debug.WriteLine(currentUser.Password);
                Debug.WriteLine(currentUser.EmailAddress);
                Debug.WriteLine(currentUser.UserAccessLevel);
                var userClaims = new List<Claim>()
                {
                    //dodaje wpisy do ciasteczka
                    new Claim(ClaimTypes.Name, currentUser.Login),
                    new Claim(ClaimTypes.Email, "zleceniamafia2.0@wp.pl"),
              };
                userClaims.Add(new Claim(ClaimTypes.Role, currentUser.UserAccessLevel.ToString()));
                //dodaje wpis do ciasteczka o poziomie dostępu
                //if (currentUser.UserAccessLevel == 0)
                //{
                //    userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                //}
                //if (currentUser.UserAccessLevel == 1)
                //{
                //    userClaims.Add(new Claim(ClaimTypes.Role, "Employee"));
                //}
                //if (currentUser.UserAccessLevel == 2)
                //{
                //    userClaims.Add(new Claim(ClaimTypes.Role, "User"));
                //}

                var grandmaIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
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