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
	public partial class EditTicketPage : ContentPage
	{
        protected string ImagePathTemp;

		public EditTicketPage (Ticket ticket)
		{
			InitializeComponent ();
            BindingContext = ticket;

            ImagePathTemp = ticket.Archivo;
		}

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryMonto.Text) || string.IsNullOrWhiteSpace(ImagePathTemp))
            {
                await DisplayAlert("Error", "El monto y la foto de la multa son obligatorios", "Ok");
                return;
            }

            var ticket = (BindingContext as Ticket);
            ticket.FechaMulta = PickerFechaMulta.Date;
            ticket.Monto = Convert.ToDouble(entryMonto.Text);
            ticket.Pagada = switchPagada.IsToggled;
            ticket.FechaPago = PickerFechaPago.Date;
            ticket.Archivo = ImagePathTemp;
            ticket.Observaciones = entryObservaciones.Text;

            try
            {
                int saved = await TicketHelper.SaveItemAsync(ticket);
                await DisplayAlert("Actualizar", "Multa actualizada correctamente", "Ok");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Guardar", "La Multa NO se pudo actualizar", "Ok");
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
                    ImagePathTemp = path;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Test", "Error al cargar una imagen", "OK");
            }
        }

        async private void fotoDetail_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TicketDetailPage(BindingContext as Ticket));
        }
    }
}