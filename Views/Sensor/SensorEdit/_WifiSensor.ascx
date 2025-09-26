<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<% var WifiGateway = Gateway.LoadBySensorID(Model.SensorID);

%>

<% if (Model.IsWiFiSensor || Model.SensorTypeID == 8 && WifiGateway != null && WifiGateway.GatewayType != null)
    { %>
<tr>
    <td>Enable Data Logging</td>
    <td>
        <%: Html.CheckBox("DataLog", Model.DataLog, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Data logging will reduce battery life.  The last 50 readings are logged even when this is off if there is a disruption in internet service.  This is for extended periods without connectivity, such as monitoring during shipping or other." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>


<tr>
    <td colspan="3">
        <div>
            <%--<img alt="Wi-Fi" src="../../Content/images/WFA_logo_3d.png" />--%>
            <h2>Wi-Fi Configuration</h2>
            <a href="/Sensor/ClearWIFISettings/<%:Model.SensorID %>" class="clearWiFi btn btn-secondary wfit-cont">Clear Wi-Fi Settings</a>
        </div>
    </td>
</tr>


<tr>
    <td>Always Flash LED</td>
    <td>
        <%: Html.CheckBox("LedActive", WifiGateway.LedActiveTime == 1, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="turning the LED on will significantly reduce battery life (by up to 1/2)." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<% if (WifiGateway.GatewayType.SupportsHostAddress)
    { %>
<tr>
    <td>Server Host Address</td>
    <td>
        <%: Html.TextBox("GatewayServerHostAddress",  (WifiGateway.ServerHostAddress), (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("GatewayServerHostAddress")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Default value: sensorsgateway.com<br/><br/>the location of the monitoring software that the sensor communicates with." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<%} %>

<% if (WifiGateway.GatewayType.SupportsHostPort)
    { %>
<tr>
    <td>Server Communication Port</td>
    <td>
        <%: Html.TextBox("GatewayPort", WifiGateway.Port, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("GatewayPort")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Default value: 3000<br/><br/>the port used for the communication to the monitoring software." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<%} %>

<tr>
    <td>Sensor IP Address</td>
    <td>
        <input type="checkbox" id="DHCP" <%:Model.CanUpdate || !WifiGateway.IsDirty ? "" : "disabled='disabled'" %> />
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Determines if the sensor uses DHCP to obtain an IP address." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<tr class="dhcpSettings">
    <td>IP Address</td>
    <td>
        <%: Html.TextBox("GatewayIP", WifiGateway.GatewayIP, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("GatewayIP")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="IP address the sensor will use on the network it joins." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr class="dhcpSettings">
    <td>Subnet Mask</td>
    <td>
        <%: Html.TextBox("GatewaySubnet", WifiGateway.GatewaySubnet, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("GatewaySubnet")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Subnet mask the of the network the sensor will belong to." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr class="dhcpSettings">
    <td>Default Gateway</td>
    <td>
        <%: Html.TextBox("DefaultRouterIP", WifiGateway.DefaultRouterIP, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("DefaultRouterIP")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="IP address of the device serving as the gateway to the internet. Typically your Wi-Fi enabled router." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr class="dhcpSettings">
    <td>DNS Server</td>
    <td>
        <%: Html.TextBox("GatewayDNS", WifiGateway.GatewayDNS, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessage("GatewayDNS")%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="IP address of the device that resolves DNS queries for the network. Often your Wi-Fi enabled router." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>


<tr>
    <td colspan="3">
        <fieldset>
            <legend>Primaork</legend>
            <div class="editor-label">
                SSID
            </div>
            <div class="editor-field">
                <%: Html.TextBox("SSID1", WifiGateway.WifiCredential(1).SSID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessage("SSID1")%>
            </div>

            <div class="editor-label">
                Security Type
            </div>
            <div class="editor-field">
                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <%: Html.DropDownList("WIFISecurityMode", WifiGateway.WifiCredential(1).WIFISecurityMode)%>
                <%}
                    else
                    { %>
                <input class="aSettings__input_input" disabled="disabled" id="securityType" value="<%:WifiGateway.WifiCredential(1).WIFISecurityMode.ToString()%>" />
                <%} %>
            </div>

            <div class="editor-label">
                Security Key
            </div>
            <div class="editor-field">

                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <input autocomplete="off" id="PassPhrase1" name="PassPhrase1" type="password" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <% if (WifiGateway.WifiCredential(1).PassPhrase.Length > 0)
                    { %>
                <div style="display: none; color: red;">
                    The value entered here will overwrite the security key on this sensor
                    <br />
                    (Leave blank to not modify)
                </div>
                <%}
                    }
                    else
                    { %>
                <input autocomplete="off" id="Password1" name="PassPhrase1" type="password" disabled="disabled" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <%} %>
            </div>
            <script>

                $('#SSID1').addClass('editField editFieldLarge aSettings__input_input');
                $('#PassPhrase1').addClass('editField editFieldLarge');
                $('#WIFISecurityMode').addClass('editField editFieldSmall');

            </script>
        </fieldset>
    </td>
</tr>

<%-- need to include an if state so that if the user is 
    a prime member these buttons will be there --%>


<% 

    bool extraWIFI = MonnitSession.AccountCan("additional_wifi_networks");
    if (extraWIFI)
    {%>
<tr>
    <td colspan="3">
        <a href="#" onclick="toggleOptionalWifi(true); return false; " id="ShowWifiSettings">Add additional Wi-Fi Networks</a>
        <a href="#" onclick="toggleOptionalWifi(false); return false; " id="HideWifiSettings" style="display: none;">Hide additional Wi-Fi Settings</a>
    </td>
</tr>

<tr class="optionalWifiSettings" style="display: none;">
    <td colspan="3">
        <fieldset>
            <legend>Secondary Wi-Fi Network (Optional)</legend>
            <div class="editor-label">
                SSID
            </div>
            <div class="editor-field">
                <%: Html.TextBox("SSID2", WifiGateway.WifiCredential(2).SSID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessage("SSID2")%>
            </div>

            <div class="editor-label">
                Security Type
            </div>
            <div class="editor-field">
                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <%: Html.DropDownList("WIFISecurityMode2", WifiGateway.WifiCredential(2).WIFISecurityMode)%>
                <%}
                    else
                    { %>
                <input class="aSettings__input_input" id="securityType2" disabled="disabled" value="<%:WifiGateway.WifiCredential(2).WIFISecurityMode.ToString()%>" />
                <%} %>
            </div>

            <div class="editor-label">
                Security Key
            </div>
            <div class="editor-field">

                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <input autocomplete="off" id="PassPhrase2" name="PassPhrase2" type="password" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <% if (WifiGateway.WifiCredential(2).PassPhrase.Length > 0)
                    { %>
                <div style="display: none; color: red;">
                    The value entered here will overwrite the security key on this sensor
                    <br />
                    (Leave blank to not modify)
                </div>
                <%}
                    }
                    else
                    { %>
                <input autocomplete="off" id="Password2" name="PassPhrase1" type="password" disabled="disabled" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <%} %>
            </div>
            <script>

                $('#SSID2').addClass('editField editFieldLarge aSettings__input_input');
                $('#PassPhrase2').addClass('editField editFieldLarge');
                $('#WIFISecurityMode2').addClass('editField editFieldSmall');

            </script>
        </fieldset>
    </td>
</tr>
<tr class="optionalWifiSettings" style="display: none;">
    <td colspan="3">
        <fieldset>
            <legend>Tertiary Wi-Fi Network (Optional)</legend>
            <div class="editor-label">
                SSID
            </div>
            <div class="editor-field">
                <%: Html.TextBox("SSID3", WifiGateway.WifiCredential(3).SSID, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessage("SSID3")%>
            </div>

            <div class="editor-label">
                Security Type
            </div>
            <div class="editor-field">
                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <%: Html.DropDownList("WIFISecurityMode3", WifiGateway.WifiCredential(3).WIFISecurityMode)%>
                <%}
                    else
                    { %>
                <input class="aSettings__input_input" id="securityType3" disabled="disabled" value="<%:WifiGateway.WifiCredential(3).WIFISecurityMode.ToString()%>" />
                <%} %>
            </div>

            <div class="editor-label">
                Security Key
            </div>
            <div class="editor-field">

                <%if (Model.CanUpdate && !WifiGateway.IsDirty)
                    { %>
                <input autocomplete="off" id="PassPhrase3" name="PassPhrase3" type="password" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <% if (WifiGateway.WifiCredential(3).PassPhrase.Length > 0)
                    { %>
                <div style="display: none; color: red;">
                    The value entered here will overwrite the security key on this sensor
                    <br />
                    (Leave blank to not modify)
                </div>
                <%}
                    }
                    else
                    { %>
                <input autocomplete="off" id="Password3" name="PassPhrase1" type="password" disabled="disabled" class="editField editFieldLarge aSettings__input_input" placeholder="Modify security key" onkeyup="checkWiFiPassLen(this);">
                <%} %>
            </div>
            <script>

                $('#SSID3').addClass('editField editFieldLarge');
                $('#PassPhrase3').addClass('editField editFieldLarge');
                $('#WIFISecurityMode3').addClass('editField editFieldSmall');

            </script>
        </fieldset>
    </td>
</tr>
<%} %>

<script type="text/javascript">
    $(document).ready(function () {

        $('#DHCP').prop('checked', '<%:WifiGateway.GatewayIP == "0.0.0.0" ? "checked" : "" %>');
        $('#DHCP').change(function () {
            if ($('#DHCP').prop('checked')) {
                $('.dhcpSettings').hide();
                $('#GatewayIP').val('0.0.0.0');
                $('#GatewaySubnet').val('0.0.0.0');
                $('#GatewayDNS').val('0.0.0.0');
            }
            else {
                $('.dhcpSettings').show();
                $('#GatewayIP').val('<%:WifiGateway.GatewayIP %>');
                $('#GatewaySubnet').val('<%:WifiGateway.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : WifiGateway.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:WifiGateway.GatewayDNS %>');
            }
        });
        $('#DHCP').change();

        setTimeout('$("#LedActive").iButton();', 500);
        setTimeout('$("#DataLog").iButton();', 500);
        setTimeout('$("#DHCP").iButton({ labelOn: "Dynamic (DHCP)" ,labelOff: "Static" });', 500);

          <%if (!string.IsNullOrEmpty(WifiGateway.WifiCredential(2).SSID))
    {%>

        toggleOptionalWifi(true);

              <%}
    else
    {%>

        setTimeout('$("#ActiveAllDay").iButton({ labelOn: "All Day" ,labelOff: "Between" });', 500);
        $('#ActiveAllDay').change(function () {
            if ($('#ActiveAllDay').prop('checked')) {
                //$(".activeTime :input").attr("disabled", true);
                $(".activeTime").hide();

                $('#ActiveStartTimeHour').val('12');
                $('#ActiveStartTimeMinute').val('00');
                $('#ActiveStartTimeAM').val('AM');
                $('#ActiveEndTimeHour').val('12');
                $('#ActiveEndTimeMinute').val('00');
                $('#ActiveEndTimeAM').val('AM');
            }
            else {
                //$(".activeTime :input").attr("disabled", $("#ActiveAllDay").attr("disabled"));
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
            $('.optionalWifiSettings').show();
        }
        else {
            $('#ShowWifiSettings').show();
            $('#HideWifiSettings').hide();
            $('.optionalWifiSettings').hide();
        }
    }

    function checkWiFiPassLen(txt) {
        if ($(txt).val().length > 0)
            $(txt).next().show();
        else
            $(txt).next().hide();
    }
    $('#GatewaySubnet').addClass('aSettings__input_input');
    $('#GatewayIP').addClass('aSettings__input_input');
    $('#DefaultRouterIP').addClass('aSettings__input_input');
    $('#GatewayDNS').addClass('aSettings__input_input');
</script>

<%} %>

