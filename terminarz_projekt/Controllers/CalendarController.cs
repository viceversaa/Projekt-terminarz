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
    /// <summary>
    /// Kontroler obsługujący model CalendarModel
    /// </summary>
    public class CalendarController : Controller
    {
        private readonly TerminarzContext _context;

        public CalendarController(TerminarzContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Funkcja wypisujaca lekcje zapisane do bazy.
        /// </summary>
        /// <returns>Widok listy lekcji.</returns>
        public async Task<IActionResult> SearchLessonKierowniku()
        {
            return _context.CalendarModel != null ?
                        View(await _context.CalendarModel.ToListAsync()) :
                        Problem("Entity set 'TerminarzContext.CalendarModel'  is null.");
        }

        /// <summary>
        /// Funkcja tworzaca dane do kalendarza.
        /// </summary>
        /// <returns>Widok stworzonego modelu kalendarza.</returns>
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


        // ?????????????????????????????????????????????????????????????????????????????????????????????????????????????????
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns>Przekierowanie do strony kalendarza.</returns>
        public ActionResult ChangeDate(int year, int month)
        {
            // Przetwórz wybraną datę i wykonaj odpowiednie czynności
            // ...

            // Przekieruj żądanie HTTP z powrotem do strony kalendarza
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Funkcja wyszukujaca dostepnosc lekcji na podstawie wprowadzonych danych.
        /// </summary>
        /// <param name="typ_zajec">Zmienna przechowujaca wprowadzony typ zajec.</param>
        /// <param name="dzien_data">Zmienna przechowujaca wybrana date.</param>
        /// <returns>Zwraca wartosc biezacego obiektu.</returns>
        private bool LessonData(string typ_zajec, string dzien_data)
        {
            return (_context.CalendarModel?.Any(e => e.typ_zajec == typ_zajec && e.dzien_data == dzien_data)).GetValueOrDefault();

        }

        /// <summary>
        /// Funkcja przeszukujaca w bazie obiekty.
        /// </summary>
        /// <param name="lekcje">Obiekt szukany</param>
        /// <returns>Widok w postaci listy znalezionych wyszukiwan badz blad o braku wynikow.</returns>
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

        /// <summary>
        /// Funkcja dodajaca nowy obiekt klasy CalendarModel.
        /// </summary>
        /// <param name="calendar">Nowy obiekt klasy.</param>
        /// <returns>Widok po stworzeniu obiektu.</returns>
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


        /// <summary>
        /// Funkcja wyswietlajaca szczegoly obiektu o zadanym ID.
        /// </summary>
        /// <param name="id">ID obiektu, ktory chcemy wyswietlic.</param>
        /// <returns>Widok rekordu o zadanym ID lub widok, ze dany rekord nie zostal odnaleziony.</returns>
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

        /// <summary>
        /// Funkcja przejscia do widoku Create.
        /// </summary>
        /// <returns>Zwraca widok do Create w folderze Calendar</returns>
        public IActionResult Create()
        {
            return View();
        }

        //?????????????????????????????????????????????????????????????????????????????????????????????????
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID rekordu poddanemu edycji.</param>
        /// <returns>Widok kalendarza.</returns>
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


        /// <summary>
        /// Funkcja uaktualniajaca obiekt o zadanym ID w modelu CalendarModel.
        /// </summary>
        /// <param name="id">ID rekordu do aktualizacji.</param>
        /// <param name="calendar">Zmienna przypisana do modelu.</param>
        /// <returns>Przekierowanie do Indexu lub widok kalendarza.</returns>
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


        /// <summary>
        /// Funkcja przekierowujaca do operacji usuniecia obiektu o danym ID.
        /// </summary>
        /// <param name="id">ID rekordu do usuniecia.</param>
        /// <returns>Widok kalendarza.</returns>
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


        /// <summary>
        /// Funkcja usuwajaca obiekt o zadanym ID.
        /// </summary>
        /// <param name="id">ID obiektu do usuniecia.</param>
        /// <returns>Przekierowanie do funkcji SearchLessonKierowniku.</returns>
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


        /// <summary>
        /// Funkcja sprawdzajaca czy istnieje wydarzenie o danym ID.
        /// </summary>
        /// <param name="id">ID obiektu wyszukiwanego.</param>
        /// <returns>Zwraca wartosc biezacego obiektu.</returns>
        private bool EventExists(int id)
        {
            return (_context.CalendarModel?.Any(e => e.ID == id)).GetValueOrDefault();

        }



    }
}
