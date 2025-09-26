<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceNTP|Server IP Address","Server IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="NTPServerIP" name="NTPServerIP" value="<%=Model.NTPServerIP == string.Empty ? "0.0.0.0" : Model.NTPServerIP%>" />
        <a id="ntpIP" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.NTPServerIP)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceNTP|Server Update Interval","Server Update Interval ")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="NTPMinSampleRate" name="NTPMinSampleRate" value="<%=  Model.NTPMinSampleRate == long.MinValue ? 30 : Model.NTPMinSampleRate%>" />
        <a id="ntpSampleRate" style="cursor: pointer;"></a>
      <%: Html.ValidationMessageFor(model => model.NTPMinSampleRate)%>
    </div>
</div>