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
	public partial class TicketsPage : ContentPage
	{
		public TicketsPage ()
		{
			InitializeComponent ();
		}

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            var car = BindingContext as Car;

            var tickets = await TicketHelper.GetTickets(car);

            TicketsListView.ItemsSource = tickets;
        }

        async private void TicketsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var ticket = e.SelectedItem as Ticket;
            await Navigation.PushAsync(new EditTicketPage(ticket));
        }

        async private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTicketPage(BindingContext as Car));
        }
    }
}