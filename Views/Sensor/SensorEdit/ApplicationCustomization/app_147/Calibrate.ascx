<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<style>
    table, th, td {
        border: 1px solid black;
    }
</style>

<%
    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);

%>


<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>

    <% if (Model.LastDataMessage == null)
        {%>
    <div class="formBody" style="font-weight: bold">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Calibration for this sensor is not available until it has checked in at least one time.")%>
    </div>
    <div class="buttons">&nbsp; </div>

    <% }
        else
        {
            string rawHumidity = Humidity_5PCal.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).HumRaw.ToString();
            string humidity = Humidity_5PCal.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Hum.ToString();

            //string rawHumidity = "0";
            //string humidity = "0";

            double rawHumidity10 = Humidity_5PCal.GetLower2MaxThresh_RawHumidity(Model);
            double calHumidity10 = Humidity_5PCal.GetUpper2MaxThresh_CalHumidity(Model);

            double rawHumidity30 = Humidity_5PCal.GetLower2CalVal1_RawHumidity(Model);
            double calHumidity30 = Humidity_5PCal.GetUpper2CalVal1_CalHumidity(Model);

            double rawHumidity50 = Humidity_5PCal.GetLower2CalVal2_RawHumidity(Model);
            double calHumidity50 = Humidity_5PCal.GetUpper2CalVal2_CalHumidity(Model);

            double rawHumidity70 = Humidity_5PCal.GetLower2CalVal3_RawHumidity(Model);
            double calHumidity70 = Humidity_5PCal.GetUpper2CalVal3_CalHumidity(Model);

            double rawHumidity90 = Humidity_5PCal.GetLower2CalVal4_RawHumidity(Model);
            double calHumidity90 = Humidity_5PCal.GetUpper2CalVal4_CalHumidity(Model);

    %>
    <div class="form-group">

        <div class="bold col-md-11 col-sm-11 col-xs-12">
            <table style="width: 100%">
                <tr>
                    <th></th>
                    <th>10%</th>
                    <th>30%</th>
                    <th>50%</th>
                    <th>70%</th>
                    <th>90%</th>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Raw Humidity</td>
                    <td><%=rawHumidity10 %></td>
                    <td><%=rawHumidity30 %></td>
                    <td><%=rawHumidity50 %></td>
                    <td><%=rawHumidity70 %></td>
                    <td><%=rawHumidity90 %></td>
                </tr>
                <tr>
                    <td style="font-weight: bold;">Humidity</td>
                    <td><%=calHumidity10 %></td>
                    <td><%=calHumidity30 %></td>
                    <td><%=calHumidity50 %></td>
                    <td><%=calHumidity70 %></td>
                    <td><%=calHumidity90 %></td>
                </tr>
            </table>
        </div>

        <div class="bold col-sm-9 col-12" style="padding-bottom: 4px;">
            <br />
            <button onclick="resetTableDefaults();" class="btn btn-primary btn-sm" type="button" value="<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Reset Table Defaults")%>">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Reset Table Defaults")%>
            </button>
            <span id="spinner"></span>
        </div>
    </div>
    <div class="clear"></div>

    <div class="">
        <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Humidity", "Humidity")%></h2>
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Last Calibrated Humidity")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input type="text" name="lastObserved" id="lastObserved" readonly="readonly" value="<%: Model.LastDataMessage != null ? humidity.ToDouble().ToString() : "" %>" />
                %
            </div>
        </div>
        <div style="clear: both;"></div>
        <br />
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Last Raw Humidity")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input type="text" name="observed" id="observed" required value="<%: Model.LastDataMessage != null ? rawHumidity.ToDouble().ToString() : "" %>" />
                %
            </div>
        </div>

        <div style="clear: both;"></div>
        <br />
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual Humidity")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input class="user-dets" type="text" name="actual" id="actualHumidity" value="" required />
                %
            </div>
        </div>
    </div>

    <script>

        function resetTableDefaults() {
            $('#spinner').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
            var SensorID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
            $.get('/Overview/SetTableValsToDefault/' + SensorID, function (result) {
                pID.html(result);
                setTimeout(function () {
                    window.location.href = window.location.href;
                }, 2000);

            });
        }

    </script>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_147/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}
%>

