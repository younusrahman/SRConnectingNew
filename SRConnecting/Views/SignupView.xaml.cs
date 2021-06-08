using SRConnecting.Models;
using SRConnecting.Services;
using SRConnecting.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SRConnecting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupView : ContentPage
    {

    

        public SignupView()
        {
            InitializeComponent();
            this.BindingContext = new SignUpViewModel();

        }

        

        private void CancelClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginView();
        }
    }
}