using System;

namespace SIS.WebServer.Authorization
{
    public abstract class AccessAthribute : Attribute
    {
        protected AccessAthribute()
        {
            RedirectUrl = "/";
        }

        protected AccessAthribute(string redirectUrl)
        {
            RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; }
    }
}
