<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceRT|TCP Timeout Seconds (default","TCP Timeout Seconds (default")%>: <%:Model.GatewayType.DefaultRealTimeInterfaceTimeout %> seconds)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="RealTimeInterfaceTimeout" name="RealTimeInterfaceTimeout" value="<%= Math.Round(Model.RealTimeInterfaceTimeout)%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.RealTimeInterfaceTimeout)%>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayInterfaceRT|Port (default","Port (default")%>: <%:Model.GatewayType.DefaultRealTimeInterfacePort %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" id="RealTimeInterfacePort" name="RealTimeInterfacePort" value="<%= Model.RealTimeInterfacePort%>" style="width: 50%;" />
        <%: Html.ValidationMessageFor(model => model.RealTimeInterfacePort)%>
    </div>
</div>







