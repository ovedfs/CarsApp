using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp.Models
{
    class UserHelper
    {
        static readonly SQLiteAsyncConnection _connection = DependencyService.Get<ISQLiteDb>().GetConnection();

        static UserHelper()
        {
            _connection.CreateTableAsync<User>().Wait();
        }

        public static Task<List<User>> GetItemsAsync()
        {
            return _connection.Table<User>().ToListAsync();
        }

        public static Task<int> SaveItemAsync(User item)
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

        public static Task<int> DeleteItemAsync(User item)
        {
            return _connection.DeleteAsync(item);
        }

        async public static Task<User> UserExist(string correo, string password)
        {
            var table = _connection.Table<User>();
            User user = await table.Where(x => x.Email == correo && x.Password == password).FirstOrDefaultAsync();

            if (user != null)
            {
                return user;
            }

            return null;
        }
    }
}
