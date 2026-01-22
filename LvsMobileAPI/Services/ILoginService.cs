using Common.ApiModels;
using LVS.ViewData;
using LvsMobileAPI.Authorization;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface ILoginService
    {
        public ResponseLogin Authenticate(UserLogin userLogin);
    }

    public class LoginService : ILoginService
    {
        private SvrSettings srv;
        private JwtUtils jwtUtils;

        public LoginService()
        {
            srv = new SvrSettings();
            jwtUtils = new JwtUtils();
        }

        public ResponseLogin Authenticate(UserLogin userLogin)
        {
            ResponseLogin log = new ResponseLogin();
            if (
                    (!userLogin.Username.Equals(string.Empty)) &&
                    (!userLogin.Password.Equals(string.Empty))
               )
            {
                UsersViewData userVD = new UsersViewData(userLogin.Username, userLogin.Password, true);
                if (
                    (userVD.User is Common.Models.Users) &&
                    (userVD.User.Id > 0) &&
                    (userVD.User.UserAuthorization.access_App)
                  )
                {
                    try
                    {
                        //string tokenString = GenerateToken(userVD.User);
                        string tokenString = jwtUtils.GenerateToken(userVD.User);
                        log.AccessGranted = true;
                        log.AccessToken = tokenString;
                        log.LoggedUser = userVD.User;
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                        log.Error = str;
                    }
                }
                else
                {
                    log.Error = "Zugriffsdaten fehlerhaft oder User besitzt keine Berechtigung!";
                }
            }
            else
            {
                log.Error = "Achtung - Suchparameter fehlen!";
            }
            return log;
        }
    }
}
