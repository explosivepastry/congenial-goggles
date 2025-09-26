<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("TCP Timeout","TCP Timeout")%> (<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="ModbusInterfaceTimeout" name="ModbusInterfaceTimeout" value="<%= Model.ModbusInterfaceTimeout%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.ModbusInterfaceTimeout)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%: Html.TranslateTag("Port","Port")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="ModbusInterfacePort" name="ModbusInterfacePort" value="<%= Model.ModbusInterfacePort%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.ModbusInterfacePort)%>
    </div>
</div>

