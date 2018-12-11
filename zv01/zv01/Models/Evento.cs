using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zv01.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTimeOffset EventDate { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public int AforoActual { get; set; }
        public int AforoTotal { get; set; }
        public int ListaEspera { get; set; }
        public AppUser AppUser { get; set; }
        public EstadoEvento Estado { get; set; }
        public int Visitas { get; set; }
    }
}
