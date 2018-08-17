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
	public partial class AddTaxPage : ContentPage
	{
        List<int> years = new List<int>();

        protected string ImagePath;

        public AddTaxPage (Car car)
		{
			InitializeComponent ();

            BindingContext = car;

            int currentYear = DateTime.Now.Year;

            for (int i = currentYear - 5; i <= currentYear + 5; i++)
            {
                years.Add(i);
            }

            yearsPicker.ItemsSource = years;
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryMonto.Text) || string.IsNullOrWhiteSpace(ImagePath) || yearsPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "El monto, la foto y el año de la tenencia son obligatorios", "Ok");
                return;
            }

            Tax tax = new Tax
            {
                Year = Convert.ToInt32(yearsPicker.SelectedItem),
                Monto = Convert.ToDouble(entryMonto.Text),
                Pagada = switchPagada.IsToggled,
                FechaPago = PickerFechaPago.Date,
                Archivo = ImagePath,
                Observaciones = entryObservaciones.Text,
                CarId = (BindingContext as Car).Id
            };

            try
            {
                int saved = await TaxHelper.SaveItemAsync(tax);
                await DisplayAlert("Guardar", "Tenencia registrada correctamente", "Ok");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Guardar", "La tenencia NO se pudo registrar", "Ok");
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