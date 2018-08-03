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
	public partial class TicketDetailPage : ContentPage
	{
		public TicketDetailPage (Ticket ticket)
		{
			InitializeComponent ();
            BindingContext = ticket;
		}

        async private void BtnCerrar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}