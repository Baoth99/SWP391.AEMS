namespace AEMS.Utilities
{
    public interface IAuthSession
    {
        public UserSession UserSession { get; }

        void SetUserSession(UserSession user);
    }

    public class AuthSession : IAuthSession
    {
        public UserSession UserSession { get; private set; }

        public void SetUserSession(UserSession user)
        {
            UserSession = user;
        }
    }

}
