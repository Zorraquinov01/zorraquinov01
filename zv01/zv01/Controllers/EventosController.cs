using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using zv01.Data;
using zv01.Models;

namespace zv01.Controllers
{
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly AzureStorageConfig _config;


        public EventosController(ApplicationDbContext context, UserManager<AppUser> userManager, IOptions<AzureStorageConfig> config)
        {
            _context = context;
            _userManager = userManager;
            _config = config.Value;
        }

        // GET: Eventos
        public async Task<IActionResult> Index(AppUser appUser, int option)
        {
            return await Filter(option);

        }
        public async Task<IActionResult> ListadoEventos(AppUser appUser, int option)
        {
            return await Filter(option);

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }
            else if(!User.IsInRole("Administrador") && !User.IsInRole("Pica"))
            {
                evento.Visitas = evento.Visitas+1;
                _context.Evento.Update(evento);
                await _context.SaveChangesAsync();
            }

            return View(evento);

        }

        // GET: Eventos/Create
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,EventDate,Description,Place,AforoActual,AforoTotal,Visitas")]string time, Evento evento)
        {
            
            DateTimeOffset eventodate = evento.EventDate;
            var timeSpanVal = time.ToString().Split(':').Select(x => Convert.ToInt32(x)).ToList();
            TimeSpan ts = new TimeSpan(timeSpanVal[0], timeSpanVal[1], 00);
            evento.EventDate = eventodate.Add(ts);
            evento.Estado = _context.EstadoEventos.Single(x => x.Id == 1);  
            

            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        //Registrarse a un evento



        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,EventDate,Description,Place,AforoActual,AforoTotal,Visitas")]string time, Evento evento)
        {

            
            DateTimeOffset eventodate = evento.EventDate;
            var timeSpanVal = time.Split(':').Select(x => Convert.ToInt32(x)).Take(2).ToList();
            TimeSpan ts = new TimeSpan(timeSpanVal[0], timeSpanVal[1], 00);
            eventodate = eventodate.Add(ts);
            evento.EventDate = eventodate;

            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            _context.Evento.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.Id == id);
        }


        private async Task<IActionResult> Filter(int option)
        {
            if (option == 0)
            {
                return View(await _context.Evento.ToListAsync());
            }

            else if (option == 1)
            {
                return View(await _context.Evento.OrderByDescending(evento => evento.Visitas).ToListAsync());

            }
            else if (option == 2)
            {
                return View(await _context.Evento.OrderByDescending(evento => evento.EventDate).ToListAsync());
            }

            else if (option == 3)
            {
                return View(await _context.Evento.OrderByDescending(evento => evento.AforoActual).ToListAsync());
            }
            else if (option == 4)
            {
                return View(await _context.Evento.OrderBy(evento => evento.AforoActual).ToListAsync());
            }
            else
            {
                return View();
            }
        }
    }
}
