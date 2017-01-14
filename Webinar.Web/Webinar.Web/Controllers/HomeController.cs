using CentisibleCalling.Web.Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Webinar.DAL;
using Webinar.DAL.Model;
using Webinar.Web.Helper;
using Webinar.ZoomAPI;

namespace Webinar.Web.Controllers
{
    public class HomeController : Controller
    {

        #region "Intialization"
        UserDB user = new UserDB();
        CustCardDB custCard = new CustCardDB();
        WebinarDB webinar = new WebinarDB();
        PISchologistDBEntities _entities = new PISchologistDBEntities();

        #endregion

        #region "Authorization"

        public class ValidateUserAttribute : AuthorizeAttribute
        {
            /// <summary>
            /// Authorize User for module if required...not using
            /// </summary>
            /// <param name="httpContext"></param>
            /// <returns></returns>
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                dynamic Result = false;

                if ((httpContext == null))
                {
                    return Result;
                }
                if (SessionManager.UserId > 0)
                {
                    Result = true;
                }
                return Result;
            }
        }
        #endregion

        public ActionResult Index()
        {
            //string api_response = string.Empty;
            //string Pi_zoom_user_id = string.Empty;
            //var users = _entities.tblUsers.ToList();
            //foreach (var user in users)
            //{
            //    api_response = ZoomApiRequest.CustCreateUser("Savin", "Sindhu", "savin@physicssimplified.com");
            //    if (!api_response.Contains("error"))
            //    {
            //        var jsondata = new JavaScriptSerializer();
            //        Dictionary<string, string> sData = jsondata.Deserialize<Dictionary<string, string>>(api_response);
            //        Pi_zoom_user_id = sData["id"].ToString();


            //    }
            //}
            //string api_response = string.Empty;
            //string Pi_zoom_user_id=string.Empty;
            //// Auto Create User on Zoom Api 
            //api_response = ZoomApiRequest.CustCreateUser("Savin", "Sindhu", "savin@physicssimplified.com");
            //if (!api_response.Contains("error"))
            //{
            //    var jsondata = new JavaScriptSerializer();
            //    Dictionary<string, string> sData = jsondata.Deserialize<Dictionary<string, string>>(api_response);
            //    Pi_zoom_user_id = sData["id"].ToString();
            //    // user.keri_zoom_user_id = keri_zoom_user_id;
            //}

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        #region "Login User"

        public bool ValidateLoginForm(string Email, string Password)
        {
            bool result = false;
            if (string.IsNullOrEmpty(Email.Trim()))
            {
                ModelState.AddModelError("UserName", "Please specify the email first.");
                result = true;
                return result;// = true;
            }
            if (string.IsNullOrEmpty(Password.Trim()))
            {
                ModelState.AddModelError("Password", "Please specify the password first.");
                result = true;
                return result;
            }
            return result;
        }

        public ActionResult LoginPost(UserVM objUserVM)
        {
            string email = objUserVM.email;
            string Password = objUserVM.password;
            if (ValidateLoginForm(email, Password))
            {
                return View("Index");
            }
            using (var _entities = new PISchologistDBEntities())
            {
                //check user exists            
                long UserID = user.GetUserID(_entities, email);
                if (UserID > 0)
                {
                    tblUser objuser = user.GetUser(_entities, UserID);
                    if (Password.Trim() != objuser.PI_password.Trim())
                    {
                        ModelState.AddModelError("ErrorMsg", "Login was unsuccessfull, Please try again.");
                        return View("Index");
                    }
                    else
                    {
                        SessionManager.UserId = objuser.PI_id;
                        SessionManager.UserName = objuser.PI_fname + " " + objuser.PI_lname;
                        SessionManager.UserIsAdmin = objuser.PI_Isadmin;
                        return RedirectToAction("DashBoard");
                    }
                }
                else
                {
                    ModelState.AddModelError("ErrorMsg", "Incorrect username or password.");
                    return View("Index");
                }
            }

        }

        [HttpPost]
        public JsonResult SignUp(UserVM model, string cardType)
        {
            try
            {
                #region "commented code for card payment"

                //if (CardStatus == "verified")
                //{
                //    itemmast itemmastinfo = custCard.GetItemAmountInfo(itemId);
                //    if (SessionManager.OrderId == 0)
                //    {
                //        orderid order = CustOrder.saveOrderId(itemId);
                //        if (order != null)
                //        {
                //            SessionManager.OrderId = order.orderid1;

                //            CustOrder.SaveldCartt(itemId, SessionManager.OrderId, WebConfigurationSettings.ITEM_QTY, itemmastinfo.itemprice, CustomerIP);
                //            var objcartInfo = _entities.GetOrderInfo(SessionManager.OrderId).FirstOrDefault();
                //            var objGetSumm = _entities.GetSumm(SessionManager.OrderId).FirstOrDefault();
                //            double? OrderAmt = WebConfigurationSettings.ITEM_ShippingCost + objGetSumm.Value + WebConfigurationSettings.ITEM_TaxAmount;


                //            //Save customer Info
                //            customer customer = Customer.saveCustomer(model.customerid, model.phoneno, model.email, model.password, model.firstname, model.lastname, model.city, model.state, model.zip, model.cid, model.billingInfo, CustomerIP, false);
                //            if (customer != null)
                //            {
                //                //Save card Info in DB
                //                bool IsCardSaved = custCard.saveCustomerCard(customer.customerid, Convert.ToDecimal(OrderAmt), model.cardNumber, cardType, model.year, model.month, model.cardVerificationcode, model.bankname);

                //                SessionManager.CustomerId = customer.customerid;
                //                SessionManager.Amount = Convert.ToDecimal(OrderAmt);
                //                bool Isphonesaved = Customer.SaveCustomerPhone(model.phoneno, customer.customerid);

                //                //Now Add the number API
                //                var result = voxplus.CreateApiAccount(model.phoneno, model.amount);
                //                NameValueCollection Collection = HttpUtility.ParseQueryString(result);
                //                string RESULT = Collection["result"];
                //                if (RESULT.ToLower() == "success")
                //                {
                //                    var OrderlineId = _entities.AddOrder(SessionManager.OrderId, SessionManager.CustomerId, WebConfigurationSettings.STORE_ID, WebConfigurationSettings.AGENT_ID, WebConfigurationSettings.AGENT_CODE, DateTime.Now, "", "", "", "", "", "", "", customer.country, 99, 0, Convert.ToDecimal(OrderAmt), "none", CustomerIP, customer.firstname, customer.lastname, true, "PLD", "WEB");

                //                    //int OrderneId = OrderlineId;
                //                    bool OrderItems = CustOrder.SaveOrderItems(itemId, SessionManager.OrderId, WebConfigurationSettings.ITEM_QTY, itemmastinfo.itemprice);
                //                    //Delete records in temp cart table 
                //                    bool deleteOrder = CustOrder.RemoveldCartt(SessionManager.OrderId);

                //                    #region "Send Email Process"

                //                    //Send ver Pld Email to Customer
                //                    string verPldEmailToCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Templates") + "\\ver-pld-mail.html");
                //                    bool verPldmailsend = EmailHelper.SendMail(customer.email, "Your Prepaid Long Distance Order", verPldEmailToCustomer);



                //                    //Send Email to Customer
                //                    string EmailToCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Templates") + "\\CustEmailNotification.html");
                //                    EmailToCustomer = EmailToCustomer.Replace("[CustomerName]", customer.firstname + " " + customer.lastname);
                //                    EmailToCustomer = EmailToCustomer.Replace("[Address]", customer.address1 + " " + customer.address2);
                //                    EmailToCustomer = EmailToCustomer.Replace("[Region]", customer.city + " " + customer.state + " " + customer.zip);
                //                    EmailToCustomer = EmailToCustomer.Replace("[country]", customer.country);
                //                    EmailToCustomer = EmailToCustomer.Replace("[ONumber]", SessionManager.OrderId.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemcode]", itemId.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemqty]", WebConfigurationSettings.ITEM_QTY.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemprice]", Convert.ToString(itemmastinfo.itemprice));
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemamt]", objGetSumm.Value.ToString());

                //                    EmailToCustomer = EmailToCustomer.Replace("[nSubTot]", objGetSumm.Value.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[ShippingCost]", WebConfigurationSettings.ITEM_ShippingCost.ToString("0.00"));
                //                    EmailToCustomer = EmailToCustomer.Replace("[taxamount]", WebConfigurationSettings.ITEM_TaxAmount.ToString("0.00"));
                //                    EmailToCustomer = EmailToCustomer.Replace("[OrderAmt]", OrderAmt.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[CardNumber]", model.cardNumber);

                //                    bool mailsend = EmailHelper.SendMail(customer.email, "Centsible Prepaid Long Distance", EmailToCustomer);

                //                    //Send SMS to Customer
                //                    bool sendSMS = (new SmsHelper()).SendMessageServiceToCustomer(SessionManager.UserId, OrderAmt.ToString());

                //                    //Send Email to Admin team
                //                    string EmailToAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Templates") + "\\AdminEmailNotification.html");
                //                    EmailToCustomer = EmailToCustomer.Replace("[CustomerId]", customer.customerid.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[CustomerName]", customer.firstname + " " + customer.lastname);
                //                    EmailToCustomer = EmailToCustomer.Replace("[Address]", customer.address1 + " " + customer.address2);
                //                    EmailToCustomer = EmailToCustomer.Replace("[Region]", customer.city + " " + customer.state + " " + customer.zip);
                //                    EmailToCustomer = EmailToCustomer.Replace("[country]", customer.country);
                //                    EmailToCustomer = EmailToCustomer.Replace("[ONumber]", SessionManager.OrderId.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemcode]", Convert.ToString(itemId));
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemqty]", WebConfigurationSettings.ITEM_QTY.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemprice]", Convert.ToString(itemmastinfo.itemprice));
                //                    EmailToCustomer = EmailToCustomer.Replace("[itemamt]", objGetSumm.Value.ToString());

