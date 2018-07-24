using CarsApp.Models;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CarsApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddFilePage : ContentPage
	{
        protected Car car;
        protected string ImagePath;

		public AddFilePage (Car car)
		{
			InitializeComponent ();
            this.car = car;
		}

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryTitulo.Text) ||
                string.IsNullOrWhiteSpace(entryDescripcion.Text) ||
                string.IsNullOrWhiteSpace(entryNotas.Text) ||
                string.IsNullOrWhiteSpace(ImagePath))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            File file = new File
            {
                Titulo = entryTitulo.Text,
                Descripcion = entryDescripcion.Text,
                Notas = entryNotas.Text,
                Archivo = ImagePath,
                CarId = car.Id
            };

            try
            {
                await FileHelper.SaveItemAsync(file);
                await DisplayAlert("Guardar", "Documento agregado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Hubo un error y el documento no se agrego a la base de datos", "OK");
                return;
            }
        }

        async private void fotoPicker_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData file = await CrossFilePicker.Current.PickFile();

                string path = DependencyService.Get<IFileManager>().SaveFile(file.DataArray);

                if (path != "Error")
                {
                    foto.Source = path;
                    ImagePath = path;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Test", "Error al cargar una imagen", "OK");
            }
        }
    }
}