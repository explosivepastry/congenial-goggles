<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Sensor>>" %>

<style type="text/css">
    .testingSensorCard {
        display: grid;
        grid-template-columns: 20px repeat(3, 1fr) 15px;
        width: 100%;
        box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 18%);
        background: var(--card-background-color);
        border-radius: 0.5rem;
        margin: .5rem;
        padding: .3rem;
        cursor: pointer;
        box-shadow: rgba(0, 0, 0, 0.16) 0px 3px 6px, rgba(0, 0, 0, 0.23) 0px 3px 6px;
        gap: 1%;
        border: solid 2px transparent;
        transition: border-color 0.3s ease-in-out;
    }

    .selected-card-border {
        border-color: #3f97f6;
    }

    .testingSensorCard .testingSensorRow:last-child {
        margin: 0;
        overflow: visible;
    }

    .testingSensorRow {
        position: relative;
        display: flex;
        justify-content: space-around;
        overflow: hidden;
        flex-direction: column;
    }

        .testingSensorRow > div {
            display: flex;
            align-items: center;
            text-align: center;
        }

            .testingSensorRow > div > span.testingSensorText {
                max-width: 12rem;
                overflow: hidden;
                white-space: nowrap;
                text-overflow: ellipsis;
                display: block;
                color: #515356;
                font-weight: bold;
            }

    .solidLED-test {
        display: block;
        background: black;
        border-radius: 50%;
        height: 18px;
        width: 18px;
        margin: .5px;
        box-shadow: 0px 5px 5px 0px rgb(0 0 0 / 30%);
        cursor: pointer;
    }

    .sensorStatus-sidebar {
        width: 100% !important;
        height: 100% !important;
        border-radius: 5px 0 0 5px;
    }

    .icon-data > svg {
        fill: #515356;
    }

    .sensorid {
        font-size: 1rem;
        font-weight: bold;
        justify-content: center;
    }

        .sensorid > span {
            padding: 2px 5px;
            width: fit-content;
        }

    .menuIcon > svg {
        height: 20px !important;
        width: 20px !important;
        margin-right: 15px;
    }

    #wifiOffTest {
        width: 30px;
        height: 30px;
        fill: red;
    }
</style>

<%="" %>

