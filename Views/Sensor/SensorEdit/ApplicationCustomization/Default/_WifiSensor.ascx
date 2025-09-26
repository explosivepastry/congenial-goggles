<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% var WifiGateway = Gateway.LoadBySensorID(Model.SensorID);
    bool hidethis = Model.SensorTypeID == 8 && WifiGateway.WifiCredential(1).SSID.Length <= 0;%>
    

<% if (Model.IsWiFiSensor && WifiGateway != null && WifiGateway.GatewayType != null && !hidethis)
    { %>
    
<div id="wifiHide">
    <p class="useAwareState">Wifi Settings</p>
    <%if (Model.SensorTypeID != 8)
        {%>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Data Logging", "Data Logging")%>
        </div>
        <div class="col sensorEditFormInput">
            <div class="form-check form-switch d-flex align-items-center ps-0">
                <label class="form-check-label"><%: Html.TranslateTag("Disabled", "Disabled")%></label>
                <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%= Model.CanUpdate ? "" : "disabled" %> name="DataLog_Manual" id="DataLog_Manual" <%=  Model.DataLog ? "checked" : "" %>>
                <label class="form-check-label"><%: Html.TranslateTag("Enabled", "Enabled")%></label>
            </div>
        </div>
    </div>
    <%} %>
    <%--    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Wi-Fi Configuration","Wi-Fi Configuration")%>
        </div>
        <div class="col sensorEditFormInput">
            <a class="btn btn-secondary btn-sm" href="/Sensor/ClearWIFISettings/<%:Model.SensorID %>"><%: Html.TranslateTag("Clear Wi-Fi Settings","Clear Wi-Fi Settings")%></a>
        </div>
    </div>--%>

    <%if (Model.SensorTypeID != 8)
        {%>

    <div class="row sensorEditForm ">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Always Flash LED","Always Flash LED")%>
        </div>
        <div class="col sensorEditFormInput">
            <div class="form-check form-switch d-flex align-items-center ps-0">
                <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%= Model.CanUpdate ? "" : "disabled" %> name="LedActive" id="LedActive" <%=   WifiGateway.LedActiveTime == 1 ? "checked" : "" %>>
                <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            </div>
        </div>
    </div>
    <%}%>



    <% if (WifiGateway.GatewayType.SupportsHostAddress && WifiGateway.IsUnlocked)
        { %>

    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Server Host Address","Server Host Address")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayServerHostAddress" name="GatewayServerHostAddress" value="<%= WifiGateway.ServerHostAddress%>" />
            <%: Html.ValidationMessage("GatewayServerHostAddress")%>
        </div>
    </div>

    <%} %>

    <% if (WifiGateway.GatewayType.SupportsHostPort && WifiGateway.IsUnlocked)
        { %>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Server Communication Port","Server Communication Port")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayPort" name="GatewayPort" value="<%= WifiGateway.Port%>" />
            <%: Html.ValidationMessage("GatewayPort")%>
        </div>
    </div>

    <%} %>

    <div class="row sensorEditForm ">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Sensor IP Address","Sensor IP Address")%>
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
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Static IP Address","IP Address")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayIP" name="GatewayIP" value="<%= WifiGateway.GatewayIP%>" />
            <%: Html.ValidationMessage("GatewayIP")%>
        </div>
    </div>
    <div class="row sensorEditForm dhcpSettings">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Network Mask","Subnet Mask")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewaySubnet" name="GatewaySubnet" value="<%= WifiGateway.GatewaySubnet%>" />
            <%: Html.ValidationMessage("GatewaySubnet")%>
        </div>
    </div>
    <div class="row sensorEditForm dhcpSettings">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Default Gateway","Default Gateway")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="DefaultRouterIP" name="DefaultRouterIP" value="<%= WifiGateway.DefaultRouterIP%>" />
            <%: Html.ValidationMessage("DefaultRouterIP")%>
        </div>
    </div>
    <div class="row sensorEditForm dhcpSettings">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Default DNS Server","DNS Server")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="GatewayDNS" name="GatewayDNS" value="<%= WifiGateway.GatewayDNS%>" />
            <%: Html.ValidationMessage("GatewayDNS")%>
        </div>
    </div>

    <div class="row sensorEditForm">

        <div class="bold col-md-6 col-sm-6 col-12" style="font-size: 0.95rem; font-weight: 600; margin: 1rem 0; padding-left: 0;">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Primary Wi-Fi Network","Primary Wi-Fi Network")%>
        </div>
        <div class="col-sm-4 col-12 mdBox">
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("SSID","SSID")%>
        </div>
        <div class="col sensorEditFormInput" autocomplete="new-password">
            <input class="form-control" type="Text" <%=Model.CanUpdate ? "" : "disabled" %> id="SSID1" name="SSID1" value="<%= WifiGateway.WifiCredential(1).SSID%>" />
            <%: Html.ValidationMessage("SSID1")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Security Type","Security Type")%>
        </div>
        <div class="col sensorEditFormInput">
            <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                { %>

            <select name="WIFISecurityMode" id="WIFISecurityMode" class="form-select">

                <%if (Model.SensorTypeID == 4)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="1" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WEP ? "selected" : "" %>>WEP</option>
                <option value="2" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <%}%>

                <%if (Model.SensorTypeID == 8)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="2" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <option value="3" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA2_PSK ? "selected" : "" %>>WPA2_PSK</option>
                <option value="4" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA_WPA2_PSK ? "selected" : "" %>>WPA_WPA2_PSK</option>
                <option value="6" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA3_PSK ? "selected" : "" %>>WPA3_PSK</option>
                <option value="7" <%:WifiGateway.WifiCredential(1).WIFISecurityMode == eWIFI_SecurityMode.WPA2_WPA3_PSK ? "selected" : "" %>>WPA2_WPA3_PSK</option>
                <%}%>

            </select>
            <%}
                else
                { %>
            <input class="form-control" disabled="disabled" id="securityType" value="<%:WifiGateway.WifiCredential(1).WIFISecurityMode.ToString()%>" />
            <%} %>
        </div>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-lg-3">
        <%: Html.TranslateTag("Security Key","Security Key")%>
    </div>
    <div class="col sensorEditFormInput">

        <%if (Model.CanUpdate && !WifiGateway.IsDirty)
            { %>
        <input id="PassPhrase1" name="PassPhrase1" type="password" autocomplete="new-password" class="form-control user-dets" placeholder="<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Modify Security Key","Modify Security Key")%>" onkeyup="checkWiFiPassLen(this);">
        <% if (WifiGateway.WifiCredential(1).PassPhrase.Length > 0)
            { %>
        <div style="display: none; color: red;">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|The value entered here will overwrite the security key on this sensor","The value entered here will overwrite the security key on this sensor")%>
            <br />
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|(Leave blank to not modify)","(Leave blank to not modify)")%>
        </div>
        <%}
            }
            else
            { %>
        <input id="Password1" name="PassPhrase1" type="password" autocomplete="new-password" disabled="disabled" class="form-control" placeholder="<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Modify Security Key","Modify Security Key")%>" onkeyup="checkWiFiPassLen(this);">
        <%} %>
    </div>
