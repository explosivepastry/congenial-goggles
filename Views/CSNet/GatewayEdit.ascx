<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>
<%

    string latestVersion = "";
    bool validTypeForUpdate = Model.GatewayType.SupportsOTASuite; //MinOTAFirmwareVersion only applies to BSN firmware capabilities..   && new Version(Model.GatewayFirmwareVersion) >= new Version(Model.GatewayType.MinOTAFirmwareVersion);

    if (!validTypeForUpdate)
    {
        validTypeForUpdate = (!string.IsNullOrWhiteSpace(Model.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(Model.GatewayType.LatestGatewayVersion));
    }

    bool requiresUpdate = false;
    if (validTypeForUpdate)
    {
        if (!string.IsNullOrWhiteSpace(Model.GatewayType.LatestGatewayPath) && !string.IsNullOrEmpty(Model.GatewayType.LatestGatewayVersion)
            && Model.GatewayFirmwareVersion != Model.GatewayType.LatestGatewayVersion && !Model.ForceToBootloader)//must at least support new firemware to flagged for it
        {
            latestVersion = Model.GatewayType.LatestGatewayVersion;
            requiresUpdate = true;
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(Model.SKU))
            {
                latestVersion = MonnitUtil.GetLatestFirmwareVersionFromMEA(Model.SKU, Model.GatewayType.IsGatewayBase);
                if (!latestVersion.Contains("Failed") && latestVersion != Model.GatewayFirmwareVersion)
                {
                    requiresUpdate = true;
                }
            }
        }
    }
%>

<div class="formtitle">
    Gateway Settings
