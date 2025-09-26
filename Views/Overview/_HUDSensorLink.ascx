<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();
 %>

<div class="col-md-12 col-xs-12">
    <div class="view-btns">

        <a id="indexlink" href="/Overview/Index" class="btn btn-default btn-lg btn-fill"><i class="fa fa-arrow-left"></i></a>

        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Control", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <a id="tabControl" href="Overview/SensorControl/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorControl")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-retweet "></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Control","Control") %></span></a>
        <%}%>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Notify", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <a id="a2" href="Overview/DeviceNotify/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("DeviceNotify")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-database"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Data","Data") %></span></a>
        <%}%>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Terminal", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <a id="tabTerminal" href="Overview/SensorTerminal/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorTerminal")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-terminal"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Terminal","Terminal") %></span></a>
        <%}%>
        <%if (MonnitSession.CustomerCan("Sensor_View_History"))
          { %><a id="tabHistory" href="/Overview/SensorHome/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorHome")? "primary" : "grey" %> btn-lg btn-fill"><%=Html.GetThemedSVG("list") %> <span class="extra"><%: Html.TranslateTag("History","History") %></span></a>
        <% } %>

        <%if (MonnitSession.CustomerCan("Sensor_View_Chart"))
          { %><a id="tabChart" href="/Overview/SensorChart/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorChart")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-area-chart"></i> <span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Chart","Chart") %></span></a>
        <% } %>
        <%if (MonnitApplicationBase.HasSensorFile(Model) && MonnitSession.CustomerCan("Sensor_View_History"))
          {%>
        <a id="tabFile" href="/Overview/SensorFileList/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorFileList")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-file-o"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|File List","File List") %></span></a>
        <%}%>
        <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
          { %><a id="tabNotification" href="/Overview/SensorNotification/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorNotification")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-bullhorn"></i> <span class="extra"><%: Html.TranslateTag("Rules","Rules") %></span></a>
        <% } %>
        <%if (MonnitSession.CustomerCan("Sensor_Edit"))
          { %><a id="tabEdit" href="/Overview/SensorEdit/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorEdit")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-cog"></i> <span class="extra"><%: Html.TranslateTag("Edit","Edit") %></span></a>
        <% } %>

        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Calibrate", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
          { %>
        <%if (MonnitSession.CustomerCan("Sensor_Calibrate"))
          { %>
        <a id="tabCalibrate" href="/Overview/SensorCalibrate/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorCalibrate")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-wrench"></i><span class="extra"><%: Html.TranslateTag("Calibrate","Calibrate") %></span></a>
        <%}%>
        <%}
          else
          {%>
        <a id="tabCertificate" href="/Overview/SensorCertificate/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("CalibrationCertificate")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-certificate "></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Certificate","Certificate") %></span></a>
        <%}%>
        <%}%>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Scale", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <%if (MonnitSession.CustomerCan("Sensor_Edit"))
          { %>
        <a id="tabScale" href="/Overview/SensorScale/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorScale")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-balance-scale"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Scale","Scale") %></span></a>
        <%} %>
        <%} %>
        <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("EquipmentInfo"), "Sensor", MonnitSession.CurrentTheme.Theme))
          {%>
        <a id="tabSensor" href="/Sensor/EquipmentInfo/<%:Model.SensorID %>" class="btn btn-<%:Request.RawUrl.Contains("SensorEquipmentInfo")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-wrench"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Equipment","Equipment") %></span></a>
        <%}%>
    </div>
</div>


