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
	public partial class AddCarPage : ContentPage
	{
        List<string> estados = new List<string>
        {
            "Estado de México", "Puebla", "Morelos", "Ciudad de México"
        };

        Dictionary<string, Color> colorNames = new Dictionary<string, Color>
        {
            {"Azul", Color.Blue}, {"Rojo", Color.Red}, {"Verde", Color.Green}, {"Amarillo", Color.Yellow}, {"Blanco", Color.White},
            {"Negro", Color.Black}
        };

		public AddCarPage ()
		{
			InitializeComponent ();

            estadosPicker.ItemsSource = estados;
            InitColorPicker();
		}

        private void InitColorPicker()
        {
            foreach (string color in colorNames.Keys)
            {
                colorPicker.Items.Add(color);
            }
        }

        async private void fotoPicker_Clicked(object sender, EventArgs e)
        {
            try
            {
                FileData file = await CrossFilePicker.Current.PickFile();
                string fileName = file.FileName;

                //await DisplayAlert("Test", fileName, "OK");

                string path = DependencyService.Get<IFileManager>().SaveFile(file.DataArray);

                if(path != "Error")
                {
                    foto.Source = path;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Test", "Error al cargar una imagen", "OK");
            }
        }
    }
}