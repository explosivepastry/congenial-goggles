<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Overview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
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
                column-count: 1;
                column-gap: 0px;
                display: inline-block;
            }

            .gridPanel {
                visibility: visible;
                width: 80%;
                height: 80% !important;
                /*box-shadow: 0px 0px 0px 1px #515356!important;*/
            }

            .timezoneDisplay {
                display: inline-block !important;
                justify-content: flex-start !important;
            }

            a[href]::after {
                content: none !important;
            }

            table, table tr td, table tr th td {
                page-break-inside: avoid !important;
            }
        }
    </style>

    <%
        string urlname = HttpUtility.ParseQueryString(Request.Url.Query).Get("name");
        string urlprofile = HttpUtility.ParseQueryString(Request.Url.Query).Get("profile");
        string urlstatus = HttpUtility.ParseQueryString(Request.Url.Query).Get("status");
        string urlsort = HttpUtility.ParseQueryString(Request.Url.Query).Get("sort");

        long networkID = 0;
        if (ViewBag.netID != null)
            networkID = ViewBag.netID;

        List<SensorGroupSensorModel> sensorlist = Model;%>

    <div class="container-fluid">
        <%if (sensorlist.Count == 0 && ViewBag.TotalSensors == 0)
            { %>
        <div>
            <div class="col-12">
                <div class="gridPanelx no-sensor-containerx">
                    <% Html.RenderPartial("_LonelyAstronaut"); %>

                    <h2 style="text-align: center;"><%: Html.TranslateTag("Overview/SensorIndex|There are no Sensors","There are no Sensors")%>.</h2>
                    <% if (MonnitSession.CustomerCan("Network_Edit"))
                        { %>
                    <% if (networkID > 0)
                        { %>
                    <a class="btn btn-primary" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>?networkID=<%:networkID%>">
                        <% }
                            else
                            { %>
                        <a class="btn btn-primary" href="/Network/NetworkSelect?AccountID=<%=MonnitSession.CurrentCustomer.AccountID %> ">
                            <% } %>
                            <%=Html.GetThemedSVG("add") %>
                        &nbsp; <%: Html.TranslateTag("Overview/SensorIndex|Add Sensor", "Add Sensor")%>
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
                <span class="ms-1"><%: Html.TranslateTag("Overview/SensorIndex|Add Sensor","Add Sensor")%></span>
            </a>
            <% } %>
        </div>

        <div class="bottom-add-btn-row media_mobile">
            <% if (MonnitSession.CustomerCan("Network_Edit"))
                { %>
            <a class="add-btn-mobile" style="box-shadow: 0px 3px 6px 1px rgba(0, 0, 0, 0.5);" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>?networkID=<%:networkID%>">
                <%=Html.GetThemedSVG("add") %>
            </a>
            <% } %>
        </div>

        <div class="rule-card_container w-100">
            <%
                string allnetworks = Html.TranslateTag("All Networks");

                networkID = MonnitSession.SensorListFilters.CSNetID;
                string networkName = "";
                if (ViewBag.netID != null)
                    networkID = ViewBag.netID;

                if (networkID < 0)
                {
                    networkName = allnetworks;
                }
                else if (sensorlist.Count < 1)
                {
                    //don't change csnetid            
                }
                else
                {
                    networkID = sensorlist[0].Sensor.CSNetID;

                }
                CSNet network = CSNet.Load(networkID);

                if (network != null)
                    networkName = network.Name.Length > 0 ? network.Name : network.CSNetID.ToString();

            %>

            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div class="col-xs-6 dfac">
                        <%=Html.GetThemedSVG("sensor") %>
                        <span class="card_container__top__title__text ms-2" id="networkname">
                            <%=networkName  %>
                            



                        </span>
                    </div>
                    <div class="col-xs-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                                <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                                    <%=Html.GetThemedSVG("filter") %>
                                </a>
                                <a id="refreshBtn" href="/" onclick="$(this).hide();$('#refreshSpinner').show();refreshList(); return false;">
                                    <%=Html.GetThemedSVG("refresh") %>
                                </a>
                                <div style="display: none;" class="spinner-border spinner-border-sm text-primary" id="refreshSpinner" role="status">
                                    <%--                                    <span class="sr-only"><%: Html.TranslateTag("Overview/SensorIndex|Loading","Loading")%>...</span>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card_container__body">
                <div class="row" id="settings" style="display: none; border: 1px solid #dbdbdb;">
                    <%
                        string filterURL = Request.Url.Host + ":" + Request.Url.Port + "/Overview/SensorIndex?id=" + networkID + "&name=" + urlname + "&profile=" + urlprofile + "&status=" + urlstatus + "&sort=" + urlsort;
                    %>

                    <!-- Filter URL Modal-->
                    <div class="modal fade" id="filterURLModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-body">
                                    <form>
                                        <div class="form-group">
                                            <label for="filterURL" class="control-label"><%: Html.TranslateTag("Overview/SensorIndex|Filter URL","Filter URL")%>:</label>
                                            <input type="text" class="form-control" id="filterURL" value="<%=filterURL%>" />
                                        </div>
                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-12 mb-2 mt-2 d-md-flex align-items-center">
                        <div class="d-flex align-items-center my-2 flex-wrap">
                            <div class="col-12 d-flex">
                                <strong><%: Html.TranslateTag("Overview/SensorIndex|Filter","Filter")%>: &nbsp;</strong>
                                <span id="filterdSensors"></span>/<span id="totalSensors"></span>
                                <span id="add_sensor_btn" data-bs-toggle="modal" data-bs-target="#filterURLModal" class="ms-2">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-share-2">
                                        <circle cx="18" cy="5" r="3"></circle><circle cx="6" cy="12" r="3"></circle><circle cx="18" cy="19" r="3"></circle><line x1="8.59" y1="13.51" x2="15.42" y2="17.49"></line><line x1="15.41" y1="6.51" x2="8.59" y2="10.49"></line>
                                    </svg>
                                </span>
                            </div>
                        </div>

                        <div class="col-12 col-md-10 d-flex flex-wrap">
                            <input style="width: 250px;" type="text" id="NameFilter" class="NameFilter form-control " placeholder="<%: Html.TranslateTag("Overview/SensorIndex|Device Name","Device Name")%>..." value="<%= string.IsNullOrEmpty(urlname) ? "" : urlname %>" />
                            <select style="width: 250px;" id="applicationFilter" class="form-select mx-lg-1">
                                <option value="-1"><%: Html.TranslateTag("Overview/SensorIndex|All Sensor Profiles","All Sensor Profiles")%></option>
                                <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                    {%>
                                <option <%= urlprofile.ToLong() == App.ApplicationID ? "selected" : "" %> value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                                <%}%>
                            </select>

                            <select style="width: 250px;" id="statusFilter" class="form-select ">
                                <option value="All"><%: Html.TranslateTag("Overview/SensorIndex|All Statuses","All Statuses")%></option>
                                <%foreach (string Stat in Enum.GetNames(typeof(Monnit.eSensorStatus)))
                                    {
                                        string statusDisplayName = Stat;
                                        if (statusDisplayName == "OK")
                                            statusDisplayName = "Active";
                                %>
                                <option <%= urlstatus == Stat ? "selected" : "" %> value='<%: Stat%>'><%:statusDisplayName %></option>
                                <%}%>
                            </select>
                        </div>
                    </div>

                    <div class="col-12 d-flex align-items-center my-2">
                        <strong><%: Html.TranslateTag("Overview/SensorIndex|Sort","Sort")%>: </strong>
                        <select id="sortFilter" class="form-select ms-4" style="width: 250px;">
                            <option <%= urlsort == "Sensor Name - Asc" ? "selected" : "" %> value="Sensor Name - Asc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Sensor Name A-Z","Sensor Name A-Z")%></option>
                            <option <%= urlsort == "Sensor Name - Desc" ? "selected" : "" %> value="Sensor Name - Desc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Sensor Name Z-A","Sensor Name Z-A")%></option>
                            <option <%= urlsort == "Last Check In - Desc" ? "selected" : "" %> value="Last Check In - Desc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Last Message New-Old","Last Message New-Old")%></option>
                            <option <%= urlsort == "Last Check In - Asc" ? "selected" : "" %> value="Last Check In - Asc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Last Message New-Old","Last Message Old-New")%></option>
                            <option <%= urlsort == "Signal - Asc" ? "selected" : "" %> value="Signal - Asc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Signal Strength Low-High","Signal Strength Low-High")%></option>
                            <option <%= urlsort == "Signal - Desc" ? "selected" : "" %> value="Signal - Desc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Signal Strength High-Low","Signal Strength High-Low")%></option>
                            <option <%= urlsort == "Battery - Asc" ? "selected" : "" %> value="Battery - Asc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Battery Low-High","Battery Low-High")%></option>
                            <option <%= urlsort == "Battery - Desc" ? "selected" : "" %> value="Battery - Desc" class="sortable"><%: Html.TranslateTag("Overview/SensorIndex|Battery High-Low","Battery High-Low")%></option>
                        </select>
                    </div>
                </div>

                <!-- _SensorDetails is populated by AtAGlanceOV -->

                <div id="sensorList" class="sensorList_main mx-md-0" style="display: flex; flex-wrap: wrap; padding: 0.25rem;"></div>
                <div class="text-center" id="loading">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden"><%: Html.TranslateTag("Overview/SensorIndex|Loading","Loading")%>...</span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>

    <script type="text/javascript">
        //only allow a single refresh request at a time (for large networks that take too much time.
        var refreshRequest = null;

        $(document).ready(function () {

            refreshRequest = getMain('/Overview/AtAGlanceOV', 'At a Glance', true)
                .done(() => {
                    // This page has all session filters cleared by the controller each load but the DOM cache doesn't revert filters in UI to default if back-button is pressed,
                    // giving the user the impression that filters are applied when they are not. 
                    // Also, the filter URL uses the DOM "selected" property, not the HTML "selected" attribute, so the URL 
                    // will not accurately represent the current state. This page sets the HTML "selected" attribute on an option if a filter has been applied. 
                    // Thus, an option with the DOM "selected" property but not the HTML "selected" attribute means that a filter in the view state is out of sync with the model state.
                    $('.NameFilter').val('');
                    if ($('#applicationFilter').children('option:selected').attr('selected') == undefined)
                        $('#applicationFilter').children(':first').attr('selected', true);
                    if ($('#statusFilter').children('option:selected').attr('selected') == undefined)
                        $('#statusFilter').children(':first').attr('selected', true);
                    if ($('#sortFilter').children('option:selected').attr('selected') == undefined)
                        $('#sortFilter').children(':first').attr('selected', true);
                });
            refreshRequest = null;

            window.setInterval('updateList()', 30000); //miliseconds 10,000 = 10 sec 

            $('#filterURLModal').on('show.bs.modal', function () {
                var networkid = <%: networkID == -1 ? "-1" : networkID.ToString()%>;

                var filtername = $('.NameFilter').val();

                var profile = document.getElementById("applicationFilter");
                var filterprofile = profile.value;

                var status = document.getElementById("statusFilter");
                var filterstatus = status.value;

                var sort = document.getElementById("sortFilter");
                var filtersort = sort.value;

                var filterURL = "<%=Request.Url.Host%>" + "/Overview/SensorIndex?id=" + networkid + "&name=" + filtername + "&profile=" + filterprofile + "&status=" + filterstatus + "&sort=" + filtersort;

                document.getElementById("filterURL").value = filterURL;

                $(this).find('.modal-body').css({
                    'max-height': '100%'
                });
            });

            <%if (ViewData["showAlertNotification"] != null && ViewData["showAlertNotification"].ToBool() == true)
        {%>
            newModal('Notifications Turned Off', '/csnet/AlertNotificationsAreOff');
            <%}%>

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuNetwork').addClass('currentPage');

            $('#iconHelp').click(function () {
                $('#iconLegend').dialog();
            });

            $('#viewAlert').on('click', function () {
                disableUnsavedChangesAlert();
                window.location = "/Notification";
            });

            $('#viewPending').on('click', function () {
                disableUnsavedChangesAlert();
                window.location = "/Notification";
            });

            $('#sortFilter').change(function (e) {
                e.preventDefault();

                var sort = $(this).val()
                var column = sort.split("-")[0];
                var dir = sort.split("-")[1];

                $.get('/Overview/SensorSortBy', { column: column, direction: dir }, function (data) {
                    if (data == "Success") {
                        refreshList();
                    }
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            });

            $("li a").each(function () {
                if (this.href == window.location.href) {
                    $(this).addClass("active");
                }
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

            $('#MainTitle').html(title);
            $('#MainLoading').fadeIn('fast');
            $('#loading').show();
            $('#sensorList').html('');
            var xhr = $.get(url, function (data) {

                $('#loading').hide();
                $('#sensorList').html(data).fadeIn(function () {
                    var urlString = window.location.href;
                    var sensID = urlString.split("#")[1];
                });
                $('#MainLoading').hide();
                $('#refreshSpinner').hide();
                $('#refreshBtn').show();

            });

            return xhr;
        }

         function refreshList() {
            if (refreshRequest != null) {
                refreshRequest.abort();
                refreshRequest = null;
            }
            refreshRequest = getMain('/Overview/AtAGlanceOV', 'At a Glance', true);

        }

        function updateList() {
            if (refreshRequest == null) {
                $.get('/Overview/AtAGlanceRefresh', function (data) {

                    $.each(eval(data), function (index, value) {
                        var tr = $('#SensorHome' + value.SensorID); //tr = $('.viewSensorDetails.' + value.SensorID);*/
                        tr.find('.corp-status').removeClass().addClass("sensorIcon corp-status sensorStatus" + value.StatusImageUrl);
                        tr.find('.glance-reading').html(value.Reading);
                        tr.find('.glance-lastDate').html(value.Date);
                        tr.find('.gatewaySignal').html(value.SignalImageUrl);
                        //console.log(value.SignalImageUrl);
                        //console.log(tr.find('.gatewaySignal'));
                        tr.find('.battIcon').html(value.PowerImageUrl);
                        tr.find('.battIcon').attr('title', value.voltageString);
                        tr.find('.pausesvg').toggle(value.notificationPaused);
                        tr.find('.pendingsvg').toggle(value.isDirty);
                        //console.log(value.voltageString)
                    });
                    refreshRequest = null;
                });
            }
        }

        var nameTimeout = null;
        function filterName(key, name) {
            clearTimeout(nameTimeout);
            if (key === 'Enter') {
                refreshList();
            } else {
                nameTimeout = setTimeout("$.get('/Overview/FilterName', { name: '" + name + "' }, function(data) { refreshList(); }); ", 1000);
            }
            console.log("refresh")
        }

    </script>

    <script type="text/javascript">
    <% if (MonnitSession.SensorListFilters.CSNetID > long.MinValue ||
                              MonnitSession.SensorListFilters.ApplicationID > long.MinValue ||
                              MonnitSession.SensorListFilters.Status > int.MinValue ||
                              MonnitSession.SensorListFilters.Name != "")
            Response.Write("$('#filterApplied').show();");
        else
            Response.Write("$('#filterApplied').hide();");
    %>

        $(document).ready(function () {

            $('.NameFilter').keyup(function (event) {
                filterName(event.key, $(this).val());
            });

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

            $('#statusFilter').change(function () {
                if (refreshRequest != null) {
                    refreshRequest.abort();
                    refreshRequest = null;
                }
                $.get('/Overview/FilterStatus', { status: $(this).val() }, function (data) {
                    refreshList();
                });
            });

            <%--$('.sortable').click(function (e) {
                e.preventDefault();

                $.get('/Overview/SensorSortBy', { column: $(this).attr("href"), direction: $(this).attr("data-direction") }, function (data) {
                    if (data == "Success") {
                        refreshList();
                    }
                    else {
                        console.log(data);
                        let values = {};
                        values.text = "<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>";
                        openConfirm(values);
                        $('#modalCancel').hide();
                    }
                });
            }).css('font-weight', 'bold').css('text-decoration', 'none');--%>

        });

        var tabToShow = '';
        
    </script>

</asp:Content>
