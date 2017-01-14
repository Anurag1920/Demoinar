using paytm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Webinar.Web
{
    public partial class paytmresponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String merchantKey = "T7MJR5A_STO4CiB6"; // Replace the with the Merchant Key provided by Paytm at the time of registration.

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                parameters.Add(key.Trim(), Request.Form[key].Trim());
            }

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }

            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                Response.Write("Checksum Matched");
            }
            else
            {
                Response.Write("Checksum MisMatch");
            }
        }
    }
}