using Microsoft.AspNetCore.Mvc;
using terminarz_projekt.Data;
using terminarz_projekt.Models;
using terminarz_projekt.Sevices;

namespace terminarz_projekt.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessLogin(Osoby osoby)
        {
            SecurityService securityServive = new SecurityService();

            if(securityServive.IsValid(osoby))
            {
                return View("LoginSuccess", osoby);
            }
            else
            {
                return View("LoginFailure", osoby);
            }
        }
    }
}
