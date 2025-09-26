<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;



    }

    ViewData["HtmlAttributes"] = dic;
%>
<form action="/Sensor/AdvancedEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false)%>



    <div class="formtitle">
          <span> <%: Model.MonnitApplication.ApplicationName%> Sensor Configuration</span>
    </div>
    <div class="formBody">

        <input type="hidden" value="/Sensor/AdvancedEdit/<%:Model.SensorID %>" name="returns" id="returns" />
        <table style="width: 100%;">


            <% 
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_SensorName.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_HeartBeat.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_UseWithRepeater.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_TimeOfDay.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_ActiveStateInterval.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_MeasurementsPerTransmission.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/ApplicationSpecific/32/_MinMaxHyst.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_SyncronizeOffset.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_Recovery.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/_WifiSensor.ascx", Model);

                bool viewable = AC_DC_500V.ShowAdvCal(Model.SensorID);
                 if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Support_Advanced"))
                {
            %>

            <tr>
                <td style="width: 250px;">Power Options Visible</td>
                <td>
                    <input type="checkbox" title="Power Options Visible" name="showAdvCal" id="showAdvCal" <%:viewable ? "checked='checked'" : "" %> />
                </td>
            </tr>
            <script type="text/javascript">
                setTimeout("$('#showAdvCal').iButton();", 500);
            </script>
            <%}
                if (viewable)
                {
                    List<SelectListItem> List = null;
                    if (Model.SensorTypeID == 4)//MoWi
                    {
                        List = new List<SelectListItem> 
                { 
                new SelectListItem{ Text="None", Value="0", Selected = Model.Calibration3 == 0},
                new SelectListItem{ Text="Digital High", Value="1", Selected = Model.Calibration3 == 1},
                new SelectListItem{ Text="Digital Low", Value="2", Selected = Model.Calibration3 == 2}
                };
                    }
                    else
                    {
                        List = new List<SelectListItem> 
                { 
                new SelectListItem{ Text="None", Value="0", Selected = Model.Calibration3 == 0},
                new SelectListItem{ Text="Digital High", Value="1", Selected = Model.Calibration3 == 1},
                new SelectListItem{ Text="Digital Low", Value="2", Selected = Model.Calibration3 == 2},
                new SelectListItem{ Text="Amplifier SP9 Active Low", Value="3", Selected = Model.Calibration3 == 3}
                };
                    }
            %>

            <tr class="powerdrop">
                <td style="width: 250px;">Power Options</td>
                <td>
                    <%: Html.DropDownList("power", List, new {@class="powerdrop", id="power" }) %>
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
            <script type="text/javascript">
                $(document).ready(function () {
                    $("#showAdvCal").iButton({ labelOn: "Visible", labelOff: "Hidden" });
                    $(function () {
                        $("#delayInterval_Slider").slider({
                            min: 50,
                            max: 5000,
                            slide: function (event, ui) {
                                $("#delay").val(ui.value);
                            }
                        });
                    });

                    $("#delay").change(function () {
                        $("#delayInterval_Slider").slider("value", $("#delay").val());
                    });
                });
                $('#power').addClass("editField editFieldMedium");
                $('#delay').addClass("editField editFieldMedium");
                $('#lowValue').addClass("editField editFieldMedium");
                $('#highValue').addClass("editField editFieldMedium");
                $('#label').addClass("editField editFieldMedium");
            </script>
            <% } %>
        </table>
        <%:Html.Partial("Tags", Model)%>
        <div style="clear: both;"></div>
    </div>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_SaveButtons.ascx", Model);%>
</form>



