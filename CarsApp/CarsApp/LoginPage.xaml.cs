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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

        async private void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryEmail.Text) ||
                string.IsNullOrWhiteSpace(entryPassword.Text))
            {
                await DisplayAlert("Error", "Ingrese su correo y password", "OK");
                return;
            }

            var user = await UserHelper.UserExist(entryEmail.Text, entryPassword.Text);

            if (user == null)
            {
                await DisplayAlert("Error", "Verifica tu correo o password", "OK");
                return;
            }

            await DisplayAlert("Login", "Tus datos son correctos", "OK");

            await Navigation.PushAsync(new MainPage());
        }

        async private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}