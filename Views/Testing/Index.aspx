<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">

    <style type="text/css">
        #CurrentNetwork {
            font-family: 'Arial';
            color: darkred;
            width: 425px;
            font-size: 1rem;
            font-weight: 700;
        }

        label[for=CurrentNetwork] {
            font-family: 'Arial Nova';
            font-weight: 700;
            font-size: 13px;
        }

        #ResetAllSensorsForShipping, #SetAllSensorsTo10SecHeartBeat, #firmwareUpdateAllSensors, #firmwareUpdateAllGateways {
            font-size: 1rem;
        }

        .sensorContainer {
            font-size: 1rem;
            width: 100%;
            padding: 1rem;
            background: #f5f5f5 !important;
            background: #80808029;
            border-radius: 0.5rem;
            box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 18%);
            box-shadow: rgb(50 50 93 / 6%) 0px 50px 100px -20px, rgb(0 0 0 / 4%) 0px 30px 60px -30px, rgb(10 37 64 / 35%) 0px -2px 6px 0px inset;
            cursor: pointer;
        }

        .testing-contain {
            background-color: white;
            border-radius: 0 10px 10px;
            box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 18%);
            padding: 1rem;
        }

        .test-net {
            width: 100%;
            padding: 0;
            border-bottom: 2px solid #5153567a;
            margin-bottom: 10px;
        }

        .test_buttons {
            font-size:1rem;
            font-weight: bold;
            display: flex;
            margin: 10px;
            padding: 11px;
            justify-content: flex-start;
            background: ghostwhite;
            box-shadow: rgb(0 0 0 / 16%) 0px 3px 6px, rgb(0 0 0 / 5%) 0px 3px 6px;
            border: none;
            color: #0067ab;
            width: 282px;
        }

        .test_buttons:hover {
            background-color: var(--options-hover-color);
            color: var(--options-text-hover);
            box-shadow: rgb(9 30 66 / 40%) 0px 2px 10px, rgb(9 30 66 / 30%) 0px 7px 13px -3px, rgb(9 30 66 / 20%) 0px -3px 0px inset;
            transform: scale(1.02)
        }
       .test_buttons:hover {
             background-color: var(--options-hover-color);
             color: var(--options-text-hover);
             box-shadow: rgb(9 30 66 / 40%) 0px 2px 10px, rgb(9 30 66 / 30%) 0px 7px 13px -3px, rgb(9 30 66 / 20%) 0px -3px 0px inset;
             transform: scale(1.02)
       }

        .button-container {
            display: flex;
            width: 100%;
            flex-wrap: wrap;
            justify-content: center;
        }

        .add-sensor-top {
            display: flex;
            width: 100%;
            align-items: center;
        }

        .add-2-test {
            justify-content: center;
            display: flex;
            width: 100%;
            align-items: center;
            gap: 20px;
        }

        .currNetwork {
            background: #f5f5f5 !important;
            width: fit-content;
            font-size: 1rem !important;
            border-radius: 5px 5px 0 0;
        }

        .btn:disabled, fieldset:disabled .btn {
            opacity: .45;
        }

        .loadingOnPage {
            display: flex;
            justify-content: center;
            margin-top: 50px;
        }

        #fail-icon, #green-pass {
            width: 20px;
            height: 20px;
            margin-right: 10px;
        }
        
        .deviceStatusOK {
            background-color: #77AB3B !important;
        }

        .deviceStatusSleeping {
            background-color: #515356 !important;
        }

        .deviceStatusWarning {
            background-color: #E6CE00 !important;
        }
      
    </style>

    <div class="row">
        <div id="deviceCards" class="col-6" style="overflow-y:auto">
            <div class="tab-content" style="margin-top: 1rem;">
                <div id="deviceCardsLoading" class="text-center">
                    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                    </div>
                </div>
                <div class="tab-pane fade show active" id="sensorsList" role="tabpanel" aria-labelledby="sensorsList-tab" style="width: 98%; height: 1000px;"></div>
                <div class="tab-pane fade" id="gatewaysList" role="tabpanel" aria-labelledby="gatewaysList-tab" style="width: 98%; height: 1000px;"></div>
            </div>
        </div>

        <div id="testingControls" class="col-6">
            <div class="col" style="margin-left: 0px;">
                <%
                    long networkID = 0;
                    if (ViewBag.Networkid != null)
                        networkID = ViewBag.Networkid;
                    CSNet network = CSNet.Load(MonnitSession.SensorListFilters.CSNetID);
                    string networkname = (network == null) ? "All Networks" : network.Name;
                    List<CSNet> networksList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
                %>

                <div class="input-group-prepend d-flex" style="margin-top: 1rem;">
                    <div>
                        <label class="input-group-text currNetwork" for="CurrentNetwork">
                            <%: Html.TranslateTag("Current Network:","Current Network: ")%>
                        </label>
                    </div>
                    <select id="CurrentNetwork" class="form-select" data-mdb-clear-button="true" style="max-width: 300px;">
                        <%foreach (CSNet ntwk in networksList)
                            { %>
                        <option style="color: #000b48; font-family: Arial" value="<%:ntwk.CSNetID %>" <%:networkID == ntwk.CSNetID ? "selected=selected" : ""  %>>
                            <%=ntwk.Name.Length > 0 ? ntwk.Name : ntwk.CSNetID.ToString() %>
                        </option>
                        <% } %>  
                    </select>

                    <button type="button" id="reloadPage" class="btn btn-primary btn-md"><%: Html.TranslateTag("Reload page","Reload Page")%></button>
                </div>

                <div class="testing-contain">

                    <div class="col-12 col-md-10 col-xl-12">
                        <div id="sensorButtons" class="sensorContainer" onclick="ToggleButtons('sbtnSensor')">
                            <div class="add-sensor-top">
                                <div>
                                    <button type="button" id="sbtnSensor" onclick="refreshSensorList()" class="btn btn-primary btn-md btn" disabled>&nbsp;<%: Html.TranslateTag("Sensor","Sensor")%></button>
                                </div>
                                <div class="add-2-test">
                                    <strong id="sAddSensorTitle">&nbsp; &nbsp; &nbsp; <%: Html.TranslateTag("Add Sensor","Add Sensor")%>:</strong>
                                    <input type="text" id="sAddSensorID" class="form-control w-50 user-dets" placeholder="<%: Html.TranslateTag("Add SensorID","Add SensorID")%>" data-id="sensorsList" required="required" name="sAddSensorID" value="" autofocus disabled />
                                </div>
                                <div>
                                    <button type="button" id="sbtnMoveSensorID" onclick="sMoveSensor();" class="btn btn-primary" value="Enter"><%: Html.TranslateTag("Enter","Enter")%></button>
                                </div>
                            </div>

                            <div class="button-container">
                                <button type="button" class="test_buttons btn btn-lg fa fa-refresh " aria-hidden="true" onclick="ResetAllSensorsforShipping();" title="<%: Html.TranslateTag("Testing/Index|ResetAllSensorsForShipping","ResetAllSensorsForShipping")%>" id="ResetAllSensorsForShipping" disabled>&nbsp;&nbsp;  Reset All Sensors For Shipping &nbsp;</button>
                                <button type="button" class="test_buttons btn btn-lg fa fa-cogs" aria-hidden="true" onclick="SetAllSensorsTo10SecHeartBeat();" id="SetAllSensorsTo10SecHeartBeat" title="<%: Html.TranslateTag("Testing/Index|SetAllSensorsTo10SecHeartBeat","SetAllSensorsTo10SecHeartBeat")%>" disabled>&nbsp;&nbsp;  <%: Html.TranslateTag("Testing/Index|Set All Sensors To 10 sec HB","Set All Sensors To 10 sec HB")%></button>
                                <button type="button" class="test_buttons btn btn-lg fa fa-wrench" onclick="firmwareUpdateAllSensors();" title="<%: Html.TranslateTag("Testing/Index|Firmware Update All Sensors","Firmware Update All Sensors")%>" id="firmwareUpdateAllSensors" disabled>&nbsp;&nbsp; <%: Html.TranslateTag("Testing/Index|Firmware Update Sensors","Firmware Update Sensors")%> </button>
                                <button type="button" class="test_buttons btn btn-lg fa fa-trash-o" onclick="sClearAllSensors();" data-id="sensorsList" title="<%: Html.TranslateTag("Testing/Index|ClearUp All Sensors","ClearUp All Sensors")%>" aria-hidden="true" id="sClearAllSensors" disabled>&nbsp;&nbsp; <%: Html.TranslateTag("Testing/Index|Remove all Sensors from Network","Remove all Sensors from Network")%></button>
                            </div>
                        </div>

                        <br />

                        <div id="gatewayButtons" class="sensorContainer" onclick="ToggleButtons('gbtnGateway')" disabled>
                            <div class="add-sensor-top">
                                <div>
                                    <button type="button" id="gbtnGateway" onclick="refreshGatewayList()" class="btn btn-primary btn-md" disabled><%: Html.TranslateTag("Gateway","Gateway")%></button>
                                </div>
                                <div class="add-2-test">
                                    <strong id="gAddGatewayTitle">&nbsp; <%: Html.TranslateTag("Add Gateway","Add Gateway")%>:</strong>
                                    <input type="text" id="gAddGatewayID" class="form-control w-50 user-dets" placeholder="<%: Html.TranslateTag("Add GatewayID","Add GatewayID")%>" name="gAddGatewayID" data-id="gatewaysList" required="required" value="" disabled />
                                </div>
                                <div>

                           <button type="button" id="gbtnMoveGatewayID" onclick="gMoveGateway();" class="btn btn-primary" value="Enter" disabled><%: Html.TranslateTag("Enter","Enter")%></button>
                           </div>
                       </div>

                            <div class="button-container">
                                <button type="button" class="test_buttons btn btn-lg fa fa-refresh" aria-hidden="true" onclick="ResetAllGatewaysforShipping();" title="<%: Html.TranslateTag("Testing/Index|ResetAllGatewaysForShipping","ResetAllGatewaysForShipping")%>" id="ResetAllGatewaysForShipping" disabled>&nbsp;&nbsp; <%: Html.TranslateTag("Testing/Index|Reset All Gateways For Shipping","Reset All Gateways For Shipping")%> </button>
                                <button type="button" class="test_buttons btn btn-lg fa fa-wrench" onclick="firmwareUpdateAllGateways();" title="<%: Html.TranslateTag("Testing/Index|Firmware Update All Sensors","Firmware Update All Sensors")%>" id="firmwareUpdateAllGateways" disabled>&nbsp;&nbsp; <%: Html.TranslateTag("Testing/Index|Firmware Update Gateways","Firmware Update Gateways")%></button>
                                <button type="button" class="test_buttons btn btn-lg fa fa-trash-o" onclick="gClearAllGateways();" data-id="gatewayslist" title="<%: Html.TranslateTag("ClearUp All Gateways","ClearUp All Gateways")%>" aria-hidden="true" id="gClearAllGateways" disabled>&nbsp; &nbsp; <%: Html.TranslateTag("Testing/Index|Remove all Gateways from Network","Remove all Gateways from Network")%></button>
                            </div>
                        </div>
                    </div>

                    <br />
                    <br />
                    <br />
            <div>

                <div class="row" id="testingMainContent"></div>
                <div id="deviceDetailsLoading" class="text-center" style="display: none;">
                    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>

