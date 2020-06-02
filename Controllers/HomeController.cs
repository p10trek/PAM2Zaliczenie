using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAM2Zaliczenie.DAL;
using PAM2Zaliczenie.Email;
using PAM2Zaliczenie.Models;

namespace PAM2Zaliczenie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
            /*
            //ustawianie nadawcy/odbiorcy
            EmailAddress from = new EmailAddress();
            from.Name = "Nazwa nadawcy";
            from.Address = "adres email nadwacy";

            EmailAddress to = new EmailAddress();
            to.Name = "nazwa odbiorcy - do pobrania z bazy danych";
            to.Address = "adres odbiorcy - do pobrania z bazy danych";

            //tworzenie wiadomosci dodawanie nadawcy, odbiorcy, tematu i treści wiadomości
            EmailMessage mail = new EmailMessage();
            mail.FromAddress = from;
            mail.ToAddress = to;
            mail.Subject = "Testowy temat";
            mail.Content = "Treść wiadomości";

            //wysylka emaila
            emailService.Send(mail);
            */

        }

        public IActionResult Index()
        {
            PAM_KillersDBContext context = new PAM_KillersDBContext();
            return View();
        }

        //[Authorize(Roles ="Admin, User")] jeżeli wymaga logowania 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}