<%    
    if (Model != null && Model.Count() > 0)
    {
        foreach (var item in Model)
        {
            DataMessage message = item.LastDataMessage;
%>

<%--CARD--%>

<div class="testingDeviceCard testingSensorCard testingSensor viewSensorDetails<%= item.SensorID %>" data-id="<%:item.SensorID %>" data-sensorid="<%= item.SensorID %>">
    <%-- --------------------------------
                        Col 1  COLOR STATUS  
       ------------------------------------ --%>

    <div class="sensorStatusHolder">
        <%= iMonnit.Controllers.AutoRefreshController.SensorStatusTestString(item) %>
    </div>

    <%-- ----------------------------------
                        Col 2  ICON/ Messages
       ----------------------------------- ------%>

    <div class="testingSensorRow">
        <div class="d-flex">
            <div title="<%=item.ApplicationID %>" class="icon-data-container">
                <div class="icon-data" style="width: 30px; height: 30px; margin-top: 0px; margin-left: 10px;">
                    <%=Html.GetThemedSVG("app" + item.ApplicationID) %>
                </div>
            </div>
            <div style="margin-right: 5px;" class="sensorId">
                <span title="<%=item.Name %>">
                    <%=item.SensorID%>
                </span>
            </div>
            || 
            <div style="margin-left: 5px; margin-right: 3px;" class="firmwareVersion" title="Firmware Version">
                <span><%: item.FirmwareVersion %></span>
            </div>
            <% if (item.IsCableEnabled)
                {%>
            ||
            <div style="margin-left: 3px;" class="cableID" title="Cable ID">
                <span><%: item.CableID %></span>
            </div>
            <%} %>
        </div>
        <div class="lastMsgDate" style="padding: 3px 0px; line-height: initial;">
            <span style="color: #0026ff;">
                <%: ExtensionMethods.OVTestingElapsedLastMessageString(item.LastCommunicationDate) %> 
            </span>
        </div>
        <div class="glance-reading" title="<%: iMonnit.Controllers.AutoRefreshController.SensorReadingTitleTestString(item) %>">
            <span class="testingSensorText" style="max-width: 25rem; font-weight: bold;">
                <%: iMonnit.Controllers.AutoRefreshController.SensorReadingTestString(item) %>
            </span>
        </div>
    </div>
    <%-- -----   End Col 2 --%>


    <%-- -------------------------
                        Col 3
       ----------------------- --%>

    <div class="testingSensorRow" style="margin-left: 20px;">
        <div class="applicationType" title="Application Type: <%: item.ApplicationName %>">
            <span class="testingSensorText">
                <%: item.ApplicationName %>
            </span>
        </div>
        <div style="" class="radioband" title="Radio Band">
            <%= iMonnit.Controllers.AutoRefreshController.SensorRadioBandTestString(item) %>
        </div>
    </div>

    <%-- -------------------------
                        Col 4
       ----------------------- --%>

    <div class="testingSensorRow">
        <div style="" class="sensorDots" title="Radio Band">
            <a style="display: flex;" class="updsinglesensdots" data-id="<%:item.SensorID%>" title="<%: Html.TranslateTag("Testing/TestingSensorList|Click to reset sensor to default configuration","Click to reset sensor to default configuration")%>">
                <div class="GeneralConfig2Dirty solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.GeneralConfig2Dirty ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0)!important;" %>"></div>
                <div class="GeneralConfig3Dirty solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.GeneralConfig3Dirty ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0);" %>"></div>
                <div class="ProfileConfigDirty solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.ProfileConfigDirty ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0);" %>"></div>
                <div class="ProfileConfig2Dirty solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.ProfileConfig2Dirty ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0);" %>"></div>
                <div class="GeneralConfigDirty solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.GeneralConfigDirty ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0);" %>"></div>
                <div class="PendingActionControlCommand solidLED-test <%:item.IsDirty ? "true" : "false" %>" style="background: <%=item.PendingActionControlCommand ? "radial-gradient(circle at 11px 4px, red, #000000a0);":"radial-gradient(circle at 11px 4px, #11AD3D, #000000a0);" %>"></div>
            </a>
        </div>
        <div class="signalStrength" title="Signal Strength">
            <%= iMonnit.Controllers.AutoRefreshController.SensorSignalStrengthTestString(item) %>
        </div>
        <div style="" class="powerSource" title="Power Source">
            <%= iMonnit.Controllers.AutoRefreshController.SensorPowerTestString(item) %>
        </div>
    </div>

    <%--   END--%>
    <div class="d-flex" style="align-items: center; justify-content: center;">
        <div class="menuIcon" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
            <%=Html.GetThemedSVG("menu") %>
        </div>
        <ul class="dropdown-menu ddm" style="padding: 0;">
            <li>
                <a class="dropdown-item menu_dropdown_item" onclick="ResetSingleSensorForShipping('<%=item.SensorID %>'); return false;" data-id="<%:item.SensorID%>" title="Reset Sensor For Shipping Sensor ID: <%=item.SensorID%>">
                    <span><%: Html.TranslateTag("Reset","Reset")%></span>
                    <%=Html.GetThemedSVG("reset") %>
                </a>
            </li>
            <li>
                <a class="dropdown-item menu_dropdown_item" target="_blank" href="/Overview/SensorEdit/<%=item.SensorID%>" title="Settings: <%=item.SensorID %>">
                    <span><%: Html.TranslateTag("Settings","Settings")%></span>
                    <%=Html.GetThemedSVG("settings") %>
                </a>
            </li>
            <li class="firmwareUpdateMenuItem" style="display: none">
                <a class="dropdown-item menu_dropdown_item" onclick="SingleSensorFirmwareUpdate('<%=item.SensorID %>'); return false;" data-id="<%:item.SensorID%>" title="Update Firmware for Sensor ID: <%=item.SensorID%>">
                    <span><%: Html.TranslateTag("Firmware Update","Firmware Update")%>&nbsp;&nbsp;</span>
                    <span class="fa fa-wrench"></span>
                </a>
            </li>
            <li>
                <hr class="my-0" />
                <a class="dropdown-item menu_dropdown_item" id="list" onclick="removeSensor('<%=item.SensorID %>'); return false;" title="Delete: <%=item.SensorID %>">
                    <span><%: Html.TranslateTag("Remove","Remove")%> </span>
                    <%=Html.GetThemedSVG("delete") %>
                </a>
            </li>
        </ul>
    </div>
</div>

<% 
    } // foreach datamessage
%>
<% 
    }
    else
    { // if (Model != null
%>

<div class="alert"><%= Html.TranslateTag("No Sensors on Network")%></div>

<% 
    }
%>

<%--jQuery/JavaScript Scripts--%>

<script>
     <%= ExtensionMethods.LabelPartialIfDebug("TestingSensorList.ascx") %>

    $(document).ready(() => {
        updateSensorList(); // start the timed auto-update process

        var updateableSensors = "<%= ViewBag.UpdateableSensorIds %>";
        $('.testingSensorCard').each(function () {
            if (updateableSensors.includes($(this).data('id'))) {
                $(this).find('.firmwareUpdateMenuItem').show();
            }
        });
    })

    var updateSensorListTimeout;
    function updateSensorList() {

        clearTimeout(updateSensorListTimeout); // ensure we don't have more than 1 refresh loop running

        if ($('#sensorsList').is('.active.show')) { // only refresh when sensorList is active/visible
            var callback = sensorUpdateCallback();
            var networkID = $('#CurrentNetwork').val();
            $.get('/AutoRefresh/AtATestingSensorRefresh/' + networkID, callback);
        }

        // Upon 'updateSensorList()' being called once, will start to auto-refresh at 5 seconds
        updateSensorListTimeout = setTimeout(function () {
            updateSensorList()
        }, 5 * 1000);
    }

    function randomIntFromInterval() {
        return Math.floor(Math.random() * (100000 - 1 + 1) + 1)
    }

    var sensorUpdateCallbackID = 0;

    function sensorUpdateCallback() {
        /*var id = randomIntFromInterval();*/
        var id = ++sensorUpdateCallbackID;
        var startTime = Date.now();
        return function (data) {
            var endTime = Date.now();
<%      
    if (System.Diagnostics.Debugger.IsAttached)
    {
%>
            //console.log(`${id}\t${startTime}\t${endTime}\t${convertMiliseconds(endTime - startTime)}`);
<%      
    }
%>
            $.each(data, function (index, value) {
                var tr = $('.viewSensorDetails' + value.SensorID);
                tr.find('.sensorStatusHolder').html(value.Status);
                tr.find('.lastMsgDate span').text(value.DisplayDate);
                tr.find('.firmwareVersion span').text(value.FirmwareVersion);
                tr.find('.signalStrength').html(value.SignalStrength);
                tr.find('.glance-reading span').text(value.Reading);
                tr.find('.glance-reading').prop('title', value.ReadingTitle);
                tr.find('.powerSource').html(value.BatteryLevel);
                tr.find('.GeneralConfig2Dirty').css('background', value.GeneralConfig2DirtyColor);
                tr.find('.GeneralConfig3Dirty').css('background', value.GeneralConfig3DirtyColor);
                tr.find('.ProfileConfigDirty').css('background', value.ProfileConfigDirtyColor);
                tr.find('.ProfileConfig2Dirty').css('background', value.ProfileConfig2DirtyColor);
                tr.find('.GeneralConfigDirty').css('background', value.GeneralConfigDirtyColor);
                tr.find('.PendingActionControlCommand').css('background', value.PendingActionControlCommandColor);
            });
        }
    }

    function convertMiliseconds(miliseconds) {
        let days, hours, minutes, seconds, milliseconds, total_hours, total_minutes, total_seconds;

        total_seconds = parseInt(Math.floor(miliseconds / 1000));
        total_minutes = parseInt(Math.floor(total_seconds / 60));
        total_hours = parseInt(Math.floor(total_minutes / 60));
        days = parseInt(Math.floor(total_hours / 24));

        milliseconds = parseInt(miliseconds % 1000);
        seconds = parseInt(total_seconds % 60);
        minutes = parseInt(total_minutes % 60);
        hours = parseInt(total_hours % 24);

        let res = { d: days, h: hours, m: minutes, s: seconds, i: milliseconds };
        res.toString = function () {
            let ds = this.d > 0 ? `${this.d} d ` : '';
            let hs = this.h > 0 ? `${this.h} h ` : '';
            let ms = this.m > 0 ? `${this.m} m ` : '';
            let ss = this.s > 0 ? `${this.s} s ` : '';
            let is = this.i > 0 ? `${this.i} ms` : '';

            return ds + hs + ms + ss + is;
        }
        return res;
    };


    $('.updsinglesensdots').click(function () {
        let sensorID = $(this).data("id");
        $.post("/Testing/ResetSingleSensorForShipping/", { "id": sensorID },
            function (data) {
                if (data == "Success") {
                    refreshSensorList();
                }
                else {
                    console.log(data);
                    showAlertModal(data);
                }
            });
    });

    function refreshSensorEdit(sensorID) {
        refreshDeviceStart();
        $.get('/Testing/LoadSensorEdit/' + sensorID, function (data) {
            $('#sensorEditTab').html(data);
            refreshDeviceEnd();
        });
    }

    function sMoveSensor() {
        var snsensID = $("#sAddSensorID").val();
        if (snsensID.length == 0) {
            showAlertModal("SensorID is Required");
            $('#sAddSensorID').focus();
            return;
        }

        var networkID = $("#CurrentNetwork").val();
        $.post('/Testing/MoveSensor/' + networkID, { sensorID: snsensID }, function (data) {
            if (data == 'Success') {
                $('#sAddSensorID').val('');
            } else {
                //alert("Failed");
                console.log(data);
                showAlertModal(data);
            }
        })
            .then(function () {
                return SetShowFullDataValue(snsensID, true);
            }
            )
            .then(function () {
                refreshSensorList();
                $('#sAddSensorID').focus();
            });
    }

    function removeSensor(item) {
        let values = {};
        values.url = `/Testing/RemoveSensor/${item}`;
        values.text = 'Are you sure you want to remove this sensor from the network ?';
        values.callback = function () {
            SetShowFullDataValue(item, false)
                .then(function () {
                    refreshSensorList();
                    $('#sAddSensorID').focus();
                });
        }
        openConfirm(values);
    }

    function SetShowFullDataValue(sensorID, showFullData) {
        return $.post('/Sensor/SetShowFullDataValue', { sensorID, showFullData }, function (result) {
            console.log(result);
        });
    }

    var popLocation = 'center';

    function referencePageRedirect() {
        $(this).text("Redirecting....");
        /*var delay = 6000;*/
        setTimeout(function () {
            window.open('https://www.monnit.com/Process/Processes/', '_blank'
            );
        });
    }

    <%--=== Current Nework ===--%>
    $('#CurrentNetwork').change(function () {
        window.location.href = '/Testing/Index/' + $('#CurrentNetwork').val();
    });

    /*=== Reset Single Sensor for Shipping ===*/
    function ResetSingleSensorForShipping(id) {
        if (confirm('Are you sure you want to reset this sensor for shipping in this network?')) {
            $.post('/Testing/ResetSingleSensorForShipping/', { "id": id }, function (data) {
                if (data == "Success") {
                    refreshSensorList();
                }
                else {
                    console.log(data);
                    showAlertModal(data);
                }
                document.getElementById('sAddSensorID').focus();
                document.getElementById('sAddSensorTitle').style.color = 'green';
                document.getElementById('sAddSensorTitle').enabled = true;
            });
        }
    }

    function SingleSensorFirmwareUpdate(sensorID) {
        $.post("/Testing/CreateOTARequest/<%= MonnitSession.CurrentCustomer.AccountID %>",
            { sensorIDs: sensorID },
            function (resultMsg) {
                console.log(resultMsg);
            });
    }

    // Highlights the selected card. Toggleborder is located in the parent file: /Testing/Index.aspx
    var cardsToHighlight = Array.from(document.querySelectorAll(".testingSensorCard"));

    cardsToHighlight.forEach(card => {
        card.addEventListener("click", () => toggleBorder(card));
    });

</script>

