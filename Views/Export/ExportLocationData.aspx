<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>
<%
    
    string attachment = "attachment; filename=LocationHistory.csv";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");
    
	Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
	
	DateTime From = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryFromDate.Date, TimeZone.Info);
    DateTime To = Monnit.TimeZone.GetUTCFromLocal(MonnitSession.HistoryToDate.Date, TimeZone.Info);
    
    //Validate From Date is after they added device to network
	if (!MonnitSession.IsCurrentCustomerMonnitAdmin && Model.StartDate != DateTime.MinValue && Model.StartDate.Ticks > MonnitSession.HistoryFromDate.Ticks)
	{
		From = Monnit.TimeZone.GetLocalTimeById(Model.StartDate, MonnitSession.CurrentCustomer.Account.TimeZoneID);
	}

	//Validate From Date not more than 7 days after To date
	if ((To - From).TotalDays > 7)
	{
		From = To.AddDays(-7);
	}
	else if ((To - From).TotalDays < 0)
	{
		From = To.AddDays(-1);
	}

    
    HttpContext.Current.Response.Write(Monnit.LocationMessage.ToCSVFile(Model.GatewayID, From, To, MonnitSession.CurrentCustomer.Account.TimeZoneID, MonnitSession.CurrentCustomer.Preferences["Date Format"], MonnitSession.CurrentCustomer.Preferences["Time Format"]));
        
    HttpContext.Current.Response.End();
%>