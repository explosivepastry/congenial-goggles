<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Gateway>>" %>
<style type="text/css">
    .auto-style2 {
        width: 596px;
    }

    .testingIconGw {
        display: flex;
        align-items: center;
    }

    .gWcol1 {
        display: flex !important;
        flex-direction: column;
        width: 164px;
        justify-content: space-evenly;
        font-weight: bold;
    }

    .gWcol2 {
        display: flex !important;
        flex-direction: column;
        min-width: 180px;
        justify-content: space-evenly;
        font-weight: bold;
    }

    .testingGatewayCard {
        display: grid;
        grid-template-columns: 20px .5fr .5fr 1fr 66px;
        width: 100%;
        background: var(--card-background-color);
        border-radius: 0.5rem;
        margin: 0.5rem;
        padding: 0.3rem;
        cursor: pointer;
        box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 23%) 0px 3px 6px, rgb(10 37 64 / 10%) 0px -2px 6px 0px inset;
        gap: 1%;
        border: solid 2px transparent;
        transition: border-color 0.3s ease-in-out;
    }

    .selected-card-border {
        border-color: #3f97f6 !important;
    }

    .gwtext {
        font-weight: bold;
    }

    .gatewayStatus-sidebar {
        width: 100% !important;
        height: 100% !important;
        border-radius: 5px 0 0 5px;
    }

    .menuIcon > svg {
        height: 20px !important;
        width: 20px !important;
        margin-right: 10px;
        margin-top: -8px;
    }
</style>

<%="" %>
<%
    long networkID = MonnitSession.GatewayListFilters.CSNetID;
    List<CSNet> networks = new List<CSNet>();
    string networkName = "";
    if (ViewBag.netID != null)
    {
        networkID = ViewBag.netID;
    }

    CSNet network = CSNet.Load(networkID);

    if (network != null)
    {
        networkName = network.Name.Length > 0 ? network.Name : network.CSNetID.ToString();
        networks = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(network.AccountID);
    }
%>

