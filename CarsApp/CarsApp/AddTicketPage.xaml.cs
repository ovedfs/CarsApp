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
	public partial class AddTicketPage : ContentPage
	{
        protected string ImagePath;

		public AddTicketPage (Car car)
		{
			InitializeComponent ();

            BindingContext = car;
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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Validar formulario y registrar multa...
        }
    }
}