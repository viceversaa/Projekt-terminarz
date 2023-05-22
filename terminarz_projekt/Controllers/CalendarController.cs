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
using System.Globalization;

namespace terminarz_projekt.Controllers
{ 
    public class CalendarController : Controller
    {
        private readonly TerminarzContext _context;

        public CalendarController(TerminarzContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SearchLessonKierowniku()
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

        public async Task<IActionResult> ProcessSearch(CalendarModel lekcje)
        { 

            if (LessonData(lekcje.typ_zajec, lekcje.dzien_data))
            {
                return View("SearchLesson", await _context.CalendarModel.Where(e => e.typ_zajec == lekcje.typ_zajec && e.dzien_data == lekcje.dzien_data).ToListAsync());
            }
            else
            {
                return View("Fail", lekcje);
            }
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,typ_zajec,pracownik,dzien_data,godzina_od,godzina_do,status")] CalendarModel calendar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendar);
                await _context.SaveChangesAsync();
            }
            return View();
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CalendarModel == null)
            {
                return NotFound();
            }

            var calendar = await _context.CalendarModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }



        // GET: Register/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CalendarModel == null)
            {
                return NotFound();
            }

            var calendar = await _context.CalendarModel.FindAsync(id);
            if (calendar == null)
            {
                return NotFound();
            }
            return View(calendar);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,typ_zajec,pracownik,dzien_data,godzina_od,godzina_do,status")] CalendarModel calendar)
        {
            if (id != calendar.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(calendar.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(calendar);
        }

        // GET: Register/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CalendarModel == null)
            {
                return NotFound();
            }

            var calendar = await _context.CalendarModel
                .FirstOrDefaultAsync(m => m.ID == id);
            if (calendar == null)
            {
                return NotFound();
            }

            return View(calendar);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CalendarModel == null)
            {
                return Problem("Entity set 'TerminarzContext.Osoby'  is null.");
            }
            var calendar = await _context.CalendarModel.FindAsync(id);
            if (calendar != null)
            {
                _context.CalendarModel.Remove(calendar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SearchLessonKierowniku));
        }


        private bool EventExists(int id)
        {
            return (_context.CalendarModel?.Any(e => e.ID == id)).GetValueOrDefault();

        }

        

    }
}
