<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<tr>
    
    <%if (Model.ApplicationID != 23 || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) > new Version("1.2.0.3"))
      { %>
    <td>Heartbeat Interval</td>
    <%}
      else if (Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) < new Version("2.2.0.0") || Model.ApplicationID == 23 && new Version(Model.FirmwareVersion) <= new Version("1.2.0.3"))
      { %>
    <td>Time Before Motion Rearm</td>
    <%} %>
    <td>
        <%: Html.TextBoxFor(model => model.ReportInterval, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> Minutes
                        <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Default value 120<br/><br/>How often the sensor communicates with the gateway if no activity recorded." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<tr>
    <td></td>
    <td colspan="2">
        <div id="ReportInterval_Slider"></div>
    </td>
</tr>



<script type="text/javascript">
    var reportInterval_array = [120, 240, 360, 720];
    var minReportInterval = <%=MonnitSession.CurrentCustomer.Account.MinHeartBeat()%>;

    if (minReportInterval == 10) {
        var reportInterval_array = [10, 20, 30, 60, 120, 240, 360, 720];
    }

    if (minReportInterval <= 1) {
        var reportInterval_array = [1, 10, 20, 30, 60, 120, 240, 360, 720];
    }

    function getReportIntervalIndex() {
        var retval = 0;
        $.each(reportInterval_array, function (index, value) {
            if (value <= $("#ReportInterval").val())
                retval = index;
        });

        return retval;
    }
   
    $('#ReportInterval_Slider').slider({
        value: getReportIntervalIndex(),
        min: 0,
        max: reportInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
          slide: function (event, ui) {
              //update the amount by fetching the value in the value_array at index ui.value
              $('#ReportInterval').val(reportInterval_array[ui.value]);
            <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
                {%>
              
              if ($('#ActiveStateInterval').val() > reportInterval_array[ui.value]) {
                  $('#ActiveStateInterval').val(reportInterval_array[ui.value]);
                  $('#ActiveStateInterval').change();
              }
              setAproxTime();
      <%}%>
          }
      });
    $("#ReportInterval").addClass('editField editFieldMedium');
    $("#ReportInterval").change(function () {
        if (isANumber($("#ReportInterval").val())) {
            //Check if less than min
            if ($("#ReportInterval").val() < minReportInterval)
                $("#ReportInterval").val(minReportInterval);

            //Check if greater than max
            if ($("#ReportInterval").val() > 720)
                $("#ReportInterval").val(720);

            $('#ReportInterval_Slider').slider("value", getReportIntervalIndex());
            <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
                {%>
            setAproxTime();

            <%}%>
        }
        else{
            $("#ReportInterval").val(<%: Model.ReportInterval%>);
            $('#ReportInterval_Slider').slider("value", getReportIntervalIndex());
        }
      });
</script>
