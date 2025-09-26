<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<%     DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

%>

<style>
    .refreshPic {
        display: none;
        cursor: pointer;
    }

    .ui-tabs-active .refreshPic {
        display: inline;
    }
</style>
<div>
    <span>Type: <span style="font-size: 16px;" class="title2"><%:Model.MonnitApplication.ApplicationName %></span></span>
    <span style="float: right;">Sensor ID: <span style="font-size: 16px;" class="title2"><%:Model.SensorID %></span></span>
</div>
<div>
    <span>Last Check-in:</span>
    <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5))
       { %>
    <span style="margin-right:5px;"  class="title2"><%: Monnit.TimeZone.GetLocalTimeById(Model.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString() %> -</span>
    <span class="title2"><%: Monnit.TimeZone.GetLocalTimeById(Model.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString() %></span>
    <% } %>
    <span style="float: right;">Belongs to Network: 
    <%CSNet Network = CSNet.Load(Model.CSNetID);
      if (Network != null)
      {%>
        <span style="font-size: 16px;" class="title2"><%= Network.Name%></span>
        <%} %>
    </span>
</div>
<div>
    <span><%: Html.TranslateTag("Next Check-in","Next Check-in")%>:</span>
    <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
       {
           if (Model.NextCommunicationDate != DateTime.MinValue)
           {
    %>
    <span style="margin-right:5px;" class="title2"><%: Monnit.TimeZone.GetLocalTimeById(Model.NextCommunicationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString()%></span>
    &nbsp;
    <span class="title2"><%: Monnit.TimeZone.GetLocalTimeById(Model.NextCommunicationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString()%></span>
    <span>
        <img alt="help" class="helpIcon" title="Expected Next Check-in is calculated from the last message received by the server.  If the message has been sent by the sensor to the gateway, but not yet delivered from the gateway to the server this estimate may be incorrect." src="<%:Html.GetThemedContent("/images/help.png")%>" /></span>
    <% }
       }
       else
       { %>
    <span class="title2">Not available</span>
    <span>
        <img alt="help" class="helpIcon" title="Expected Next Check-in is calculated from the last message received by the server.  If the message has been sent by the sensor to the gateway, but not yet delivered from the gateway to the server this estimate may be incorrect." src="<%:Html.GetThemedContent("/images/help.png")%>" /></span>
    <% } %>
    <%if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
      { %>
    <% if (Model.LastDataMessage != null)
       {
           if (!Model.IsWiFiSensor)
           { %>
				<span style="float: right;">GatewayID: <span class="title2"><%:Model.LastDataMessage.GatewayID > 0 ? Model.LastDataMessage.GatewayID.ToString() : ""%></span></span>
				<%  if (Model.ParentID > 0 && Model.ParentID != Model.LastDataMessage.GatewayID)
					{
						Sensor sens = Sensor.Load(Model.ParentID);
						if (sens != null && sens.ApplicationID == 45)
						{%>
							<span style="float: right; margin-right: 5px;">Repeater: <span class="title2"><%:Model.ParentID > 0 ? Model.ParentID.ToString() : ""%></span></span>
						<%}
					}
           }
           else
           {
               Gateway gate = Gateway.Load(Model.LastDataMessage.GatewayID);%>
			   <span style="float: right;">MacAddress: <span class="title2"><%: gate.MacAddress%></span></span>

		<%}
       } %><% } %>
</div>
<div class="tabContainer">
    <ul>
        
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("ApplicationSpecific\\{0}\\Control", Model.ApplicationID), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <li><a id="tabControl" href="/Sensor/Control/<%:Model.SensorID %>">Control 
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%}%>
         <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("ApplicationSpecific\\{0}\\Notify", Model.ApplicationID), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <li><a id="a2" href="/Sensor/Notify/<%:Model.SensorID %>">Data 
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%}%>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("ApplicationSpecific\\{0}\\Terminal", Model.ApplicationID), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <li><a id="tabTerminal" href="/Sensor/Terminal/<%:Model.SensorID %>">Terminal 
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%}%>
        <%if (MonnitSession.CustomerCan("Sensor_View_History"))
          { %><li><a id="tabHistory" href="/Sensor/History/<%:Model.SensorID %>">History
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
          </a></li>
        <% } %>
        <%if (!Request["IsModal"].ToBool())
          { %>
        <%if (MonnitSession.CustomerCan("Sensor_View_Chart"))
          { %><li><a id="tabChart" href="/Sensor/SensorReadingsChart/<%:Model.SensorID %>">Chart
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
          </a></li>
        <% } %>
        <%if (MonnitApplicationBase.HasSensorFile(Model) && MonnitSession.CustomerCan("Sensor_View_History"))
          {%>
        <li><a id="tabFile" href="/Sensor/FileList/<%:Model.SensorID %>">File List
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
        <%}%>
        <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
          { %><li><a id="tabNotification" href="/Notification/List/<%:Model.SensorID %>">Notifications
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
          </a></li>
        <% } %>
        <%if (MonnitSession.CustomerCan("Sensor_Export_Data"))
          { %><li><a class="exportTab" id="tabExport" href="/Sensor/Export/<%:Model.SensorID %>">Export
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
          </a></li>
        <% } %>
        <%if (MonnitSession.CustomerCan("Sensor_Edit"))
          { %><li><a id="tabEdit" href="/Sensor/Edit/<%:Model.SensorID %>">Edit
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
          </a></li>
        <% } %>

        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("ApplicationSpecific\\{0}\\Calibrate", Model.ApplicationID), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <%
            CalibrationCertificate certificate = CalibrationCertificate.LoadBySensor(Model);
            if ((certificate != null && certificate.isInternalCert) || (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow)))
          { %>
        <%if (MonnitSession.CustomerCan("Sensor_Calibrate"))
          { %>
        <li><a id="tabCalibrate" href="/Sensor/Calibrate/<%:Model.SensorID %>"><%: Html.TranslateTag("Calibrate","Calibrate")%>
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" /></a></li>
        <%}%>
        <%}
          else
          {%>
        <li><a id="tabCertificate" href="/Sensor/CalibrationCertificate/<%:Model.SensorID %>">Certificate</a></li>
        <%}%>
        <%}%>
        <%int id = Convert.ToInt32(Model.ApplicationID); %>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("ApplicationSpecific\\{0}\\Scale", Model.ApplicationID), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <%if (MonnitSession.CustomerCan("Sensor_Edit"))
          { %>
        <li><a id="tabScale" href="/Sensor/Scale/<%:Model.SensorID %>">Scale
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" /></a></li>
        <%} %>
        <%} %>
         <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("EquipmentInfo"), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <li><a id="tabSensor" href="/Sensor/EquipmentInfo/<%:Model.SensorID %>">Equipment 
            <img class="refreshPic" src="/Content/images/SmallRefresh.png" alt="refresh" />
        </a></li>
         <%}%>
        <% } %>
    </ul>
</div>

<script type="text/javascript">
    $(function () {
        var index = $('#tab<%:Request["tab"]%>').parent().index();
        if (index < 0)
            index = 0;

        $(".tabContainer").on("tabsbeforeload", function (event, ui) {
           
            ui.panel.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
        });
      
        $(".tabContainer").tabs({ active: index });
       

        $('.refreshPic').click(function () {
           
            var tabContainer = $('.tabContainer').tabs();
            var active = tabContainer.tabs('option', 'active');
            tabContainer.tabs('load', active);
           
        });

        
        $(".helpIcon").tipTip();
    });
</script>

