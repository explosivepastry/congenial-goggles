<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    Monnit.DataMessage message = Model.LastDataMessage;

    string Display = string.Empty;
    if (message != null)
    {
        Display = message.DisplayData;
    }
    else
    {
        Display = "No data gathered";
    }
    
%>

<span><%: Html.TranslateTag("Sensor","Sensor")%>:</span>
<span style="font-size: 16px;" class="title2"><%=  (Model.SensorName) %></span>
<br />
<span>Last Check-in:</span>
<% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
   { %>
<span style="font-size: 16px;" class="title2"><%:  (Monnit.TimeZone.GetLocalTimeById(Model.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString()) %></span>
<span style="font-size: 16px;" class="title2"><%:  (Monnit.TimeZone.GetLocalTimeById(Model.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString()) %></span>
<% } %>

<span style="float: right;">Belongs to Network: 
    <%if (Model.CSNetID > 0)
      { %>
    <span class="title2"><%=CSNet.Load(Model.CSNetID).Name%></span>
    <%} %>
</span>

<table width="100%">
    <tr>
        <td align="center" width="120px">
            <div>
                <img class="applicationIcon" src="<%: Html.GetThemedContent(string.Format("/images/{1}/app{0}.png",Model.ApplicationID, Model.SensorType.WitType.ToString()))%>" alt="Sensor" />
            </div>
        </td>
        <td align="center" width="120px">
            <div class="sensorReadingDisplay" style="max-width: 120px; overflow: auto;"><%:Display %></div>
            <div>&nbsp;</div>
        </td>
        <td align="center" width="100px">
            <% if (message != null)
               {
                   if (Model.PowerSourceID == 3)
                       Response.Write("<img src='/Content/Images/Battery/Line.png' alt='Line Feed' />");
                   else if (Model.PowerSourceID == 4)
                       Response.Write(string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage));
                   else
                       Html.RenderPartial("/Views/Gauge/Battery.ascx", message.Battery.ToDouble());
               } %>
        </td>
        <td align="center" width="120px">
            <% if (message != null)
               {
                   //if (Model.EstimatedLinkMode)
                   //    message.SignalStrength = -100;
                   ViewData["Sensor"] = Model;
                   Html.RenderPartial("/Views/Gauge/SignalStrength.ascx", message);
               } %>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div><%: Html.TranslateTag("Next Check-in","Next Check-in")%>:</div>
            <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
               {
                   if (Model.NextCommunicationDate != DateTime.MinValue)
                   { %>
            <span style="margin-right:5px;" class="title2"><%:  (Monnit.TimeZone.GetLocalTimeById(Model.NextCommunicationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString())%></span>
            &nbsp;
            <span class="title2"><%:  (Monnit.TimeZone.GetLocalTimeById(Model.NextCommunicationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString())%></span>
            <span>
                <img alt="help" class="helpIcon" title="Expected Next Check-in is calculated from the last message received by the server.  If the message has been sent by the sensor to the gateway, but not yet delivered from the gateway to the server this estimate may be incorrect." src="<%:Html.GetThemedContent("/images/help.png")%>" /></span>
            <%}
                   else
                   {%>  <span class="title2">Not available</span> <%}
               }
               else
               { %>
            <span class="title2">Not available</span>
            <span>
                <img alt="help" class="helpIcon" title="Expected Next Check-in is calculated from the last message received by the server.  If the message has been sent by the sensor to the gateway, but not yet delivered from the gateway to the server this estimate may be incorrect." src="<%:Html.GetThemedContent("/images/help.png")%>" /></span>
            <% } %>
        </td>
        <td align="center">Current Reading
        </td>
        <td align="center">Battery Level
        </td>
        <td align="center">Signal Strength
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <div class="tabContainer">
                <ul id="nav">
                    <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                      { %><li><a href="/Sensor/History/<%:Model.SensorID %>">History</a></li>
                    <% } %>
                    <%if (!Request["IsModal"].ToBool())
                      { %>
                    <%if (MonnitApplicationBase.HasSensorFile(Model))
                      {%>
                    <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                      { %><li><a href="/Sensor/FileList/<%:Model.SensorID %>">File List</a></li>
                    <% } %>
                    <%}%>
                    <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                      { %><li><a href="/Notification/List/<%:Model.SensorID %>">Notifications</a></li>
                    <% } %>
                    <%if (MonnitSession.CustomerCan("Sensor_View_Chart"))
                      { %><li><a href="/Sensor/SensorReadingsChart/<%:Model.SensorID %>">Chart</a></li>
                    <% } %>
                    <%if (MonnitSession.CustomerCan("Sensor_Export_Data"))
                      { %><li><a href="/Sensor/Export/<%:Model.SensorID %>">Export</a></li>
                    <% } %>
                    <%if (MonnitSession.CustomerCan("Sensor_Edit"))
                      { %><li><a href="/Sensor/Edit/<%:Model.SensorID %>" onclick="getMain('/Sensor/Edit/<%:Model.SensorID %>', 'Edit: <%:Model.SensorName.Replace("'","").Replace("\"","") %>', false); return false;">Edit</a></li>
                    <% } %>
                    <%if (MonnitApplicationBase.HasCalibration(Model))
                      {%>
                    <%if (MonnitSession.CustomerCan("Sensor_Calibrate"))
                      { %><li><a href="/Sensor/Calibrate/<%:Model.SensorID %>"><%: Html.TranslateTag("Calibrate","Calibrate")%></a></li>
                    <% } %>
                    <% } %>
                    <% } %>
                </ul>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">
    $(function () {
        $(".tabContainer").tabs();
    });
</script>

