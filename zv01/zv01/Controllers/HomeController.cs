using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using zv01.Models;

namespace zv01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            //var mail = new MailMessage();
            //mail.From = new MailAddress("correoenvio");
            //mail.To.Add("correodestinatario");
            //mail.Subject = "PRUEBA ENVIO";
            //mail.IsBodyHtml = true;
            //string htmlBody;
            //htmlBody = "!!El envio ha resultado!!";
            //mail.Body = htmlBody;
            //SmtpServer.Port = 587;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("mailaddress", "password");
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Send(mail);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }


        //hola
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
