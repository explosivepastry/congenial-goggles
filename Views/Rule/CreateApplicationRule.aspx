<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateApplicationRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
        var GatewayList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(MonnitSession.CurrentCustomer.AccountID);
    %>
    <%=Html.Partial("_CreateNewRuleProgressBar") %>

    <div class="rule-card_container w-100" id="top">

        <div class="pick-a-device">
            <%: Html.TranslateTag("Pick a Device")%>
        </div>

        <!-- Nav tabs -->
        <ul class="nav nav-tabs" id="myTab" role="tablist">
            <%if (ViewBag.ShowSensors == true)
                { %>
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="home-tab" data-bs-toggle="tab" data-bs-target="#sensors" type="button" role="tab" aria-controls="home" aria-selected="true">Sensors</button>
            </li>
            <%} %>
            <%if (ViewBag.ShowGateways == true)
                { %>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="profile-tab" data-bs-toggle="tab" data-bs-target="#gateway" type="button" role="tab" aria-controls="profile" aria-selected="false">Gateways</button>
            </li>
            <%} %>
        </ul>

        <!-- Tab panes -->


        <%if (ViewBag.ShowSensors == true)
            { %>
        <div class="tab-content">
            <div class="tab-pane active" id="sensors" role="tabpanel" aria-labelledby="home-tab" tabindex="0">
                <div class="rule-title d-flex justify-content-between">
                    <div class="col-6 dfac">
                        <span class="rule-title"><%: Html.TranslateTag("Choose a Sensor")%></span>
                    </div>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 45px;">
                                <a class='filter-icon' href="#" onclick="toggleSensorFilterOptions()">
                                    <%=Html.GetThemedSVG("filter") %>			
                                </a>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="sensorFilterOptions">
                    <div style="flex-wrap:wrap; gap:0.5rem;" class="d-flex align-items-center switchDirectionOnSm">
                        <div style="gap:0.5rem;" class="switchDirectionOnSm d-flex align-items-center">
                            <div>
                                <strong><%: Html.TranslateTag("Filter", "Filter")%>: &nbsp;</strong>
                                <span id="filteredSensors">0</span>/<span style="padding-right: 0.25rem;" id="totalSensors">0</span>
                                 </div>
                                <input type="text" id="NameFilter" class="filter-sensor-input user-dets" style="width:250px;" placeholder="<%: Html.TranslateTag("Device Name")%>..." value="<%= MonnitSession.SensorListFilters.Name %>" />
                           

                            <select style="width: 250px;" id="applicationFilter" class="form-select mx-lg-1">
                                <option value="-1"><%: Html.TranslateTag("Rule/CreateApplicationRule|All Sensor Profiles","All Sensor Profiles")%></option>
                                <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                    {%>
                                <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                                <%}%>
                            </select>

                            <select style="width: 250px;" id="statusFilter" class="form-select ">
                                <option value="All"><%: Html.TranslateTag("Rule/CreateApplicationRule|All Statuses","All Statuses")%></option>
                                <%foreach (string Stat in Enum.GetNames(typeof(Monnit.eSensorStatus)))
                                    {
                                        string statusDisplayName = Stat;
                                        if (statusDisplayName == "OK")
                                            statusDisplayName = "Active";
                                %>
                                <option value='<%: Stat%>'><%:statusDisplayName %></option>
                                <%}%>
                            </select>

                        </div>
                        <div class="d-flex align-items-center switchDirectionOnSm" style="gap:0.5rem">
                            <strong><%: Html.TranslateTag("Rule/CreateApplicationRule|Sort By","Sort By")%>: </strong>
                            <select id="sortFilter" class="form-select" style="width: 250px;">
                                <option value="Sensor Name - Asc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Sensor Name A-Z","Sensor Name A-Z")%></option>
                                <option value="Sensor Name - Desc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Sensor Name Z-A","Sensor Name Z-A")%></option>
                                <option value="Last Check In - Desc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Last Message New-Old","Last Message New-Old")%></option>
                                <option value="Last Check In - Asc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Last Message New-Old","Last Message Old-New")%></option>
                                <option value="Signal - Asc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Signal Strength Low-High","Signal Strength Low-High")%></option>
                                <option value="Signal - Desc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Signal Strength High-Low","Signal Strength High-Low")%></option>
                                <option value="Battery - Asc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Battery Low-High","Battery Low-High")%></option>
                                <option value="Battery - Desc" class="sortable"><%: Html.TranslateTag("Rule/CreateApplicationRule|Battery High-Low","Battery High-Low")%></option>
                            </select>
                        </div>
                    </div>
                </div>

                <div class="card_container__body hasScroll-rule" id="triggerSensors" style="display: flex; flex-wrap: wrap;">
                    <div id="sensorsLoading" class="text-center w-100">
                        <div class="loading-div">
                            <div class="spinnerAlign spinner-border text-primary my-3" role="status">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%} %>
            <%if (ViewBag.ShowGateways == true)
                { %>

            <%--------------PICK GATEWAY-------%>

            <div class="tab-pane" id="gateway" role="tabpanel" aria-labelledby="profile-tab" tabindex="0">
                <div class="card_container__top__title_rule d-flex justify-content-between">
                    <div class="col-6 dfac">
                        <span class="rule-title"><%: Html.TranslateTag("Choose a Gateway")%></span>
                    </div>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 45px;">
                                <a class='filter-icon' href="#" onclick="$('#gatewayFilterOptions').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>			
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="filter_container" id="gatewayFilterOptions" style="display: none;">
                    <div class="filter-sensor">
                        <div>
                            <strong><%: Html.TranslateTag("Filter", "Filter")%>: &nbsp;</strong>
                            <span id="filterdSensors"></span><span id="totalSensors"></span>
                        </div>

                        <div>
                            <input class="filter-sensor-input" type="text" id="NameFilterGateway" placeholder="<%= Html.TranslateTag("Device Name") %>..." />
                        </div>
                    </div>
                </div>

                <div class="card_container__body hasScroll-rule" id="triggerGateways" style="display: flex; flex-wrap: wrap;">
                </div>
            </div>
        </div>
    </div>

    <script>
        $(document).ready(function () {
            refreshGatewayList();
        });

    </script>

    <%} %>

    <style>
        #sensorFilterOptions {
            display: none;
        }

        svg {
            height: 100px;
            fill: #FFF;
        }

        #svg_api path {
            fill: white !important;
        }

        .x_panel .svg_icon {
            height: 25px;
            width: 25px;
        }

        .card_container__top__title2 {
            text-overflow: ellipsis;
            max-width: 100%;
            display: flex;
            border-bottom: 0.2px solid #e6e6e6;
            width: 100%;
            font-weight: 600;
            font-size: 17px;
            justify-content: flex-start;
            align-items: center;
            padding: 5px 10px 10px 5px;
            flex-wrap: wrap;
            font-size: 17px;
            font-weight: bold;
            margin: 5px 15px 10px;
        }

        .card_container__top__title_device {
            white-space: nowrap;
            text-overflow: ellipsis;
            max-width: 100%;
            display: flex;
            padding-left: 15px;
            padding-right: 15px;
            overflow: hidden;
            font-weight: 600;
            color: #707070;
            font-size: 17px;
        }

        @media screen and (max-width: 1098px){
            .switchDirectionOnSm{
                flex-direction:column;
            }
        }
    </style>

    <script>
        var nameTimeout = null;
        $(function () {
            refreshSensorList();

            $('#NameFilter').keyup(function () {
                filterName($(this).val());
            });

            $('#applicationFilter').change(function () {
                $.get('/Overview/FilterAppID', { appID: $(this).val() }, function (data) {
                    refreshSensorList();
                });
            });

            $('#statusFilter').change(function () {
                $.get('/Overview/FilterStatus', { status: $(this).val() }, function (data) {
                    refreshSensorList();
                });
            });

            $('#sortFilter').change(function (e) {
                e.preventDefault();

                var sort = $(this).val()
                var column = sort.split("-")[0];
                var dir = sort.split("-")[1];

                $.get('/Overview/SensorSortBy', { column: column, direction: dir }, function (data) {
                    if (data == "Success") {
                        refreshSensorList();
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            });


            $('#NameFilterGateway').keyup(function () {
                filterNameGateway($(this).val());
            });
        });

        function loadAppSettings() {
            $('#newEventConfigurationHolder').html('');
            var val = $("#Datum").val().split("&");
            $.get("/Rule/CreateApplicationTrigger/" + val[0] + "?datumIndex=" + val[1], function (partialView) {
                $("#newEventConfigurationHolder").html(partialView);
            });

        }

        function refreshSensorList() {
            $('#triggerSensors').html("<div class=\"spinnerAlign spinner-border text-primary my-3\" role=\"status\"></div>");
            $.get('/Rule/SensorForRuleList', { networkID: $('#networkFilter option:selected').val() != -1 ? $('#networkFilter option:selected').val() : null }, function (data) {
                $('#triggerSensors').html(data);
            });
        }

        function refreshGatewayList() {
            $('#triggerGateways').html("<div class=\"spinnerAlign spinner-border text-primary my-3\" role=\"status\"></div>");
            $.get('/Rule/GatewayForRuleList', { networkID: $('#networkFilter option:selected').val() != -1 ? $('#networkFilter option:selected').val() : null }, function (data) {
                $('#triggerGateways').html(data);
            });
        }
        function filterNameGateway(name) {

            if (nameTimeout != null)
                clearTimeout(nameTimeout);

            nameTimeout = setTimeout("$.get('/Overview/FilterGatewayName', { name: '" + name + "' }, function(data) { refreshGatewayList(); }); ", 1000);
        }

        function filterName(name) {

            if (nameTimeout != null)
                clearTimeout(nameTimeout);

            nameTimeout = setTimeout("$.get('/Overview/FilterName', { name: '" + name + "' }, function(data) { refreshSensorList(); }); ", 1000);
        }

        const triggerTabList = document.querySelectorAll('#myTab button')
        triggerTabList.forEach(triggerEl => {
            const tabTrigger = new bootstrap.Tab(triggerEl)

            triggerEl.addEventListener('click', event => {
                event.preventDefault()
                tabTrigger.show()
            })
        })

        function toggleSensorFilterOptions() {
            $('#sensorFilterOptions').slideToggle('slow');
        }

    </script>

</asp:Content>
