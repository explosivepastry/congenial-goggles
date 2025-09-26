<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%="" %>

<%
    List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
    var GatewayList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(MonnitSession.CurrentCustomer.AccountID);
    long notiid = ViewBag.NotiID;
%>

<div class="actionsDeviceContainer rule-card_container w-100">
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

    <%if (ViewBag.ShowSensors == true)
        { %>
    <div class="tab-content">
        <div class="tab-pane active" id="sensors" role="tabpanel" aria-labelledby="home-tab" tabindex="0">
            <div class="trigger-device__top">
                <div class="card_container__top" style="border-bottom: .2px solid #e6e6e6; margin-bottom: -8px; align-items: center">
                    <div class="card_container__top__title mt-2" style="border-bottom: none">
                        <span class="me-2"><%=Html.GetThemedSVG("sensor") %></span>
                        <%: Html.TranslateTag("Events/Triggers|Available Sensors")%>
                    </div>
                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Task Help", "Task Help") %>" data-bs-target=".taskHelp">
                        <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
                    </a>
                    <div class="clearfix"></div>
                </div>
                <div style="margin: 13px 0; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                    <font color="gray">
                        <%: Html.TranslateTag("Events/TriggersSensors|Click Sensor to enable/disable","Click Sensor to enable/disable")%>
                    </font>
                    <a href="#" onclick="$('#settings').toggle(); return false;">
                        <%=Html.GetThemedSVG("filter") %>			
                    </a>
                </div>
            </div>

            <%---------HELP MODAL--------%>
            <div class="modal fade taskHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/Triggers|Sensor List Help")%></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                <div>
                                    <div class="col- word-choice" style="display: flex; gap: .5rem;">
                                        <div class="check-markAB">
                                            <%=Html.GetThemedSVG("certificate") %>
                                        </div>
                                        <%: Html.TranslateTag("Rule/Triggers|Primary Sensor")%>
                                    </div>
                                    <div class=" word-def">
                                        <div class="word-def">
                                            <%: Html.TranslateTag("Rule/Triggers|Clicking these buttons in the list will set the corresponding sensor as the primary sensor for the rule. If the sensor has a custom scale, the rule will utilize it. Please note that each rule can have only one primary sensor.")%>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div>
                                    <div class="col- word-choice" style="display: flex; gap: .5rem;">
                                        <div class="check-markAB ListBorderNotActive">
                                            <%=Html.GetThemedSVG("circle-check") %>
                                        </div>
                                        <%: Html.TranslateTag("Rule/Triggers|Apply Rule to Sensor")%>
                                    </div>
                                    <div class=" word-def" ">
                                        <%: Html.TranslateTag("Rule/Triggers|Clicking these buttons in the list will apply the current rule to the corresponding sensor. One rule can apply to many sensors.")%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-12">
                <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
                    <div class="col-12 col-md-2" style="padding-top: 13px">
                        <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                        <span id="filterdSensors"></span>/<span id="totalSensors"></span>
                    </div>

                    <div class="col-12 col-md-3">
                        <input type="text" id="SensorNameFilter" class="form-control user-dets" placeholder="Device Name..." style="width: 200px;" />
                    </div>
                    <div class="col-12 col-md-3">
                        <select id="applicationFilter" class="form-select" style="width: 200px;">
                            <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles","All Sensor Profiles")%></option>
                            <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                                {%>
                            <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                            <%}%>
                        </select>
                    </div>
                    <div class="col-12 col-md-3">
                        <select id="networkFilter" class="form-select" style="width: 200px;">
                            <option value="-1"><%: Html.TranslateTag("Overview/Index|All Networks","All Networks")%></option>
                            <%foreach (var g in GatewayList)
                                {%>
                            <option value='<%:g.CSNetID %>'><%:g.Name %></option>
                            <%}%>
                        </select>
                    </div>
                    <div class="col-12 col-md-1 dfjcac">
                        <input type="checkbox" id="selectedSensorsFilter" />
                        <div class="ms-1">Selected</div>
                    </div>
                </div>
                <div id="triggerSensors" class="hasScroll d-flex" style="flex-wrap: wrap;">
                </div>
                <div id="sensorsLoading" class="text-center" style="display: none;">
                    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
            </div>
        </div>
        <%} %>


        <%if (ViewBag.ShowGateways == true)
            { %>
        <div class="tab-pane" id="gateway" role="tabpanel" aria-labelledby="profile-tab" tabindex="0">
            <div class="trigger-device__top">
                <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px;">
                    <div class="card_container__top__title  mt-2">
                        <span class="me-2"><%=Html.GetThemedSVG("gateway") %></span>

                        <%: Html.TranslateTag("Events/Triggers|Trigger Gateways")%>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div
                    style="margin: 13px 0; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                    <font color="gray">
                        <%: Html.TranslateTag("Events/TriggersSensors|Click Gateway to enable/disable","Click Gateway to enable/disable")%>
                    </font>

                    <a href="#" onclick="$('#gatewaysettings').toggle(); return false;">
                        <%=Html.GetThemedSVG("filter") %>			
                    </a>
                </div>
                <div class="col-12 col-md-12">
                    <div style="margin-top: 0px;">

                        <div class="row" id="gatewaysettings" style="display: none;">
                            <div class="col-12 col-md-2" style="padding-top: 13px">
                                <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                                <span id="filterdGateways"></span>/<span id="totalGateways"></span>
                            </div>
                            <div class="col-12 col-md-3">
                                <input type="text" id="GatewayNameFilter" class="form-control user-dets" placeholder="Device Name..." style="max-width: 200px;" />
                            </div>
                            <div class="col-12 col-md-3">
                             <select id="gatewayTypeFilter" class="form-select" style="width: 200px;">
								<option value="-1">All Gateway Types</option>
								<%foreach (GatewayType gt in (from gl in iMonnit.Controllers.CSNetController.GetGatewayList() select GatewayType.Load(gl.GatewayTypeID)).Distinct())
                                    {%>
								  <!-- Don't show PoE, LTE, or Mowi-->  
									 <%if (gt.GatewayTypeID != 35 && gt.GatewayTypeID != 36 && gt.GatewayTypeID != 11 && gt.GatewayTypeID != 38)
                                         { %>
											<option value='<%: gt.GatewayTypeID%>');><%: gt.Name %></option>
									 <%} %>
								<%}%>
							</select>
                            </div>
                            <div class="col-12 col-md-1 dfjcac">
                                <input type="checkbox" id="selectedGatewaysFilter" />
                                <div class="ms-1">Selected</div>
                            </div>
                        </div>
                        <div id="triggerGateways" class="hasScroll">
                        </div>
                        <div id="gatewaysLoading" class="text-center" style="display: none;">
                            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                                <span class="visually-hidden">Loading...</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</div>

