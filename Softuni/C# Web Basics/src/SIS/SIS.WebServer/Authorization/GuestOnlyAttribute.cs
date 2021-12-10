namespace SIS.WebServer.Authorization
{
    public class GuestOnlyAttribute : AccessAthribute
    {
        public GuestOnlyAttribute() : base()
        {
        }

        public GuestOnlyAttribute(string redirectUrl) : base(redirectUrl)
        {
        }
    }
}
