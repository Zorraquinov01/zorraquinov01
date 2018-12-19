using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using zv01.Data;
using zv01.Models;

namespace zv01.Services
{

    public class Zv01Services
    {
        private readonly ApplicationDbContext _context;

        public Zv01Services(ApplicationDbContext context)
        {
            _context = context;
        }

        public Evento EventoMaxVis()
        {
            var record = _context.Evento.OrderByDescending(x => x.Visitas).Include(x=>x.Imgs).First();
            return record;
        }
        public Evento EventoMaxAfo()
        {
            var record = _context.Evento.OrderByDescending(x => x.AforoActual).Include(x=>x.Imgs).First();
            return record;
        }
        public Evento EventoLast()
        {
            var record = _context.Evento.OrderByDescending(x => x.Id).Include(x => x.Imgs).First();
            return record;
        }

        public List<Evento> EventList()
        {
            List<Evento> eventList = _context.Evento.ToList();
            return eventList;
        }
        public List<Reserva> ReservasList()
        {
            List<Reserva> reservasList = _context.Reserva.ToList();
            return reservasList;
        }

     
    }

}
