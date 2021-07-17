using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Acme.Models
{
    public class Reserva
    {
        
        public int Id { get; set; }
        [Required]
        public DateTime HoraInicio { get; set; }
        [Required]
        public DateTime HoraFin { get; set; }
        [Required]
        public bool? ServicioCatering { get; set; }
        [Required]
        public int? IdSala { get; set; }
    }
}
