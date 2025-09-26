<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<%
    long visMapID = Model.VisualMapID;

    // Gateway Types for filter
    IEnumerable<GatewayType> gatewayTypes = ViewBag.GatewayTypes;
    
    // Filter values
    string gatewayNameFilterValue = MonnitSession.GatewayListFilters.Name;
    long gatewayTypeFilterValue = MonnitSession.GatewayListFilters.GatewayTypeID;
    //long gatewayStatusFilterValue = MonnitSession.GatewayListFilters.Status;
%>

<div class="row" id="gatewayFilters" style="padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
    <div class="col-12 col-md-2" style="padding-top: 13px">
        <div>
            <strong><%: Html.TranslateTag("Map/GatewayFilters|Filtered/Total","Filtered/Total")%>: &nbsp;</strong>
            <span class="filteredGatewaysCount"></span>/<span class="totalGatewaysCount"></span>
        </div>
        <div>
            <strong><%: Html.TranslateTag("Map/GatewayFilters|Selected/Filtered","Selected/Filtered")%>: &nbsp;</strong>
            <span class="selectedGatewaysCount"></span>/<span class="filteredGatewaysCount"></span>
        </div>
    </div>

    <div class="col-12 col-md-3">
        <input type="text" id="gatewayNameFilter" class="form-control" style="max-width: 200px;" placeholder="<%: Html.TranslateTag("Map/GatewayFilters|Device Name...","Device Name...")%>" value="<%= string.IsNullOrEmpty(gatewayNameFilterValue) ? "" : gatewayNameFilterValue %>" />
    </div>						
    <div class="col-12 col-md-3">
        <select id="gatewayTypeFilter" class="form-select" style="width: 200px;">
        <option value="-1"><%: Html.TranslateTag("Map/GatewayFilters|All Gateway Types","All Gateway Types")%></option>
        <%foreach (GatewayType gt in gatewayTypes)
            {%>
            <!-- Don't show PoE, LTE, or Mowi-->  
                <%if (gt.GatewayTypeID != 35 && gt.GatewayTypeID != 36 && gt.GatewayTypeID != 11 && gt.GatewayTypeID != 38)
                    { %>
                    <option <%= gatewayTypeFilterValue == gt.GatewayTypeID ? "selected" : "" %> value='<%: gt.GatewayTypeID%>');><%: gt.Name %></option>
                <%} %>
        <%}%>
        </select>
    </div>

<%--    todo:jfk replace w/ loop over vals passed in ViewBag
<div class="col-12 col-md-3">
<select id="gatewayStatusFilter" class="form-select" style="width: 250px;">
<option value="All"><%: Html.TranslateTag("Overview/GatewayGrid|All Statuses","All Statuses")%></option>
<option value='OK'><%: Html.TranslateTag("Overview/GatewayGrid|OK","OK")%></option>
<option value='Warning'><%: Html.TranslateTag("Overview/GatewayGrid|Warning","Warning")%></option>
<option value='Sleeping'><%: Html.TranslateTag("Overview/GatewayGrid|Sleeping","Sleeping")%></option>
</select>
</div>--%>

    <div class="col-12 col-md-1">
        <label>
            <input type="checkbox" id="selectedGatewaysFilter" />
           <%: Html.TranslateTag("Selected","Selected")%>
        </label>    
    </div>
</div>

<div class="col-12" style="padding-top: 13px">            
    <div class="text-center" id="gatewaysAreLoading" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden"><%: Html.TranslateTag("Map/GatewayFilters|Loading...","Loading...")%></span>
        </div>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        // todo:jfk replace keyup() b/c fires a request for every letter types
        // maybe add button and enable Enter key to fire
        // example from AdminSearch.aspx
        //function enterSubmit(e, button) {
        //    if (e.keyCode === 13) { // keycode 13 is the "Enter" key
        //        e.preventDefault(); // Ensure it is only this code that runs
        //        $('#' + button).click();
        //    }
        //}

        $('#gatewayNameFilter').keyup(function () {
            gatewaysLoading();
            $.get('/Overview/FilterGatewayName', { name: $(this).val() }, function (data) {
                refreshGatewayList();
            });
        });

        $('#gatewayTypeFilter').change(function () {
            gatewaysLoading();
            $.get('/Overview/FilterGatewayTypeID', { gatewayTypeID: $(this).val() }, function (data) {
                refreshGatewayList();
            });
        });

        //// todo:jfk
        //$('#gatewayStatusFilter').change(function () {
        //    gatewaysLoading();
        //    $.get('/Overview/FilterGatewayStatus', { status: $(this).val() }, function (data) {
        //        refreshGatewayList();
        //    });
        //});

        $('#selectedGatewaysFilter').click(function () {
            var fgcf = $('#filteredGateways')
                .children()
                .filter(function () {
                    return $(this).find('div.ListBorderNotActive').length > 0
                });
            if ($(this).prop('checked')) {
                fgcf.hide();
            } else {
                fgcf.show();
            }
        });
    });

    function gatewaysLoading() {
        $("#filteredGatewaysPartial").hide();
        $("#gatewaysAreLoading").show();
    }

    function gatewaysLoaded() {
        $("#gatewaysAreLoading").hide();
        $("#filteredGatewaysPartial").show();
    }
    function refreshGatewayList(auto) {
        var id = <%= visMapID %>;
        $.get('/Map/FilteredGateways', { id: id }, function (data) {
            $('#filteredGatewaysPartial').html(data);
            gatewaysLoaded();
        });
    }

</script>

