namespace SIS.WebServer.Authorization
{
    public class AuthorizeAttribute : AccessAthribute
    {
        public AuthorizeAttribute() : base()
        {
        }

        public AuthorizeAttribute(string redirectUrl) : base(redirectUrl)
        {
        }
    }
}
