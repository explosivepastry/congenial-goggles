<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayIP|DHCP","DHCP")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Static","Static")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" id="DHCP">
            <label class="form-check-label"><%: Html.TranslateTag("Dynamic","Dynamic")%></label>
        </div>
        <input type="hidden" class="form-control" name="DHCPorStatic" id="DHCPorStatic" value="<%:Model.GatewayIP == "0.0.0.0" ? "dhcp" : "static" %>" />
    </div>
</div>

<div class="row sensorEditForm DHCPDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayIP|Static IP (Use DHCP","IP Address (Use DHCP")%>: 0.0.0.0)
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.GatewayIP)%>
        <%: Html.ValidationMessageFor(model => model.GatewayIP)%>
    </div>
</div>

<div class="row sensorEditForm DHCPDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayIP|Network Mask","Subnet Mask")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.GatewaySubnet)%>
        <%: Html.ValidationMessageFor(model => model.GatewaySubnet)%>
    </div>
</div>


<div class="row sensorEditForm DHCPDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayIP|Default Gateway","Default Gateway")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.DefaultRouterIP)%>
        <%: Html.ValidationMessageFor(model => model.DefaultRouterIP)%>
    </div>
</div>

<div class="row sensorEditForm DHCPDiv">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_GatewayIP|Default DNS Server","DNS Server")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.GatewayDNS)%>
        <%: Html.ValidationMessageFor(model => model.GatewayDNS)%>
    </div>
</div>

<script type="text/javascript">

    $('#GatewaySubnet').addClass('form-control');
    $('#GatewayIP').addClass('form-control');
    $('#DefaultRouterIP').addClass('form-control');
    $('#GatewayDNS').addClass('form-control');

    $(document).ready(function () {

        $('#DHCP').prop('checked', '<%:Model.GatewayIP == "0.0.0.0" ? "checked" : "" %>');

        $('#DHCP').change(function () {
            if ($('#DHCP').prop('checked')) {
                $('#DHCPorStatic').val('dhcp');
                $('.DHCPDiv').hide();
                $('#GatewayIP').val('0.0.0.0');
                $('#GatewaySubnet').val('0.0.0.0');
                $('#GatewayDNS').val('0.0.0.0');
            }
            else {
                $('#DHCPorStatic').val('static');
                $('.DHCPDiv').show();
                $('#GatewayIP').val('<%:Model.GatewayIP %>');
                $('#GatewaySubnet').val('<%:Model.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : Model.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:Model.GatewayDNS %>');
            }
        });

        $('#DHCP').change();

        $('.bootTooggle').bootstrapToggle();
    });

</script>