</div>
<form action="/CSNet/GatewayEdit/<%:Model.GatewayID %>" id="gatewayEdit_<%:Model.GatewayID %>" method="post">
    <div class="formBody">
        <input type="hidden" id="returns" name="returns" value="/CSNet/GatewayEdit/<%:Model.GatewayID %>" />

        <table style="width: 100%">

            <%: Html.ValidationSummary(true) %>
            <tr>
                <td>
                    <%: Html.LabelFor(model => model.Name) %>
                </td>
                <td>
                    <input class="aSettings__input_input" type="text" id="Name" name="Name" value="<%= Model.Name %>" />
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.Name) %>
                </td>
            </tr>
            <% if (Model.GatewayType != null &&
       (Model.GatewayTypeID != 2 && Model.GatewayTypeID != 28 && Model.GatewayTypeID != 29 || Model.GatewayFirmwareVersion != "1.0.1.0"))//Null or Cradlepoint v 1.0.1.0
                { %>

            <% if (!string.IsNullOrEmpty(Model.MacAddress))
                {
                    switch (Model.GatewayTypeID)
                    {
                        case 4:
                        case 5:
                        case 6:
                        case 7: %>
            <tr>
                <td>MAC Address
                </td>
                <td>
                    <%: Model.MacAddress.Length == 12 ? Model.MacAddress.Insert(10, ":").Insert(8, ":").Insert(6, ":").Insert(4, ":").Insert(2, ":") : Model.MacAddress%>
                </td>

            </tr>
            <%break;
                case 17:
                case 18:
                case 22:
                case 23:%>
            <tr>
                <td>MEID
                </td>
                <td>
                    <%try
                        {%>
                    <%: Convert.ToInt64(Model.MacAddress.Split('|')[0]).ToString("X")%>
                    <%

                        }
                        catch { }%>
                </td>
            </tr>
            <tr>
                <td>Phone
                </td>
                <td>
                    <%try
                        {%>
                    <%: Model.MacAddress.Split('|')[1].Insert(6, "-").Insert(3, ") ").Insert(0, "(")%>
                    <%}
                        catch { }%>
                </td>

            </tr>
            <%break;
                case 24:
                case 25:
                    string[] simstrings = Model.MacAddress.Split('|');
                    if (simstrings.Length < 3) simstrings = new string[] { "Error in parsing", "Error in parsing", "Error in parsing" };
            %>
            <tr>
                <td>IMSI</td>
                <td><%: simstrings[0] %></td>
            </tr>
            <tr>
                <td>ICCID</td>
                <td><%: simstrings[1] %></td>
            </tr>
            <tr>
                <td>IMEI</td>
                <td><%: simstrings[2] %></td>
            </tr>
            <%      break;
                }//End Switch %>
            <%}//End Mac Address %>

            <%if (Model.GatewayType.SupportsHeartbeat)
                { %>
            <tr>
                <td>Heartbeat Minutes (default: <%:Model.GatewayType.DefaultReportInterval%>)
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.ReportInterval)%>
                    <script>
                        $('#ReportInterval').change(function () {
                            if ($('#ReportInterval').val() < 0)
                                $('#ReportInterval').val(0);

                            if ($('#ReportInterval').val() > 720)
                                $('#ReportInterval').val(720);
                        });

                    </script>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.ReportInterval)%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="ReportInterval_Slider"></div>

                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsNetworkListInterval)
                { %>
            <tr>
                <td>Refresh Network List Minutes (default: <%:Model.GatewayType.DefaultNetworkListInterval%>)
                </td>
                <td>
                    <% double nettemp = Model.NetworkListInterval; %>
                    <%: Html.TextBox("NetworkListInterval", nettemp)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.NetworkListInterval)%>
                </td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="NetworkListInterval_Slider"></div>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsGPSReportInterval)
                { %>
            <tr>
                <td>Record Location Every Minutes (default: <%:Model.GatewayType.DefaultGPSReportInterval%>)
                </td>
                <td>
                    <% double GPStemp = Model.GPSReportInterval; %>
                    <%: Html.TextBox("GPSReportInterval", GPStemp)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.GPSReportInterval)%>
                </td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="GPSReportInterval_Slider"></div>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsPollInterval)
                { %>
            <tr>
                <td>Poll Rate Minutes (default: <%:Math.Round(Model.GatewayType.DefaultPollInterval) %>)
                </td>
                <td>
                    <%: Html.TextBox("PollInterval", Model.PollInterval)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.PollInterval)%>
                </td>

            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="PollInterval_Slider"></div>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsChannel)
                { %>
            <tr>
                <td>Channel Mask (Default: <%:Model.GatewayType.DefaultChannelMask%>)
                </td>

                <td>
                    <%: Html.TextBoxFor(model => model.ChannelMask)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.ChannelMask)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsNetworkIDFilter)
                { %>
            <tr>
                <td>Define a NetworkID (Default: <%:Model.GatewayType.DefaultNetworkIDFilter%>)
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.NetworkIDFilter)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.NetworkIDFilter)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsGatewayIP)
                { %>
            <tr>
                <td>Use DHCP
                </td>
                <td class="editor-field">
                    <div class="form-check form-switch d-flex align-items-center ps-0">
                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox"  id="DHCP">
                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                    </div>
                </td>
            </tr>
            <tr class="DHCPDiv">
                <td>IP Address (Use DHCP: 0.0.0.0)
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.GatewayIP)%>
                </td>


                <td>
                    <%: Html.ValidationMessageFor(model => model.GatewayIP)%>
                </td>
            </tr>
            <tr class="DHCPDiv">
                <td>Subnet Mask
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.GatewaySubnet)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.GatewaySubnet)%>
                </td>
            </tr>
            <% if (Model.GatewayType.SupportsDefaultRouterIP)
                { %>
            <tr class="DHCPDiv">
                <td>Default Gateway
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.DefaultRouterIP)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.DefaultRouterIP)%>
                </td>
            </tr>
            <%} %>
            <tr class="DHCPDiv">
                <td>DNS Server
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.GatewayDNS)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.GatewayDNS)%>
                </td>
            </tr>
            <%}//ENd Supports Gateway IP %>
            <% if (Model.GatewayType.SupportsObserveAware)
                { %>
            <tr>
                <td>Force Transmit on Aware
                </td>
                <td>
                    <%: Html.CheckBoxFor(model => model.ObserveAware)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.ObserveAware)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsForceLowPower && new Version(Model.GatewayFirmwareVersion) >= new Version("1.2.0.0"))
                { %>
            <tr>
                <td>
                    Gateway Power Mode
                </td>
                <td>
                    <%: Html.DropDownList<eGatewayPowerMode>("GatewayPowerMode", Model.GatewayPowerMode)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.GatewayPowerMode)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsCellAPNName)
                { %>
            <tr class="CellularGatewaySettings">
                <td>Cellular APN Name
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.CellAPNName)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.CellAPNName)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayTypeID == 8 || Model.GatewayTypeID == 9 || Model.GatewayTypeID == 13 || Model.GatewayTypeID == 14 || Model.GatewayTypeID == 19 || Model.GatewayTypeID == 20 || Model.GatewayTypeID == 21)
                {//iMetrik Cell Gateway  %>
            <% if (Model.GatewayType.SupportsUsername)
                { %>
            <tr class="CellularGatewaySettings">
                <td>Cellular Network Username
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.Username)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.Username)%>
                </td>
            </tr>
            <%} %>

            <% if (Model.GatewayType.SupportsPassword)
                { %>
            <%string Password = string.Empty;
                if (Model.Password.Length > 0)
                {
                    Password = MonnitSession.UseEncryption ? Model.Password.Decrypt() : Model.Password;
                }%>
            <tr class="CellularGatewaySettings">
                <td>Cellular Network Password
                </td>
                <td>
                    <%: Html.TextBox("Password", Password)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.Password)%>
                </td>
            </tr>
            <%} %>
            <%}// End iMetrick Cell Gateway %>
            <% if (Model.GatewayType.SupportsSecondaryDNS)
                { %>
            <tr class="CellularGatewaySettings">
                <td>Primary DNS
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.GatewayDNS)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.GatewayDNS)%>
                </td>
            </tr>
            <tr class="CellularGatewaySettings">
                <td>Secondary DNS
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.SecondaryDNS)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.SecondaryDNS)%>
                </td>
            </tr>

            <%}%>

            <% if (Model.GatewayTypeID == 30 || Model.GatewayTypeID == 32 || Model.GatewayTypeID == 33)
                {
                    if (new Version(Model.GatewayFirmwareVersion) > new Version("1.0.2.0"))
                    { %>
            <tr>
                <td>Auto Reset command fires every
                </td>
                <td>
                    <input type="text" class="editField editFieldSmall aSettings__input_input" id="ResetInterval" name="ResetInterval" value="<%=  Model.ResetInterval == int.MinValue ? 0 : Model.ResetInterval%>" />
                    (Hours)
                </td>
                <td>
                    <%: Html.ValidationMessageFor(model => model.ResetInterval)%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <div id="ResetInterval_Slider"></div>

                </td>
            </tr>
            <%} %>
            <%} %>

            <% if (Model.GatewayType.SupportsRemoteNetworkReset)
                { %>
            <tr>
                <td>Reform Network
                </td>
                <td>
                    <a href="#" id="Reform">Reform</a>
                </td>
            </tr>
            <%} %>

            <tr>
                <td>Reset Gateway to Factory Defaults
                </td>
                <td>
                    <a href="#" id="Reset">Reset</a>
                </td>
            </tr>

            <%if (MonnitSession.CustomerCan("Customer_Can_Update_Firmware"))
                {
                    if (requiresUpdate)
                    { %>
            <tr>
                <td>Update Gateway Firmware
                </td>
                <td>
                    <% if (!Model.ForceToBootloader)
                        { %>
                    <a href="#" id="Update">Update to <%:latestVersion %></a>
                    <% }
                        else
                        { %>
                                Update Pending
                            <% } %>
                </td>
            </tr>
            <% }
                else
                { %>
            <tr>
                <td></td>
                <td>Firmware up to date
                </td>
            </tr>
            <% }
                    }
                }%>
        </table>
    </div>
    <div class="buttons">

        <button type="button" onclick="checkGatewayForm(<%:Model.GatewayID%>,<%:Model.GatewayTypeID%>);" value="Save" class="gen-btn">
            Save
        </button>
        <div style="clear: both;"></div>
    </div>
