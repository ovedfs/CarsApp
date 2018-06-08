using System;
using System.IO;
using SQLite;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(CarsApp.SQLiteDb))]

namespace CarsApp
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
			var documentsPath = ApplicationData.Current.LocalFolder.Path;
        	var path = Path.Combine(documentsPath, "CarsApp.db3");
        	return new SQLiteAsyncConnection(path);
        }
    }
}

