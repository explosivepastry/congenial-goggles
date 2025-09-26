<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("TCP Timeout","TCP Timeout")%>(<%: Html.TranslateTag("Minutes","Minutes")%>)
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="ModbusInterfaceTimeout" name="ModbusInterfaceTimeout" value="<%=Model.ModbusInterfaceTimeout == long.MinValue ? 5 : Model.ModbusInterfaceTimeout%>" style="max-width:172px;"/>
        <a id="singleQueueExp" style="cursor: pointer;"></a>
        <%: Html.ValidationMessageFor(model => model.ModbusInterfaceTimeout)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Port","Port")%> <%: Html.TranslateTag("(Default: 502)","(Default")%>: 502)
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="ModbusInterfacePort" name="ModbusInterfacePort" value="<%=Model.ModbusInterfacePort == int.MinValue ? 502 : Model.ModbusInterfacePort%>" />
        <a id="interfacePort" style="cursor: pointer;"></a>
      <%: Html.ValidationMessageFor(model => model.ModbusInterfacePort)%>
    </div>
</div>