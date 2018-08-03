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

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryMonto.Text) || string.IsNullOrWhiteSpace(ImagePath))
            {
                await DisplayAlert("Error", "El monto y la foto de la multa son obligatorios", "Ok");
                return;
            }

            Ticket ticket = new Ticket
            {
                FechaMulta = PickerFechaMulta.Date,
                Monto = Convert.ToDouble(entryMonto.Text),
                Pagada = switchPagada.IsToggled,
                FechaPago = PickerFechaPago.Date,
                Archivo = ImagePath,
                Observaciones = entryObservaciones.Text,
                CarId = (BindingContext as Car).Id
            };

            try
            {
                int saved = await TicketHelper.SaveItemAsync(ticket);
                await DisplayAlert("Guardar", "Multa registrada correctamente", "Ok");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Guardar", "La Multa NO se pudo registrar", "Ok");
                return;
            }
        }
    }
}