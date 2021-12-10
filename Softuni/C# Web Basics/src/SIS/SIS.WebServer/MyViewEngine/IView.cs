﻿using SIS.WebServer.DataManager;

namespace SIS.WebServer.MyViewEngine
{
    public interface IView
    {
        public string ExecuteTemplate(object viewModel, IdentityUser user);
    }
}
