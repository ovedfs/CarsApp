using CarsApp.Models;
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
	public partial class FilesPage : ContentPage
	{
		public FilesPage ()
		{
			InitializeComponent ();
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var car = BindingContext as Car;

            var files = await FileHelper.GetFiles(car);

            FilesListView.ItemsSource = files;
        }

        async private void FilesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var file = e.SelectedItem as File;

            await Navigation.PushModalAsync(new FileDetailPage(file));
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFilePage(BindingContext as Car));
        }
    }
}