                //                    EmailToCustomer = EmailToCustomer.Replace("[nSubTot]", objGetSumm.Value.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[ShippingCost]", WebConfigurationSettings.ITEM_ShippingCost.ToString("0.00"));
                //                    EmailToCustomer = EmailToCustomer.Replace("[taxamount]", WebConfigurationSettings.ITEM_TaxAmount.ToString("0.00"));
                //                    EmailToCustomer = EmailToCustomer.Replace("[OrderAmt]", OrderAmt.ToString());
                //                    EmailToCustomer = EmailToCustomer.Replace("[CardNumber]", model.cardNumber);

                //                    EmailHelper.SendMail(ConfigurationManager.AppSettings["EMADDRESS"], "Centsible-New Web Site Acct", EmailToCustomer);
                //                    SessionManager.OrderId = 0;
                //                    return Json(new { success = Constants.success }, JsonRequestBehavior.AllowGet);
                //                    #endregion
                //                }
                //                else
                //                {
                //                    var OrderlineId = _entities.AddOrder(SessionManager.OrderId, SessionManager.CustomerId, WebConfigurationSettings.STORE_ID, WebConfigurationSettings.AGENT_ID, WebConfigurationSettings.AGENT_CODE, DateTime.Now, "", "", "", "", "", "", "", customer.country, 99, 0, Convert.ToDecimal(OrderAmt), "none", CustomerIP, customer.firstname, customer.lastname, false, "PLD", "WEB");
                //                    return Json(new { success = Constants.IncompleteOrder }, JsonRequestBehavior.AllowGet);
                //                }
                //            }
                //            else
                //            {
                //                SessionManager.OrderId = 0;
                //                return Json(new { success = Constants.unsuccessful }, JsonRequestBehavior.AllowGet);
                //            }
                //        }
                //        else
                //        {
                //            SessionManager.OrderId = 0;
                //            return Json(new { success = Constants.unsuccessful }, JsonRequestBehavior.AllowGet);
                //        }
                //    }
                #endregion
                string api_response = string.Empty;
                string Pi_zoom_user_id = string.Empty;
                // Auto Create User on Zoom Api 
                api_response = ZoomApiRequest.CustCreateUser(model.firstname, model.lastname, model.email);
                if (!api_response.Contains("error"))
                {
                    var jsondata = new JavaScriptSerializer();
                    Dictionary<string, string> sData = jsondata.Deserialize<Dictionary<string, string>>(api_response);
                    Pi_zoom_user_id = sData["id"].ToString();
                    // user.keri_zoom_user_id = keri_zoom_user_id;
                }

