<%@ Page Language="C#" AutoEventWireup="true" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        String merchantKey = "T7MJR5A_STO4CiB6"; // Replace the with the Merchant Key provided by Paytm at the time of registration.

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        string paytmChecksum = "";
        var path = Server.MapPath("Content/log/log.txt");
        using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.Write))
        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs))
        {
            sw.WriteLine("Got response at :" + DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt"));
        }
        try
        {
            foreach (string key in Request.Form.Keys)
            {
                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.Write))
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fs))
                {
                    sw.WriteLine(key.Trim() + ":" + Request.Form[key].Trim());
                }
                parameters.Add(key.Trim(), Request.Form[key].Trim());
            }

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }

            if (paytm.CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                Response.Write("Checksum Matched");
            }
            else
            {
                Response.Write("Checksum MisMatch");
            }
        }
        catch (Exception)
        {

            // throw;
        }

    }
</script>