</form>
<script type="text/javascript">

    function reloadActiveTab() {
        var tabContainter = $('.tabContainer').tabs();
        var active = tabContainter.tabs('option', 'active');
        tabContainter.tabs('load', active);
    }
    var resetInterval_array = [1, 2, 6, 12, 24, 48, 72, 168, 720, 2191, 4383, 8760];
    var reportInterval_array = [1, 5, 10, 15, 30, 60, 120, 240, 480, 720];
    var pollInterval_array = [0, 1, 2, 5, 10, 30];

    function getReportIntervalIndex() {
        var retval = 0;
        $.each(reportInterval_array, function (index, value) {
            if (value <= $("#ReportInterval").val())
                retval = index;
        });

        return retval;
    }

    function getResetIntervalIndex() {
        var retval = 0;
        $.each(resetInterval_array, function (index, value) {
            if (value <= $("#ResetInterval").val())
                retval = index;
        });

        return retval;
    }

    function getGPSIntervalIndex() {
        var retval = 0;
        $.each(reportInterval_array, function (index, value) {
            if (value <= $("#GPSReportInterval").val())
                retval = index;
        });

        return retval;
    }

    function getNetworkListIntervalIndex() {
        var retval = 0;
        $.each(reportInterval_array, function (index, value) {
            if (value <= $("#NetworkListInterval").val())
                retval = index;
        });

        return retval;
    }

    function getPollIntervalIndex() {
        var retval = 0;
        $.each(pollInterval_array, function (index, value) {
            if (value <= $("#PollInterval").val())
                retval = index;
        });

        return retval;
    }


    $(function () {
        setTimeout('$("#ObserveAware").iButton({ labelOn: "Yes" , labelOff: "No" });', 500);
        setTimeout('$("#DHCP").iButton({ labelOn: "Dynamic" , labelOff: "Static" });', 500);
        $('#DHCP').prop('checked', '<%:Model.GatewayIP == "0.0.0.0" ? "checked" : "" %>');
        $('#DHCP').change(function () {
            if ($('#DHCP').prop('checked')) {
                $('.DHCPDiv').hide();
                $('#GatewayIP').val('0.0.0.0');
                $('#GatewaySubnet').val('0.0.0.0');
                $('#GatewayDNS').val('0.0.0.0');
            }
            else {
                $('.DHCPDiv').show();
                $('#GatewayIP').val('<%:Model.GatewayIP %>');
                $('#GatewaySubnet').val('<%:Model.GatewaySubnet == "0.0.0.0" ? "255.255.255.0" : Model.GatewaySubnet %>');
                $('#GatewayDNS').val('<%:Model.GatewayDNS %>');
            }
        });
        $('#DHCP').change();

        $('#Reset').click(function (e) {
            e.preventDefault();
            var GatewayID = <%: Model.GatewayID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#gatewayEdit_<%:Model.GatewayID %>').parent();

            if (confirm("Are you sure you want to reset this gateway to defaults?")) {

                $.post('/CSNet/Reset/', { id: GatewayID, url: returnUrl }, function (data) {
                    pID.html(data);
                });
            }
        });


        $('#Reform').click(function (e) {
            e.preventDefault();
            var GatewayID = <%: Model.GatewayID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#gatewayEdit_<%:Model.GatewayID %>').parent();

            if (confirm("Are you sure you want to reform this gateway?")) {

                $.post('/CSNet/Reform/', { id: GatewayID, url: returnUrl }, function (data) {
                    pID.html(data);
                });
            }

        });

        $('#Update').click(function (e) {
            e.preventDefault();
            var GatewayID = <%: Model.GatewayID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#gatewayEdit_<%:Model.GatewayID %>').parent();

            if (confirm("Are you sure you want to update this gateway?")) {

                $.post('/CSNet/GatewayFirmwareUpdate/', { id: GatewayID, url: returnUrl }, function (data) {
                    pID.html(data);
                });
            }
        });

        $('#ExternalInterfaceConfiguration').click(function (e) {
            e.preventDefault();
            newModal('Configure Interfaces on Gateway: <%:Model.Name.Replace("'","\\'")%>', '/CSNet/GatewayInterfaceConfiguration/<%:Model.GatewayID%>', 500, 800);
        });



        $('#ResetInterval_Slider').slider({
            value: getResetIntervalIndex(),
            min: 1,
            max: resetInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                $('#ResetInterval').val(resetInterval_array[ui.value]);

            }
        });
        $("#ResetInterval").addClass('editField editFieldSmall');
        $("#ResetInterval").change(function () {
            //Check if less than min
            if ($("#ResetInterval").val() < 1)
                $("#ResetInterval").val(1);

            //Check if greater than max
            if ($("#ResetInterval").val() > 8760)
                $("#ResetInterval").val(8760);

            $('#ResetInterval_Slider').slider("value", getResetIntervalIndex());

        });


        $('#ReportInterval_Slider').slider({
            value: getReportIntervalIndex(),
            min: 0,
            max: reportInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                $('#ReportInterval').val(reportInterval_array[ui.value]);
                <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
    {%>setAproxTime(); <%}%>
            }
        });
        $("#ReportInterval").addClass('editField editFieldSmall');
        $("#ReportInterval").change(function () {
            //Check if less than min
            if ($("#ReportInterval").val() < 0)
                $("#ReportInterval").val(0);

            //Check if greater than max
            if ($("#ReportInterval").val() > 720)
                $("#ReportInterval").val(720);

            $('#ReportInterval_Slider').slider("value", getReportIntervalIndex());
            <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
    {%>setAproxTime();<%}%>
        });

        $('#NetworkListInterval_Slider').slider({
            value: getNetworkListIntervalIndex(),
            min: 0,
            max: reportInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                $('#NetworkListInterval').val(reportInterval_array[ui.value]);
                <%  if (MonnitSession.AccountCan("sensor_advanced_edit"))
    {%> setAproxTime(); <%}%>
            }
        });
        $("#NetworkListInterval").addClass('editField editFieldSmall');
        $("#NetworkListInterval").change(function () {
            //Check if less than min
            if ($("#NetworkListInterval").val() < 60)
                $("#NetworkListInterval").val(60);

            //Check if greater than max
            if ($("#NetworkListInterval").val() > 720)
                $("#NetworkListInterval").val(720);

            $('#NetworkListInterval_Slider').slider("value", getNetworkListIntervalIndex());

        });

        $('#GPSReportInterval_Slider').slider({
            value: getGPSIntervalIndex(),
            min: 0,
            max: reportInterval_array.length - 1,
            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                $('#GPSReportInterval').val(reportInterval_array[ui.value]);

            }
        });
        $("#GPSReportInterval").addClass('editField editFieldSmall');
        $("#GPSReportInterval").change(function () {
            //Check if less than min
            if ($("#GPSReportInterval").val() < 10)
                $("#GPSReportInterval").val(10);

            //Check if greater than max
            if ($("#GPSReportInterval").val() > 720)
                $("#GPSReportInterval").val(720);

            $('#GPSReportInterval_Slider').slider("value", getGPSIntervalIndex());

        });

        $('#PollInterval_Slider').slider({
            value: getPollIntervalIndex(),
            min: 0,
            max: pollInterval_array.length - 1,
                        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
            slide: function (event, ui) {
                //update the amount by fetching the value in the value_array at index ui.value
                $('#PollInterval').val(pollInterval_array[ui.value]);

            }
        });
        $("#PollInterval").addClass('editField editFieldSmall');
        $("#PollInterval").change(function () {
            //Check if less than min
            if ($("#PollInterval").val() < 0)
                $("#PollInterval").val(0);

            //Check if greater than max
            if ($("#PollInterval").val() > 720)
                $("#PollInterval").val(720);

            $('#PollInterval_Slider').slider("value", getPollIntervalIndex());

        });

    });
    $('#GatewaySubnet').addClass('aSettings__input_input');
    $('#GatewayIP').addClass('aSettings__input_input');
    $('#DefaultRouterIP').addClass('aSettings__input_input');
    $('#GatewayDNS').addClass('aSettings__input_input');

</script>
