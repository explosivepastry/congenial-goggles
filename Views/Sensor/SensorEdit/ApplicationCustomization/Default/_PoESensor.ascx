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
<p class="useAwareState">Ethernet Settings</p>
<div class="row sensorEditForm">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("LED Lights","LED Lights")%>
    </div>

    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Disabled","Disabled")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=PoEGateway.NetworkListInterval > 0 ? "checked" : "" %> id="NetworkListInterval" name="NetworkListInterval">
            <label class="form-check-label"><%: Html.TranslateTag("Enabled","Enabled")%></label>
        </div>
        <%: Html.ValidationMessageFor(g => PoEGateway.NetworkListInterval)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor|Sensor IP Address","Sensor IP Address")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Static","Static")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="DHCP">
            <label class="form-check-label"><%: Html.TranslateTag("Dynamic","Dynamic")%></label>
        </div>
        <%--        <input type="hidden" class="form-control" name="DHCPorStatic" id="DHCPorStatic" value="<%:PoEGateway.GatewayIP == "0.0.0.0" ? "dhcp" : "static" %>" />--%>
    </div>
</div>

<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-lg-3 ">
        <%: Html.TranslateTag("Static IP Address","IP Address")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayIP" name="GatewayIP" value="<%= PoEGateway.GatewayIP%>" />
        <%: Html.ValidationMessage("IPAddress")%>
    </div>
</div>

<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Network Mask","Subnet Mask")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewaySubnet" name="GatewaySubnet" value="<%= PoEGateway.GatewaySubnet%>" />
        <%: Html.ValidationMessage("GatewaySubnet")%>
    </div>
</div>

<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Default Gateway","Default Gateway")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="DefaultRouterIP" name="DefaultRouterIP" value="<%= PoEGateway.DefaultRouterIP%>" />
        <%: Html.ValidationMessage("DefaultRouterIP")%>
    </div>
</div>

<div class="row sensorEditForm dhcpSettings">
    <div class="col-12 col-lg-3">
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
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("URL","URL")%>
    </div>

    <div class="col sensorEditFormInput">
        <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayServerHostAddress" name="GatewayServerHostAddress" value="<%= PoEGateway.ServerHostAddress%>" />
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-lg-3">
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
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor|Update Sensor Firmware","Update Sensor Firmware")%>
    </div>
    <div class="col sensorEditFormInput">
        <%if (PoEGateway.ForceToBootloader)
            { %>
        <%: Html.TranslateTag("Default|Pending","Pending")%>
        <%}
            else
            { %>
        <button id="btnOtaUpdate" class="btn btn-secondary btn-sm" type="button"><%: Html.TranslateTag("Update","Update")%></button>
        <%} %>
    </div>
</div>
<%} %>

<div class="row sensorEditForm">
    <div class="col-12 col-lg-3" id="macAddressTitle">
        <%: Html.TranslateTag("MAC Address","MAC Address")%>
    </div>

    <div class="col sensorEditFormInput">
        <%: PoEGateway.MacAddress %>
    </div>
</div>

<div class="row sensorEditForm" id="poeGatewayInfo">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Gateway ID","Gateway ID")%>
    </div>

    <div class="col sensorEditFormInput">
        <%: PoEGateway.GatewayID %>
    </div>
</div>

<script type="text/javascript">

    $('#GatewaySubnet').addClass('form-control');
    $('#GatewayIP').addClass('form-control');
    $('#DefaultRouterIP').addClass('form-control');
    $('#GatewayDNS').addClass('form-control');

    $(document).ready(function () {

        $('#DHCP').prop('checked', '<%:PoEGateway.GatewayIP == "0.0.0.0" ? "checked" : "" %>');

        $('#DHCP').change(function () {
            if ($('#DHCP').prop('checked')) {
                $('#DHCPorStatic').val('dhcp');
                $('.dhcpSettings').hide();
                $('#GatewayIP').val('0.0.0.0');
                $('#GatewaySubnet').val('0.0.0.0');
                $('#GatewayDNS').val('0.0.0.0');
            }
            else {
                $('#DHCPorStatic').val('static');
                $('.dhcpSettings').show();
                $('#GatewayIP').val('<%:PoEGateway.GatewayIP %>');
                $('#GatewaySubnet').val('<%:PoEGateway.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : PoEGateway.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:PoEGateway.GatewayDNS %>');
            }
        });

        $('#DHCP').change();

        $('.bootTooggle').bootstrapToggle();

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