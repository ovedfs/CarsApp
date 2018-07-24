using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp.Models
{
    class FileHelper
    {
        static readonly SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static FileHelper()
        {
            _connection.CreateTableAsync<File>().Wait();
        }

        public static Task<List<File>> GetItemsAsync()
        {
            return _connection.Table<File>().ToListAsync();
        }

        public static Task<int> SaveItemAsync(File item)
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

        public static Task<int> DeleteItemAsync(File item)
        {
            return _connection.DeleteAsync(item);
        }

        public static Task<List<File>> GetFiles(Car car)
        {
            var query = _connection.Table<File>().Where(c => c.CarId.Equals(car.Id));
            return query.ToListAsync();
        }
    }
}
