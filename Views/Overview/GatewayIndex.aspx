<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Gateway Overview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%
            string urlname = HttpUtility.ParseQueryString(Request.Url.Query).Get("name");
            string urltypeID = HttpUtility.ParseQueryString(Request.Url.Query).Get("typeID");
            string urlstatus = HttpUtility.ParseQueryString(Request.Url.Query).Get("status");

            long networkID = 0;
            if (ViewBag.netID != null)
                networkID = ViewBag.netID;
            if (Model.Count == 0)
            { %>
        <div>

            <div class="col-12">
                <div class="gridPanelx no-sensor-containerx">
                    <% Html.RenderPartial("_LonelyAstronaut"); %>
                    <h2 style="text-align: center;"><%: Html.TranslateTag("Overview/GatewayIndex|There are no Gateways","There are no Gateways")%>.</h2>
                    <% if (MonnitSession.CustomerCan("Network_Edit"))
                        { %>

                    <a class="btn btn-primary" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>?networkID=<%:networkID%>">


                        <%=Html.GetThemedSVG("add") %>
                        &nbsp; <%: Html.TranslateTag("Add Gateway","Add Gateway")%>
                    </a>
                    <% } %>
                </div>
            </div>
        </div>
        <%}
            else
            { %>

        <div class="top-add-btn-row media_desktop d-flex">
            <div class="timezoneDisplay" style="flex: auto;">
                <p style="font-size: 15px; margin-bottom: 0;"><b><%: Html.TranslateTag("Overview/SensorIndex|Local Time","Local Time")%>:</b><%: DateTime.UtcNow.OVToLocalTimeShort(MonnitSession.CurrentCustomer.Account.TimeZoneID)%></p>
            </div>

            <% if (MonnitSession.CustomerCan("Network_Edit"))
                { %>
            <a class="btn btn-primary" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>?networkID=<%:networkID%>">
                <%=Html.GetThemedSVG("add") %>
                &nbsp; <%: Html.TranslateTag("Add Gateway","Add Gateway")%>
            </a>
            <% } %>
        </div>

        <div class="rule-card_container w-100" style="margin-top: 1rem">
            <%
                long csnetID = MonnitSession.SensorListFilters.CSNetID;
                string networkName = "";
                if (ViewBag.netID != null)
                {
                    csnetID = ViewBag.netID;
                }

                string allnetworks = Html.TranslateTag("All Networks");

                if (csnetID < 0)
                {
                    networkName = allnetworks;
                }

                CSNet network = CSNet.Load(csnetID);

                if (network != null)
                    networkName = network.Name.Length > 0 ? network.Name : network.CSNetID.ToString();
            %>

            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="dfac">
                        <%=Html.GetThemedSVG("gateway") %>
                        <div class="card_container__top__title__text ms-2">
                            <%=networkName%>
                        </div>
                    </div>

                    <div id="">
                        <div class="powertour-hook" id="hook-two" style="float: right; margin-bottom: 5px;">
                            <div class="dfac" style="height: 30px;">
                                <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a href="/" id="refreshBtn" onclick="$(this).hide();$('#refreshSpinner').show();getMain(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                                <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" id="settings" style="display: none;">
                <div class="d-flex align-items-center my-2 flex-wrap">
                    <div class="me-2 col-12 col-lg-1">
                        <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                        <span id="filterdGateways"></span>/<span id="totalGateways"></span>
                    </div>

                    <input type="text" class="GatewayNameFilter form-control" placeholder="<%: Html.TranslateTag("Device Name","Device Name")%>..." style="width: 250px;" value="<%= string.IsNullOrEmpty(urlname) ? "" : urlname %>" />

                    <select id="gatewayTypeFilter" class="form-select mx-lg-1" style="width: 250px;">
                        <option value="-1"><%: Html.TranslateTag("Overview/GatewayIndex|All Gateway Types","All Gateway Types")%></option>
                        <%foreach (GatewayTypeShort App in GatewayTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                            {%>
                        <!-- Don't show PoE, LTE, or Mowi-->
                        <%if (App.GatewayTypeID != 35 && App.GatewayTypeID != 36 && App.GatewayTypeID != 11 && App.GatewayTypeID != 38)
                            { %>
                        <option <%= urltypeID.ToLong() == App.GatewayTypeID ? "selected" : "" %> value='<%: App.GatewayTypeID%>'><%:App.Name %></option>
                        <% }
                            }%>
                    </select>

                    <select id="statusFilter" class="form-select" style="width: 250px;">
                        <option <%= urlstatus == "All" ? "selected" : "" %> value="All"><%: Html.TranslateTag("All Statuses","All Statuses")%></option>
                        <option <%= urlstatus == "OK" ? "selected" : "" %> value="OK"><%: Html.TranslateTag("Active","Active")%></option>
                        <option <%= urlstatus == "Warning" ? "selected" : "" %> value="Warning"><%: Html.TranslateTag("Warning","Warning")%></option>
                        <option <%= urlstatus == "Offline" ? "selected" : "" %> value="Offline"><%: Html.TranslateTag("Offline","Offline")%></option>
                    </select>

                    <label style="margin-left: 5px;">
                        <input type="checkbox" id="hasLastMessageFilter" />
                        <%: Html.TranslateTag("Has Last Message","Has Last Message")%>
                    </label>
                </div>
            </div>

            <!-- _GatewayDetails is populated by Gateways -->
            <div id="Main" class="sensorList_main row p-2 mx-auto mx-md-0"></div>
            <div class="text-center" id="loading">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
                </div>
            </div>
            <%} %>
        </div>
    </div>

    <script type="text/javascript">
        $('#hasLastMessageFilter').click(function () {
            $('#Main .glanceView').children().filter(function () {
                return $(this).find('.glance-lastDate').text().includes('Unavailable');
                /*return !$(this).find('.glance-lastDate').filter(":contains('Unavailable')");*/
            }).toggle();
        });

        var refreshRequest = null;
        $(document).ready(function () {
            refreshRequest = getMain('/Overview/Gateways', 'Gateways', true);

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuNetwork').addClass('currentPage');

            //window.setInterval('updateList()', 30000); //miliseconds 10,000 = 10 sec

            <%if (ViewData["showAlertNotification"] != null && ViewData["showAlertNotification"].ToBool() == true)
        {%>
            newModal('Notifications Turned Off', '/csnet/AlertNotificationsAreOff');
            <%}%>

            $('#networkSelect').change(function () {
                if (refreshRequest != null) {
                    refreshRequest.abort();
                    refreshRequest = null;
                }
                window.location.href = $(this).val();
            });

            $('.GatewayNameFilter').keyup(function () {
                if (nameTimeout != null)
                    clearTimeout(nameTimeout);

                nameTimeout = setTimeout("$.get('/Overview/FilterGatewayName', { name: '" + $(this).val() + "' }, function(data) { getMain('/Overview/Gateways', 'Gateways', true); }); ", 250);
            });

            $('#gatewayTypeFilter').change(function () {
                $.get('/Overview/FilterGatewayTypeID', { gatewayTypeID: $(this).val() }, function (data) {
                    getMain('/Overview/Gateways', 'Gateways', true);
                });
            });

            $('#statusFilter').change(function () {
                $.get('/Overview/FilterGatewayStatus', { status: $(this).val() }, function (data) {
                    getMain('/Overview/Gateways', 'Gateways', true);
                });
            });
        });

        var nameTimeout = null;

        function keepAlive() {
            if (typeof resetSessionTimeout === "function")
                resetSessionTimeout();
        }
        function queryString(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

        var returnUrl;
        var returnTitle;
        function getMain(url, title, setReturn) {
            keepAlive();
            if (!url) {
                url = returnUrl;
                title = returnTitle;
            }
            if (!title) {
                title = "Information";
            }

            if (setReturn) {
                returnUrl = url;
                returnTitle = title;
            }

            if ($(window).scrollTop() > $('#MainTitle').offset().top) {
                $(window).scrollTop($('#MainTitle').offset().top);
            }

            $('#MainTitle').html(title);
            $('#Main').html('');
            $('#loading').show();
            var xhr = $.get(url, function (data) {
                $('#Main').html(data).fadeIn();
                $('#loading').hide();
                $('#refreshSpinner').hide();
                $('#refreshBtn').show();
                $('#hasLastMessageFilter').prop('checked', false)
            });

            return xhr;
        }

        function refreshList(auto) {
            if (refreshRequest != null) {
                refreshRequest.abort();
                refreshRequest = null;
            }
            refreshRequest = getMain('/Overview/Gateways', 'Gateway', true);
        }

        function updateList() {
            $.get('/Overview/GatewaysRefresh', function (data) {
                $.each(eval(data), function (index, value) {
                    var tr = $('.viewGatewayDetails.' + value.GatewayID);
                    tr.find('.superstatusIcon').attr("src", value.StatusImageUrl);
                    tr.find('.overviewDate').html(value.Date);
                    tr.find('.sigIcon img').attr("src", value.SignalImageUrl);
                    tr.find('.battIcon img').attr("src", value.PowerImageUrl);
                    tr.find('.pauseIcon').toggle(value.NotificationPaused);
                    tr.find('.dirtyIcon').toggle(value.IsDirty);
                });
            });
        }

        function AckAllButton(anchor) {

            var href = $(anchor).attr('href');
            if (confirm('Are you sure you want to acknowledge all active notifications?')) {
                $.post(href, function (data) {
                    if (data == "Success") {
                        window.location.href = window.location.href;
                    }
                    else if (data == "Unauthorized") {
                        showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge notifications")%>");
                    }
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to acknowledge notification")%>");
                });
            }
        }
    </script>

</asp:Content>
