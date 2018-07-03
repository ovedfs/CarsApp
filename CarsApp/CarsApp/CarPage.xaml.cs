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
	public partial class CarPage : TabbedPage
	{
		public CarPage (Car car)
		{
			InitializeComponent ();
            BindingContext = car;
		}
	}
}