using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinSetupTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
			
            InitializeComponent();
			DisplayAlert("Starting", "Yes, everything looks good!", "Cool");
        }

        private void Button_Click(object sender, EventArgs e)
        {
            DisplayAlert("Really?", "Yes, everything looks good!", "Thanks");
        }
    }
}
