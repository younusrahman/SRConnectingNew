using SRConnecting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SRConnecting.Services;
using System.ComponentModel;
using SRConnecting.Views;

namespace SRConnecting.ViewModels
{
    class SignUpViewModel: INotifyPropertyChanged
    {
        //static ServerService UserService;
        static FireBaseDatabase UserService;


        public SignUpViewModel()
        {
            //UserService = new ServerService();
            UserService = new FireBaseDatabase();
        }



        public ICommand SignUpBTClickedCommand { get => new Command(async () => await SignUpBTClicked()); }






        public event PropertyChangedEventHandler PropertyChanged;


        private string _FirstName = string.Empty;

        public string FirstName
        {
            set
            {
                if (_FirstName != value) { _FirstName = value; }
                OnFirstNamePropertyChanged(FirstName);
            }
            get
            {
                return _FirstName;
            }
        }

        void OnFirstNamePropertyChanged(string _FirstName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_FirstName_)));
        }




        private string _LastName = string.Empty;

        public string LastName
        {
            set
            {
                if (_LastName != value) { _LastName = value; }
                OnLastNamePropertyChanged(LastName);
            }
            get
            {
                return _LastName;
            }
        }

        void OnLastNamePropertyChanged(string _LastName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_LastName_)));
        }




        private string _Email = string.Empty;

        public string Email
        {
            set
            {
                if (_Email != value) { _Email = value; }
                OnEmailPropertyChanged(Email);
            }
            get
            {
                return _Email;
            }
        }

        void OnEmailPropertyChanged(string _Email_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_Email_)));
        }





        private string _Password = string.Empty;

        public string Password
        {
            set
            {
                if (_Password != value) { _Password = value; }
                OnPasswordPropertyChanged(Password);
            }
            get
            {
                return _Password;
            }
        }

        void OnPasswordPropertyChanged(string _Password_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_Password_)));
        }



        private string _Mobile = string.Empty;

        public string Mobile
        {
            set
            {
                if (_Mobile != value) { _Mobile = value; }
                OnMobilePropertyChanged(Mobile);
            }
            get
            {
                return _Mobile;
            }
        }

        void OnMobilePropertyChanged(string _Mobile_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_Mobile_)));
        }






        private string _ImageURL = string.Empty;

        public string ImageURL
        {
            set
            {
                if (_ImageURL != value) { _ImageURL = "Defaultuserprofile"; } // Change to value later here to work with profile image; 
                OnImageURLPropertyChanged(ImageURL);
            }
            get
            {
                return _ImageURL;
            }
        }

        void OnImageURLPropertyChanged(string _ImageURL_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_ImageURL_)));
        }





        public bool _ActivityIndicatorIsVisible = false;
        public bool ActivityIndicatorIsVisible
        {
            get
            {
                return _ActivityIndicatorIsVisible;
            }
            set
            {
                _ActivityIndicatorIsVisible = value;
                OnActivityIndicatorIsVisiblePropertyChanged("ActivityIndicatorIsVisible");
            }
        }
        protected virtual void OnActivityIndicatorIsVisiblePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand OpenLegendCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (ActivityIndicatorIsVisible)
                    {
                        ActivityIndicatorIsVisible = false;
                        //    _legendVisibility = false;
                    }
                    else if (!ActivityIndicatorIsVisible)
                    {
                        ActivityIndicatorIsVisible = true;
                        //_legendVisibility = true;
                    }

                }));
            }
        }


        private async Task SignUpBTClicked()
        {
            {

                try
                {
                    OpenLegendCommand.Execute(null);


                    if (FirstName == string.Empty || LastName == string.Empty || Password == string.Empty || Mobile == string.Empty || Email == string.Empty || await UserService.IsUserExists(Email))
                    {
                        await Application.Current.MainPage.DisplayAlert("Registration Failed", "You may not have entered your information correctly or Email already exist in our database", "OK");
                        return;

                    }

                    var respone = await UserService.RegisterUser(FirstName, LastName, Email, Password, Mobile, ImageURL);

                    if (respone)
                    {
                        await Application.Current.MainPage.DisplayAlert("Success", "Congratulations! A warm welcome to the SRConnecting!", "OK");
                        App.Current.MainPage = new LoginView();
                        

                    }
                    else
                    {

                        await Application.Current.MainPage.DisplayAlert("Registration Failed", "Conntect admin to get help ", "OK");
                       
                    }

                    return;


                }
                catch (Exception)
                {


                    await Application.Current.MainPage.DisplayAlert("Ërror", "Opps! Something went wrong with our server", "OK");


                }
                finally
                {
                    OpenLegendCommand.Execute(null);
                }



            }
            




            

        }


    }
}