<style>
    .check-markAB > svg {
        height: 20px;
        width: 20px;
        fill: grey;
        cursor: default;
    }
</style>

<script type="text/javascript">
	<% if (MonnitSession.SensorListFilters.CSNetID > long.MinValue ||
      MonnitSession.SensorListFilters.ApplicationID > long.MinValue ||
      MonnitSession.GatewayListFilters.GatewayTypeID > long.MinValue ||
      MonnitSession.SensorListFilters.Status > int.MinValue ||
      MonnitSession.SensorListFilters.Name != "")
        Response.Write("$('#filterApplied').show();");
    else
        Response.Write("$('#filterApplied').hide();");
    %>

    let sensorNameSearchTimeoutID;
    let sensorNameSearchRequest;
    let gatewayNameSearchTimeoutID;
    let gatewayNameSearchRequest;

    $(document).ready(function () {
<% if (ViewBag.ShowGateways)
    { %>
        refreshGatewayList();
<% } %>
<% if (ViewBag.ShowSensors)
    { %>
        refreshSensorList();
<% } %>

        $('#SensorNameFilter').keyup(function (e) {
            clearTimeout(sensorNameSearchTimeoutID);
            if (sensorNameSearchRequest)
                sensorNameSearchRequest.abort();

            if (e.keyCode === 13) {
                $.get('/Overview/FilterName', { name: '" + name + "' }, function (data) { refreshSensorList(); });
            } else {
                sensorNameSearchTimeoutID = setTimeout("$.get('/Overview/FilterName', { name: '" + $(this).val() + "' }, function(data) { refreshSensorList(); }); ", 1000);
            }
        });

        $('#GatewayNameFilter').keyup(function (e) {
            clearTimeout(gatewayNameSearchTimeoutID);
            if (gatewayNameSearchRequest)
                gatewayNameSearchRequest.abort();

            if (e.keyCode === 13) {
                $.get('/Overview/FilterGatewayName', { name: '" + name + "' }, function (data) { refreshGatewayList(); });
            } else {
                gatewayNameSearchTimeoutID = setTimeout("$.get('/Overview/FilterGatewayName', { name: '" + $(this).val() + "' }, function(data) { refreshGatewayList(); }); ", 1000);
            }
        });

        $('#applicationFilter').change(function () {
            $.get('/Overview/FilterAppID', { appID: $(this).val() }, function (data) {
                refreshSensorList();
            });
        });

        $('#gatewayTypeFilter').change(function () {
            $.get('/Overview/FilterGatewayTypeID', { gatewayTypeID: $(this).val() }, function (data) {
                refreshGatewayList();
            });
        });

        //for filtering by network
        $('#networkFilter').change(function () {
            refreshSensorList();
        });

        $('#selectedSensorsFilter').click(function () {
            $('#triggerSensors').children().filter(function () {
                return $(this).find('div.ListBorderNotActive').length > 0
            }).toggle();
        });

        $('#selectedGatewaysFilter').click(function () {
            $('#triggerGateways').children().filter(function () {
                return $(this).find('div.ListBorderNotActive').length > 0
            }).toggle();
        });
    });

    function refreshSensorList(auto) {
        $('#triggerSensors').html('');
        $('#sensorsLoading').show();
        var id = <%:notiid%>;
        sensorNameSearchRequest = $.get('/Rule/TriggerSensorList',
            {
                id: id,
                networkID: $('#networkFilter option:selected').val() != -1 ? $('#networkFilter option:selected').val() : null
            },
            function (data) {
                $('#sensorsLoading').hide();
                $('#triggerSensors').html(data);
            });
    }

    function refreshGatewayList(auto) {
        $('#triggerGateways').html('');
        $('#gatewaysLoading').show();
        var id = <%:notiid%>;
        gatewayNameSearchRequest = $.get('/Rule/TriggerGatewayList', { id: id }, function (data) {
            $('#gatewaysLoading').hide();
            $('#triggerGateways').html(data);
        });
    }


</script>
