<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<OTARequest>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensors to Update
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%=Html.GetThemedSVG("sensor") %>
                    &nbsp;
                    <%: Html.TranslateTag("Network/SensorsToUpdate|Update Sensors", "Update Sensors")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content px-2">

                <div style="background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                    <font color="gray">
                        <%: Html.TranslateTag("Network/SensorsToUpdate|Click Sensor to select/de-select","Click Sensor to select/de-select")%>
                    </font>
                    <a href="#" onclick="$('#settings').toggle(); return false;">
                        <%=Html.GetThemedSVG("filter") %>
                    </a>
                </div>
                <div class="row" id="settings" style="display: none; width: 100%; margin: auto; border: 1px solid #dbdbdb;">
                    <div class="col-12 col-md-2 ps-3 my-auto">
                        <strong><%: Html.TranslateTag("Filter", "Filter")%>: &nbsp;</strong>
                        <span id="filteredSensors"></span>/<span id="totalSensors"></span>
                    </div>
                    <div class="col-12 col-md-3 my-auto">
                        <input type="text" id="NameFilter" placeholder="<%: Html.TranslateTag("Network/SensorsToUpdate|Device Name...","Device Name...")%>" style="width: 200px;" class="form-control my-2" />
                    </div>

                    <div class="col-12 col-md-3">
                        <select id="ApplicationFilter" style="width: 200px;" class="form-select my-2">
                            <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles","All Sensor Profiles")%></option>
                            <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                {%>
                            <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                            <%}%>
                        </select>
                    </div>
                </div>

                <div id="UpdateSpinner" class="text-center" style="display: none;">
                    <%--col-12 --%>
                    <div class="spinner-border text-primary" style="margin-top: 15px;" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Loading...","Loading...")%></span>
                    </div>
                </div>

                <div class="col-12" style="background: #eee;">
                    <div class="scrollx-child " style="margin-top: 0px;">
                        <div id="UpdateSensorList" class="p-2" style="background: white;">
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div style="padding-top: 10px;">
                        <div class="d-flex justify-content-end">
                            <span id="errMessage" class="me-2" style="color: red; font-weight: bold; align-items: center; display: flex;"></span>
                            <input type="button" value="<%: Html.TranslateTag("Network/SensorsToUpdate|Update All","Update All")%>" id="UpdateAllButton" class="btn btn-primary btn-sm me-2" />
                            <input type="button" value="<%: Html.TranslateTag("Network/SensorsToUpdate|Update Selected","Update Selected")%>" id="UpdateButton" class="btn btn-primary btn-sm" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%

            if (MonnitSession.HasUpdateableGateways(MonnitSession.CurrentCustomer.AccountID))
            {
        %>
        <div class="rule-card_container w-100 updateLayout">
            <div class="updateIconAB"><%=Html.GetThemedSVG("downloadFirmware") %></div>
            <p style="font-size: 1rem; margin: 0; text-align: center;">You also have gateways that require updates.</p>
            <a class="btn-primary btn btn-primary btn-sm user-dets" href="/Network/GatewaysToUpdate">Update Gateways</a>
        </div>
        <%
            }
        %>
        <div class="col-12">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <div class="col-xs-6">
                            <%=Html.GetThemedSVG("pending") %>
                        &nbsp;
                        <%: Html.TranslateTag("Network/SensorsToUpdate|Pending Updates","Pending Updates")%>
                        </div>
                        <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                            <div style="float: right; margin-bottom: 5px;">
                                <div class="btn-group" style="height: 30px;">
                                    <a id="refreshBtn" href="#" onclick="return false;">
                                        <%=Html.GetThemedSVG("refresh") %>
                                    </a>
                                    <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                        <%--    <span class="sr-only">Loading...</span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_content" id="pendingUpdateDiv" style="padding-right: 10px;">
                </div>

                <div class="x_content" id="pendingUpdateWifiDiv" style="padding-right: 10px;">
                </div>

                <div style="margin: 0 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px; display: none;" id="noUpdatesDiv">
                    No Pending Updates
                </div>

            </div>
        </div>
    </div>

    <script type="text/javascript">

        var nameTimer = null;

        $(document).ready(function () {


            $('.container-fluid').click((e) => {
                if (e.target.id !== 'UpdateButton') {
                    $('#errMessage').html('');
                }
            })

            LoadUpdateSensorList();
            LoadPendingUpdateSensorList();

            LoadPendingUpdateNextList();

            $('#ApplicationFilter').change(function () {
                LoadUpdateSensorList();
            });

            $('#NameFilter').keyup(function () {
                clearTimeout(nameTimer);
                nameTimer = setTimeout(LoadUpdateSensorList, 1000);
            });

            $("#UpdateButton").click(function () {
                if ($('.updateChk:checked').length == 0) {
                    $("#errMessage").html("Please Select at least one sensor.");
                    return;
                }

                $("#UpdateButton").hide();
                $("#UpdateAllButton").hide();
                $("#UpdateSensorList").hide();
                $("#UpdateSpinner").show();

                var ids = "";
                $('.updateChk:checked').each(function () {
                    ids += this.name + '|';
                });
                $.post("/Network/CreateOTARequest/<%=ViewBag.AccountID%>", { sensorIDs: ids }, function (data) {
                    window.location.href = window.location.href;
                });
            });

            $("#UpdateAllButton").click(function () {
                $('.updateChk:not([data="bad-signal-unable-to-check"])').prop('checked', true);
                $('.ListBorder').removeClass('ListBorderNotActive').addClass('ListBorderActive');
                $("#UpdateButton").click();
            });
        });

        function LoadUpdateSensorList() {
            $("#UpdateSpinner").show();
            $('#UpdateSensorList').hide();

            //Load list of sensors with available updates
            $.post("/Network/SensorsToUpdateRefresh/<%=ViewBag.AccountID%>", { nameFilter: $('#NameFilter').val(), applicationFilter: $('#ApplicationFilter').val() }, function (data) {
                $("#UpdateSpinner").hide();
                $('#UpdateSensorList').html(data);
                $('#UpdateSensorList').show();
            });
        }

        function toggleSensor(sensorID, applicationID) {
            var add = $('.notiSensor' + sensorID).hasClass('ListBorderNotActive');
            const cardInQuestion = $('#cardForSensor_' + sensorID);
            const hasEnoughSignalToUpdate = hasDescendantWithDataInfo(cardInQuestion);

            if (add && !hasEnoughSignalToUpdate) {
                $('.notiSensor' + sensorID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                $('#update_' + sensorID + '_' + applicationID).prop('checked', true);
            } else {
                $('.notiSensor' + sensorID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                $('#update_' + sensorID + '_' + applicationID).prop('checked', false);
            }

        }

        function LoadPendingUpdateSensorList() {
            $('#refreshBtn').hide();
            $('#refreshSpinner').show();

            $.get("/Network/PendingUpdateRefresh/<%=ViewBag.AccountID%>", function (data) {
                $('#pendingUpdateDiv').html(data);
                $('#refreshSpinner').hide();
                $('#refreshBtn').show();

                checkPendingUpdates();
            });
        }

        $('#refreshBtn').click(function () {
            LoadPendingUpdateSensorList();
            LoadPendingUpdateNextList();
        });


        function LoadPendingUpdateNextList() {
            $('#refreshBtn').hide();
            $('#refreshSpinner').show();

            $.get("/Network/PendingNextSensorsUpdateRefresh/<%=ViewBag.AccountID%>", function (data) {
                $('#pendingUpdateWifiDiv').html(data);
                $('#refreshSpinner').hide();
                $('#refreshBtn').show();

                checkPendingUpdates();
            });
        }

        function checkPendingUpdates() {
            const hasPendingUpdates = $('#pendingUpdateDiv').html().trim() !== '' || $('#pendingUpdateWifiDiv').html().trim() !== '';
            if (hasPendingUpdates) {
                $('#noUpdatesDiv').hide();
            }
            else {
                $('#noUpdatesDiv').show();
            }
        }

        function removeOTARequest(requestID) {

            //$('#deleteDiv_' + requestID).hide();
            $(`#${requestID}`).show();

            let values = {};
            values.url = `/Network/CancelOTARequest?id=${requestID}`;
            values.text = 'Are you sure you want to cancel this update?';
            openConfirm(values);
        }

        function removeOTAGatewayRequest(gatewayID) {

            //$('#deleteDiv_' + gatewayID).hide();
            $(`#${gatewayID}`).show();

            let values = {};
            values.url = `/Network/CancelOTAGatewayRequest?id=${gatewayID}`;
            values.text = 'Are you sure you want to cancel this update?';
            openConfirm(values);

        }

        function hasDescendantWithDataInfo(element) {
            if (element.attr('data-info') === 'data-signalToLowToSelect') {
                return true;
            }

            for (let i = 0; i < element.children().length; i++) {
                if (hasDescendantWithDataInfo(element.children().eq(i))) {
                    return true;
                }
            }

            return false;
        }

    </script>

    <style>
        .triggerDevice__container {
            margin: 0;
            padding: 10px 10px;
        }

        #UpdateSensorList {
            display: flex;
            flex-wrap: wrap;
            margin-right: 0px;
        }

        .updateIconAB > svg {
            fill: var(--help-highlight-color);
            width: 35px;
            height: 35px;
        }

        .updateLayout {
            display: flex;
            flex-direction: row;
            align-items: center;
            align-content: flex-start;
            justify-content: space-evenly;
        }

        @media only screen and (max-width: 450px) {
            .updateLayout {
                display: flex;
                flex-direction: column;
                align-items: center;
                align-content: flex-start;
                justify-content: space-evenly;
                gap: 0.5rem;
            }
        }

        @media only screen and (min-width: 2100px) {
            .updateLayout {
                justify-content: center;
                gap: 3rem;
            }
        }
    </style>
</asp:Content>
