using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webinar.Web.Helper
{
    public class SessionManager
    {
        public static int UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] != null)
                    return (int)HttpContext.Current.Session["UserId"];
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["UserId"] = value;
            }
        }


        public static string Email
        {
            get
            {
                if (HttpContext.Current.Session["Email"] != null)
                    return HttpContext.Current.Session["Email"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["Email"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["UserName"] != null)
                    return HttpContext.Current.Session["UserName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static bool? UserIsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["UserIsAdmin"] != null)
                    return (bool)HttpContext.Current.Session["UserIsAdmin"];
                else
                    return false;
            }
            set
            {
                HttpContext.Current.Session["UserIsAdmin"] = value;
            }
        }


    }
}