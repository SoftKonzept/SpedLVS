using LvsScan.Portable.Settings;
using LvsScan.Portable.ViewModels.Login;
using LvsScan.Portable.Views.Menu;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LvsScan.Portable.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = ViewModel = new LoginViewModel();
            this.ViewModel.IsBusy = false;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            string str = string.Empty;
            this.ViewModel.Username = "sl2";
            this.ViewModel.Password = "???";
#if DEBUG
            //---- VORGABEN ---------------
            this.ViewModel.Username = "Admin";
            this.ViewModel.Password = "lvs";
#else
#endif
            Task.Delay(10);
            btnLogin_Clicked(this, new System.EventArgs());
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void btnLogin_Clicked(object sender, System.EventArgs e)
        {
            if (ViewModel.IsLoginButtonEnabled)
            {
                ViewModel.IsBusy = true;
                //await Task.Run(() => ViewModel.DoLogin()).Wait()
                Task.Run(() => ViewModel.DoLogin()).Wait();
                if (InternalSettings.AccessGranted)
                {
                    Application.Current.MainPage = new FlyoutMenuPage();
                }
                else
                {
                    await DisplayAlert("ACHTUNG", ViewModel.Log.Error, "ok");
                    ViewModel.IsBusy = false;
                }
            }
            else
            {
                await DisplayAlert("ACHTUNG", "Eingabe für Username und Passwort fehlen!", "ok");
            }
        }
    }
}