using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnecting.Views.ShellViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShellFooter : ContentView
    {
        public ShellFooter()
        {
            InitializeComponent();
        }

        private void Logout_Clicked(object sender, EventArgs e)
        {


            App.Current.MainPage = new LoginView();
            
        }
    }
}