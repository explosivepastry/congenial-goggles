<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<% 
    long visMapID = Model.VisualMapID;

    // Sensor Types for filter
    List<ApplicationTypeShort> sensorTypes = ViewBag.SensorTypes;

    // Networks for filter
    List<CSNet> networks = ViewBag.Networks;

    // todo:jfk
    // Sensor Statuses for filter
    //Dictionary<string, int> sensorStatuses = ViewBag.SensorStatuses;

    // Filter values
    string sensorNameFilterValue = MonnitSession.SensorListFilters.Name;
    //int sensorStatusFilterValue = MonnitSession.SensorListFilters.Status;
    long sensorTypeFilterValue = MonnitSession.SensorListFilters.ApplicationID;
    long sensorNetworkFilterValue = MonnitSession.SensorListFilters.CSNetID;
%>

<div class="row" id="sensorFilters" style="padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;"> 
    <div class="col-12 col-md-2" style="padding-top: 13px">
        <div>
            <strong><%: Html.TranslateTag("Filtered/Total","Filtered/Total")%>: &nbsp;</strong>
            <span class="filteredSensorsCount"></span>/<span class="totalSensorsCount"></span>
        </div>
        <div>
            <strong><%: Html.TranslateTag("Selected/Filtered","Selected/Filtered")%>: &nbsp;</strong>
            <span class="selectedSensorsCount"></span>/<span class="filteredSensorsCount"></span>
        </div>
    </div>
    <div class="col-12 col-md-3">
        <input type="text" id="sensorNameFilter" class="form-control" style="width: 200px;" placeholder="Device Name..." value="<%= string.IsNullOrEmpty(sensorNameFilterValue) ? "" : sensorNameFilterValue %>" />
    </div>
    <div class="col-12 col-md-3">
        <select id="sensorTypeFilter" class="form-select" style="width: 200px;">
            <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Types","All Sensor Types")%></option>
        <%foreach (ApplicationTypeShort App in sensorTypes)
            {%>
            <option <%= sensorTypeFilterValue == App.ApplicationID ? "selected" : "" %> value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
            <%}%>
        </select>
    </div>
    <div class="col-12 col-md-3">
        <select id="networkFilter" class="form-select" style="width: 200px;">
            <option value="-1"><%: Html.TranslateTag("Overview/Index|All Networks","All Networks")%></option>
            <%foreach (CSNet Ntwk in networks)
                {%>
            <option <%= sensorNetworkFilterValue == Ntwk.CSNetID ? "selected" : "" %> value='<%= Ntwk.CSNetID %>'><%= Ntwk.Name %></option>
               
            <%}%>
        </select>
    </div>
<%--    todo:jfk
    <div class="col-12 col-md-2">
        <select style="width: 250px;" id="statusFilter" class="form-select ">
            <option value="All"><%: Html.TranslateTag("Overview/Index|All Statuses", "All Statuses")%></option>
            <%foreach ( KeyValuePair<string, int> Status in sensorStatuses)
                {%>
            <option <%= sensorStatusFilterValue == Status.Value ? "selected" : "" %> value='<%: Status.Value %>'><%=Status.Key %></option>
            <%}%>
        </select>
    </div>--%>
    <div class="col-12 col-md-1">
        <label>
            <input type="checkbox" id="selectedSensorsFilter" />
            Selected
        </label>    
    </div>
</div>
<div class="col-12" style="padding-top: 13px">            
    <div class="text-center" id="sensorsAreLoading" style="display: none;">
        <div class="spinner-border text-primary" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    </div>
</div>

<script type="text/javascript">

    var searchTimeout = null;
    var searchRequest = null;

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
        $('#sensorNameFilter').keyup(function () {
            sensorsLoading();
            if (searchTimeout != null)
                clearTimeout(searchTimeout);
            if (searchRequest != null) {
                searchRequest.abort();
                searchRequest = null;
            }
            searchTimeout = setTimeout("sensorNameSearch(" + $(this).val() + ")", 1000);            
        });

        $('#sensorTypeFilter').change(function () {
            sensorsLoading();
            $.get('/Overview/FilterAppID', { appID: $(this).val() }, function (data) {
                refreshSensorList();
            });
        });

        //// todo:jfk
        //$('#sensorStatusFilter').change(function () {
        //    sensorsLoading();
        //    $.get('/Overview/FilterStatusID', { sensorStatusID: $(this).val() }, function (data) {
        //        refreshSensorList();
        //    });
        //});

        $('#networkFilter').change(function () {
            sensorsLoading();
            $.get('/Overview/FilterCSNetID', { csnetID: $(this).val() }, function (data) {
                refreshSensorList();
            });
        });

        $('#selectedSensorsFilter').click(function () {
            var fscf = $('#filteredSensors')
                .children()
                .filter(function () {
                    return $(this).find('div.ListBorderNotActive').length > 0
            });
            if ($(this).prop('checked')) {
                fscf.hide();
            } else {
                fscf.show();
            }
        });
    });

    function sensorNameSearch(text) {
        $.get('/Sensor/FilterName', { name: text }, function (data) {
            refreshSensorList();
        });
    }

    function sensorsLoading() {
        $("#filteredSensorsPartial").hide();
        $("#sensorsAreLoading").show();
    }

    function sensorsLoaded() {
        $("#sensorsAreLoading").hide();
        $("#filteredSensorsPartial").show();
    }

    function refreshSensorList(auto) {
        var id = <%= visMapID %>;
        $.get('/Map/FilteredSensors', { id: id }, function (data) {
            $('#filteredSensorsPartial').html(data);
            sensorsLoaded();
        });
    }

</script>