</div>
<% 
    bool extraWIFI = MonnitSession.AccountCan("additional_wifi_networks");
    if (extraWIFI)
    {%>
<div class="row sensorEditForm">
    <a style="padding-left: 0; margin: 1rem 0px;" class="toggleBtnAB" onclick="toggleOptionalWifi(true);" id="ShowWifiSettings"><%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Add additional Wi-Fi Networks","Add additional Wi-Fi Networks")%>
        <%: Html.GetThemedSVG("hide") %> 
    </a>
    <a class="toggleBtnAB" onclick="toggleOptionalWifi(false);" id="HideWifiSettings" style="display: none; padding-left: 0; margin: 1rem 0px;"><%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Hide additional Wi-Fi Settings","Hide additional Wi-Fi Settings")%>
        <%: Html.GetThemedSVG("show") %>
    </a>
</div>
<%} %>

<%if (extraWIFI)
    { %>
<div class="optionalWifiSettings col-12" style="display: none;">
    <div class="row sensorEditForm">
        <div class="col-sm-4 col-12" style="font-size: 0.95rem; font-weight: 600; margin: 1rem 0; padding-left: 0;">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Secondary Wi-Fi Network (Optional)","Secondary Wi-Fi Network (Optional)")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("SSID","SSID")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("SSID2", WifiGateway.WifiCredential(2).SSID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            <%: Html.ValidationMessage("SSID2")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Security Type","Security Type")%>
        </div>
        <div class="col sensorEditFormInput">
            <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                { %>
            <select name="WIFISecurityMode2" id="WIFISecurityMode2" class="form-select">

                <%if (Model.SensorTypeID == 4)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="1" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WEP ? "selected" : "" %>>WEP</option>
                <option value="2" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <%}%>

                <%if (Model.SensorTypeID == 8)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="2" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <option value="3" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA2_PSK ? "selected" : "" %>>WPA2_PSK</option>
                <option value="4" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA_WPA2_PSK ? "selected" : "" %>>WPA_WPA2_PSK</option>
                <option value="6" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA3_PSK ? "selected" : "" %>>WPA3_PSK</option>
                <option value="7" <%:WifiGateway.WifiCredential(2).WIFISecurityMode == eWIFI_SecurityMode.WPA2_WPA3_PSK ? "selected" : "" %>>WPA2_WPA3_PSK</option>
                <%}%>

            </select>
            <%}
                else
                { %>
            <input class="form-control user-dets" disabled="disabled" id="securityType2" value="<%:WifiGateway.WifiCredential(2).WIFISecurityMode.ToString()%>" />
            <%} %>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Security Key","Security Key")%>
        </div>
        <div class="col sensorEditFormInput">
            <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                { %>
            <input autocomplete="off" id="PassPhrase2" name="PassPhrase2" type="password" class="form-control" placeholder="<%: Html.TranslateTag("Modify Security Key","Modify Security Key")%>" onkeyup="checkWiFiPassLen(this);">
            <% if (WifiGateway.WifiCredential(2).PassPhrase.Length > 0)
                { %>
            <div style="display: none; color: red;">
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|The value entered here will overwrite the security key on this sensor","The value entered here will overwrite the security key on this sensor")%>
                <br />
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|(Leave blank to not modify)","(Leave blank to not modify)")%>
            </div>
            <%}
                }
                else
                { %>
            <input autocomplete="off" id="Password2" name="PassPhrase1" type="password" disabled="disabled" class="form-control" placeholder="<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Modify Security Key","Modify Security Key")%>" onkeyup="checkWiFiPassLen(this);">
            <%} %>
        </div>
    </div>