<br /><br /><br />

<%--<%}%>--%>
</div>
<%--testing controls--%>
</div>
<%-- row--%>

<style type="text/css">

        #sensorsList, #gatewaysList {
            max-height: 1050px;
        }

        .innerTab.tab-pane {
            max-height: 850px;
            overflow-y: auto;
        }

        .sensorDetails.icon.icon-signal-0:before, .sensorDetails.icon.icon-signal-1:before, .sensorDetails.icon.icon-signal-2:before,
        .sensorDetails.icon.icon-signal-3:before, .sensorDetails.icon.icon-signal-4:before, .sensorDetails.icon.icon-signal-5:before {
            padding-top: 25px;
            font-size: 50px;
        }

        .first .svg_icon {
            width: 40px;
            height: 70px;
            padding-top: 25px;
        }

        .dropright {
            position: relative;
        }

       .dropright .dropdown-menu {
            top: 0;
            left: 100%;
            margin-top: -1px;
       }

        select {
            color: #9e9e9e;
        }

        option:not(:first-of-type) {
            color: #0026ff;
        }

        .auto-style1 {
            flex: 0 0 auto;
            width: 41%
        }

    </style>

    <script type="text/javascript">

        <%= ExtensionMethods.LabelPartialIfDebug("Testing_Index.aspx") %>

        var deviceType;
        $(document).ready(() => {
            deviceType = "<%= MonnitSession.TestingToolSession.DeviceType == "gateway" ? "gbtnGateway" : "sbtnSensor" %>";
            ToggleButtons(deviceType);
        });

        $("#sAddSensorID").keypress(function (e) {
            if (e.key === 'Enter') {
                $('#sbtnMoveSensorID').click();
            }
        });

        $("#gAddGatewayID").keypress(function (e) {
            if (e.key === 'Enter') {
                $('#gbtnMoveGatewayID').click();
            }
        });

        $('#networkNameGateway').change(function (id) {
            window.location.href = '/Testing/index/' + $('#networkNameGateway').val();
        });

        <%--=== Current Nework ===--%>
        $('#CurrentNetwork').change(function () {
            var id = $('#CurrentNetwork').val();
            window.location.href = '/Testing/Index/' + $('#CurrentNetwork').val();
        });

        $('body').on('click', '.right_col',
            function (e) {
                // ignore this method if the click originated within the dropdown method
                if ($(e.target).closest('.menuIcon').length != 0 || $(e.target).closest('.dropdown-menu').length != 0) {
                    return;
                }

                var closest = $(e.target).closest('.testingDeviceCard');

                // if click was not inside any card, clear the card details
                if (closest.length == 0) {
                    $('#testingMainContent').html('');
                    return;
                }

                var isSensor = closest.attr('class').includes('testingSensor');
                var isGateway = closest.attr('class').includes('testingGateway');
                var isLocGPS = closest.attr('class').includes('testingLocGPS');

                var id = closest.data('id');

                var url = '';
                if (isSensor)
                    url = '/Testing/LoadSensorDetails/' + id;
                else if (isGateway)
                    url = '/Testing/LoadGatewayDetails/' + id;
                else if (isLocGPS)
                    url = '/Gateway/Details/' + id;
                else {
                    return;
                }

                $('#testingMainContent').html("");
                $('#deviceDetailsLoading').show();
                $.get(url, function (data) {
                    $('#deviceDetailsLoading').hide();
                    $('#testingMainContent').html(data);

                    if (isSensor)
                        refreshSensorHistory(id);
                    else
                        refreshGatewayHistory(id);
                });
            }
        );

        $('body').on('click', '.innerbtnToggle, .innerTabToggle, .TabSenHist, .TabSenEdit, .TabSenCalib, .TabGtwHist, .TabGtwEdit, .TabGtwLocGPS',
            function (e) {
                e.stopPropagation();
                var toggleClass = $(this).attr('data-toggleclass');

                $('#deviceInfoTabs button').addClass('btn-inactive').removeClass('btn-active');
                $(this).addClass('btn-active').removeClass('btn-inactive');

                var divID = $(this).attr('data-id');

                let deviceId = $('#deviceInfoTabs').data('id');

                if (divID.includes("sensorHistoryTab")) {
                    refreshSensorHistory(deviceId);
                }
                else if (divID.includes("gatewayHistoryTab")) {
                    refreshGatewayHistory(deviceId);
                }
                else if (divID.includes("sensorEditTab")) {
                    refreshSensorEdit(deviceId);
                }
                else if (divID.includes("gatewayEditTab")) {
                    refreshGatewayEdit(deviceId);
                }
                else if (divID.includes("sensorCalibrateTab")) {
                    refreshSensorCalibrate(deviceId);
                }
                else if (divID.includes("gatewayLocGPSTab")) {
                    refreshGatewayLocGPS(deviceId);
                }

                var togglePane = $(this).attr('data-togglepane');
                $('.' + togglePane + '.active').removeClass('active show');
                $('#' + divID).addClass('active show');
            }
        );

        function ToggleButtons(buttonId) {
            if (buttonId == 'gbtnGateway' && $('#gbtnGateway').attr('disabled')) {

                refreshGatewayList();

                document.getElementById("sbtnSensor").disabled = true;
                document.getElementById("sAddSensorTitle").disabled = true;
                document.getElementById("sAddSensorTitle").style.color = "grey";
                document.getElementById("sAddSensorID").disabled = true;
                document.getElementById("sbtnMoveSensorID").disabled = true;
                document.getElementById("ResetAllSensorsForShipping").disabled = true;
                document.getElementById("sClearAllSensors").disabled = true;
                document.getElementById("SetAllSensorsTo10SecHeartBeat").disabled = true;
                document.getElementById("firmwareUpdateAllSensors").disabled = true;

                document.getElementById("gbtnGateway").disabled = false;
                document.getElementById("gAddGatewayTitle").disabled = false;
                document.getElementById("gAddGatewayTitle").style.color = "green";
                document.getElementById("gAddGatewayID").disabled = false;
                document.getElementById("gbtnMoveGatewayID").disabled = false;
                document.getElementById("ResetAllGatewaysForShipping").disabled = false;
                document.getElementById("gClearAllGateways").disabled = false;
                document.getElementById("firmwareUpdateAllGateways").disabled = false;

                document.getElementById("gAddGatewayID").focus();
                $('#sensorsList').removeClass('active show');
                $('#gatewaysList').addClass('active show');
            }
            else if (buttonId == 'sbtnSensor' && $('#sbtnSensor').attr('disabled')) {

                refreshSensorList();

                document.getElementById("gbtnGateway").disabled = true;
                document.getElementById("gAddGatewayTitle").disabled = true;
                document.getElementById("gAddGatewayTitle").style.color = "grey";
                document.getElementById("gAddGatewayID").disabled = true;
                document.getElementById("gbtnMoveGatewayID").disabled = true;
                document.getElementById("gClearAllGateways").disabled = true;
                document.getElementById("ResetAllGatewaysForShipping").disabled = true;
                document.getElementById("firmwareUpdateAllGateways").disabled = true;

                document.getElementById("sbtnSensor").disabled = false;
                document.getElementById("sAddSensorTitle").disabled = false;
                document.getElementById("sAddSensorTitle").style.color = "green";
                document.getElementById("sAddSensorID").disabled = false;
                document.getElementById("sbtnMoveSensorID").disabled = false;
                document.getElementById("sClearAllSensors").disabled = false;
                document.getElementById("ResetAllSensorsForShipping").disabled = false;
                document.getElementById("SetAllSensorsTo10SecHeartBeat").disabled = false;
                document.getElementById("firmwareUpdateAllSensors").disabled = false;

                document.getElementById("sAddSensorID").focus();
                $('#gatewaysList').removeClass('active show');
                $('#sensorsList').addClass('active show');
            }
        }



        const toggleBorder = (card) => {
            const priorSelection = document.querySelector(".selected-card-border");
            if (priorSelection) {
                priorSelection.classList.remove("selected-card-border");
            }

            if (card) {
                card.classList.add("selected-card-border");
            }
        };

        const setupHighlighting = () => {
            document.addEventListener("click", function (event) {
                if (!cardsToHighlight.includes(event.target.closest(".selected-card-border"))) {
                    toggleBorder();
                }

            });
        }


        // == refreshSensorList ==
        var refreshSensorListReq;
        function refreshSensorList() {
            if (refreshSensorListReq) {
                refreshSensorListReq.abort();
            }
            if (refreshGatewayListReq) {
                refreshGatewayListReq.abort();
            }

            $('#sensorsList').html("");
            $('#testingMainContent').html("");
            $('#deviceCardsLoading').show();
            refreshSensorListReq = $.get('/Testing/TestingSensorList/' + $('#CurrentNetwork').val(), function (data) {
                $('#sensorsList').html(data);
                $('#deviceCardsLoading').hide();
                $('#sAddSensorID').focus();
                setupHighlighting()
            });
            
        }


        // == refreshGatewayList ==
        var refreshGatewayListReq;
        function refreshGatewayList() {

            if (refreshGatewayListReq) {
                refreshGatewayListReq.abort();
            }
            if (refreshSensorListReq) {
                refreshSensorListReq.abort();
            }

            $('#gatewaysList').html("");
            $('#deviceCardsLoading').show();
            refreshGatewayListReq = $.get('/Testing/TestingGatewayList/' + $('#CurrentNetwork').val(), function (data) {
                if (data.startsWith('Failed')) {
                    showSimpleMessageModal(data);
                }
                else {
                    $('#gatewaysList').html(data);
                }

                $('#deviceCardsLoading').hide();
                $('#gAddGatewayID').focus();
                setupHighlighting();

            });
        }

        var sureFirmwareUpdateAllSensors = "<%: Html.TranslateTag("Are you sure you want to update the firmware on all Sensors?")  %>";

        function firmwareUpdateAllSensors() {
            var ids = "";
            $('.testingSensorCard').each(function () {
                ids += $(this).data('id') + "|"
            });

            let values = {};
            values.text = sureFirmwareUpdateAllSensors;
            values.url = "/Testing/CreateOTARequest/<%= MonnitSession.CurrentCustomer.AccountID %>";
            values.params = { sensorIDs: ids };
            values.callback = function (resultMsg) {
                console.log(resultMsg);

                let resultTable = "<div class='row' style='text-decoration: underline;font-weight: bold;'><div class='col-2'>Result</div><div class='col-3'>Gateway ID</div><div class='col-3'>Sensor ID</div><div class='col-4'>Sensor SKU</div></div>\r\n";
                let resultJson = $.parseJSON(resultMsg);
                resultTable += $.map(resultJson, r => { return `<div class='row'><div class='col-2'>${r.ResultIcon}</div><div class='col-3'>${r.GatewayID}</div><div class='col-3'>${r.SensorID}</div><div class='col-4'>${r.SensorSKU}</div></div>\r\n`; }).join("");

                values = {};
                values.header = "Firmware Update Sensors Results";
                values.html = resultTable;
                values.callback = () => { refreshSensorList(); };
                showCallbackModal(values);
            };

            $('#callbackModal .modal-dialog').css("max-width","700px");
            openConfirm(values);
        }

        var sureFirmwareUpdateAllGateways = "<%: Html.TranslateTag("Are you sure you want to update the firmware on all Gateways?")%>";

        function firmwareUpdateAllGateways() {
            var ids = "";
            $('.testingGatewayCard').each(function () {
                ids += $(this).data('id') + "|"
            });

            let values = {};
            values.text = sureFirmwareUpdateAllGateways;
            values.url = "/Network/CreateGatewayUpdateRequest/<%= MonnitSession.CurrentCustomer.AccountID %>";
            values.params = { gatewayIDs: ids };
            values.callback = function (resultMsg) {
                
                let resultTable = "<div class='row' style='text-decoration: underline;font-weight: bold;'><div class='col-4'>Result</div><div class='col-8'>Gateway ID</div></div>\r\n";
                let resultJson = $.parseJSON(resultMsg);
                resultTable += $.map(resultJson, r => { return `<div class='row'><div class='col-4'>${r.ResultIcon}</div><div class='col-8'>${r.GatewayID}</div></div>\r\n`; }).join("");
                
                values = {};
                values.header = "Firmware Update Gateways Results";
                values.html = resultTable;
                values.callback = () => { refreshGatewayList(); };
                showCallbackModal(values);
            };

            $('#callbackModal .modal-dialog').css("max-width","400px");
            openConfirm(values);
        }
    
        /*=== Set All Sensors to 10hb ===*/
        var sureSetAllSensorsTo10SecHeartBeat = "<%: Html.TranslateTag("Are you sure you want to reset all Sensors to have a 10 second hearbeat?")%>";

        function SetAllSensorsTo10SecHeartBeat() {
            let values = {};
            values.text = sureSetAllSensorsTo10SecHeartBeat;
            values.url = "/Testing/SetAllSensorsTo10SecHeartBeat/" + $('#CurrentNetwork').val();
            values.callback = function (data) {
                if (data != "Success") {
                    console.log(data);
                    showAlertModal(data);
                }
                refreshSensorList();
            };
            openConfirm(values);
        }

        /*=== Reset All Gateway for Shipping ===*/
        var sureResetGatewaysForShipping = "<%: Html.TranslateTag("Are you sure you want to reset all Gateways on this network for shipping?")%>";

        function ResetAllGatewaysforShipping() {
            let values = {};
            values.text = sureResetGatewaysForShipping;
            values.url = '/Testing/ResetAllGatewaysForShipping/' + $('#CurrentNetwork').val();
            values.callback = function (data) {
                if (data != "Success") {
                    console.log(data);
                    showAlertModal(data);
                }
                refreshGatewayList();
            };
            openConfirm(values);
        }

        /*=== Reset All Sensors for Shipping ===*/
        var sureResetSensorsForShipping = "<%: Html.TranslateTag("Are you sure you want to reset all Sensors on this network for shipping?")%>";

        function ResetAllSensorsforShipping() {
            let values = {};
            values.text = sureResetSensorsForShipping;
            values.url = '/Testing/ResetAllSensorsForShipping/' + $('#CurrentNetwork').val();
            values.callback = function (data) {
                if (data != "Success") {
                    console.log(data);
                    showAlertModal(data);
                }
                refreshSensorList();
            };
            openConfirm(values);
        }

        /*===  Clear All Sensors ===*/
        var SureToClearAllSensors = "<%: Html.TranslateTag("Testing/ClearSensors|Are you sure you want to remove all Sensors from this network ?")%>";

        function sClearAllSensors() {
            let values = {};
            values.text = SureToClearAllSensors;
            values.url = '/Testing/ClearSensors/' + $('#CurrentNetwork').val();
            values.callback = function (data) {
                if (data != "Success") {
                    console.log(data);
                    showAlertModal(data);
                }
                refreshSensorList();
            };
            openConfirm(values);
        }

        /*===  Clear All Gateways ===*/
        var SureToClearAllGateways = "<%: Html.TranslateTag("Testing/ClearGateways|Are you sure you want to remove all Gateways from this network ?")%>";

        function gClearAllGateways() {
            let values = {};
            values.text = SureToClearAllGateways;
            values.url = '/Testing/ClearGateways/' + $('#CurrentNetwork').val();
            values.callback = function (data) {
                if (data != "Success") {
                    console.log(data);
                    showAlertModal(data);
                }
                refreshGatewayList();
            };
            openConfirm(values);
        }

        $("#reloadPage").click(function () {
            location.reload();
            document.getElementById('sAddSensorID').focus();
            document.getElementById('sAddSensorTitle').style.color = 'green';
            document.getElementById('sAddSensorTitle').enabled = true;
        });

    </script>

    <!-- Message Modal (with Callback) -->
    <div class="modal fade" id="callbackModal" tabindex="-1" aria-labelledby="callbackModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content shadow-lg">
                <div class="modal-header">
                    <h5 class="modal-title" id="callbackModalLabel"></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p class="message"></p>
                </div>
                <div class="modal-footer">
                    <div id="callbackModalCountdown" style="display:none;"></div>
                    <button type="button" id="confirmCallbackBtn" class="btn btn-primary">OK</button>
                    <button class="btn btn-primary" id="confirmCallbackLoading" type="button" disabled style="display: none;">
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        <span class="visually-hidden">Loading...</span>
                    </button>
                </div>
            </div>
        </div>
        <script>
            <%= ExtensionMethods.LabelPartialIfDebug("callbackModal")  %>

            $("#confirmCallbackBtn").click(
                function (e) {

                    $("#confirmCallbackBtn").hide();
                    $('#confirmCallbackLoading').show();
                    confirmCallbackModalCallback();
                }
            );

            let confirmCallbackCountdown = null;

            let showCallbackModalCallback = null;
            let showCallbackModal = (values) => {
                $('#callbackModal .modal-header').text(values.header);
                $('#callbackModal .modal-body').html(values.html); // passing 'html' will overwrite 'text'


                if (values.callback != null) {
                    showCallbackModalCallback = values.callback;
                } else {
                    showCallbackModalCallback = () => { };
                }

                $('#callbackModal').modal('show');
            }

            $('#callbackModal').on('show.bs.modal', () => {
                $('#callbackModalCountdown').show();
                callbackModalCountdown(6);
            })

            let callbackModalCountdownReq;
            let callbackModalCountdown = n => {
                n--;
                $('#callbackModalCountdown').text(n);
                if (n > 0) {
                    callbackModalCountdownReq = setTimeout(
                        () => {
                            callbackModalCountdown(n);
                        }
                        , 1000
                    );
                } else {
                    $('#callbackModalCountdown').hide();
                    $('#confirmCallbackBtn').click();
                }
            }

            let confirmCallbackModalCallback = () => {
                clearTimeout(callbackModalCountdownReq);

                $('#confirmCallbackLoading').hide();
                $("#confirmCallbackBtn").show();
                $('#callbackModal').modal('hide');

                showCallbackModalCallback();
            }
        </script>

        <style type="text/css">

            #callbackModal .modal-dialog {
                max-width: 700px;
                max-width: 400px
            }

            #callbackModal .modal-header {
                font-weight: 800;
            }
            
            #callbackModal .modal-footer {
                display: flex;
                justify-content: space-between;
            }

        </style>

    </div>

</asp:Content>









