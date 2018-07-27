using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp.Models
{
    class TicketHelper
    {
        static readonly SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static TicketHelper()
        {
            _connection.CreateTableAsync<Ticket>().Wait();
        }

        public static Task<List<Ticket>> GetItemsAsync()
        {
            return _connection.Table<Ticket>().ToListAsync();
        }

        public static Task<int> SaveItemAsync(Ticket item)
        {
            if (item.Id != 0)
            {
                return _connection.UpdateAsync(item);
            }
            else
            {
                return _connection.InsertAsync(item);
            }
        }

        public static Task<int> DeleteItemAsync(Ticket item)
        {
            return _connection.DeleteAsync(item);
        }

        public static Task<List<Ticket>> GetTickets(Car car)
        {
            var query = _connection.Table<Ticket>().Where(c => c.CarId.Equals(car.Id));
            return query.ToListAsync();
        }
    }
}
