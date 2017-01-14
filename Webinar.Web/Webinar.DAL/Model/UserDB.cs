using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace Webinar.DAL.Model
{
    public class UserDB
    {
        private PISchologistDBEntities _entities;
        public UserDB()
        {
            _entities = new PISchologistDBEntities();
        }

        #region "User"

        public long GetUserID(PISchologistDBEntities _entities, string email)
        {
            long id = 0;

            var item = _entities.tblUsers.Where(p => p.PI_email == email).FirstOrDefault();
            if (item != null)
            {
                id = item.PI_id;
            }

            return id;
        }

        public tblUser GetUser(PISchologistDBEntities _entities, long Id)
        {
            tblUser item = _entities.tblUsers.FirstOrDefault(p => p.PI_id == Id);
            return item;
        }

        public tblZoomInstaller GetInstallerInfo(PISchologistDBEntities _entities, long Id)
        {
            tblZoomInstaller item = _entities.tblZoomInstallers.FirstOrDefault(p => p.UserId == Id);
            return item;
        }

        public tblUser saveCustomer(int customerid, string phone, string email, string password, string firstname, string lastname, string city, string state, string zip, int country, string billingInfo, bool cust_inactive, string Pi_zoom_user_id)
        {
            try
            {
                // var _country = _entities.countries.Where(x => x.ID == country).FirstOrDefault();
                var objuserinfo = _entities.tblUsers.Where(x => x.PI_email == email).FirstOrDefault();
                if (objuserinfo == null)
                {
                    tblUser _user = new tblUser();
                    _user.PI_email = email;
                    _user.PI_phone_number = phone;
                    _user.PI_password = password;
                    _user.PI_fname = firstname;
                    _user.PI_lname = lastname;
                    _user.PI_zoom_user_id = Pi_zoom_user_id;
                    _user.PI_timezone = "GMT+5:30";
                    _user.PI_created_date = DateTime.Now;
                    _user.PI_Isadmin = false;
                    //_user.zip = zip;
                    //_user.state = state;
                    //_user.country = (_country != null) ? _country.country1 : null;
                    _entities.tblUsers.Add(_user);
                    _entities.SaveChanges();

                    var objcustomer = _entities.tblUsers.OrderByDescending(x => x.PI_id).FirstOrDefault();
                    return objcustomer;
                }
                else
                {
                    var Updatecustomer = _entities.tblUsers.Where(x => x.PI_id == objuserinfo.PI_id).FirstOrDefault();
                    Updatecustomer.PI_email = email;
                    Updatecustomer.PI_phone_number = phone;
                    Updatecustomer.PI_password = password;
                    Updatecustomer.PI_fname = firstname;
                    Updatecustomer.PI_lname = lastname;
                    //Updatecustomer.city = city;
                    //Updatecustomer.zip = zip;
                    //Updatecustomer.state = state;
                    //Updatecustomer.country = (_country != null) ? _country.country1 : null;
                    _entities.Entry(Updatecustomer).State = System.Data.Entity.EntityState.Modified;
                    _entities.SaveChanges();

                    var objcustomer = _entities.tblUsers.OrderByDescending(x => x.PI_id == objuserinfo.PI_id).FirstOrDefault();
                    return objcustomer;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }

    public class UserVM
    {
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string password { get; set; }
        public string confirmpassword { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public bool inactive { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int stateId { get; set; }
        public string zip { get; set; }
        public int cid { get; set; }
        public string fax { get; set; }
        public string cardType { get; set; }
        public string ipaddress { get; set; }
        public Nullable<decimal> totamt { get; set; }
        public Nullable<decimal> peramt { get; set; }
        public SelectList Statelist { get; set; }
        public SelectList Countrylist { get; set; }
        public SelectList amountlist { get; set; }
        public string amount { get; set; }
        public string cardNumber { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public string cardVerificationcode { get; set; }
        public string bankname { get; set; }
        public string UserMessage { get; set; }
        public string billingInfo { get; set; }

        public string card { get; set; }
        public bool removeCheck { get; set; }

        public tblUser user { get; set; }
        public List<tblUser> userList { get; set; }
        public string installermsg { get; set; }

    }
}
