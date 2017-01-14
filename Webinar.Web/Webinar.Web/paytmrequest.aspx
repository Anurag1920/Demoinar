﻿<%@ Page Language="C#" AutoEventWireup="true"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Random rnd = new Random();

        var orderid = rnd.Next(10000);
        var amount = 12;
        String merchantKey = "T7MJR5A_STO4CiB6";
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("MID", "PiScho94182826905052");
        parameters.Add("CHANNEL_ID", "WEB");
        parameters.Add("INDUSTRY_TYPE_ID", "Retail");
        parameters.Add("WEBSITE", "WEB_STAGING");
        // parameters.Add("EMAIL", "annucs2009008@gmail.com");
        parameters.Add("MOBILE_NO", "7777777777");
        parameters.Add("CUST_ID", "123");
        parameters.Add("ORDER_ID", orderid.ToString());
        parameters.Add("TXN_AMOUNT", amount.ToString());

        parameters.Add("CALLBACK_URL", @"http://physicssimplified.com/cms/paytmresponse.aspx"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

        string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);
        string paytmURL = "https://pguat.paytm.com/oltp-web/processTransaction?orderid=" + orderid;
        string outputHTML = "<html>";
        outputHTML += "<head>";
        outputHTML += "<title>Merchant Check Out Page</title>";
        outputHTML += "</head>";
        outputHTML += @"<body onload='document.forms[""f1""].submit()'>";
        outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
        outputHTML += "<table border='1'>";
        outputHTML += "<tbody>";
        foreach (string key in parameters.Keys)
        {
            outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
        }
        outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
        outputHTML += "</tbody>";
        outputHTML += "</table>";
        //outputHTML += "<script type='text/javascript'>";
        //outputHTML += "document.f1.submit();";
     //  outputHTML += "
        outputHTML += "</form>";
        outputHTML += "</body>";
        outputHTML += "</html>";
        Response.Write(outputHTML);
    }
</script>