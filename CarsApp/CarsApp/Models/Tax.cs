using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarsApp.Models
{
    class Tax
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Year { get; set; }
        public DateTime FechaPago { get; set; }
        public double Monto { get; set; }
        public bool Pagada { get; set; }
        public string Archivo { get; set; }
        public string Observaciones { get; set; }

        [Indexed]
        public int CarId { get; set; }
    }
}
