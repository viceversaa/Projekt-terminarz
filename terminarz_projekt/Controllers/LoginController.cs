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

        public IActionResult ProcessLogin(UserModel userModel)
        {
            /*TerminarzContext terminarzContext = new TerminarzContext();*/
            SecurityService securityServive = new SecurityService();

            if(securityServive.IsValid(userModel))
            {
                return View("LoginSuccess", userModel);
            }
            else
            {
                return View("LoginFailure", userModel);
            }
        }
    }
}
