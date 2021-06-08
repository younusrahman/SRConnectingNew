using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnecting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountRecoveryViews : ContentPage
    {
        public AccountRecoveryViews()
        {
            InitializeComponent();
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginView();
        }
    }
}