using Common.ApiModels;
using LvsScan.Portable.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LvsScan.Portable.ViewModels.Login
{
    public class LoginViewModel : BaseViewModel
    {
        private api_Login api_Login;
        public LoginViewModel()
        {
            Title = "LOGIN";
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                SetProperty(ref _Username, value);
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                SetProperty(ref _Password, value);
            }
        }

        private ResponseLogin _log;
        public ResponseLogin Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        private bool _IsLoginButtonEnabled;
        public bool IsLoginButtonEnabled
        {
            get
            {
                _IsLoginButtonEnabled = false;
                _IsLoginButtonEnabled = ((Username != null) && (!Username.Equals(string.Empty)));
                _IsLoginButtonEnabled = ((Password != null) && (!Password.Equals(string.Empty)));
                return _IsLoginButtonEnabled;
            }
        }
        public async Task DoLogin()
        {
            IsBusy = true;

            UserLogin LoginInquiry = new UserLogin()
            {
                Username = Username,
                Password = Password,
            };

            api_Login = new api_Login();
            var login = await api_Login.Login(LoginInquiry);

            if (login is ResponseLogin)
            {
                Log = login;
                if (login.AccessGranted)
                {
                    LvsScan.Portable.Settings.InternalSettings.Username = Username;
                    LvsScan.Portable.Settings.InternalSettings.Password = Password;
                    LvsScan.Portable.Settings.InternalSettings.AccessToken = login.AccessToken;
                    LvsScan.Portable.Settings.InternalSettings.AccessGranted = login.AccessGranted;
                    ((App)Application.Current).LoggedUser = login.LoggedUser.Copy();
                }
                else
                {
                    LvsScan.Portable.Settings.InternalSettings.Username = string.Empty;
                    LvsScan.Portable.Settings.InternalSettings.Password = string.Empty;
                    LvsScan.Portable.Settings.InternalSettings.AccessToken = string.Empty;
                    LvsScan.Portable.Settings.InternalSettings.AccessGranted = false;
                }
            }

            //login = new ResponseLogin();
            //login.AccessGranted = true;
            //login.AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9naXZlbm5hbWUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBZG1pbmlzdHJhdG9yIiwibmJmIjoxNjgxNzU4NTE4LCJleHAiOjE2ODE3NjIxMTgsImlzcyI6Imh0dHBzOi8vMTkyLjE2OC4xLjU1OjQ0MyIsImF1ZCI6Imh0dHBzOi8vMTkyLjE2OC4xLjU1OjQ0MyJ9.hvaa3nNA1Z4Nb46wWTyTgCIkId3NwLMSR6EE_cEFB4o";

            //LvsScan.Portable.Settings.InternalSettings.Username = Username;
            //LvsScan.Portable.Settings.InternalSettings.Password = Password;
            //LvsScan.Portable.Settings.InternalSettings.AccessToken = login.AccessToken;
            //LvsScan.Portable.Settings.InternalSettings.AccessGranted = login.AccessGranted;
            //login.LoggedUser = new Common.Models.Users();
            //login.LoggedUser.Id = 1;
            //login.LoggedUser.LoginName = "TestAdmin";
            //login.LoggedUser.Name = Username;
            //login.LoggedUser.pass = Password;

            ((App)Application.Current).LoggedUser = login.LoggedUser.Copy();

            IsBusy = false;
        }
    }
}
