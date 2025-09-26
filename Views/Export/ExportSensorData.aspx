<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<long>" %>
<%

    Sensor sensor = Sensor.Load(Model);

    if (sensor == null || !MonnitSession.CurrentCustomer.CanSeeAccount(sensor.AccountID) || (sensor.AccountID == MonnitSession.CurrentCustomer.AccountID && !MonnitSession.CurrentCustomer.CanSeeSensor(sensor)))
    {
        sensor = new Sensor();
    }

    Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
    DateTime From = ViewBag.FromDate ?? Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryFromDate, TimeZone.Info);
    DateTime To = ViewBag.ToDate ?? Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryToDate, TimeZone.Info);

    string fileName = "SensorHistory";
    // Optional metadata the can be included in the filename 
    if (true)// && sensor.SensorID > 0)   // (ViewBag.AppendSensorID == true)
    {
        fileName += "_" + sensor.SensorID;
    }
    if (true)   // (ViewBag.AppendDateTimeRange == true)
    {
        fileName += string.Format("_{0}-{1}", From.ToString("yyyyMMdd"), To.ToString("yyyyMMdd"));
    }
    if (true)   // (ViewBag.AppendTimestamp == true)
    {
        fileName += "_" + (DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds.ToInt();
    }

    fileName += ".csv";

    //string attachment = "attachment; filename=SensorHistory.csv";
    string attachment = "attachment; filename=" + fileName;
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.ContentEncoding = Encoding.Default;
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");

    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && sensor.StartDate != DateTime.MinValue && sensor.StartDate.Ticks > From.Ticks)
    {
        From = sensor.StartDate;
    }

    int limit = 5000;
    if (From.Date == To.Date)
        limit = 10000;//Allow more if they are limiting to a single day


    switch (Request.QueryString["uxExportAll"])
    {

        case "Account":
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileAccount(sensor.AccountID, From, To, limit,ViewBag.Dictionary,MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"],MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        case "Network":
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileNetwork(sensor.CSNetID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        default:
            HttpContext.Current.Response.Write(Monnit.DataMessage.ToCSVFileSensor(sensor.SensorID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
    }

    HttpContext.Current.Response.End();
%>