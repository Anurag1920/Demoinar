using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webinar.DAL.Model
{
    public class WebinarDB
    {
        private PISchologistDBEntities _entities;
        public WebinarDB()
        {
            _entities = new PISchologistDBEntities();
        }

        /// <summary>
        /// Save video session details 
        /// </summary>
        /// <param name="keriLogs"></param>
        /// <returns></returns>
        public bool SaveWebinarSessionDetails(tblWebinarDetail WebinarsessionInfo)
        {
            bool result = false;
            if (WebinarsessionInfo != null)
            {
                _entities.tblWebinarDetails.Add(WebinarsessionInfo);
                _entities.SaveChanges();
                result = true;
            }
            return result;
        }

        public tblWebinarDetail GetWebinarInfo()
        {
            var webinardetail = _entities.tblWebinarDetails.OrderByDescending(x => x.WebinarId).FirstOrDefault();
            return webinardetail;
        }

       
    }
    public class WebinarVM
    {
        public int WebinarId { get; set; }
        public int WebinaruserId { get; set; }
        public string Webinar_starturl { get; set; }
        public string Webinar_joinurl { get; set; }
        public DateTime Webinar_starttime { get; set; }
        public DateTime Webinar_endtime { get; set; }
        public int Webinar_duration { get; set; }
        public int Webinar_type { get; set; }
        public string Webinar_timezone { get; set; }
        public string Webinar_meeting_id { get; set; }
        public int Webinar_meeting_status { get; set; }
        public string Webinar_host_id { get; set; }
    }
}