                //Save customer Info
                tblUser objUserInfo = user.saveCustomer(model.userid, model.phone, model.email, model.password, model.firstname, model.lastname, model.city, model.state, model.zip, model.cid, model.billingInfo, true, Pi_zoom_user_id);
                if (objUserInfo != null)
                {
                    //Save card Info in DB
                    // bool IsCardSaved = custCard.saveCustomerCard(objUserInfo.PI_id, Convert.ToDecimal(model.amount), model.cardNumber, cardType, model.year, model.month, model.cardVerificationcode, model.bankname);


                    return Json(new { success = Constants.success }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = Constants.unsuccessful }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                string Except = ex.Message;
                return Json(new { success = false, Message = Except }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region "DashBoard"
        [ValidateUser()]
        public ActionResult DashBoard()
        {
            UserVM objUserVM = new UserVM();
            ViewData["custid"] = SessionManager.UserId;
            return View("DashBoard", objUserVM);
        }

        public ActionResult LogOff()
        {
            SessionManager.UserId = 0;
            SessionManager.UserName = string.Empty;
            Session.Clear();
            // Delete the user details from cache.
            Session.Abandon();
            return RedirectToAction("Index");
        }
        #endregion

        /// <summary>
        /// Start Webinar Here
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ValidateUser()]
        public JsonResult StartWebinar()
        {
            string api_response = string.Empty;
            string StartUrl = string.Empty;
            string Message = string.Empty;
            //get user detail
            tblWebinarDetail WebinarSessionDetail = null;
            try
            {
                tblUser objuserinfo = user.GetUser(_entities, SessionManager.UserId);

                #region "Start Webinar"

                api_response = ZoomApiRequest.CreateWebinar("Physics Webinar", "JF9ZMFo2RPeQcBiGlUVM9g", "5", DateTime.Now.AddMinutes(2), "60", 0);
                if (!api_response.Contains("error"))
                {
                    var jsondata = new JavaScriptSerializer();
                    Dictionary<string, string> sData = jsondata.Deserialize<Dictionary<string, string>>(api_response);

                    //insert meeting Info 
                    WebinarSessionDetail = new tblWebinarDetail();
                    WebinarSessionDetail.Webinar_meeting_id = sData["id"].ToString();
                    WebinarSessionDetail.Webinar_host_id = sData["host_id"].ToString();
                    WebinarSessionDetail.Webinar_meeting_status = int.Parse(sData["status"].ToString());
                    WebinarSessionDetail.Webinar_starturl = sData["start_url"].ToString();
                    WebinarSessionDetail.Webinar_joinurl = sData["join_url"].ToString();
                    WebinarSessionDetail.Webinar_starttime = DateTime.Parse(sData["created_at"].ToString());
                    string uuid = sData["uuid"].ToString();
                    bool webinarsession = webinar.SaveWebinarSessionDetails(WebinarSessionDetail);

                    //Launch video
                    System.Web.HttpBrowserCapabilitiesBase browser = Request.Browser;
                    string agent = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();
                    if (agent.ToLower().Contains("windows nt"))
                    {
                        //launch zoom video start meeting on windows/mac(without branding)
                        StartUrl = "zoommtg://zoom.us/start?browser=" + browser.Browser.ToLower() + "&confno=" + WebinarSessionDetail.Webinar_meeting_id + "&zc=0&stype=" + 99 + "&sid=" + WebinarSessionDetail.Webinar_host_id + "&uid=" + WebinarSessionDetail.Webinar_host_id + "&token=" + GetZoomHostToken(WebinarSessionDetail.Webinar_host_id) + "&uname=Physics Simplified";

                        Message = Constants.success;
                    }
                    else
                    {
                        //StartUrl = "zoomus://zoom.us/start?browser=" + browser.Browser.ToLower() + "&confno=" + WebinarSessionDetail.Webinar_meeting_id + "&zc=0&stype=" + 99 + "&sid=" + WebinarSessionDetail.Webinar_host_id + "&uid=" + WebinarSessionDetail.Webinar_host_id + "&token=" + GetZoomHostToken(WebinarSessionDetail.Webinar_host_id) + "&uname=Physics Simplified";

                        Message = Constants.OS;
                    }
                }
                else
                {
                    Message = Constants.unsuccessful;
                }

                #endregion
            }
            catch (Exception ex)
            {
                Message = Constants.unsuccessful;
            }
            return Json(new { success = Message, StartWebinarUrl = StartUrl }, JsonRequestBehavior.AllowGet);

        }

        protected string GetZoomHostToken(string keri_host_id)
        {
            string zoom_host_token = string.Empty;
            string api_response = ZoomApiRequest.GetUser(keri_host_id);
            if (!api_response.Contains("error"))
            {
                var data = new JavaScriptSerializer();
                Dictionary<string, string> sData = data.Deserialize<Dictionary<string, string>>(api_response);
                zoom_host_token = sData["token"];
            }
            return zoom_host_token;
        }

        /// <summary>
        /// Join Webinar Here
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ValidateUser()]
        public JsonResult JoinWebinar()
        {
            string Message = string.Empty;
            string JoinUrl = string.Empty;
            string api_response = string.Empty;

            //Get User info
            tblUser objuserinfo = user.GetUser(_entities, SessionManager.UserId);
            string zoom_user_id = objuserinfo.PI_zoom_user_id;
            tblWebinarDetail objWebinarsessionInfo = webinar.GetWebinarInfo();
            string webinar_meeting_id = objWebinarsessionInfo.Webinar_meeting_id;

            //launch meeting
            System.Web.HttpBrowserCapabilitiesBase browser = Request.Browser;
            string agent = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();
            if (agent.ToLower().Contains("windows nt"))
            {
                //Register Webinar 
                api_response = ZoomApiRequest.RegisterWebinar(webinar_meeting_id, objuserinfo.PI_fname, objuserinfo.PI_lname, objuserinfo.PI_email);
                if (!api_response.Contains("error"))
                {
                    var jsondata = new JavaScriptSerializer();
                    Dictionary<string, string> sData = jsondata.Deserialize<Dictionary<string, string>>(api_response);
                   
                    //launch zoom video start meeting on windows/mac(without branding)
                    JoinUrl = "zoommtg://zoom.us/join?confno=" + webinar_meeting_id + "&pwd=xxxx&zc=0&browser=" + browser.Browser.ToLower() + "&uname=" + objuserinfo.PI_fname + " " + objuserinfo.PI_lname + "&email=" + objuserinfo.PI_email;
                }
                Message = Constants.success;
            }
            else
            {
                Message = Constants.OS;
            }

            return Json(new { success = Message, JoinWebinarUrl = JoinUrl }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// check zoom software insalled or not
        /// </summary>
        /// <param name="p_name">zoom</param>
        /// <returns></returns>
        public JsonResult IsApplicationInstalled()
        {
            string p_name = "zoom";
            string displayName;
            RegistryKey key;
            bool status = false;

            // search in: CurrentUser
            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            foreach (String keyName in key.GetSubKeyNames())
            {
                RegistryKey subkey = key.OpenSubKey(keyName);
                displayName = subkey.GetValue("DisplayName") as string;
                if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                {
                    status = true;
                }
            }

            #region "Local Machine"
            //// search in: LocalMachine_32
            //key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            //foreach (String keyName in key.GetSubKeyNames())
            //{
            //    RegistryKey subkey = key.OpenSubKey(keyName);
            //    displayName = subkey.GetValue("DisplayName") as string;
            //    if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
            //    {
            //        status = true;
            //    }
            //}

            //// search in: LocalMachine_64
            //key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
            //foreach (String keyName in key.GetSubKeyNames())
            //{
            //    RegistryKey subkey = key.OpenSubKey(keyName);
            //    displayName = subkey.GetValue("DisplayName") as string;
            //    if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
            //    {
            //        status = true;
            //    }
            //}
            #endregion

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);

        }

    }
}