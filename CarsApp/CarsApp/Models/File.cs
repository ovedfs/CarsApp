using SQLite;

namespace CarsApp.Models
{
    public class File
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Archivo { get; set; }
        public string Notas { get; set; }

        [Indexed]
        public int CarId { get; set; }
    }
}
