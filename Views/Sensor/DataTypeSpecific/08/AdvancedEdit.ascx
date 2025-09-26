<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;

        
    }

    ViewData["HtmlAttributes"] = dic;
%>
<form action="/Sensor/AdvancedEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>

    <div class="formtitle">
          <span> <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span>
    </div>
    <div class="formBody">
        <input type="hidden" value="/Sensor/AdvancedEdit/<%:Model.SensorID %>" name="returns" id="returns" />

        <table style="width: 100%;">
            <%--Sensor Name--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);%>

            <%--Is Sensor Active--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_IsSensorActive.ascx", Model);%>

            <%--Heartbeat--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);%>

            <%--Use with Repeater--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_UseWithRepeater.ascx", Model);%>

            <%--Time of Day Active    <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/22/_LowHighLabel.ascx", Model);%>--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_TimeOfDay.ascx", Model);%>

            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_Aware.ascx", Model);%>

            <%--Active State Interval--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_ActiveStateInterval.ascx", Model);%>
            <% 
                if (Monnit.VersionHelper.IsVersion_1_0(Model))
                {
                    //Don't render the Profile settings for version 1.0 Sensors
                }
                else
                {
                    if (Model.MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
                    {
               
        
            %>
            <%--Measurements Per Transmission--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_MeasurementsPerTransmission.ascx", Model);%>

            <%--Min Max Hyst--%>
            <% Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/22/_MinMaxHyst.ascx", Model);%>

            <%--Syncronize Offset--%>
            <%if (Model.SensorTypeID != 4)
              { %>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SyncronizeOffset.ascx", Model);%>

            <% } %>


            <%
            }
            else //eApplicationProfileType.Trigger
            {%>

            <%--Event Detection--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_EventDetection.ascx", Model);%>

            <%--Rearm Time--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_RearmTime.ascx", Model);%>
            <% }
        }
            %>

            <%--Recovery--%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);%>
            <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);%>
            <%
                bool viewable = Analog.ShowAdvCal(Model.SensorID);

                 if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                {
            %>

            <tr>
                <td style="width: 250px;">Viewable Content</td>
                <td>
                    <input type="checkbox" title="Viewable Content" name="showAdvCal" id="showAdvCal" <%if (viewable) { Response.Write("checked='checked'"); }  %> />
                </td>
            </tr>

            <%}
           if (viewable)
           {
            %>

            <tr class="powerdrop">
                <td style="width: 250px;">Power Options</td>
                <td>
                    <%: Html.DropDownList("power", new List<SelectListItem> 
                                                                { 
                                                                new SelectListItem{ Text="None", Value="0", Selected = Model.Calibration3 == 0},
                                                                new SelectListItem{ Text="Digital High", Value="1", Selected = Model.Calibration3 == 1},
                                                                new SelectListItem{ Text="Digital Low", Value="2", Selected = Model.Calibration3 == 2},
                                                                new SelectListItem{ Text="Amplifier SP9 Active Low", Value="3", Selected = Model.Calibration3 == 3}
                                                                
                                                                }, new {@class="powerdrop", id="power" }) %>
                </td>
            </tr>

            <tr class="delaymil">
                <td>Delay in milliseconds</td>
                <td>
                    <%: Html.TextBox("delay",Model.Calibration4, new { id = "delay", @class="delaymil" })%> milliseconds
                </td>
                <td class="delaymil">
                    <img alt="help" class="helpIcon" title="The Amount of Time delayed default 0" src="<%:Html.GetThemedContent("/images/help.png")%>" />
                </td>
            </tr>

            <tr class="delaymil">
                <td></td>
                <td colspan="2">
                    <div id="delayInterval_Slider"></div>
                </td>
            </tr>
            <% } %>
            <script type="text/javascript">
                $(document).ready(function(){
                    $("#showAdvCal").iButton({ labelOn: "View" , labelOff: "Hide" });
                    $(function() {
                        $( "#delayInterval_Slider" ).slider({
                            range: "max",
                            min: 0,
                            max: 50, 
                        
                            slide: function( event, ui ) {
                                $( "#delay" ).val( ui.value );
                            }
                        });
                    });
            
                    $("#delay").change(function() {
                        $("#delayInterval_Slider").slider("value",$("#delay").val());
                    }); 
                });  

                $('#power').addClass("editField editFieldMedium");
                $('#delay').addClass("editField editFieldMedium");
                $('#lowValue').addClass("editField editFieldMedium");
                $('#highValue').addClass("editField editFieldMedium");
                $('#label').addClass("editField editFieldMedium");
            </script>
        </table>
        <%:Html.Partial("Tags", Model)%>
        <div style="clear: both;"></div>
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>

