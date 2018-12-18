﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace zv01.Services
{
    public class Zv01Services
    {
        public void EmailSender(string Email)
        {
            System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("correoenvio");
            mail.To.Add("correodestinatario");
            mail.Subject = "PRUEBA ENVIO";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = "!!El envio ha resultado!!";
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("mailaddress", "password");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            
        }
    }

}
