<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.CreditSetting>" %>

<%

    List<Sensor> sensors = Sensor.LoadByAccountID(Model.AccountID).Where(s => new Version(s.FirmwareVersion) >= new Version("25.45.0.0")).OrderBy(m => m.SensorName).ToList();
    int sensorTotal = sensors.Count();
%>

<div class="col-12 ">

    <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px; display: flex; flex-direction: column; justify-content: flex-start; align-items: start;">
        <div class="clearfix"></div>
        <div style="padding: 5px; font-size: 13px; font-weight: bold; display: flex; flex-direction: column;">
            <span style="display: flex; justify-content: flex-start; align-items: center;">
                <%: Html.TranslateTag("Events/TriggersSensors|Click","Click")%>

                <span class="finger-print fp-inactive">
                    <%=Html.GetThemedSVG("finger-print") %>
                </span>
                <%: Html.TranslateTag("Events/TriggersSensors|to activate","to activate")%>
            </span>

            <span class="finger-print fp-active">
                <%: Html.TranslateTag("Events/TriggersSensors|Click","Click")%>
                <%=Html.GetThemedSVG("finger-print") %>

                <%: Html.TranslateTag("Events/TriggersSensors|to deactivate","to deactivate")%>
            </span>

            <span style="display: flex; justify-content: flex-start; align-items: center;">
                <%: Html.TranslateTag("Events/TriggersSensors|Once saved, your print is locked in and you may click","Once saved, your print is locked in and you may click")%>
                <span class="fp-copy fp-check">
                    <%=Html.GetThemedSVG("printCheck") %>
                </span>
                <%: Html.TranslateTag("Events/TriggersSensors|to copy SensorPrint Code","to copy SensorPrint Code")%>.
            </span>
        </div>
    </div>

    <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
        <font color="gray">
            <%: Html.TranslateTag("Events/TriggersSensors|Click Sensor to enable/disable","Click Sensor to enable/disable")%>
        </font>
        <div class="btn-group">
            <a href="#" class="me-2" onclick="$('#settings').toggle(); return false;">
                <%=Html.GetThemedSVG("filter") %>
            </a>
            <a href="/" onclick="refreshSensorList(); return false;">
                <%=Html.GetThemedSVG("refresh") %>
            </a>
        </div>
    </div>

    <div style="margin: -5px 5px;">
        <div class="col-12 scrollContainer bsInset">
            <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
                <div class="col-12 col-md-2" style="padding-top: 13px">
                    <strong><%: Html.TranslateTag("Filter","Filter")%>: &nbsp;</strong>
                    <span id="filterdSensors"></span>/<span id="totalSensors"><%=sensorTotal %></span>
                </div>
                <div class="col-12 col-md-3">
                    <input type="text" id="NameFilter" class="form-control" placeholder="Device Name..." style="width: 250px;" />
                </div>
                <div class="col-12 col-md-3">
                    <select id="applicationFilter" class="form-select" style="width: 250px;">
                        <option value="-1"><%: Html.TranslateTag("Overview/Index|All Sensor Profiles","All Sensor Profiles")%></option>
                        <%foreach (ApplicationTypeShort App in ApplicationTypeShort.LoadAllByAccountID(MonnitSession.CurrentCustomer.AccountID))
                            {%>
                        <option value='<%: App.ApplicationID%>'><%:App.ApplicationName %></option>
                        <%}%>
                    </select>
                </div>

            </div>
            <div id="triggerSensors" class="sensorList_main scrollx-child mh300 ">
                <%=Html.Partial("_SensorListDetails",sensors) %>
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

<script type="text/javascript">
    <% if (MonnitSession.SensorListFilters.CSNetID > long.MinValue ||
MonnitSession.SensorListFilters.ApplicationID > long.MinValue ||
MonnitSession.SensorListFilters.Status > int.MinValue ||
MonnitSession.SensorListFilters.Name != "")
        Response.Write("$('#filterApplied').show();");
    else
        Response.Write("$('#filterApplied').hide();");
    %>

    var IDarray = [];
    var creditsAvailable = Number(<%=Model.CreditsAvailable%>);

    $(document).ready(function () {


        $('#NameFilter').keyup(function () {
            filterName($(this).val());
        });

        $('#applicationFilter').change(function () {

            $.get('/Overview/FilterAppID', { appID: $(this).val() }, function (data) {
                refreshSensorList();
            });
        });

    });

    function refreshSensorList() {
        var id = <%:Model.AccountID%>;
        $.get('/Retail/SensorPrintSensorList', { id: id }, function (data) {
            $('#triggerSensors').html(data);
        });
    }

    function filterName(name) {

        nameTimeout = setTimeout("$.get('/Overview/FilterName', { name: '" + name + "' }, function(data) { refreshSensorList(); }); ", 250);
    }

    function setClass(id, checked) {
        if (checked) {
            $('#sensor_' + id).removeClass('ListBorderNotActive').addClass('ListBorderActive');
        }
        else {
            $('#sensor_' + id).removeClass('ListBorderActive').addClass('ListBorderNotActive');
        }
    }

    function toggleSensor(id) {
        var sensorId = id;
        var checked = false;

        if ($("#ckb_" + sensorId).is(':checked')) {
            $("#ckb_" + sensorId).prop('checked', false);
        }
        else {
            $("#ckb_" + sensorId).prop('checked', true);
            checked = true;
        }

        if (checked) {

            if ((creditsAvailable > 0) && (creditsAvailable > IDarray.length)) {
                IDarray.push(sensorId);
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Insufficient Credits")%>");

                if ($("#ckb_" + sensorId).is(':checked')) {
                    $("#ckb_" + sensorId).prop('checked', false);
                }
                else {
                    $("#ckb_" + sensorId).prop('checked', true);
                    checked = true;
                }
                return;
            }
        }
        else {
            const index = IDarray.indexOf(sensorId);
            if (index > -1) {
                IDarray.splice(index, 1);
            }
        }
        setClass(sensorId, checked);
    }

    function processCredits() {

        if (IDarray.length > 0) {

            $.post('/Retail/ApplySensorPrint/<%=Model.AccountID%>', { ids: IDarray.toString() }, function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    }

    function copyToClipboard(valuetoCopy) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val(valuetoCopy).select();
        document.execCommand("copy");
        $temp.remove();

        showSimpleMessageModal("SensorPrint <%=Html.TranslateTag("copied to clipboard")%>");
    }


</script>

<style>
    .print svg {
        fill: #fff;
    }

    #svg_printCheck {
        fill: #22AE73;
    }
</style>
