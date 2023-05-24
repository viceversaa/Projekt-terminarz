using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using terminarz_projekt.Data;
using terminarz_projekt.Models;
using System.Linq;
using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;

namespace terminarz_projekt.Controllers
{
    public class RegisterController : Controller
    {
        private readonly TerminarzContext _context;

        public RegisterController(TerminarzContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Funkcja przejscia do widoku Create.
        /// </summary>
        /// <returns>Zwraca widok do LoginPanel w folderze Register.</returns>
        public IActionResult LoginPanel()
        {
            return View();
        }

        /// <summary>
        /// Funkcja przejscia do widoku Create.
        /// </summary>
        /// <returns>Zwraca widok do LoginSuccess w folderze Register.</returns>
        public IActionResult LoginSuccess()
        {
            return View();
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
        /// <returns>Zwraca widok do Create w folderze Register.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Funkcja przejscia do widoku Create.
        /// </summary>
        /// <returns>Zwraca widok do Success w folderze Register.</returns>
        public IActionResult Success()
        {
            return View();
        }

        /// <summary>
        /// Funkcja dodajaca nowy obiekt klasy Osoby.
        /// </summary>
        /// <param name="osoby">Nowy obiekt klasy.</param>
        /// <returns>Widok pomyslnej rejestracji.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Imie,Nazwisko,drugie_imie,plec,data_urodzenia,nr_telefonu,email,Hasło")] Osoby osoby)
        {
            if (ModelState.IsValid)
            {
                _context.Add(osoby);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View("Success");
        }


        //?????????????????????????????????????????????????????????????????????????????????????????????????
        /// <summary>
        /// Funkcja umożliwiająca wyszukiwwanie czy dana osoba istnieje w bazie danych
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

        ////// <summary>
        /// Funkcja sprawdzajaca czy istnieje osoba o danym ID.
        /// </summary>
        /// <param name="id">ID obiektu wyszukiwanego.</param>
        /// <returns>Zwraca wartosc True/False czy dany obiekt istnieje.</returns>
        private bool OsobyExists(int id)
        {
                return (_context.Osoby?.Any(e => e.ID == id)).GetValueOrDefault();
           
        }

        /// <summary>
        /// Funkcja sprawdzajaca czy istnieje osoba o zadanym emmial i Haslo.
        /// Zastosowana w procesie logowania.
        /// </summary>
        /// <param name="email">Wprowadzony przez uzytkownika email.</param>
        /// <param name="Hasło">Wprowadzone przez uzytkownika haslo.</param>
        /// <returns>Zwraca wartosc True/False czy dany obiekt istnieje.</returns>
        private bool DataExists(string email, string Hasło)
        {
            return (_context.Osoby?.Any(e => e.email == email && e.Hasło == Hasło)).GetValueOrDefault();

        }

        /// <summary>
        /// Funkcja logowania w roli kierownika.
        /// </summary>
        /// <param name="osoby">Model do sprawdzenia.</param>
        /// <returns>Widok pomyslnego lub blednego logowania jako kierownik lub funkcje Index.</returns>
        public IActionResult ProcessLogin(Osoby osoby)
         {
            if (DataExists(osoby.email, osoby.Hasło))
             {
                if(osoby.email == "kierownik" && osoby.Hasło == "kierownik")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                return View("LoginSuccess", osoby);
             }
             else
             {
                 return View("LoginFailure", osoby);
             }
             
         }


    }
}
