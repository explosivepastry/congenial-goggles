<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>
<%
    
    string attachment = "attachment; filename=GatewayHistory.csv";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");
    
    Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
    
    DateTime From = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryFromDate.Date, TimeZone.Info);
    DateTime To = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryToDate.Date.AddDays(1), TimeZone.Info);
    
    if (!MonnitSession.IsCurrentCustomerMonnitAdmin && Model.StartDate != DateTime.MinValue && Model.StartDate.Ticks > From.Ticks)
    {
        From = Model.StartDate;
    }
   
    int limit = 2500;
    if (MonnitSession.HistoryFromDate.Date == MonnitSession.HistoryToDate.Date)
        limit = 10000;//Allow more if they are limiting to a single day
    
    switch (Request.QueryString["uxExportAll"])
    {
           
        case "Account":
            HttpContext.Current.Response.Write(Monnit.GatewayMessage.ToCSVFileAccountGateway(CSNet.Load(Model.CSNetID).AccountID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        case "Network":
            HttpContext.Current.Response.Write(Monnit.GatewayMessage.ToCSVFileNetworkGateway(Model.CSNetID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
        default:
            HttpContext.Current.Response.Write(Monnit.GatewayMessage.ToCSVFileGateway(Model.GatewayID, From, To, limit, ViewBag.Dictionary, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
            break;
    }
        
    HttpContext.Current.Response.End();
%>