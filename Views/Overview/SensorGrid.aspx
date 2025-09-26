<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorGrid
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <style>
        @media print {
            body * {
                visibility: hidden;
            }

            .siteLogo {
                visibility: visible;
            }

            #networkSelect, .form-Control * {
                visibility: visible;
            }

            #hook-five * {
                visibility: visible;
            }

            #hook-five {
                position: absolute;
                left: 22px !important;
                column-count: 2;
                column-gap: 0px;
                display: inline-block;
            }
            /*li a #userName {
        visibility:visible;
    }*/
            .gridPanel {
                visibility: visible;
                width: 80%;
                height: 80% !important;
                /*box-shadow: 0px 0px 0px 1px #515356!important;*/
            }

            a[href]::after {
                content: none !important;
            }

            table, table tr td, table tr th td {
                page-break-inside: avoid !important;
            }
        }
    </style>
    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">
            <div id="MainTitle" style="display: none;"></div>
            <div style="height: 20px">

                <div class="col-xs-6 powertour-hook" id="hook-one" style="padding-right: 2px !important;">
                    <% List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
                        if (CSNetList.Count > 1)
                        {%>
                    <select id="networkSelect" class="form-control" style="height: 30px;">
                        <option value='/Overview/SensorIndex/?id=-1&view=Grid'><%: Html.TranslateTag("All Networks","All Networks")%></option>
                        <%foreach (CSNet Network in CSNetList)
                            { %>
                        <option value='/Overview/SensorIndex/?id=<%:Network.CSNetID%>&view=Grid' <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected" : "" %>><%=Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                        <% } %>
                    </select>
                    <% } %>
                </div>

            </div>
            <div class="col-xs-6" id="MainRefresh" style="padding-left: 2px !important;">
                <div class="powertour-hook" id="hook-two" style="float: right; margin-bottom: 5px; background: #F7F7F7;">
                    <!--<div class="btn-group -->
                    <!-- <div class="btn-group-->
                    <div style="height: 30px;">
                        <!--<a title="Grid View" class="btn btn-default -->
                        <a title="List View" class="ico-btn extra" href="/Overview/SensorIndex/?id=<%:ViewBag.netID%>&view=List" id="grid">
                            <!--style="padding-top:0px!important">
							<!--<i class="glyphicon glyphicon-th" style="top: 3px;font-size: 1.0 em !important;padding: 0.4 em 0.7 em !important;"></i>-->
                            <img src="../../Content/images/iconmonstr-menu-2-240.png" style="height: 16px; margin: 0px; margin-top: 7px; margin-right: 10px; margin-left: 10px;">
                        </a>
                        <a href="#" onclick="$('#settings').toggle(); return false;">
                            <%--<a href="#" onclick="$('#settings').toggle(); return false;" class="btn btn-default ico-btn">
                            <img src="<%=Html.GetThemedContent("/images/sort.png")%>" alt="View Options" title="View Options" />
                            <i class="fa fa-filter"></i>--%>
                            <img src="../../Content/images/iconmonstr-filter-1-240 (1).png" style="height: 16px; margin: 0px; margin-top: 5px; margin-right: 10px;">
                        </a>
                        <a href="/" onclick="getMain(); return false;">
                            <%--<a href="/" onclick="getMain(); return false;" class="btn btn-default ico-btn">
                            <img src="<%=Html.GetThemedContent("/images/refresh.png")%>" alt="Refresh" title="Refresh" />
                            <i class="fa fa-refresh"></i>--%>
                            <img src="../../Content/images/iconmonstr-refresh-3-240 (1).png" style="height: 20px; margin: 0px; margin-top: 5px; margin-right: 13px;">
                        </a>
                    </div>
                </div>
            </div>
            <!-- End Main Refresh -->
        </div>
        <!-- End Form Title -->

        <div class="clearfix"></div>

        <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
            <div class="col-xs-12 col-md-2" style="padding-top: 13px">
                <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                <span id="filterdSensors"></span>/<span id="totalSensors"></span>
            </div>
            <div class="col-xs-12 col-md-3">
                <input type="text" class="NameFilter" placeholder="Device Name..." style="height: 25px; width: 140px; margin-top: 11px;" />
            </div>
            <div class="col-xs-12 col-md-3">
                <select id="applicationFilter" style="margin-top: 11px; width: 140px; height: 25px;">
                    <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles","All Sensor Profiles")%></option>
                    <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                        {%>
                    <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                    <%}%>
                </select>
            </div>
            <div class="col-xs-12 col-md-3">
                <select id="statusFilter" style="margin-top: 11px; width: 140px; height: 25px;">
                    <option value="All"><%: Html.TranslateTag("Overview/Index|All Statuses","All Statuses")%></option>
                    <%foreach (string Stat in Enum.GetNames(typeof(Monnit.eSensorStatus)))
                        {%>
                    <option value='<%: Stat%>'><%:Stat %></option>
                    <%}%>
                </select>
            </div>
            <div class="col-xs-12 col-md-2"></div>
            <div class="col-xs-12 col-md-6">
                <strong><%: Html.TranslateTag("Overview/Index|Sort","Sort")%>: </strong>
                <select id="sortFilter" style="margin-top: 11px; width: 226px; height: 25px;">
                    <option value="Sensor Name - Asc" class="sortable"><%: Html.TranslateTag("Overview/Index|Sensor Name A-Z","Sensor Name A-Z")%></option>
                    <option value="Sensor Name - Desc" class="sortable"><%: Html.TranslateTag("Overview/Index|Sensor Name Z-A","Sensor Name Z-A")%></option>
                    <option value="Last Check In - Desc" class="sortable"><%: Html.TranslateTag("Overview/Index|Last Message New-Old","Last Message New-Old")%></option>
                    <option value="Last Check In - Asc" class="sortable"><%: Html.TranslateTag("Overview/Index|Last Message New-Old","Last Message Old-New")%></option>
                    <option value="Signal - Asc" class="sortable"><%: Html.TranslateTag("Overview/Index|Signal Strength Low-High","Signal Strength Low-High")%></option>
                    <option value="Signal - Desc" class="sortable"><%: Html.TranslateTag("Overview/Index|Signal Strength High-Low","Signal Strength High-Low")%></option>
                    <option value="Battery - Asc" class="sortable"><%: Html.TranslateTag("Overview/Index|Battery Low-High","Battery Low-High")%></option>
                    <option value="Battery - Desc" class="sortable"><%: Html.TranslateTag("Overview/Index|Battery High-Low","Battery High-Low")%></option>
                </select>
            </div>
        </div>

        <div class="clearfix"></div>
        <div class="powertour-hook" id="hook-five">
            <div class="powertour-hook" id="hook-four">
                <div class="powertour-hook" id="hook-three">
                    <div id="Main" style="width: 100%;"></div>
                    <div id="MainLoading" style="text-align: center;">
                        <div id="loadingGIF" class="text-center" style="display: none;">
                            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End of fullform -->



    <script type="text/javascript">
        //only allow a single refresh request at a time (for large networks that take too much time.
        var refreshRequest = null;

        $(document).ready(function () {

            refreshRequest = getMain('/Overview/AtAGlanceGrid', 'At a Glance', true);

            $('.NameFilter').keyup(function () {
                filterName($(this).val());
            });

            window.setInterval('updateList()', 30000); //miliseconds 10,000 = 10 sec
        //window.setInterval('refreshGatewayList(true)', 300000) // 60,000 = 1 minute; 300,000 = 5 min

            <%if (ViewData["showAlertNotification"] != null && ViewData["showAlertNotification"].ToBool() == true)
        {%>
            newModal('Notifications Turned Off', '/csnet/AlertNotificationsAreOff');
            <%}%>

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuNetwork').addClass('currentPage');


            $('#networkSelect').change(function () {
                if (refreshRequest != null) {
                    refreshRequest.abort();
                    refreshRequest = null;
                }
                window.location.href = $(this).val();
            });

            $('#applicationFilter').change(function () {
                if (refreshRequest != null) {
                    refreshRequest.abort();
                    refreshRequest = null;
                }
                $.get('/Overview/FilterAppID', { appID: $(this).val() }, function (data) {
                    refreshList();
                });
            });

            $('#viewAlert').on('click', function () {
                disableUnsavedChangesAlert();
                window.location = "/Notification";
            });

            $('#viewPending').on('click', function () {
                disableUnsavedChangesAlert();
                window.location = "/Notification";
            });

            $('#statusFilter').change(function () {
                if (refreshRequest != null) {
                    refreshRequest.abort();
                    refreshRequest = null;
                }
                $.get('/Overview/FilterStatus', { status: $(this).val() }, function (data) {
                    refreshList();
                });
            });
        });

        function keepAlive() {
            //this method part of autoLogout.js if included on page
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
            $('#Main').hide();
            $('#MainTitle').html(title);
            $('#MainLoading').fadeIn('fast');
            var xhr = $.get(url, function (data) {
                $('#Main').html(data).fadeIn(function () {
                    var urlString = window.location.href;
                    var sensID = urlString.split("#")[1];

                    if (sensID != null)
                        //window.location.hash = '#SensorHome' + sensID;

                        //document.getElementById('SensorHome' + sensID).scrollIntoView();
                });
                $('#MainLoading').hide();
            });

            return xhr;
        }

        function refreshList(auto) {
            if (refreshRequest != null) {
                refreshRequest.abort();
                refreshRequest = null;
            }
            refreshRequest = getMain('/Overview/AtAGlanceGrid', 'At a Glance', true);
        }

        function AckAllNotiButton(anchor) {
            var href = '/Overview/AcknowledgeAllActiveNotifications?AckMethod=Browser_Notification';
            if (confirm('Are you sure you want to acknowledge all active notifications?')) {
                $.post(href, function (data) {
                    if (data == "Success") {
                        window.location.href = window.location.href;
                    }
                    else if (data == "Unauthorized") {
                        showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge notifications")%>");
                    }
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to acknowledge notifications")%>");
                });
            }
        }

        function ResetAllNotiButton(anchor) {
            var href = "/Overview/ResetAllPendingConditions"
            if (confirm('Are you sure you want to reset all pending notifications?')) {
                $.post(href, function (data) {
                    if (data == "Success") {
                        window.location.href = window.location.href;
                    }
                    else if (data == "Unauthorized") {
                        showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to reset notifications")%>");
                    }
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to reset notification")%>");
                });
            }
        }

        function updateList() {
            if (refreshRequest == null) {
                $.get('/AutoRefresh/AtAGlanceRefresh', function (data) {
                    //alert(eval(data).length);
                    $.each(eval(data), function (index, value) {
                        var tr = $('.viewSensorDetails.' + value.SensorID);
                        tr.find('.superstatusIcon').attr("src", value.StatusImageUrl);
                        tr.find('.overviewReading').html(value.Reading);
                        tr.find('.overviewDate').html(value.Date);
                        tr.find('.sigIcon img').attr("src", value.SignalImageUrl);
                        tr.find('.battIcon img').attr("src", value.PowerImageUrl);
                        tr.find('.battIcon div').html(value.Voltage);
                        tr.find('.pauseIcon').toggle(value.notificationPaused);
                        tr.find('.dirtyIcon').toggle(value.isDirty);
                    });

                    refreshRequest = null;
                });
            }
        }

        var nameTimeout = null;
        function filterName(name) {
            if (nameTimeout != null)
                clearTimeout(nameTimeout);
            if (refreshRequest != null) {
                refreshRequest.abort();
                refreshRequest = null;
            }
            nameTimeout = setTimeout("$.get('/Overview/FilterName', { name: '" + name + "' }, function(data) { refreshList(); }); ", 250);
        }


        $('#sortFilter').change(function (e) {
            e.preventDefault();

            var sort = $(this).val()
            var column = sort.split("-")[0];
            var dir = sort.split("-")[1];

            $.get('/Overview/SensorSortBy', { column: column, direction: dir }, function (data) {
                if (data == "Success")
                    getMain();
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        });

    </script>
</asp:Content>
