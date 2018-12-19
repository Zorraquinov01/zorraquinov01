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

        public static void EmailSender(string Email)
        {
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("holabuenosdias9999999@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "Registro en el evento completado. ";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Felicidades, te has registrado con éxito en el evento.";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("holabuenosdias9999999@gmail.com", "HectorPeio1.");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            
        }

        public static void EmailSenderLE(string Email)
        {
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("holabuenosdias9999999@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "Registro en Lista de Espera";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Te has registrado en la Lista de Espera.";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("holabuenosdias9999999@gmail.com", "HectorPeio1.");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

        }

        public static void EmailSenderCA(string Email)
        {
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("holabuenosdias9999999@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "Reserva Cancelada";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "Has cancelado tu reserva con éxito.";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("holabuenosdias9999999@gmail.com", "HectorPeio1.");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);    
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
