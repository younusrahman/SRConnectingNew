using SRConnecting.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SRConnecting
{
    class AppShellViewModel
    {
        public ICommand SignoutCommand { get => new Command(async () => await Signout()); }

        private async Task Signout()
        {
            //await Shell.Current.DisplayAlert("to do", "Whatz upp", "OK");
            await Shell.Current.GoToAsync(nameof(LoginView));
        }
    }
}
