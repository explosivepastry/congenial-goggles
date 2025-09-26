<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Gateway>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GatewaysToUpdate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="rule-card_container w-100" style="margin-top: 1rem; height: 43rem;">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%=Html.GetThemedSVG("gateway") %>
                    &nbsp;
                    <%: Html.TranslateTag("Network/SensorsToUpdate|Update Gateways", "Update Gateways")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content px-2">


                <div style="background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                    <font color="gray">
                        <%: Html.TranslateTag("Network/GatewaysToUpdate|Click Gateway to select/de-select","Click Gateway to select/de-select")%>
                    </font>
                    <a href="#" onclick="$('#settings').toggle(); return false;">
                        <%=Html.GetThemedSVG("filter") %>
                    </a>
                </div>
                <div class="row" id="settings" style="display: none; width: 100%; margin: auto;">

                    <div class="col-12 col-md-2 ps-3 my-auto">
                        <strong><%: Html.TranslateTag("Filter", "Filter")%>: &nbsp;</strong>
                        <span id="filteredGateways"></span>/<span id="totalGateways"></span>
                    </div>

                    <div class="col-12 col-md-3 my-auto">
                        <input type="text" id="GatewayNameFilter" placeholder="<%: Html.TranslateTag("Device Name...","Device Name...")%>" style="width: 200px;" class="form-control my-2 user-dets" />
                    </div>

                    <div class="col-12 col-md-3 my-auto">
                        <select id="gatewayTypeFilter" class="form-select user-dets mx-lg-1" style="width: 250px;">
                            <option value="-1"><%: Html.TranslateTag("Overview/GatewayGrid|All Gateway Types","All Gateway Types")%></option>
                            <%foreach (GatewayTypeShort App in GatewayTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                {%>
                            <!-- Don't show PoE, LTE, or Mowi-->
                            <%if (App.GatewayTypeID != 35 && App.GatewayTypeID != 36 && App.GatewayTypeID != 11 && App.GatewayTypeID != 38)
                                { %>
                            <option value='<%: App.GatewayTypeID%>'><%:App.Name %></option>
                            <% }
                                }%>
                        </select>
                    </div>
                </div>


                <div class="col-12 hasScroll text-center">
                    <div id="UpdateSpinner" class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
                <div class="col-12" style="background: #eee;">
                    <div class="ov-scroll250 " style="margin-top: 0px; max-height: 460px;">
                        <div id="UpdateGatewayList" class="p-2" style="background: white;">
                        </div>
                    </div>
                </div>

                <div class="col-12">
                    <div style="padding-top: 10px;">
                        <div class="d-flex justify-content-end">
                            <input type="button" value="<%: Html.TranslateTag("Update All", "Update All")%>" id="UpdateAllButton" class="btn btn-primary btn-sm me-2 user-dets" />
                            <input type="button" value="<%: Html.TranslateTag("Update Selected", "Update Selected")%>" id="UpdateButton" class="btn btn-primary btn-sm user-dets" />
                            <span id="errMessage" style="color: red; font-weight: bold;"></span>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <%

            if (MonnitSession.HasOTASuiteGateways(MonnitSession.CurrentCustomer.AccountID) && MonnitSession.HasOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID))
            {
        %>
        <div class="rule-card_container w-100 updateLayout">
            <div class="updateIconAB"><%=Html.GetThemedSVG("downloadFirmware") %></div>
            <p style="font-size: 1rem; margin: 0; text-align: center;">You also have sensors that require updates.</p>
            <a class="btn btn-primary btn-sm user-dets" href="/Network/SensorsToUpdate">Update Sensors</a>
        </div>
        <%
            }
        %>
        <div class="col-12">
            <div class="rule-card_container w-100" style="margin-top: 1rem;">
                <div class="card_container__top">
                    <div class="card_container__top__title d-flex justify-content-between">
                        <div class="col-xs-6">
                            <%=Html.GetThemedSVG("pending") %>
                        &nbsp;
                        <%: Html.TranslateTag("Pending Updates", "Pending Updates")%>
                        </div>
                        <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                            <div style="float: right; margin-bottom: 5px;">
                                <div class="btn-group" style="height: 30px;">
                                    <a id="refreshBtn" href="#" onclick="return false;">
                                        <%=Html.GetThemedSVG("refresh") %>
                                    </a>
                                    <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                        <%--                                        <span class="sr-only">Loading...</span>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_content" id="pendingUpdateDiv" style="padding-right: 10px;">
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">

        var nameTimer = null;

        $(document).ready(function () {

            LoadUpdateGatewayList();
            LoadPendingUpdateGatewayList();

            $('#gatewayTypeFilter').change(function () {
                LoadUpdateGatewayList();
            });

            $('#GatewayNameFilter').keyup(function () {
                if (nameTimer) {
                    clearTimeout(nameTimer);
                }
                nameTimer = setTimeout(LoadUpdateGatewayList, 1000);
            });

            $("#UpdateButton").click(function () {
                if ($('.updateChk:checked').length == 0) {
                    $("#errMessage").html("Please Select at least one gateway.");
                    return;
                }

                $("#UpdateButton").hide();
                $("#UpdateAllButton").hide();
                $("#UpdateGatewayList").hide();

                $("#UpdateSpinner").show();

                var ids = "";
                $('.updateChk:checked').each(function () {
                    ids += this.name + '|';
                    $(this).parent().parent().parent().hide();
                });

                $.post("/Network/CreateGatewayUpdateRequest/<%=ViewBag.AccountID%>", { gatewayIDs: ids }, function (data) {
                    window.location.href = window.location.href;
                });

            });

            $("#UpdateAllButton").click(function () {
                $('.updateChk').prop('checked', true);
                $('.otaGateway').removeClass('ListBorderNotActive').addClass('ListBorderActive');
                $("#UpdateButton").click();
            });
        });

        function LoadUpdateGatewayList() {
            $("#UpdateSpinner").show();
            $('#UpdateGatewayList').hide();

            //Load list of Gateways with available updates
            $.post("/Network/GatewaysToUpdateRefresh/<%=ViewBag.AccountID%>", { nameFilter: $('#GatewayNameFilter').val(), gatewayTypeFilter: $('#gatewayTypeFilter').val() }, function (data) {
                $("#UpdateSpinner").hide();
                $('#UpdateGatewayList').html(data);
                $('#UpdateGatewayList').show();
            });
        }

        function toggleGateway(gatewayID, gatewayTypeID) {
            var add = $('.notiGateway' + gatewayID).hasClass('ListBorderNotActive');

            if (add) {
                $('.notiGateway' + gatewayID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                $('#update_' + gatewayID + '_' + gatewayTypeID).prop('checked', true);
            } else {
                $('.notiGateway' + gatewayID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                $('#update_' + gatewayID + '_' + gatewayTypeID).prop('checked', false);
            }

        }

        function LoadPendingUpdateGatewayList() {
            $('#refreshBtn').hide();
            $('#refreshSpinner').show();

            $.get("/Network/PendingGatewayUpdateRefresh/<%=ViewBag.AccountID%>", function (data) {
                $('#pendingUpdateDiv').html(data);
                $('#refreshSpinner').hide();
                $('#refreshBtn').show();
            });
        }
        $('#refreshBtn').click(function () {
            LoadPendingUpdateGatewayList();

        });

        function removeOTAGatewayRequest(gatewayID) {

            //$('#deleteDiv_' + gatewayID).hide();
            $(`#${gatewayID}`).show();

            let values = {};
            values.url = `/Network/CancelOTAGatewayRequest?id=${gatewayID}`;
            values.text = 'Are you sure you want to cancel this update?';
            openConfirm(values);

        }
    </script>

    <style>
        .triggerDevice__container {
            margin: 0;
            padding: 10px 10px;
        }

        #UpdateGatewayList {
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
