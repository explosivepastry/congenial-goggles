<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% if (Model.IsPoESensor)
    {
        Gateway PoEGateway = Gateway.LoadBySensorID(Model.SensorID);
        if (PoEGateway != null && PoEGateway.GatewayType != null)
        {
            bool showOtaUpdate = false;

            if (!MonnitSession.IsEnterpriseAdmin && !MonnitSession.IsEnterprise)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(PoEGateway.SKU))
                    {
                        string latestVersion = MonnitUtil.GetLatestFirmwareVersionFromMEA(PoEGateway.SKU, PoEGateway.GatewayType.IsGatewayBase);

                        if (!latestVersion.Contains("Failed") && latestVersion != PoEGateway.GatewayFirmwareVersion)
                        {
                            showOtaUpdate = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Log("_PoESensor[.ascx][GetLatestFirmwareVersionFromMEA] ");
                }
            }
%>


<div class="row sensorEditForm schedule-time-toggle">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_PoESensor|Sensor IP Address","Sensor IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Static","Static")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="DHCP" name="DHCP" <%:Model.CanUpdate ? "" : "disabled='disabled'" %>>
            <label class="form-check-label"><%: Html.TranslateTag("Dynamic","Dynamic")%></label>
        </div>
    </div>
</div>

<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Static IP Address","IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayIP" name="GatewayIP" value="<%= PoEGateway.GatewayIP%>" />
        <%: Html.ValidationMessage("GatewayIP")%>
    </div>
</div>
<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Network Mask","Subnet Mask")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewaySubnet" name="GatewaySubnet" value="<%= PoEGateway.GatewaySubnet%>" />
        <%: Html.ValidationMessage("GatewaySubnet")%>
    </div>
</div>
<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Default Gateway","Default Gateway")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="DefaultRouterIP" name="DefaultRouterIP" value="<%= PoEGateway.DefaultRouterIP%>" />
        <%: Html.ValidationMessage("DefaultRouterIP")%>
    </div>
</div>
<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Default DNS Server","DNS Server")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayDNS" name="GatewayDNS" value="<%= PoEGateway.GatewayDNS%>" />
        <%: Html.ValidationMessage("GatewayDNS")%>
    </div>
</div>

<!-- If the gateway is unlocked-->
<%if (PoEGateway.IsUnlocked)
    { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("URL","URL")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayServerHostAddress" name="GatewayServerHostAddress" value="<%= PoEGateway.ServerHostAddress%>" />
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Port","Port")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled"  %> name="GatewayPort" id="GatewayPort" value="<%=PoEGateway.Port%>" />
    </div>
</div>
<%} %>


<%if (showOtaUpdate)
    {%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_PoESensor|Update Sensor Firmware","Update Sensor Firmware")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (PoEGateway.ForceToBootloader)
            { %>
        <%: Html.TranslateTag("Pending","Pending")%>
        <%}
            else
            { %>
        <button id="btnOtaUpdate" class="btn btn-grey" type="button" style="padding: 0px;"><%: Html.TranslateTag("Update","Update")%></button>
        <%} %>
    </div>
</div>
<%} %>

<div class="row sensorEditForm schedule-time-toggle">
    <div class="col-12 col-md-3" id="macAddressTitle">
        <%: Html.TranslateTag("MAC Address","MAC Address")%>
    </div>

    <div class="col sensorEditFormInput">
        <%: PoEGateway.MacAddress %>
    </div>
</div>

<div class="row sensorEditForm schedule-time-toggle" id="poeGatewayInfo">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway ID","GatewayID")%>
    </div>

    <div class="col sensorEditFormInput">
        <%: PoEGateway.GatewayID %>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('#poeGatewayInfo').hide();
        $('#macAddressTitle').dblclick(function (e) {

            $('#poeGatewayInfo').show();
        });

        $('#DHCP').prop('checked', '<%:PoEGateway.GatewayIP == "0.0.0.0" ? "checked" : "" %>');
        $('#DHCP').change(function () {
            if ($('#DHCP').prop('checked')) {
                $('.dhcpSettings').hide();
                $('#GatewayIP').val('0.0.0.0');
                $('#GatewaySubnet').val('0.0.0.0');
                $('#GatewayDNS').val('0.0.0.0');
                $('#DefaultRouterIP').val('0.0.0.0');
            }
            else {
                $('.dhcpSettings').show();
                $('#GatewayIP').val('<%:PoEGateway.GatewayIP %>');
                $('#GatewaySubnet').val('<%:PoEGateway.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : PoEGateway.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:PoEGateway.GatewayDNS %>');
                $('#DefaultRouterIP').val('<%:PoEGateway.DefaultRouterIP %>');
            }
        });

        $('#DHCP').change();

        $('#btnOtaUpdate').click(function (e) {
            e.preventDefault();

            var obj = $(this);
            var oldHtml = $(this).html();
            $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

            $.post('/Overview/OTAUpdate', { id: '<%:Model.SensorID%>' }, function (data) {

                if (data == 'Success') {
                    window.location.href = window.location.href;
                } else {
                    obj.html(oldHtml);
                }
            });
        });
    });

</script>
<%}
    }%>