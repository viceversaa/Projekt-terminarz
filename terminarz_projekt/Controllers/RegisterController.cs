using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using terminarz_projekt.Data;
using terminarz_projekt.Models;
using terminarz_projekt.Sevices;
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

        public IActionResult LoginPanel()
        {
            return View();
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

        // GET: Register
        public async Task<IActionResult> Index()
        {
            return _context.Osoby != null ?
                        View(await _context.Osoby.ToListAsync()) :
                        Problem("Entity set 'TerminarzContext.Osoby'  is null.");
        }

        // GET: Register/Details/5
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

        // GET: Register/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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


        // GET: Register/Edit/5
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

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Register/Delete/5
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

        // POST: Register/Delete/5
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

        /*private bool OsobyExists(int id)
        {
            return (_context.Osoby?.Any(e => e.ID == id)).GetValueOrDefault();
        }*/

        private bool OsobyExists(int id)
        {
                return (_context.Osoby?.Any(e => e.ID == id)).GetValueOrDefault();
           
        }


        private bool DataExists(string email, string Hasło)
        {
            return (_context.Osoby?.Any(e => e.email == email && e.Hasło == Hasło)).GetValueOrDefault();

        }

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
