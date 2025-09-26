<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>

<%
    int gatewayTotal = 0;
    List<Gateway> gateways = ViewBag.GatewayList;
    string GatewayListType = ViewBag.GatewayListType;
    string clickSvgIcon = string.IsNullOrWhiteSpace(GatewayListType) ? "lock" : "gps-pin";

%>

<div class="col-12">
    <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px; display: flex; flex-direction: column; justify-content: flex-start; align-items: start;">
        <div class="clearfix"></div>
        <div style="padding: 5px; font-size: 13px; font-weight: bold; display: flex; flex-direction: column;">
            <span style="display: flex; justify-content: flex-start; align-items: center;">
                <%: Html.TranslateTag("Events/TriggersSensors|Click","Click")%>
                <span class="print" style="padding: 6px 6px 6px 7px; display: flex; justify-content: center; align-items: center; border-radius: 50%;">
                    <%=Html.GetThemedSVG("lockbox") %>
                </span>
                <%: Html.TranslateTag("Events/TriggersSensors|to unlock","to unlock")%>
            </span>

            <span id="deselectGpsSpanIcon" style="display: flex; justify-content: flex-start; align-items: center; margin-top: 5px;">
                <%: Html.TranslateTag("Events/TriggersSensors|Click","Click")%>
                <span class="go-print" style="padding: 6px 6px 6px 7px; display: flex; justify-content: center; align-items: center; border-radius: 50%;">
                    <%=Html.GetThemedSVG("lockbox") %>
                </span>
                <%: Html.TranslateTag("Events/TriggersSensors|to deselect","to deselect")%>
            </span>

            <span style="display: flex; justify-content: flex-start; align-items: center; gap: 10px;">
                <%: Html.TranslateTag("Events/TriggersSensors|Once saved, your gateway" + (string.IsNullOrWhiteSpace(GatewayListType) ? "" : "'s GPS") + " will be unlocked","Once saved, your gateway will be unlocked")%>
                <span style="padding: 6px 6px 6px 7px; display: flex; justify-content: center; align-items: center; border-radius: 50%;">
                    <%=Html.GetThemedSVG("unlock") %>
                </span>
            </span>
        </div>
    </div>

    <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
        <font color="gray">
            <%: Html.TranslateTag("Events/TriggersSensors|Click Gateway to enable/disable","Click Gateway to enable/disable")%>
        </font>
        <div class="btn-group">
            <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                <%=Html.GetThemedSVG("filter") %>
            </a>
            <a href="/" onclick="refreshGatewayUnlockList(); return false;">
                <%=Html.GetThemedSVG("refresh") %>
            </a>
        </div>
    </div>

    <div style="margin: -5px 5px;">
        <div class="col-12 scroll-container bsInset">
            <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
                <div class="col-12 col-md-2" style="padding-top: 13px">
                    <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                    <span id="filteredGateways"></span>/<span id="totalGateways"><%=gatewayTotal %></span>
                </div>
                <div class="col-12 col-md-3">
                    <input type="text" id="GatewayNameFilter" class="form-control" placeholder="Device Name..." style="width: 250px;" value="<%:MonnitSession.GatewayListFilters.Name %>" />
                </div>
                <%if (string.IsNullOrWhiteSpace(GatewayListType))
                    {%>
                <div class="col-12 col-md-3">
                    <select id="gatewayTypeFilter" class="form-select mx-lg-1" style="width: 250px;">
                        <option value="-1"><%: Html.TranslateTag("Overview/GatewayGrid|All Gateway Types","All Gateway Types")%></option>
                        <%foreach (GatewayTypeShort App in GatewayTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                            { // Don't show PoE, LTE, or Mowi
                                if (App.GatewayTypeID != 35 && App.GatewayTypeID != 36 && App.GatewayTypeID != 11)
                                { %>
                        <option value='<%: App.GatewayTypeID%>' <%:MonnitSession.GatewayListFilters.GatewayTypeID == App.GatewayTypeID ? "selected=selected" : ""%>><%:App.Name %></option>
                        <% }
                            } %>
                    </select>
                </div>
                <%} %>

                <div class="col-12 col-md-3">
                    <select id="statusFilter" class="form-select" style="width: 250px;">
                        <option value="All" <%:MonnitSession.GatewayListFilters.Status == int.MinValue ? "selected=selected" : ""%>><%: Html.TranslateTag("Overview/GatewayGrid|All Statuses","All Statuses")%></option>
                        <option value='OK' <%:MonnitSession.GatewayListFilters.Status == eSensorStatus.OK.ToInt() ? "selected=selected" : ""%>><%: Html.TranslateTag("Overview/GatewayGrid|OK","OK")%></option>
                        <option value='Warning' <%:MonnitSession.GatewayListFilters.Status == eSensorStatus.Warning.ToInt() ? "selected=selected" : ""%>><%: Html.TranslateTag("Overview/GatewayGrid|Warning","Warning")%></option>
                        <%--<option value='Sleeping' <%:MonnitSession.GatewayListFilters.Status == eSensorStatus.Sleeping.ToInt() ? "selected=selected" : ""%>><%: Html.TranslateTag("Overview/GatewayGrid|Sleeping","Sleeping")%></option>--%>
                        <option value='Offline' <%:MonnitSession.GatewayListFilters.Status == eSensorStatus.Offline.ToInt() ? "selected=selected" : ""%>><%: Html.TranslateTag("Overview/GatewayGrid|Offline","Offline")%></option>
                    </select>
                </div>

            </div>
            <div id="triggerGateways" class="sensorList_main scrollx-child mh300 ">
                <%=Html.Partial("_GatewayListDetails", gateways, new ViewDataDictionary{ { "TotalGatewayUnlocks", gatewayTotal }, { "GatewayListType", GatewayListType }, { "ClickSvgIcon", clickSvgIcon } }) %>
            </div>
        </div>
    </div>

    <div>
        <div class="bold  dfjcfeac" style="padding: 10px;">
            <button class="btn btn-primary" type="button" onclick="processCredits()" value="<%: Html.TranslateTag("Save","Save")%>">
                <%: Html.TranslateTag("Save","Save")%>
            </button>
        </div>

    </div>
</div>
<style>
    .print > svg {
        width: 40px
    }

        .print > svg path {
            fill: #969696;
        }

    .go-print > svg {
        width: 40px;
    }

    .go-print svg path {
        fill: #4FB848;
    }
</style>

<script type="text/javascript">
    <% if (MonnitSession.GatewayListFilters.SensorListFiltersCSNetID > long.MinValue ||
MonnitSession.GatewayListFilters.GatewayTypeID > long.MinValue ||
MonnitSession.GatewayListFilters.Status > int.MinValue ||
MonnitSession.GatewayListFilters.Name != "")
        Response.Write("$('#filterApplied').show();");
    else
        Response.Write("$('#filterApplied').hide();");
    %>

    var IDarray = [];
    var creditsAvailable = Number(<%=Model.CreditsAvailable%>);

    $(document).ready(function () {
        $('#GatewayNameFilter').keyup(function () {
            filterName($(this).val());
        });

        $('#gatewayTypeFilter').change(function () {
            $.get('/Overview/FilterGatewayTypeID', { gatewayTypeID: $(this).val() }, function (data) {
                refreshGatewayUnlockList();
            });
        });

        $('#statusFilter').change(function () {
            $.get('/Overview/FilterGatewayStatus', { status: $(this).val() }, function (data) {
                refreshGatewayUnlockList();
            });
        });

    });

    function refreshGatewayUnlockList() {

        $('.ListBorderActive').each(function () {
            var id = this.id.split("_")[1];
            toggleGateway(id);
        });

        var id = <%:Model.AccountID%>;
        var href = '<%:GatewayListType%>'.length == 0 ? '/Retail/GatewayUnlockList/' : '/Retail/GatewayUnlockGpsList/';
        $.get(href, { id: id }, function (data) {
            $('#triggerGateways').html(data);
        });
    }

    function filterName(name) {
        nameTimeout = setTimeout("$.get('/Overview/FilterGatewayName', { name: '" + name + "' }, function(data) { refreshGatewayUnlockList(); }); ", 250);
    }

    function setClass(id, checked) {
        if (checked) {
            $('#gateway_' + id).removeClass('ListBorderNotActive').addClass('ListBorderActive');
        }
        else {
            $('#gateway_' + id).removeClass('ListBorderActive').addClass('ListBorderNotActive');
        }
    }

    function toggleGateway(id) {
        var gatewayID = id;
        var checked = false;

        if ($("#ckb_" + gatewayID).is(':checked')) {
            $("#ckb_" + gatewayID).prop('checked', false);
        }
        else {
            $("#ckb_" + gatewayID).prop('checked', true);
            checked = true;
        }

        if (checked) {

            if ((creditsAvailable > 0) && (creditsAvailable > IDarray.length)) {
                IDarray.push(gatewayID);
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Insufficient Credits")%>");

                if ($("#ckb_" + gatewayID).is(':checked')) {
                    $("#ckb_" + gatewayID).prop('checked', false);
                }
                else {
                    $("#ckb_" + gatewayID).prop('checked', true);
                    checked = true;
                }
                return;
            }
        }
        else {
            const index = IDarray.indexOf(Number(gatewayID));
            if (index > -1) {
                IDarray.splice(index, 1);
            }
        }
        setClass(gatewayID, checked);
    }

    function processCredits() {
        if (IDarray.length > 0) {
            var hrefMethodName = '<%:GatewayListType%>'.length == 0 ? 'ApplyGatewayUnlock' : 'ApplyGatewayUnlockGps';

            $.post('/Retail/' + hrefMethodName + '/<%=Model.AccountID%>', { deviceIDs: IDarray.toString() }, function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    }
</script>

<style>
    .ListBorderNotActive svg circle,
    span.print svg {
        fill: #fff;
    }

    .print svg circle {
        fill: #d9d9d9;
    }

    .ListBorderActive svg circle,
    #deselectGpsSpanIcon .print svg circle {
        fill: #21CE99;
    }
</style>
