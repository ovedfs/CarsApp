using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp.Models
{
    class CarHelper
    {
        static readonly SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static CarHelper()
        {
            _connection.CreateTableAsync<Car>().Wait();
        }

        public static Task<List<Car>> GetItemsAsync()
        {
            return _connection.Table<Car>().ToListAsync();
        }

        public static Task<int> SaveItemAsync(Car item)
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

        public static Task<int> DeleteItemAsync(Car item)
        {
            return _connection.DeleteAsync(item);
        }

        public static Task<List<Car>> GetCars(User user)
        {
            var query = _connection.Table<Car>().Where(c => c.UserId.Equals(user.Id));
            return query.ToListAsync();
        }
    }
}
