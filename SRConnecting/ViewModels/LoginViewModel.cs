using SRConnecting.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SRConnecting.Services;
using Xamarin.Essentials;
using System.ComponentModel;
using Xamarin.Forms.Xaml;
using SRConnecting.Models;

namespace SRConnecting.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class LoginViewModel :  INotifyPropertyChanged
    {

        // Server For Usually User
            FireBaseDatabase Userdb;


        // Sigout waay ----------------------------------------------------------------------Start
        public ICommand SigOutWayCommand { get => new Command(() => SigOutWay()) ; }

        // Sigout waay ---------------------------------------------------End





        //After login should show username and email on flyout header---------------- start
        // Don't forget to add -->  INotifyPropertyChanged;
        // Don,T forget to add in xaml -->
        //    <ContentPage.BindingContext>
        //        <ViewModels:Filename/> 
        //    </ContentPage.BindingContext>



        private string _UserFullName = string.Empty;

        public string UserFullName
        {
            set
            {
                if (_UserFullName != value) { _UserFullName = value; }
                OnUserFullNamePropertyChanged("UserFullName");
            }
            get
            {
                return _UserFullName;
            }
        }


        void OnUserFullNamePropertyChanged(string _UserFullName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_UserFullName_)));
        }
        //After login should show username and email on flyout header---------------- End







        // ActivityIndicator  ------------------------------------------------------------------- Start

        private bool _ActivityIndicatorVisable = false;

        void OnActivityIndicatorVisablePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Föratt jobba med INotifyPropertyChanged boolen, jag har skapat en Snippet som heter : INPCBOOL
        public bool ActivityIndicatorVisable
        {
            get
            {
                return _ActivityIndicatorVisable;
            }
            set
            {
                _ActivityIndicatorVisable = value;
                OnActivityIndicatorVisablePropertyChanged("ActivityIndicatorVisable");
            }
        }

        public ICommand ActivityIndicatorVisableCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (ActivityIndicatorVisable)
                    {
                        ActivityIndicatorVisable = false;
                        //    _ActivityIndicatorVisable = false;
                    }
                    else if (!ActivityIndicatorVisable)
                    {
                        ActivityIndicatorVisable = true;
                        //_ActivityIndicatorVisable = true;
                    }

                }));
            }
        }

        // ActivityIndicator ---------------------------------- End



        // Remember Password and Useremail -------------------------------------------------------Start

        // Don't forget to add -->  INotifyPropertyChanged;
        // Don,T forget to add in xaml -->
        //    <ContentPage.BindingContext>
        //        <ViewModels:Filename/> 
        //    </ContentPage.BindingContext>
        //  Call Command --> Commandname.Execute(null);

        public bool _RememberPassEmail = false;
        public bool RememberPassEmail
        {
            get
            {
                return _RememberPassEmail;
            }
            set
            {
                _RememberPassEmail = value;
                OnRememberPassEmailPropertyChanged("RememberPassEmail");
            }
        }

        void OnRememberPassEmailPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand RememberPassEmailCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (RememberPassEmail)
                    {
                        RememberPassEmail = false;
                        //    _RememberPassEmail = false;
                    }
                    else if (!RememberPassEmail)
                    {
                        RememberPassEmail = true;
                        //_RememberPassEmail = true;
                    }

                }));
            }
        }

        // Remember Password and Useremail ---------------------------------------End







        // If Usersaved password then password box will be Disable ---------------------------------- Start
        // Don't forget to add -->  INotifyPropertyChanged;
        // Don,T forget to add in xaml -->
        //    <ContentPage.BindingContext>
        //        <ViewModels:Filename/> 
        //    </ContentPage.BindingContext>
        //  Call Command --> Commandname.Execute(null);

        public bool _PaswordEntryIsEnable = true;
        public bool PaswordEntryIsEnable
        {
            get
            {
                return _PaswordEntryIsEnable;
            }
            set
            {
                _PaswordEntryIsEnable = value;
                OnPaswordEntryIsEnablePropertyChanged("PaswordEntryIsEnable");
            }
        }
        void OnPaswordEntryIsEnablePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand PaswordEntryIsEnableCommand
        {
            get
            {
                return new Command((() =>
                {
                    if (PaswordEntryIsEnable)
                    {
                        PaswordEntryIsEnable = false;
                        //    _PaswordEntryIsEnable = false;
                    }
                    else if (!PaswordEntryIsEnable)
                    {
                        PaswordEntryIsEnable = true;
                        //_PaswordEntryIsEnable = true;
                    }

                }));
            }
        }
        //If Usersaved password then password box will be Disable ----- End



        //EmailCheck--------------------------------------------------------------------------------- Start

        // Do not forget to add 
        //  xmlns:toolkit="http://xamarin.com/schemas/2020/toolkit"
        //  download nuget also  ->> Xamarin.CommunityToolkit
        //<Entry.Behaviors>
        //    <toolkit:EventToCommandBehavior EventName = "Unfocused"
        //                                     Command="{Binding EmailEntryUnfocused}" />
        //</Entry.Behaviors>


        private bool _EmailErrorMsg = false;
        public bool EmailErrorMsg
        {
            get
            {
                return _EmailErrorMsg;
            }
            set
            {
                _EmailErrorMsg = value;
                OnEmailErrorMsgPropertyChanged("EmailErrorMsg");
            }
        }
        void OnEmailErrorMsgPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand EmailEntryUnfocused
        {
            get
            {

                return new Command((async () =>
                {
                    //SecureStorage.RemoveAll();
                    //Preferences.Clear();
                    Userdb = new FireBaseDatabase();
                    bool response = await Userdb.IsUserExists(UserEmail);
                    if (response && UserEmail != string.Empty)
                    {


                        if (Preferences.ContainsKey(UserEmail))
                        {

                            RememberPassEmailCommand.Execute(null);

                            UserPassword = await SecureStorage.GetAsync(UserEmail);

                            PaswordEntryIsEnableCommand.Execute(null);


                        }
                    }
                    else if (!response && UserEmail != string.Empty)
                    {
                        EmailErrorMsg = true;
                        //_EmailErrorMsg = true;
                        return;
                    }
                    _EmailErrorMsg = false;
                }));
            }
        }


        // EmailCheck ------------------------------------- End














        public event PropertyChangedEventHandler PropertyChanged;

        private string _UserEmail = string.Empty;
        private string _UserPassword = string.Empty;


        public string UserEmail
        {
            set
            {
                if (_UserEmail != value) { _UserEmail = value; }
                OnUserEmailPropertyChanged(UserEmail);
            }
            get
            {
                return _UserEmail;
            }


        }
        public string UserPassword
        {
            set
            {
                if (_UserPassword != value) { _UserPassword = value; }
                OnUserPasswordPropertyChanged(UserPassword);
            }
            get
            {
                return _UserPassword;
            }


        }


        void OnUserEmailPropertyChanged(string useremail)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(useremail)));
        }
        void OnUserPasswordPropertyChanged(string userpassword)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(userpassword)));
        }      








  










        // För att jobba med event exampel click event or focuse, vi måste göra lite tricks
        // 1. ladda ner nugget Xamarin Community Toolkit.
        // 2. lägga till : xmlns:toolkit="http://xamarin.com/schemas/2020/toolkit"
        // 3. sista är den -->
        //<Entry.Behaviors>
        //    <toolkit:EventToCommandBehavior EventName = "Unfocused"
        //                                     Command="{Binding EmailEntryUnfocused}" />
        //</Entry.Behaviors>

        // SingOut Depands on How user login --------------------------------------------------------- Start
        private void SigOutWay()
        {
            if (IsGoogleLogedIn)
            {
                GoogleLogout();
            }
            else
            {
                SignOutUsually();
            }

        }
        // SingOut Depands on How user login --------------------End

        

        // Account Recovery -------------------------------------------------------------------------- Start
        public ICommand AccountRecoveryCommand { get => new Command(() => AccountRecoveryClicked()); }
        private void AccountRecoveryClicked()
        {
            

            App.Current.MainPage = new AccountRecoveryViews();


        }
        // Account Recovery -----------------------------------End



        // SingUp ------------------------------------------------------------------------------------ Start
        public ICommand SignUpCommand { get => new Command(() => SignUpClicked()); }
        private void SignUpClicked()
        {
            App.Current.MainPage = new SignupView();

        }
        // SingUp------------- --------------------End


        // Log in with useremail and password --------------------------------------------------------- Start
        // 

        public ICommand LoginCommand { get => new Command(async () => await LoginBtClicked()); }
        private async Task LoginBtClicked()
        {

            ActivityIndicatorVisableCommand.Execute(null);
            try
            {
                Userdb = new FireBaseDatabase();

                bool result = await Userdb.LoginUser(UserEmail, UserPassword);
                if (result)
                {

                    if (RememberPassEmail && !Preferences.ContainsKey(UserEmail))
                    {

                        await SecureStorage.SetAsync(UserEmail, UserPassword);
                        await SecureStorage.SetAsync((UserEmail + RememberPassEmail), RememberPassEmail.ToString());
                        Preferences.Set(UserEmail, UserEmail);

                        await Application.Current.MainPage.DisplayAlert("Saved", "Saved", "OK");

                    }
                    else if (!RememberPassEmail && Preferences.ContainsKey(UserEmail))
                    {

                        SecureStorage.Remove(UserEmail);
                        SecureStorage.Remove(UserEmail + RememberPassEmail);
                        Preferences.Remove(UserEmail);

                        await Application.Current.MainPage.DisplayAlert("Remember", "removed", "OK");
                    }



                    //await Application.Current.MainPage.Navigation.PushAsync(new AppShell());
                    App.Current.MainPage = new AppShell();




                }
                else
                {
                    await Task.Delay(5000);
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid Password or Email", "OK");

                }


            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");

            }
            finally
            {
                ActivityIndicatorVisableCommand.Execute(null);
            }


        }

        private void SignOutUsually()
        {
            App.Current.MainPage = new LoginView();
        }

        // Log in with useremail and password -------- End








        // Google Servicce -----------------------------------------------------------------------------Start
        private readonly IGoogleManager _googleManager = DependencyService.Get<IGoogleManager>();
 
        public bool IsGoogleLogedIn { get; set; }


        public ICommand GoogleLoginommand { get => new Command(GoogleLogin); }

        public ICommand GoogleLogoutCommand { get => new Command(GoogleLogout); }

        private void GoogleLogout()
        {
            if (IsGoogleLogedIn)
            {
                _googleManager.Logout();
            }

            IsGoogleLogedIn = false;
            App.Current.MainPage = new LoginView();
        }

        private void GoogleLogin()
        {
            ActivityIndicatorVisableCommand.Execute(null);
            _googleManager.Login(OnGoogleLoginComplete);

        }

        private void OnGoogleLoginComplete(GoogleUser googleUser, string message)
        {
            
            if (googleUser != null)
            {

                
                UserFullName = googleUser.Name;
                UserEmail = googleUser.Email;
                var imgProfile = googleUser.Picture;
                IsGoogleLogedIn = true;
                App.Current.MainPage = new AppShell();
                return;
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Message", message, "Ok");
                ActivityIndicatorVisableCommand.Execute(null);
                return;
            }
            
        }

        // Google Servicce --------------------End

    }
}