</div>

<div class="optionalWifiSettings col-12" style="display: none;">
    <div class="row sensorEditForm">
        <div class="bold col-sm-4 col-12" style="font-size: 0.95rem; font-weight: 600; margin: 1rem 0; padding-left: 0;">
            <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Tertiary Wi-Fi Network (Optional)","Tertiary Wi-Fi Network (Optional)")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("SSID","SSID")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("SSID3", WifiGateway.WifiCredential(3).SSID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            <%: Html.ValidationMessage("SSID3")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Security Type","Security Type")%>
        </div>
        <div class="col sensorEditFormInput">
            <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                { %>
            <select name="WIFISecurityMode3" id="WIFISecurityMode3" class="form-select">

                <%if (Model.SensorTypeID == 4)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="1" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WEP ? "selected" : "" %>>WEP</option>
                <option value="2" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <%}%>

                <%if (Model.SensorTypeID == 8)
                    {%>
                <option value="0" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.OPEN ? "selected" : "" %>>Open</option>
                <option value="2" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA ? "selected" : "" %>>WPA</option>
                <option value="3" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA2_PSK ? "selected" : "" %>>WPA2_PSK</option>
                <option value="4" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA_WPA2_PSK ? "selected" : "" %>>WPA_WPA2_PSK</option>
                <option value="6" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA3_PSK ? "selected" : "" %>>WPA3_PSK</option>
                <option value="7" <%:WifiGateway.WifiCredential(3).WIFISecurityMode == eWIFI_SecurityMode.WPA2_WPA3_PSK ? "selected" : "" %>>WPA2_WPA3_PSK</option>
                <%}%>

            </select>
            <%}
                else
                { %>
            <input class="form-control user-dets" disabled="disabled" id="securityType3" value="<%:WifiGateway.WifiCredential(3).WIFISecurityMode.ToString()%>" />
            <%} %>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-lg-3">
            <%: Html.TranslateTag("Security Key","Security Key")%>
        </div>
        <div class="col sensorEditFormInput">
            <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                { %>
            <input autocomplete="off" id="PassPhrase3" name="PassPhrase3" type="password" class="form-control" placeholder="<%: Html.TranslateTag("Modify security key","Modify security key")%>" onkeyup="checkWiFiPassLen(this);">
            <% if (WifiGateway.WifiCredential(3).PassPhrase.Length > 0)
                { %>
            <div style="display: none; color: red;">
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|The value entered here will overwrite the security key on this sensor","The value entered here will overwrite the security key on this sensor")%>
                <br />
                <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|(Leave blank to not modify)","(Leave blank to not modify)")%>
            </div>
            <%}
                }
                else
                { %>
            <input autocomplete="off" id="Password3" name="PassPhrase1" type="password" disabled="disabled" class="form-control" placeholder="<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor|Modify Security Key","Modify Security Key")%>" onkeyup="checkWiFiPassLen(this);">
            <%} %>
        </div>
    </div>
