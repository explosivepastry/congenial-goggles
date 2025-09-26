<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.PreAggregatePageModel>" %>



<div class="x_title">
    <h2  style="overflow:unset;"><%: Html.TranslateTag("Overview/SensorHud|Battery Life","Battery Life")%></h2>
    <div class="clearfix"></div>
</div>
<div class="x_content">
       <div class="col-12 col-lg-12" style="text-align:center;">
        <%
            if (Model.PreAggregateList.Count > 0)
            {
                long powerSourceID = Model.sensor.PowerSourceID;
                                
                double avgVoltage = Model.PreAggregateList.Average(m => m.Avg_Voltage);
                double avgBattery = PowerSource.Load(powerSourceID).Percent(avgVoltage);
        %>
            <div class="battIcon">
                <% if (powerSourceID == 3 || avgVoltage == 0)
                       Response.Write("<img src='" + Html.GetThemedContent("/Images/Battery/Line.png") + "' alt='Line Feed' />");
                   else if (powerSourceID == 4)
                       Response.Write(string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", avgVoltage));
                   else
                   {
                       Html.RenderPartial("/Views/Gauge/Battery.ascx", avgBattery);
                   } 
                %>
            </div>
            <span  style="font-size: 2.5em; font-weight: bold;"><%=avgBattery.ToString("##") %></span>
        <%}
        else
        { %>
        <span style="font-size: 2.0em;"><%: Html.TranslateTag("Overview/SensorHud|Offline","Offline")%></span>
        <%} %>
    </div>
</div>

















