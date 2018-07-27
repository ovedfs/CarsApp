using SQLite;
using System;

namespace CarsApp.Models
{
    public class Ticket
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime FechaMulta { get; set; }
        public double Monto { get; set; }
        public bool Pagada { get; set; }
        public DateTime FechaPago { get; set; }
        public string Archivo { get; set; }
        public string Observaciones { get; set; }

        [Indexed]
        public int CarId { get; set; }
    }
}
