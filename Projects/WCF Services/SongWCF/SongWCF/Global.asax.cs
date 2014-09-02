using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SongWCF
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_OnStart(object sender, EventArgs e)
        {
            //For concurrent XML source access control
            Application["ConcurrentFlag01"] = false;
            Application["ConcurrentFlag02"] = false;
            Application["ConcurrentTurnFlag"] = false;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}