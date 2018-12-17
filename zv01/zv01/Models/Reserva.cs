using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zv01.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public Evento Evento { get; set; }
        public AppUser AppUser { get; set; }
        public DateTimeOffset FechaReserva {get; set;}
        public EstadoReserva EstadoReserva { get; set; }
        public QRImg QRs { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
