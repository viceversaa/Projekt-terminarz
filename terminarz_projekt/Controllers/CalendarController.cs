using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using terminarz_projekt.Data;
using terminarz_projekt.Models;
using terminarz_projekt.Migrations;
using terminarz_projekt.Sevices;
using System.Linq;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using Microsoft.EntityFrameworkCore;

namespace terminarz_projekt.Controllers
{ 
    public class CalendarController : Controller
    {
        private readonly TerminarzContext _context;

        public CalendarController(TerminarzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Search()
        {
            return _context.CalendarModel != null ?
                        View(await _context.CalendarModel.ToListAsync()) :
                        Problem("Entity set 'TerminarzContext.CalendarModel'  is null.");
        }
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

        private bool LessonData(string typ_zajec, string dzien_data)
        {
                return (_context.CalendarModel?.Any(e => e.typ_zajec == typ_zajec && e.dzien_data == dzien_data)).GetValueOrDefault();
                   
        }

        public IActionResult ProcessSearch(CalendarModel lekcje)
        { 

            if (LessonData(lekcje.typ_zajec, lekcje.dzien_data))
            {
                return View("Search", lekcje);
            }
            else
            {
                return View("Fail", lekcje);
            }
            

        }

        public IActionResult LessonList()
        {
            return View();
        }

    }
}
