using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webinar.ZoomAPI
{
    /// <summary>
    /// common settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Get Api Key
        /// </summary>
        public static string APIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["Api_Key"].ToString();
            }
        }

        /// <summary>
        /// Get Api Secret key
        /// </summary>
        public static string APISecret
        {
            get { return ConfigurationManager.AppSettings["Api_Secret"].ToString(); }
        }

        /// <summary>
        /// Get Api End Point
        /// </summary>
        public static string API_Url
        {
            get { return ConfigurationManager.AppSettings["Api_Url"].ToString(); }
        }

        /// <summary>
        /// Get Api Data Type
        /// </summary>
        public static string APIDataType
        {
            get { return ConfigurationManager.AppSettings["DataType"].ToString(); }
        }

        /// <summary>
        /// Get Api User Type e.g (basic,pro,corp)
        /// </summary>
        public static string APIUserType
        {
            get { return ConfigurationManager.AppSettings["UserType"].ToString(); }
        }

        /// <summary>
        /// Get Page Number
        /// </summary>
        public static string PageNumber
        {
            get { return ConfigurationManager.AppSettings["PageNumber"].ToString(); }
        }

        /// <summary>
        /// Get Api User Type e.g (basic,pro,corp)
        /// </summary>
        public static string PageSize
        {
            get { return ConfigurationManager.AppSettings["PageSize"].ToString(); }
        }

        /// <summary>
        ///  meeting type-> instant or schedule or recurring
        /// </summary>
        public static string MeetingType
        {
            get { return ConfigurationManager.AppSettings["MeetingType"].ToString(); }
        }

        /// <summary>
        ///  meeting start type-> video or screen share
        /// </summary>
        public static string MeetingStartType
        {
            get { return ConfigurationManager.AppSettings["MeetingStartType"].ToString(); }
        }

        /// <summary>
        ///  meeting Time 
        /// </summary>
        public static string MeetingStartTime
        {
            get { return ConfigurationManager.AppSettings["MeetingStartTime"].ToString(); }
        }

        /// <summary>
        ///  meeting duration
        /// </summary>
        public static string MeetingDuration
        {
            get { return ConfigurationManager.AppSettings["MeetingDuration"].ToString(); }
        }

        /// <summary>
        ///  meeting time zone
        /// </summary>
        public static string MeetingTimeZone
        {
            get { return ConfigurationManager.AppSettings["MeetingTimeZone"].ToString(); }
        }


        /// <summary>
        ///  meeting Audio type-> Telephony or voip or both share
        /// </summary>
        public static string MeetingAudioType
        {
            get { return ConfigurationManager.AppSettings["MeetingAudioType"].ToString(); }
        }


        /// <summary>
        ///  meeting Audio type-> Telephony or voip or both share
        /// </summary>
        public static bool Zoom_disable_chat
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["zoom_disable_chat"].ToString()); }
        }

    }
}
