using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.Data.Model;
using System.Web.SessionState;
using HaushaltsRechner.Business.SearchParameter;

namespace HaushaltsRechner.UIProcess
{
    public static class SessionManager
    {
        private static string currentUser = "CurrentUser";
        private static string searchparameter = "SearchParameter";

        public static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;                       
            }
        }

        public static USER CurrentUser
        {
            get
            {
                return Session[currentUser] != null
                    ? Session[currentUser] as USER
                    : null;
            }
            set
            {
                Session[currentUser] = value;
            }
        }

        public static SearchParameterBase SearchParameter
        {
            get
            {
                return Session[searchparameter] != null
                    ? Session[searchparameter] as SearchParameterBase
                    : null;
            }
            set
            {
                Session[searchparameter] = value;
            }
        }

    }
}