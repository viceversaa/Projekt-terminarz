using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using terminarz_projekt.Data;
using terminarz_projekt.Models;

namespace terminarz_projekt.Controllers
{ 
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            // Pobierz aktualną datę
            DateTime currentDate = DateTime.Today;

            // Przygotuj dane kalendarza do przesłania do widoku
            CalendarModel model = new CalendarModel();
            model.Month = currentDate.Month;
            model.Year = currentDate.Year;
            model.DaysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);

            return View(model);
        }

        public ActionResult ChangeDate(int year, int month)
        {
            // Przetwórz wybraną datę i wykonaj odpowiednie czynności
            // ...

            // Przekieruj żądanie HTTP z powrotem do strony kalendarza
            return RedirectToAction("Index");
        }

        

    }
}
