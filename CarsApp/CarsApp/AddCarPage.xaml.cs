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
	public partial class AddCarPage : ContentPage
	{
        protected User user;

        List<string> estados = new List<string>
        {
            "Estado de México", "Puebla", "Morelos", "Ciudad de México"
        };

        Dictionary<string, Color> colorNames = new Dictionary<string, Color>
        {
            {"Azul", Color.Blue}, {"Rojo", Color.Red}, {"Verde", Color.Green}, {"Amarillo", Color.Yellow}, {"Blanco", Color.White},
            {"Negro", Color.Black}
        };

        protected string ImagePath;

		public AddCarPage (User user)
		{
			InitializeComponent ();

            this.user = user;

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
            if(string.IsNullOrWhiteSpace(placasEntry.Text) ||
                string.IsNullOrWhiteSpace(marcaEntry.Text) ||
                string.IsNullOrWhiteSpace(modeloEntry.Text) ||
                string.IsNullOrWhiteSpace(aliasEntry.Text) ||
                estadosPicker.SelectedIndex == -1 ||
                colorPicker.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(ImagePath))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios", "OK");
                return;
            }

            Car car = new Car
            {
                Placas = placasEntry.Text,
                Marca = marcaEntry.Text,
                Modelo = Convert.ToInt32(modeloEntry.Text),
                Alias = aliasEntry.Text,
                Estado = estadosPicker.SelectedItem.ToString(),
                Color = colorPicker.SelectedItem.ToString(),
                Foto = ImagePath,
                UserId = user.Id
            };

            try
            {
                await CarHelper.SaveItemAsync(car);
                await DisplayAlert("Guardar", "Coche agregado correctamente", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Hubo un error y el coche no se agrego a la base de datos", "OK");
                return;
            }
        }
    }
}