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

        public ReservasController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reserva.Where(x => x.EstaBorrado == false).ToListAsync());
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

        public IActionResult EmptyList()
        {
            return View();
        }
        public IActionResult ReservasPorEvento()
        {
            return View();
        }

        public async Task<IActionResult> ReservasPorEvento(int evento, Evento even, Reserva reserva)
        {
            if (evento != 0)
            {
                evento = 7;
                Evento eventoId = await _context.Evento.SingleAsync(x => x.Id == evento);
                int idEvento = eventoId.Id;
                List<Reserva> rpe = await _context.Reserva.Where(x => x.Evento.Id == idEvento).Include(x => x.Evento).ToListAsync();
                return View(rpe);
            }
            else
            {
                return View();
            }
        }

        // Registrarse a un evento
        public async Task<IActionResult> RegisterEvent(string time, Evento evento, AppUser appUser)
        {
            AppUser currentUser = await _userManager.GetUserAsync(User);
            int idEvento = evento.Id;
            bool isRegistered = true;
            DateTimeOffset fecha = DateTimeOffset.Now;
            Evento even = _context.Evento.Include(x => x.Estado).Single(x => x.Id == idEvento);
            Reserva r = new Reserva();
            List<Reserva> userReservas = await _context.Reserva.Where(x => x.AppUser.Id == currentUser.Id).Include(x => x.Evento).ToListAsync();

            if(userReservas.Count!=0)
            {

                foreach (var reserva in userReservas.Where(x => x.AppUser.Id == currentUser.Id))
                {
                    if (reserva.Evento.Id == idEvento && reserva.EstaBorrado == false)
                    {
                        isRegistered = true;

                        return RedirectToAction("Registered", "Reservas");
                    }
                    else
                    {
                        isRegistered = false;
                    }
                }
            }
            else if(userReservas.Count==0)
            {
                    isRegistered = false;              
            }

            
            if (even.Estado.Id == 1 && !isRegistered)
            {
                r = new Reserva
                {
                    AppUser = currentUser,
                    EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 1),
                    Evento = _context.Evento.Single(x => x.Id == idEvento),
                    FechaReserva = fecha,
                    EstaBorrado = false,
                    HaAsistido = false
                };
                _context.Reserva.Add(r);
                await _context.SaveChangesAsync();
            }
            else if (even.Estado.Id == 4 && !isRegistered)
            {
                r = new Reserva
                {
                    AppUser = currentUser,
                    EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 2),
                    Evento = _context.Evento.Single(x => x.Id == idEvento),
                    FechaReserva = fecha,
                    EstaBorrado = false,
                    HaAsistido = false
                };

                _context.Reserva.Add(r);
                await _context.SaveChangesAsync();
            }
            return View(r);
        }

        public async Task<IActionResult> Registered()
        {
            return View();
        }

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
        public async Task<IActionResult> DeleteConfirmed(int id, Evento evento)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            reserva.EstaBorrado = true;
            reserva.EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 3);
            evento.AforoActual = evento.AforoActual - 1;
            _context.Reserva.Update(reserva);

            await _context.SaveChangesAsync();
            return RedirectToAction("ReservasUsuario", "Reservas");
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ReservasUsuario()
        {
            AppUser usuario = await _userManager.GetUserAsync(User);
            List<Reserva> userReservas = new List<Reserva>();

            if (userReservas.Count != 0 || _context.Reserva.Where(x => x.AppUser.Id == usuario.Id).Include(x => x.Evento) != null)
            {
                userReservas = await _context.Reserva.Where(x => x.AppUser.Id == usuario.Id).Where(x => x.EstaBorrado == false).Include(x => x.Evento).Include(x => x.EstadoReserva).ToListAsync();
                return View(userReservas);
            }
            else
            {
                return RedirectToAction("EmptyList", "Reservas");
            }
        }

        public async Task<IActionResult> EventosUsuario()
        {

            AppUser usuario = await _userManager.GetUserAsync(User);
            List<Evento> userEventos = await _context.Evento.Where(x => x.AppUser.Id == usuario.Id).ToListAsync();
            return View(userEventos);
        }
    }
}
