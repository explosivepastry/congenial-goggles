<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>
<%

    //string attachment = "attachment; filename=RuleHistory.csv";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    //HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentEncoding = Encoding.Default;
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");

    Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);

    DateTime From = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryFromDate.Date, TimeZone.Info);
    DateTime To = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryToDate.Date, TimeZone.Info);



    //Validate From Date not more than 7 days after To date
    if ((To - From).TotalDays > 7)
    {
        From = To.AddDays(-7);
    }
    else if ((To - From).TotalDays < 0)
    {
        From = To.AddDays(-1);
    }

    string attachment = string.Format("attachment;filename=RuleHistory_{0}_{1}.csv", MonnitSession.CurrentCustomer.FormatDateTime(From), MonnitSession.CurrentCustomer.FormatDateTime(To));
    attachment = Regex.Replace(attachment, @"\s{1,}", "_");

    HttpContext.Current.Response.AddHeader("content-disposition", attachment);

    HttpContext.Current.Response.Write(NotificationHistory.ToCSVFile(Model.AccountID, From, To,1000,Model.NotificationID,MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));

    HttpContext.Current.Response.End();
%>