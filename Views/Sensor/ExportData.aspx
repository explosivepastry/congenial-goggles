<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>
<%
    
    string attachment = "attachment; filename=SensorHistory.csv";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.ContentEncoding = Encoding.Default;
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "text/csv";
    HttpContext.Current.Response.AddHeader("Pragma", "public");
    
    Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
	DateTime fromDate = MonnitSession.HistoryFromDate.Date;

	DateTime From = Monnit.TimeZone.GetUTCFromLocal(fromDate, TimeZone.Info);
    DateTime To = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryToDate.Date.AddDays(1), TimeZone.Info);

    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && Model.StartDate != DateTime.MinValue && Model.StartDate.Ticks > From.Ticks)
    {
        From = Model.StartDate;
    }
    
    int limit = 5000;
    if (MonnitSession.HistoryFromDate.Date == MonnitSession.HistoryToDate.Date)
        limit = 10000;//Allow more if they are limiting to a single day
    //if (Request.QueryString["uxExportAll"] == "on")
    //{
    //    if (MonnitSession.CurrentCustomer.IsAdmin)
    //        HttpContext.Current.Response.Write(Monnit.Sensor.ToCSVFileAccount(Model.AccountID, From, To, TimeZone.Info));
    //    else
    //        HttpContext.Current.Response.Write(Monnit.Sensor.ToCSVFile(Model.CSNetID, From, To, TimeZone.Info));
    //}
    //else
    //{
    //    HttpContext.Current.Response.Write(Model.ToCSVFile(From, To, TimeZone.Info));
    //}


    //Dictionary<string, string> dic = ViewBag.Dictionary;

    switch (Request.QueryString["uxExportAll"])
    {

        case "Account":
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileAccount(Model.AccountID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        case "Network":
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileNetwork(Model.CSNetID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        default:
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileSensor(Model.SensorID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
    }
        
    HttpContext.Current.Response.End();
%>