</div>
<%} %>

<script type="text/javascript">
    $(document).ready(function () {

        $('#GatewaySubnet').addClass('form-control');
        $('#GatewayIP').addClass('form-control');
        $('#DefaultRouterIP').addClass('form-control');
        $('#GatewayDNS').addClass('form-control');

        $('#DHCP').prop('checked', '<%:WifiGateway.GatewayIP == "0.0.0.0" ? "checked" : "" %>');

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
                $('#GatewayIP').val('<%:WifiGateway.GatewayIP %>');
                $('#GatewaySubnet').val('<%:WifiGateway.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : WifiGateway.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:WifiGateway.GatewayDNS %>');
            }
        });

        $('#DHCP').change();

        $('.bootTooggle').bootstrapToggle();

         <%if (!string.IsNullOrEmpty(WifiGateway.WifiCredential(2).SSID))
    {%>

        toggleOptionalWifi(true);

              <%}
    else
    {%>

        $('#ActiveAllDay').change(function () {
            if ($('#ActiveAllDay').prop('checked')) {

                $(".activeTime").hide();

                $('#ActiveStartTimeHour').val('12');
                $('#ActiveStartTimeMinute').val('00');
                $('#ActiveStartTimeAM').val('AM');
                $('#ActiveEndTimeHour').val('12');
                $('#ActiveEndTimeMinute').val('00');
                $('#ActiveEndTimeAM').val('AM');
            }
            else {

                $(".activeTime").show();
            }
        });
        $('#ActiveAllDay').change();

                <%}%>

        $(".clearWiFi").click(function (e) {
            e.preventDefault();

            $.get($(this).attr("href"), function (data) {
                if (data == "Success")
                    getMain();
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        });
    });
    function toggleOptionalWifi(show) {

        if (show) {
            $('#ShowWifiSettings').hide();
            $('#HideWifiSettings').show();
            $('.optionalWifiSettings').slideToggle('slow');
        }
        else {
            $('#ShowWifiSettings').show();
            $('#HideWifiSettings').hide();
            $('.optionalWifiSettings').slideToggle('slow');
        }
    }

    function checkWiFiPassLen(txt) {
        if ($(txt).val().length > 0)
            $(txt).next().show();
        else
            $(txt).next().hide();
    }
    $('#SSID1').addClass('form-control user-dets');
    $('#GatewaySubnet').addClass('form-control user-dets');
    $('#GatewayIP').addClass('form-control user-dets');
    $('#DefaultRouterIP').addClass('form-control user-dets');
    $('#GatewayDNS').addClass('form-control user-dets');
    $('#WIFISecurityMode').addClass('form-select');
    $('#SSID2').addClass('form-control user-dets');
    $('#WIFISecurityMode2').addClass('form-select');
    $('#SSID3').addClass('form-control user-dets');
    $('#WIFISecurityMode3').addClass('form-select');

</script>

<style>
    #WIFISecurityMode, #WIFISecurityMode2, #WIFISecurityMode3 {
        margin-left: 0;
    }

    .sensorEditFormInput {
        padding-left: 0 !important;
    }

    .toggleBtnAB, .toggleBtnAB svg {
        cursor: pointer;
        color: var(--prime-btn-color);
        fill: var(--prime-btn-color);
    }

        .toggleBtnAB:hover .toggleBtnAB svg:hover {
            color: var(--prime-color-hover);
            fill: var(--prime-color-hover);
        }
</style>
<%} %>