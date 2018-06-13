using SQLite;

namespace CarsApp.Models
{
    public class Car
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Placas { get; set; }
        public string Marca { get; set; }
        public int Modelo { get; set; }
        public string Color { get; set; }
        public string Foto { get; set; }
        public string Estado { get; set; }
        public string Alias { get; set; }

        [Indexed]
        public int UserId { get; set; }
    }
}
