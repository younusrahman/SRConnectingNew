using SRConnecting.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SRConnecting
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SignupView", typeof(SignupView));
            Routing.RegisterRoute(nameof(AccountRecoveryViews), typeof(AccountRecoveryViews));
            BindingContext = new AppShellViewModel();
           
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginView();
        }
    }
}
