using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp.Models
{
    class TaxHelper
    {
        static readonly SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static TaxHelper()
        {
            _connection.CreateTableAsync<Tax>().Wait();
        }

        public static Task<List<Tax>> GetItemsAsync()
        {
            return _connection.Table<Tax>().ToListAsync();
        }

        public static Task<int> SaveItemAsync(Tax item)
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

        public static Task<int> DeleteItemAsync(Tax item)
        {
            return _connection.DeleteAsync(item);
        }

        public static Task<List<Tax>> GetTaxes(Car car)
        {
            var query = _connection.Table<Tax>().Where(c => c.CarId.Equals(car.Id));
            return query.ToListAsync();
        }
    }
}
