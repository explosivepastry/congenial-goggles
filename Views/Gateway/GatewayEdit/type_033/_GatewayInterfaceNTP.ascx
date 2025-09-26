<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceNTP|Server IP Address","Server IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="NTPServerIP" name="NTPServerIP" value="<%= Model.NTPServerIP%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.NTPServerIP)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%: Html.TranslateTag("Gateway/_GatewayInterfaceNTP|Update Interval","Update Interval")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="NTPMinSampleRate" name="NTPMinSampleRate" value="<%= Model.NTPMinSampleRate%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.NTPMinSampleRate)%>
    </div>
</div>







