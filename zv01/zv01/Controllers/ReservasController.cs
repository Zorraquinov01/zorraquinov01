using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zv01.Data;
using zv01.Models;
using zv01.Services;


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
            return View(await _context.Reserva.Where(x => x.EstaBorrado == false).Include(x => x.Evento).Include(x => x.AppUser).ToListAsync());
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

        public async Task<IActionResult> ReservasPorEvento(int evento, Evento even, Reserva reserva)
        {
            if (evento != 0)
            {
                Evento eventoId = await _context.Evento.SingleAsync(x => x.Id == evento);
                int idEvento = eventoId.Id;
                List<Reserva> rpe = await _context.Reserva.Where(x => x.Evento.Id == idEvento).Include(x => x.Evento).ToListAsync();
                return View(rpe);
            }
            else
            {
                return View(await _context.Reserva.Include(x => x.Evento).Include(x => x.AppUser).OrderBy(x=>x.Evento.Id).ToListAsync());
            }
        }

        // Registrarse a un evento
        public async Task<IActionResult> RegisterEvent(string time, string qrRead, Evento evento, AppUser appUser)
        {
            AppUser currentUser = await _userManager.GetUserAsync(User);
            int idEvento = evento.Id;
            bool isRegistered = true;
            DateTimeOffset fecha = DateTimeOffset.Now;
            Evento even = _context.Evento.Include(x => x.Estado).Single(x => x.Id == idEvento);
            Reserva r = new Reserva();
            List<Reserva> userReservas = await _context.Reserva.Where(x => x.AppUser.Id == currentUser.Id).Include(x => x.Evento).ToListAsync();

            if (userReservas.Count != 0)
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
            else if (userReservas.Count == 0)
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
                    HaAsistido = false,
                    UrlQr= qrRead
                };
                _context.Reserva.Add(r);
                await _context.SaveChangesAsync();
                //EmailSender(currentUser.Email, r.EstadoReserva.Id);

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
                //EmailSender(currentUser.Email, r.EstadoReserva.Id);

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
        public async Task<IActionResult> Delete(int? id, int idevento)
        {
            if (id == null)
            {
                return NotFound();
            }

            Reserva reserva = await _context.Reserva
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        public async Task<IActionResult> Borrar(int id, int eventoid, string userid)
        {
            AppUser currentUser = await _context.Users.FindAsync(userid);
            var reserva = await _context.Reserva.FindAsync(id);
            Reserva reservaEspera;
            int resId = reserva.Id;
            reserva.EstaBorrado = true;
            reserva.EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 3);
            _context.Reserva.Update(reserva);
            await _context.SaveChangesAsync();
            //EmailSender(currentUser.Email, reserva.EstadoReserva.Id);
            Evento evento = _context.Evento.Single(x => x.Id == eventoid);
            if (evento.ListaEspera == 0)
            {
                evento.AforoActual = evento.AforoActual - 1;
            }
            else
            {
                evento.ListaEspera = evento.ListaEspera - 1;
                reservaEspera = _context.Reserva.FirstOrDefault(x => x.EstadoReserva.Id == 2);
                reservaEspera.EstadoReserva = _context.EstadoReservas.Single(x => x.Id == 1);

                _context.Reserva.Update(reservaEspera);
            }

            _context.Evento.Update(evento);
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

        public IActionResult EmailSender(string Email, int estadoReserva)
        {
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("holabuenosdias9999@hotmail.com");
            mail.To.Add(Email);
            if (estadoReserva == 1)
            {
                mail.Subject = "Tu reserva";
            }
            else if(estadoReserva == 2)
            {
                mail.Subject = "En Lista de Espera";
            }
            else
            {
                mail.Subject = "Reserva Cancelada";
            }
            mail.IsBodyHtml = true;
            string htmlBody;
            if(estadoReserva == 1)
            {
                htmlBody = "Enhorabuena, te has inscrito con éxito en el evento. Consulta el apartado 'Mis Reservas' de nuestra página.";
                mail.Body = htmlBody;
            }
            if (estadoReserva == 2)
            {
                htmlBody = "Tu reserva está en lista de espera. Consulta el apartado 'Mis Reservas' de nuestra página.";
                mail.Body = htmlBody;
            }
            if (estadoReserva == 3)
            {
                htmlBody = "Has cancelado tu reserva para el evento. Consulta el apartado 'Mis Reservas' de nuestra página.";
            mail.Body = htmlBody;
            }           
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("holabuenosdias9999@hotmail.com", "Hectorpeio1.");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return RedirectToAction("ReservasUsuario", "Reservas");
        }

    }
}
