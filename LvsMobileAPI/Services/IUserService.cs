using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.Authorization;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IUserService
    {
        public Users GetUser(int myId);
    }


    public class UserService : IUserService
    {
        private SvrSettings srv;
        private JwtUtils jwtUtils;

        public UserService()
        {
            srv = new SvrSettings();
            jwtUtils = new JwtUtils();
        }

        public Users GetUser(int myId)
        {
            Users retUser = new Users();
            if (myId > 0)
            {
                UsersViewData userVD = new UsersViewData(new LVS.Globals._GL_USER(), myId);
                if (
                        (userVD.User is Common.Models.Users) &&
                        (userVD.User.Id > 0) &&
                        (userVD.User.UserAuthorization.access_App)
                      )
                {
                    retUser = userVD.User.Copy();
                }
            }
            return retUser;
        }
    }
}
