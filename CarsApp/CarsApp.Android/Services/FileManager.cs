using System;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(CarsApp.FileManager))]

namespace CarsApp
{
    public class FileManager : IFileManager
    {
        public string SaveFile(byte[] stream)
        {
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);

            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string filePath = System.IO.Path.Combine(dir.ToString(), filename);

            try
            {
                System.IO.File.WriteAllBytes(filePath, stream);

                return filePath;
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}