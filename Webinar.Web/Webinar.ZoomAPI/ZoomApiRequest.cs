using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Webinar.ZoomAPI
{
    public class ZoomApiRequest
    {
        #region "Variables"

        private static string BaseUrl = Settings.API_Url;
        private static string Key = Settings.APIKey;
        private static string Secret = Settings.APISecret;
        private static string DataType = Settings.APIDataType;
        private static string UserType = Settings.APIUserType;
        private static string MeetingType = Settings.MeetingType;
        private static string MeetingStartType = Settings.MeetingStartType;
        private static string MeetingAudioType = Settings.MeetingAudioType;
        private static bool Zoom_disable_chat = Settings.Zoom_disable_chat;
        #endregion

        #region "Methods"

        /// <summary>
        /// Make Api Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string MakeAPIRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.UnsafeAuthenticatedConnectionSharing = true;
            request.ContentLength = 0;


            /******* Get response **********/
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                /*******grab the response **********/
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                            #region Comment
                            //XmlDocument xmlDoc = new XmlDocument();
                            //xmlDoc.Load(response.GetResponseStream());
                            //XmlReader xmlReader = new XmlNodeReader(xmlDoc);
                            //responseValue.ReadXml(xmlReader);   
                            #endregion
                        }
                }
                return responseValue;
            }
        }

        #region "User"
        /// <summary>
        /// create user
        /// </summary>
        /// <returns></returns>
        public static string CreateUser(string first_name, string last_name, string email)
        {
            string fullUrl = BaseUrl + "user/create" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&email=" + email + "&type=" + UserType + "&first_name=" + first_name + "&last_name=" + last_name;

            return MakeAPIRequest(fullUrl);
        }


        /// <summary>
        /// Auto create User
        /// </summary>
        /// <returns></returns>
        public static string AutoCreateUser(string first_name, string last_name, string email, string password)
        {
            string fullUrl = BaseUrl + "user/autocreate" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&email=" + email + "&type=" + UserType + "&password=" + password + "&first_name=" + first_name + "&last_name=" + last_name;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// create Cust user, has no password and is not able to log into the Zoom web site or client.
        /// </summary>
        /// <returns></returns>
        public static string CustCreateUser(string first_name, string last_name, string email)
        {
            string fullUrl = BaseUrl + "user/custcreate" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&email=" + email + "&type=" + UserType + "&disable_chat=" + Zoom_disable_chat + "&first_name=" + first_name + "&last_name=" + last_name;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Update User or Patient
        /// </summary>
        /// <returns></returns>
        public static string UpdateUser(string user_id, string first_name, string last_name)
        {
            string fullUrl = BaseUrl + "user/update" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&id=" + user_id + "&type=" + UserType + "&first_name=" + first_name + "&last_name=" + last_name;
            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Delete User or Patient
        /// </summary>
        /// <returns></returns>
        public static string DeleteUser(string user_id)
        {
            string fullUrl = BaseUrl + "user/delete" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&id=" + user_id;
            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Get User from Api
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static string GetUser(string user_id)
        {
            string fullUrl = BaseUrl + "user/get" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&id=" + user_id;
            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Get User List from Api
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public string UsersDetailList(string user_id)
        {
            string fullUrl = BaseUrl + "user/list" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&page_size=" + Settings.PageSize + "&page_number=" + Settings.PageNumber;
            return MakeAPIRequest(fullUrl);
        }

        #endregion

        #region "Meeting"

        /// <summary>
        ///Create meetings(instant or schedule)
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>

        public static string CreateMeeting(string host_user_id, string meeting_topic)
        {
            string fullUrl = string.Empty;
            /* Instant Meetings */
            fullUrl = BaseUrl + "meeting/create" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&topic=" + meeting_topic + "&type=" + MeetingType + "&option_jbh=" + true + "&option_start_type=" + MeetingStartType + "&option_no_video_host=" + false + "&option_audio=" + MeetingAudioType;

            return MakeAPIRequest(fullUrl);

        }

        /// <summary>
        /// Delete meeting
        /// </summary>
        /// <returns></returns>
        public string DeleteMetting(string host_user_id, int meeting_id)
        {
            string fullUrl = string.Empty;
            fullUrl = BaseUrl + "meeting/delete" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&id=" + meeting_id;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// End meeting Details
        /// </summary>
        /// <returns></returns>
        public static string EndMeeting(string host_user_id, int meeting_id)
        {
            string fullUrl = BaseUrl + "meeting/end" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&id=" + meeting_id;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Get meeting Details
        /// </summary>
        /// <returns></returns>
        public static string GetMeetingsDetails(string host_user_id, long meeting_id)
        {
            string fullUrl = BaseUrl + "meeting/get" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&id=" + meeting_id;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Update Meeting Details
        /// </summary>
        /// <returns></returns>
        public string UpdateMeetingDetails(string host_user_id, int meeting_id)
        {
            string fullUrl = BaseUrl + "meeting/update" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&id=" + meeting_id + "&topic=" + "My Meeting" + "&type=" + Settings.MeetingType + "&start_time=" + DateTime.Now + "&duration=" + Settings.MeetingDuration + "&timezone=" + Settings.MeetingTimeZone + "&option_start_type=" + Settings.MeetingStartType;

            return MakeAPIRequest(fullUrl);
        }


        /// <summary>
        /// List Meetings
        /// </summary>
        /// <returns></returns>
        public string ListMeetings(string host_user_id, int meeting_id)
        {
            string fullUrl = BaseUrl + "meeting/list" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_user_id + "&page_size=" + Settings.PageSize + "&page_number=" + Settings.PageNumber;

            return MakeAPIRequest(fullUrl);
        }

        #endregion

        #region "Webinar"

        /// <summary>
        /// create Webinar
        /// </summary>
        /// <returns></returns>
        public static string CreateWebinar(string topic, string host_id, string webinar_type, DateTime start_time, string duration, int approval_type)
        {
            string fullUrl = BaseUrl + "webinar/create" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&topic=" + topic + "&type=" + webinar_type + "&start_time=" + start_time + "&duration=" + duration + "&timezone=GMT+5:30&approval_type=" + approval_type + "&option_audio=both&option_enforce_login=true";

            // api_key=hgAbio4nTOWVBBkvp8arWA&api_secret=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw&data_type=JSON&host_id=hHpiyL67RNmK7rqjj4VOgQ&topic=test&type=5&start_time=2016-12-20T22%3A00%3A00Z&duration=60&timezone=GMT%2B5%3A30&approval_type=2&option_audio=both&option_enforce_login=true
            //api_key=hgAbio4nTOWVBBkvp8arWA&api_secret=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw&data_type=JSON&host_id=hHpiyL67RNmK7rqjj4VOgQ&topic=tesrt&type=5&start_time=2016-12-18T15%3A00%3A00Z&duration=60&timezone=GMT%2B5%3A30&approval_type=2&option_audio=both&option_enforce_login=true
            return MakeAPIRequest(fullUrl);
        }


        /// <summary>
        /// Delete Webinar
        /// </summary>
        /// <returns></returns>
        public static string DeleteWebinar(string host_id, string meeting_Id)
        {
            string fullUrl = BaseUrl + "webinar/delete" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&id=" + meeting_Id;
            //api_key=hgAbio4nTOWVBBkvp8arWA&api_secret=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw&data_type=JSON&host_id=JF9ZMFo2RPeQcBiGlUVM9g&id=JF9ZMFo2RPeQcBiGlUVM9g
            return MakeAPIRequest(fullUrl);
        }


        /// <summary>
        /// List all registrationless Webinars
        /// </summary>
        /// <returns></returns>
        public static string ListWebinar(string host_id)
        {
            string fullUrl = BaseUrl + "webinar/list" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&page_size=30&page_number=1";
            return MakeAPIRequest(fullUrl);

        }


        /// <summary>
        /// upcoming registration-based Webinars
        /// </summary>
        /// <returns></returns>
        public static string RegistrationBasedWebinar(string host_id)
        {
            string fullUrl = BaseUrl + "webinar/list/registration" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&page_size=30&page_number=1";

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Get a Webinar via Webinar ID
        /// </summary>
        /// <returns></returns>
        public static string GetWebinarInfo(string host_id, string meeting_Id)
        {
            string fullUrl = BaseUrl + "webinar/get" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&id=" + meeting_Id;

            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Update a Webinar via Webinar ID
        /// </summary>
        /// <returns></returns>
        public static string UpdateWebinar(string host_id, string meeting_Id, string webinar_type, DateTime start_time, string duration, int approval_type)
        {
            string fullUrl = BaseUrl + "webinar/update" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&id=" + meeting_Id + "&type=" + webinar_type + "&start_time=" + start_time + "&duration=" + duration + "&timezone=GMT+5:30&approval_type=" + approval_type + "&option_audio=both";

            //api_key=hgAbio4nTOWVBBkvp8arWA&api_secret=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw&data_type=JSON&host_id=JF9ZMFo2RPeQcBiGlUVM9g&id=222&type=5&start_time=2016-12-6T20%3A00%3A00Z&duration=60&timezone=GMT%2B5%3A30&approval_type=2&option_audio=both
            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Update a Webinar via Webinar ID
        /// </summary>
        /// <returns></returns>
        public static string EndWebinarInfo(string host_id, string meeting_Id)
        {
            string fullUrl = BaseUrl + "webinar/end" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&id=" + meeting_Id;
            //=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw&data_type=JSON&host_id=JF9ZMFo2RPeQcBiGlUVM9g&id=44
            return MakeAPIRequest(fullUrl);
        }

        /// <summary>
        /// Register a Webinar via Webinar ID
        /// </summary>
        /// <returns></returns>
        public static string RegisterWebinar(string meeting_id, string first_name, string last_name, string email)
        {
            string fullUrl = BaseUrl + "webinar/register" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&id=" + meeting_id + "&first_name=" + first_name + "&last_name=" + last_name + "&email=" + email;
            return MakeAPIRequest(fullUrl);
        }


        /// <summary>
        /// Registrant a Webinar via Webinar ID
        /// </summary>
        /// <returns></returns>
        public static string RegisterantApprove(string host_id, string meeting_Id)
        {
            string fullUrl = BaseUrl + "webinar/registrants/approve" + "?api_key=" + Key + "&api_secret=" + Secret + "&data_type=" + DataType + "&host_id=" + host_id + "&id=" + meeting_Id;
            //  api_key=hgAbio4nTOWVBBkvp8arWA api_secret=qeH6cIruBU0CIigyhKMaf8pFE4HfRDUbq3Fw data_type=JSON  host_id=JF9ZMFo2RPeQcBiGlUVM9g id=22 registrant_ids=2222
            return MakeAPIRequest(fullUrl);
        }




        #endregion
        #endregion
    }
}
