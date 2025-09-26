<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    string DataMsg = string.Empty;
    Sensor sensor = (Sensor)ViewBag.Sensor;  //ViewBag:  transfers data from controller to view (get values dynamically: retrieving data (Sensor) from ViewBag)
%>
<style>
    /* TODO: Temporary styles, doesn't look like we have styles defined for .btn-active/inactive, fix before releasing */
    .btn a, .btn a i {
        color: white;
    }
    
.Tab-Btns{
        background: #0067AB;
        color: #FFFFFF;   
    }

    .Tab-Btns:hover {
        transition: .2s ease;
        color:#FFFFFF;
        background: #2699FB;
		border-color: #2699FB;
    }

    .btn-inactive {
        background: #6C757D;
        color: white;       
    }
/*
    .btn-inactive:hover{
        transition: .2s ease;
        color: white;
        background: #5B9BD5;
		border-color: #5B9BD5;
    }*/
</style>

<style>
    #reloadEdit {
        display: none;
    }
</style>

<style type="text/css">
    .Green { background-color: green; }
    .Red { background-color: red; }
</style>

<div class="cleafix"></div>
<br />

<!-- Tab Section -->
<div id="deviceInfoTabs" class="row" data-id="<%= Model.SensorID %>">
    <ul class="nav nav-tabs "  role="tablist" style="justify-content: space-evenly; padding: 10px">
        <li class="nav-item" style="margin-left: 10px;">
            <div class="btn-group">
                <button type="button" class="Tab-Btns TabSenHist innerTabToggle btn btn-active" data-id="sensorHistoryTab" data-toggleclass="TabSensHist innerTabToggle" data-togglepane="innerTab.tab-pane">
                    <a class="active" id="sensorHistoryTab-tab" data-toggle="tab" role="tab" aria-controls="sensorHistoryTab" aria-selected="true"><%: Html.TranslateTag("Sensor History","Sensor History")%> <i class="fa fa-history" aria-hidden="true"></i></a>
                </button>
            </div>
        </li>

        <li class="nav-item">
            <div class="btn-group">
                <button type="button" class="Tab-Btns TabSenEdit btn innerTabToggle btn btn-inactive" data-id="sensorEditTab" data-toggleclass="TabSenEdit innerTabToggle" data-togglepane="innerTab.tab-pane">
                    <a class="active" id="sensorEditTab-tab" data-toggle="tab" role="tab" aria-controls="sensorEditTab" aria-selected="false"><%: Html.TranslateTag("Edit Sensor","Edit Sensor")%> <i class="fa fa-pencil-square-o" aria-hidden="true"></i></a>
                </button>
            </div>
        </li>
        
        <%bool isCalibrateVisible = false;

            if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Calibrate", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
            {
                if (!string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
                {
                    if (MonnitSession.CustomerCan("Sensor_Calibrate"))
                    {
                        isCalibrateVisible = true;
                    %>
                        <li class="nav-item">
                            <div class="btn-group">
                                <button type="button" class="Tab-Btns TabSenCalib innerTabToggle btn btn-inactive" data-id="sensorCalibrateTab" data-toggleclass="TabSenCalib innerTabToggle" data-togglepane="innerTab.tab-pane">
                                    <a class="active" id="sensorCalibrateTab-tab" data-toggle="tab" role="tab" aria-controls="sensorCalibrateTab" aria-selected="false"><%: Html.TranslateTag("Calibrate","Calibrate")%> <i class="fa fa-tachometer" aria-hidden="true"></i></a>
                                </button>
                            </div>
                        </li>
                  <%}
                }
            }%>
    </ul>

   <div class="tab-content" >
        <div class="TabSenHist innerTab tab-pane fade show active" id="sensorHistoryTab" role="tabpanel" aria-labelledby="sensorHistoryTab-tab">
        </div>
        <div class="TabSenEdit innerTab tab-pane fade" id="sensorEditTab" role="tabpanel" aria-labelledby="sensorEditTab-tab">
        </div>  
      <%if (isCalibrateVisible)
		{ %>
            <div class="TabSenCalib innerTab tab-pane fade" id="sensorCalibrateTab" role="tabpanel" aria-labelledby="sensorCalibrateTab-tab">
            </div>
      <%} %>
    </div>
</div>

<script type="text/javascript">

    <%= ExtensionMethods.LabelPartialIfDebug("LoadSensorDetails.ascx") %>

    function refreshDeviceStart() {
        $('#deviceInfoTabs .tab-content').hide();
        $('#deviceDetailsLoading').show();
    }

    function refreshDeviceEnd() {
        $('#deviceInfoTabs .tab-content').show();
        $('#deviceDetailsLoading').hide();
    }

    $('.testingHistoryRow').first().attr('data-guid');
    
    function refreshSensorHistory(sensorID) {
            $.get('/Testing/LoadSensorHistory/' + sensorID, function (data) {
            $('#sensorHistoryTab').html(data);
            updateSensorHistory(sensorID);
            });
    }
    
    var updateSensorHistoryTimeout;
    function updateSensorHistory(sensorID) {

        clearTimeout(updateSensorHistoryTimeout);
        var timestamp = $('#testingSensorHistoryTable .testingHistoryRow').first().attr('data-timestamp');

        if ($('#sensorHistoryTab').is('.active.show') && timestamp) {
            $.get('/Testing/UpdateSensorHistory/',
                {
                    id: sensorID,
                    timestamp
                },
                function (data) {
                    $.each(data, (idx, row) => {
                        var r = $(row);
                        $('#testingSensorHistoryTable').prepend(r);
                        flash(r);
                        $('#testingHistoryRecordCount').text(parseInt($('#testingHistoryRecordCount').text()) + 1)
                    });

                    //Upon 'updateSensorHistory()' being called once, will start to auto-update at 5 seconds
                    updateSensorHistoryTimeout = setTimeout(function () {
                        updateSensorHistory(sensorID)
                    }, 5 * 1000);
                }
            );
        }

        function flash(elem) {
            //var elem = $(elem);
            //let bgc = elem.css('background-color');
            //elem.css('background-color', 'rgba(123, 333, 111, 0.2)');
            elem.toggleClass('testingHistoryRowFade');

            setTimeout(() => {
                elem.toggleClass('testingHistoryRowFade');
            }, 1000);
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
                        tr.find('.pendingSvg').toggle(value.isDirty);
                    });
                    refreshRequest = null;
                });
            }
        }
    }
</script>

<script type="text/javascript">

    function refreshSensorEdit(sensorID) {
        refreshDeviceStart();
        $.get('/Testing/LoadSensorEdit/' + sensorID, function (data) {
            $('#sensorEditTab').html(data);
            refreshDeviceEnd();
        });
    }

    function refreshSensorCalibrate(sensorID) {
        refreshDeviceStart();
        $.get('/Testing/LoadSensorCalibrate/' + sensorID, function (data) {
            $('#sensorCalibrateTab').html(data);
            refreshDeviceEnd();
        });
    }

</script>

