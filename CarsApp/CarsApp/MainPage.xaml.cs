using CarsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarsApp
{
	public partial class MainPage : ContentPage
	{
        protected User user;

		public MainPage(User user)
		{
			InitializeComponent();
            this.user = user;

            BindingContext = user;
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var cars = await CarHelper.GetCars(user);

            CarsListView.ItemsSource = cars;
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCarPage());
        }
    }
}
