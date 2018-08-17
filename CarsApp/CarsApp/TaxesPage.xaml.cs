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
	public partial class TaxesPage : ContentPage
	{
		public TaxesPage ()
		{
			InitializeComponent ();
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var car = BindingContext as Car;
            var taxes = await TaxHelper.GetTaxes(car);
            TaxesListView.ItemsSource = taxes;
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaxPage(BindingContext as Car));
        }

        private void TaxessListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //
        }
    }
}