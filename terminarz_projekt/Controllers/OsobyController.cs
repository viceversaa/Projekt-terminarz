using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using terminarz_projekt.Data;
using terminarz_projekt.Models;

namespace terminarz_projekt.Controllers
{
    /// <summary>
    /// Kontroler dla modelu Osoby.
    /// </summary>
    public class OsobyController : Controller
    {
        private readonly TerminarzContext _context;

        public OsobyController(TerminarzContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Funkcja wyswietlajaca liste osob w bazie.
        /// </summary>
        /// <returns>Widok listy na temat osob.</returns>
        public async Task<IActionResult> Index()
        {
              return _context.Osoby != null ? 
                          View(await _context.Osoby.ToListAsync()) :
                          Problem("Entity set 'TerminarzContext.Osoby'  is null.");
        }

        /// <summary>
        /// Funkcja wyswietlajaca informacje o osobie o zadanym ID.
        /// </summary>
        /// <param name="id">ID szukanego modelu.</param>
        /// <returns>Widok informacji na temat danej osoby.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var osoby = await _context.Osoby
                .FirstOrDefaultAsync(m => m.ID == id);
            if (osoby == null)
            {
                return NotFound();
            }

            return View(osoby);
        }

        /// <summary>
        /// Funkcja przejscia do widoku Create.
        /// </summary>
        /// <returns>Zwraca widok do Create w folderze Osoby.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Funkcja dodajaca nowy obiekt klasy Osoby.
        /// </summary>
        /// <param name="osoby">Nowy obiekt klasy.</param>
        /// <returns>Widok po stworzeniu obiektu.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Imie,Nazwisko,drugie_imie,plec,data_urodzenia,nr_telefonu,email,Hasło")] Osoby osoby)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osoby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(osoby);
        }

        //?????????????????????????????????????????????????????????????????????????????????????????????????
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID rekordu poddanemu edycji.</param>
        /// <returns>Widok osoby.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var osoby = await _context.Osoby.FindAsync(id);
            if (osoby == null)
            {
                return NotFound();
            }
            return View(osoby);
        }

        /// <summary>
        /// Funkcja uaktualniajaca obiekt o zadanym ID w modelu Osoby.
        /// </summary>
        /// <param name="id">ID rekordu do aktualizacji.</param>
        /// <param name="osoby">Zmienna przypisana do modelu.</param>
        /// <returns>Przekierowanie do Indexu lub widok osoby.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Imie,Nazwisko,drugie_imie,plec,data_urodzenia,nr_telefonu,email,Hasło")] Osoby osoby)
        {
            if (id != osoby.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(osoby);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OsobyExists(osoby.ID))
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
            return View(osoby);
        }

        /// <summary>
        /// Funkcja przekierowujaca do operacji usuniecia obiektu o danym ID.
        /// </summary>
        /// <param name="id">ID rekordu do usuniecia.</param>
        /// <returns>Widok osoby.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Osoby == null)
            {
                return NotFound();
            }

            var osoby = await _context.Osoby
                .FirstOrDefaultAsync(m => m.ID == id);
            if (osoby == null)
            {
                return NotFound();
            }

            return View(osoby);
        }

        /// <summary>
        /// Funkcja usuwajaca obiekt o zadanym ID.
        /// </summary>
        /// <param name="id">ID obiektu do usuniecia.</param>
        /// <returns>Przekierowanie do funkcji Index.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Osoby == null)
            {
                return Problem("Entity set 'TerminarzContext.Osoby'  is null.");
            }
            var osoby = await _context.Osoby.FindAsync(id);
            if (osoby != null)
            {
                _context.Osoby.Remove(osoby);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Funkcja sprawdzajaca czy istnieje osoba o danym ID.
        /// </summary>
        /// <param name="id">ID obiektu wyszukiwanego.</param>
        /// <returns>Zwraca wartosc True/False czy dany obiekt istnieje.</returns>
        private bool OsobyExists(int id)
        {
          return (_context.Osoby?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
