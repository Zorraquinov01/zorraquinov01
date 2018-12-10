using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zv01.Data;
using zv01.Models;

namespace zv01.Controllers
{
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ReservasController(ApplicationDbContext context, UserManager<AppUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserva.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            return View();
        }

       // Registrarse a un evento

        public async Task<IActionResult> RegisterEvent(string time, Evento evento, AppUser appUser)
        {
            AppUser currentUser = await _userManager.GetUserAsync(User);
            int idEvento = evento.Id;
            DateTimeOffset fecha = DateTimeOffset.Now;

            Reserva r = new Reserva
            {
                AppUser = currentUser,
                EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 1),
                Evento = _context.Evento.Single(x => x.Id == idEvento),
                FechaReserva = fecha

            };
             _context.Reserva.Add(r);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        //Reserva cu = _context.Reserva.LastOrDefault(m => m.AppUser.Id == currentUser.Id);

        //return RedirectToAction("Create", "Reservas");


        //int idasign = _context.Evento.Where(x => x.Id) ;
        //if (idasign != null)
        //{
        //    Reserva res = _context.Evento.LastOrDefault(m => m.Id == idasign);
        //}
        //_context.Reserva.EventoId.Add(idasign);
        //await _context.SaveChangesAsync();
        //DateTimeOffset eventodate = evento.EventDate;
        //var timeSpanVal = time.ToString().Split(':').Select(x => Convert.ToInt32(x)).ToList();
        //TimeSpan ts = new TimeSpan(timeSpanVal[0], timeSpanVal[1], 00);
        //eventodate = eventodate.Add(ts);

        //if (ModelState.IsValid)
        //{
        //    _context.Add(evento);
        //    return RedirectToAction(nameof(Index));
        //}








        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaReserva")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }




        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaReserva")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
    }
}
