<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NotificationRecorded>" %><?xml version="1.0" encoding="UTF-8"?>
<%
    string DeviceName = "";
    CSNet Network = null;
    int NetworkCount = 0;
    Account acnt = null;
    long TimeZoneID = long.MinValue;

    if (Model.SensorID > 0)
    {
        Sensor s = Sensor.Load(Model.SensorID);
        DeviceName = s.SensorName;
        Network = CSNet.Load(s.CSNetID);

    }
    if (Model.GatewayID > 0)
    {
        Gateway g = Gateway.Load(Model.GatewayID);
        DeviceName = g.Name;
        Network = CSNet.Load(g.CSNetID);
    }

    if (Network != null)
    {
        acnt = Account.Load(Network.AccountID);
        TimeZoneID = acnt.TimeZoneID;
        NetworkCount = CSNet.LoadByAccountID(Network.AccountID).Count();
    }



    DateTime LocalTime = Model.NotificationDate;
    if (TimeZoneID > 0)
        LocalTime = Monnit.TimeZone.GetLocalTimeById(Model.NotificationDate, TimeZoneID);
    
    string DateString = string.Format("At {0}{1}", LocalTime.ToShortTimeString(), TimeZoneID > 0 ? "" : " UTC");
        
    if (Model.NotificationDate < DateTime.UtcNow.AddHours(-12))//older than 12 hours
    {
        string strDay = LocalTime.Day.ToString();
        switch (LocalTime.Day)
        {
            case 1:
            case 21:
            case 31:
                strDay += "st";
                break;
            case 2:
            case 22:
                strDay += "nd";
                break;
            case 3:
            case 23:
                strDay += "rd";
                break;
            default:
                strDay += "th";
                break;
        }
        DateString = string.Format("On {0:MMMM} {1} {2}", LocalTime, strDay, DateString);
    }
%><Response>
    <Pause length="1"/>
	<%if (Request["Repeat"].ToBool() == false) { %>
    <Play><%: Html.GetThemedContent("NotificationPreamble.mp3")%></Play>
	<Pause length="1"/>
    <%}%>
	<Say><%:DateString %>. <%:DeviceName %> 
        <%if(NetworkCount > 1){ %> from network <%:Network.Name %>. <%} %>
        reported: <%:Regex.Replace(Model.Reading, @"<[^>]+>|&nbsp;|\r|\t", " ").Trim() %></Say>
	<Pause length="1"/>
	<Say><%:Model.NotificationText %></Say>
	<Pause length="1"/>
     <Gather action="/notification/TwilioCall/<%:Model.CustomerID %>?notificationRecordedID=<%:Model.NotificationRecordedID %>&amp;Repeat=True" method="POST"  numDigits="1" timeout="5">  
         <Say>Press one to acknowledge this notification</Say>
    <%if (Request["Repeat"].ToBool() == false)
      { %>
	
		<Say>Press two to repeat</Say>
	
	<%}%>
    </Gather> 
	<Say>Goodbye</Say>
</Response>
