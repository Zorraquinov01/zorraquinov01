using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zv01.Models;

namespace zv01.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<zv01.Models.Evento> Evento { get; set; }
        public DbSet<zv01.Models.Reserva> Reserva { get; set; }
        public DbSet<zv01.Models.AppRole> AppRole { get; set; }
        public DbSet<zv01.Models.EstadoReserva> EstadoReservas { get; set; }
        public DbSet<zv01.Models.EstadoEvento> EstadoEventos { get; set; }
        public DbSet<zv01.Models.EventImg> EventImg { get; set; }
        public DbSet<zv01.Models.QRImg> QRImg { get; set; }
    }
}