<div class="card_container__body px-0">
    <div class="glanceView" style="display: flex; flex-wrap: wrap; margin-top: 1rem;">
        <%
            if (Model != null && Model.Count() > 0)
            {
                foreach (var item in Model)
                {
                    if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36)//don't show wifi gateways here
                        continue;

                    GatewayMessage lastGatewayMessage = GatewayMessage.LoadLastByGateway(item.GatewayID);

        %>



        <%--HERE FOR TABS:--%>
        <div class="testingDeviceCard testingGatewayCard eventsList__tr testingGateway testingGps viewGatewayDetails<%= item.GatewayID %>" id="GatewayHome<%: item.GatewayID %>" data-id="<%:item.GatewayID %>" title="<%= Html.TranslateTag("Click the Gateway card to display Gateway History, Edit, and GPS Location", "Click the Gateway card to display Gateway History, Edit, and GPS Location")%>">
            <%-- --------------------------------
                        Col 1  COLOR STATUS  
       ------------------------------------ --%>
            <div class="gatewayStatusHolder">
                <%= iMonnit.Controllers.AutoRefreshController.GatewayStatusTestString(item) %>
            </div>

            <%-- ----------------------------------
                        Col 2  ICON/ Messages
       ----------------------------------- ------%>
            <div class="gWcol1">
                <div class="gatewayId">
                    <span title="<%=item.Name %>">
                        <%=item.GatewayID%>
                    </span>
                </div>
                <div class="icon gwicon testingIconGw ">
                    <%=Html.GetThemedSVGForGateway(item.GatewayTypeID) %>
                </div>
                <div align="middle" style="padding: 0px;">
                    <div class="lastDate">
                        <span style="color: #0026ff;">
                            <%= ExtensionMethods.OVTestingElapsedLastMessageString(item.LastCommunicationDate)  %>
                        </span>
                    </div>
                </div>
            </div>

            <%-- -------------------------
                      Col  3  Cell/ last message
       ------------------------------------- --%>
            <div class="gWcol2">
                <div>

                    <%= iMonnit.Controllers.AutoRefreshController.GatewayRadioBandTestString(item) %>
                </div>
                <div class="divCellCenter holder holder<%:item.Status.ToString() %> ">
                </div>

                <div style="display: flex; flex-direction: column; justify-content: flex-end; align-items: center;">
                    <div title="Paused" class="isPaused col-xs-6 pendingEditIcon pausesvg" style="padding-top: 5px; <%= item.isPaused() ? "": "display: none" %>">
                        <%=Html.GetThemedSVG("pause") %>
                    </div>

                    <div title="Settings Update Pending" class="isDirty col-xs-6 pendingEditIcon pendingsvg" style="padding-top: 5px; <%= item.IsDirty ? "": "display: none" %>">
                        <%=Html.GetThemedSVG("Pending_Update") %>
                    </div>
                </div>
            </div>

            <%-- -------------------------
                      Col  4  firmware/ signal strength
       ------------------------------------- --%>

            <div class="d-flex" style="flex-direction: column; justify-content: space-around; width: fit-content;">
                <div class="d-flex" style="justify-content: space-between;">
                    <div class="firmwareVersion">
                        <span class="gwtext"><%: item.GatewayFirmwareVersion %></span>
                    </div>
                    <div class="deviceCount d-flex" style="align-items: baseline;">
                        <%= Html.GetThemedSVG("devices") %>
                        <span class="gwtext" style="margin-left: 0.2rem;">
                            <%: item.LastKnownDeviceCount %>
                        </span>
                    </div>
                </div>
                <div class="signalStrength" title="Signal Strength">
                    <%--gwtext--%>
                    <%--<%: Html.TranslateTag("Signal Strength") %>:--%>
                    <%= iMonnit.Controllers.AutoRefreshController.GatewaySignalStrengthTestString(item.GatewayTypeID, item.CurrentSignalStrength) %>
                </div>
                <div class="powerTest" title="Power Source">
                    <%= iMonnit.Controllers.AutoRefreshController.GatewayCurrentPowerTestString(item)  %>
                </div>
            </div>

            <div class="d-flex" style="flex-direction: column; justify-content: space-between;">
                <div>
                    <input type="button" style="float: right; margin-top: 5px; margin-right: 10px;" class="btn btn-primary btn-sm" onclick="ReformGateways(<%=item.GatewayID %>);" value="Reform" id="ReformGtw" />
                </div>

                <%--  |||||| Menu display  ||||||  --%>

                <div class="menuIcon" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" style="text-align: right;">
                    <%=Html.GetThemedSVG("menu") %>
                </div>

                <ul class="dropdown-menu shadow rounded" aria-labelledby="dropdownMenuButton" style="padding: 0;">
                    <% if (MonnitSession.CustomerCan("Gateway_Can_Change_Network"))
                        { %>

                    <li>
                        <a class="dropdown-item menu_dropdown_item" onclick="ResetSingleGatewayForShipping('<%=item.GatewayID%>'); return false;" data-id="<%:item.GatewayID%>" title="Reset Gateway For Shipping Gateway ID: <%=item.GatewayID%>">
                            <span><%: Html.TranslateTag("Reset", "Reset")%></span>
                            <%=Html.GetThemedSVG("reset") %>
                        </a>
                    </li>
                    <%} %>
                    <%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                        {%>
                    <li>

                        <a class="dropdown-item menu_dropdown_item" target="_blank"
                            href="/Overview/GatewayEdit/<%=item.GatewayID %>">
                            <span><%: Html.TranslateTag("Settings", "Settings")%></span>
                            <%=Html.GetThemedSVG("settings") %>
                        </a>
                    </li>
                    <%} %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item" target="_blank"
                            href="/Overview/GatewaySensorList/<%=item.GatewayID %>">
                            <span><%: Html.TranslateTag("Sensors","Sensors")%></span>
                            <%=Html.GetThemedSVG("sensor") %>
                        </a>
                    </li>
                    <li class="firmwareUpdateMenuItem" style="display: none">
                        <a class="dropdown-item menu_dropdown_item" onclick="SingleGatewayFirmwareUpdate('<%=item.GatewayID %>'); return false;" data-id="<%:item.GatewayID%>" title="Update Firmware for Gateway ID: <%=item.GatewayID%>">
                            <span><%: Html.TranslateTag("Firmware Update", "Firmware Update")%>&nbsp;&nbsp;</span>
                            <span class="fa fa-wrench"></span>
                        </a>
                    </li>

                    <li>
                        <hr class="my-0" />
                        <a class="dropdown-item menu_dropdown_item" onclick="removeGateway('<%:item.GatewayID %>'); return false;" id="list">
                            <span>
                                <%: Html.TranslateTag("Remove", "Remove")%> 
                            </span>
                            <%=Html.GetThemedSVG("delete") %>
                        </a>
                    </li>
                </ul>
            </div>
        </div>

        <% } %>   <%--foreach--%>
        <%
            }
            else
            {
        %>      <%--if (Model != null--%>
        <div class="alert">No Gateways on Network</div>
        <% 
            }
        %>
    </div>
</div>


<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("TestingGatewayList.ascx") %>

    $(document).ready(() => {
        updateGatewayList(); // start the timed auto-update process
        var updateableGateways = "<%= ViewBag.UpdateableGatewayIds %>";
        $('.testingGatewayCard').each(function () {
            if (updateableGateways.includes($(this).data('id'))) {
                $(this).find('.firmwareUpdateMenuItem').show();
            }
        });
    });

    /*move Gateway*/
    function gMoveGateway() {
        var ggatewayID = $("#gAddGatewayID").val();
        if (ggatewayID.length == 0) {
            showSimpleMessageModal("<%=Html.TranslateTag("GatewayID is required")%>");
            $('#gAddGatewayID').focus();
            return;

        }
        $('#gatewaysList').html("");
        var networkID = $("#CurrentNetwork").val();
        $.post('/Testing/MoveGateway/' + networkID, { gatewayID: ggatewayID }, function (data) {
            if (data == 'Success') {
                $('#gAddGatewayID').val('');
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Failed")%>");
            }
            refreshGatewayList();
            $('#gAddGatewayID').focus();
        });
    };

    function removeGateway(item) {
        let values = {};
        values.url = `/CSNet/Remove/${item}`;
        values.text = 'Are you sure you want to remove  gateway from the network?';
        values.callback = refreshGatewayList;
        openConfirm(values);
    }


    /*===== reform  ====*/
    var SureToReformGateways = "<%= Html.TranslateTag("Testing/Reform|Are you sure you want to reform this gateway?","Are you sure you want to reform this gateway?") %>";

    //function ReformGateways(GatewayID) {
    function ReformGateways(id) {
        if (confirm(SureToReformGateways)) {
            $.post("/Testing/Reform/" + id, function (data) {
                if (data == "Success") {
                    refreshGatewayList();
                }
                else {
                    showSimpleMessageModal(data);
                }
            });
        }
    }

    var updateGatewayListTimeout;
    function updateGatewayList() {

        clearTimeout(updateGatewayListTimeout); // ensure we don't have more than 1 refersh loop running

        if ($('#gatewaysList').is('.active.show')) { // only refresh when gateway list is active

            var networkid = $('#CurrentNetwork').val();
            $.get('/AutoRefresh/AtATestingGatewayRefresh/' + networkid, function (data) {
                $.each(data, function (index, value) {
                    var tr = $('.viewGatewayDetails' + value.GatewayID);
                    tr.find('.gatewayStatusHolder').html(value.Status);
                    tr.find('.lastDate span').text(value.DisplayDate);
                    tr.find('.signalStrength').html(value.SignalStrength);
                    tr.find('.firmwareVersion span').text(value.FirmwareVersion);
                    tr.find('.deviceCount span').text(value.DeviceCount);
                    tr.find('.powerTest').html(value.PowerTest);
                    tr.find('.isPaused').toggle(value.isPaused);
                    tr.find('.isDirty').toggle(value.isDirty);
                });
            });
        }

        // Upon 'updateGatewayList()' being called once, will start to auto-refresh at 5 seconds
        updateGatewayListTimeout = setTimeout(function () {
            updateGatewayList()
        }, 5 * 1000);
    }

      <%--=== Current Nework ===--%>
    $('#CurrentNetwork').change(function () {
        var id = $('#CurrentNetwork').val();
        window.location.href = '/Testing/Index/' + $('#CurrentNetwork').val();
    });

    /*=== Reset Single Gateway for Shipping ===*/
    function ResetSingleGatewayForShipping(id) {
        if (confirm('Are you sure you want to reset this gateway for shipping in this network?')) {
            $.post('/Testing/ResetSingleGatewayForShipping/', { "id": id }, function (data) {
                if (data == "Success") {
                    refreshGatewayList();
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
                document.getElementById('gAddGatewayID').focus();
                document.getElementById('gAddGatewayTitle').style.color = 'green';
                document.getElementById('gAddGatewayTitle').enabled = true;
            });
        }
    }

    function SingleGatewayFirmwareUpdate(gatewayID) {
        // TODO: display results in modal
        $.post("/Network/CreateGatewayUpdateRequest/<%= MonnitSession.CurrentCustomer.AccountID %>",
            { gatewayIDs: gatewayID },
            function (resultMsg) {
                console.log(resultMsg);
            });
    }

    // Highlights the selected card. Toggleborder is located in the parent file: /Testing/Index.aspx
    var cardsToHighlight = Array.from(document.querySelectorAll(".testingGatewayCard"));

    cardsToHighlight.forEach(card => {
        card.addEventListener("click", () => toggleBorder(card));
    });


</script>
