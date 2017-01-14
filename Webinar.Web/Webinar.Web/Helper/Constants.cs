using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CentisibleCalling.Web.Helper
{

    /// <summary>
    /// Constant messages
    /// </summary>
    public class Constants
    {
        public const string success = "1";
        public const string partialsuccess = "2";
        public const string unsuccessful = "3";
        public const string alreadyexist = "4";
        public const string CreditCardFailure = "5";
        public const string SessionFailure = "6";
        public const string updatesuccess = "7";
        public const string IncompleteOrder = "8";
        public const string NoTextMessage = "9";
        public const string InstallerMsg = "10";
        public const string OS = "11";

    }

    /// <summary>
    /// Messages
    /// </summary>
    public class Messages
    {
        //Email Messages
        public const string Forgotemailsuccess = "1";
        public const string Forgotemailfail = "2";
        public const string NotExist = "3";

        //Order Messages
        public const string OrderSuccess = "1";
        public const string OrderFailure = "2";
        public const string OrderTransactionFailure = "3";
        public const string OrderInprogress = "4";
        public const string DataFailure = "5";

        //Webinar Messages
        public const string WebinarSuccess = "1";
        public const string WebinarFailure = "2";
        public const string WebinarTransactionFailure = "3";

    }



    /// <summary>
    /// Web config settings
    /// </summary>
    public class WebConfigurationSettings
    {

        /// <summary>
        /// Get AGENT_ID
        /// </summary>
        public static short ITEM_QTY
        {
            get { return Convert.ToInt16(ConfigurationManager.AppSettings["ITEM_QTY"].ToString()); }
        }

        /// <summary>
        /// Get STORE_ID
        /// </summary>
        public static int? STORE_ID
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["STORE_ID"].ToString()); }
        }

        /// <summary>
        /// Get AGENT_ID
        /// </summary>
        public static int? AGENT_ID
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["AGENT_ID"].ToString()); }
        }

        /// <summary>
        /// Get AGENT_CODE
        /// </summary>
        public static string AGENT_CODE
        {
            get { return ConfigurationManager.AppSettings["AGENT_CODE"].ToString(); }
        }

        /// <summary>
        /// Get ShippingCost
        /// </summary>
        public static int ITEM_ShippingCost
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ITEM_ShippingCost"].ToString()); }
        }

        /// <summary>
        /// Get AGENT_ID
        /// </summary>
        public static int ITEM_TaxAmount
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["ITEM_TaxAmount"].ToString()); }
        }

        /// <summary>
        /// Get AccessNumber
        /// </summary>
        public static string AccessNumber
        {
            get { return ConfigurationManager.AppSettings["AccessNumber"].ToString(); }
        }

        /// <summary>
        /// Get smsAtt
        /// </summary>
        public static string smsAtt
        {
            get { return ConfigurationManager.AppSettings["smsAtt"].ToString(); }
        }


        /// <summary>
        /// Get smsVerizon
        /// </summary>
        public static string smsVerizon
        {
            get { return ConfigurationManager.AppSettings["smsVerizon"].ToString(); }
        }

        /// <summary>
        /// Get smsSprint
        /// </summary>
        public static string smsSprint
        {
            get { return ConfigurationManager.AppSettings["smsSprint"].ToString(); }
        }


        /// <summary>
        /// Get smsTmobile
        /// </summary>
        public static string smsTmobile
        {
            get { return ConfigurationManager.AppSettings["smsTmobile"].ToString(); }
        }

        /// <summary>
        /// Get smsMetro
        /// </summary>
        public static string smsMetro
        {
            get { return ConfigurationManager.AppSettings["smsMetro"].ToString(); }
        }
        /// <summary>
        /// Get smsFrom
        /// </summary>
        public static string smsFrom
        {
            get { return ConfigurationManager.AppSettings["smsFrom"].ToString(); }
        }

        /// <summary>
        /// Get smsHost
        /// </summary>
        public static string smsHost
        {
            get { return ConfigurationManager.AppSettings["smsHost"].ToString(); }
        }

        /// <summary>
        /// Get pp Partner
        /// </summary>
        public static string ppPartner
        {
            get { return ConfigurationManager.AppSettings["ppPartner"].ToString(); }
        }

        /// <summary>
        /// Get pp User
        /// </summary>
        public static string ppUser
        {
            get { return ConfigurationManager.AppSettings["ppUser"].ToString(); }
        }

        /// <summary>
        /// Get ppVendor
        /// </summary>
        public static string ppVendor
        {
            get { return ConfigurationManager.AppSettings["ppVendor"].ToString(); }
        }

        /// <summary>
        /// Get ppPwd
        /// </summary>
        public static string ppPwd
        {
            get { return ConfigurationManager.AppSettings["ppPwd"].ToString(); }
        }

        /// <summary>
        /// Get host Port
        /// </summary>
        public static int hostPort
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["hostPort"].ToString()); }
        }

        /// <summary>
        /// Get time Out
        /// </summary>
        public static int timeOut
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["timeOut"].ToString()); }
        }


        //Zoom video settings
        public static string ZoomIosAndroidProtocol
        {
            get { return ConfigurationManager.AppSettings["ZoomIosAndroidProtocol"]; }
        }

        //Zoom video settings
        public static string ZoomWindowMacProtocol
        {
            get { return ConfigurationManager.AppSettings["ZoomWindowMacProtocol"]; }
        }


    }


}