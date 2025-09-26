<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%="" %>
<div class="cool-grid-col-2">
    <form id="sendCommandPageForm">

        <div class="choose-task">
            <div class="control-unit_container w-100">
                <div class="card_container__top ccu-custom">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Command Control Unit")%>
                    </div>
                </div>

                <div id="textAreaError" class="form-group" style="margin-top: 1em; font-weight: bold; color: orangered;">
                </div>

                <div class="command-thermo_container " style="border-radius: 20px!important;">
                    <div class="control-search-header">

                        <div class="container111">
                            <input class="nosubmit form-control user-dets" id="filter" type="search" placeholder="<%: Html.TranslateTag("Rule/SendCommand|Device Name","Device Name")%>">
                        </div>
                    </div>
                    <div class="control-unit_grid" id="deviceList">

                        <% 
                            foreach (var device in Model)
                            {%>
                        <div filter="<%: device.SensorName.ToLower() %>" class="deviceHolder" id="controlUnitDevice_<%=device.SensorID %>">
                            <%=Html.Partial("CommandControlUnitDetails",device) %>
                        </div>
                        <%} %>
                    </div>

                </div>
                <div class="ccu-button">
                    <button id="go-back" type="button" class=" user-dets btn btn-secondary" title="<%=Html.TranslateTag("Back") %>"><%: Html.TranslateTag("Back")%></button>
                    <button type="button" class=" user-dets btn btn-primary" id="saveControlUnitTextBtn"><%: Html.TranslateTag("Save")%></button>
                </div>
            </div>

        </div>
    </form>
</div>
<input type="hidden" id="scroller" />



<script type="text/javascript">
    var relayNumber;
    var relayLastOpenHeight = 187;

    $(document).ready(function () {

        $('#filter').keyup(function (e) {
            e.preventDefault();
            $('.deviceHolder').hide();
            let query = $('#filter').val().toLowerCase();
            if (query.length > 0) {
                $('.deviceHolder[filter*=' + query + ']').show();
            } else {
                $('.deviceHolder').show();
            }
        });

    });

    function toggleTimer(deviceID, relayIndex) {
        let checkbox = $('#Indefinite' + relayIndex + '_' + deviceID);

        //Click Checkbox to set check state so the slider will move
        checkbox.click();

        if (!checkbox.prop('checked')) {
            if (relayIndex == 1) {
                //Hide timer show text
                $('#relay1Minute_' + deviceID).parents('div.timerHolder').hide().next().show();

                //Set Values to 0 (Indefinite)
                $('#relay1Minute_' + deviceID).val('0');
                $('#relay1Second_' + deviceID).val('0');
            } else {
                //Hide timer show text
                $('#relay2Minute_' + deviceID).parents('div.timerHolder').hide().next().show();

                //Set Values to 0 (Indefinite)
                $('#relay2Minute_' + deviceID).val('0');
                $('#relay2Second_' + deviceID).val('0');
            }
        }
        else {
            if (relayIndex == 1) {
                //Show timer hide text
                $('#relay1Minute_' + deviceID).parents('div.timerHolder').show().next().hide();
            } else {
                //Show timer hide text
                $('#relay2Minute_' + deviceID).parents('div.timerHolder').show().next().hide();
            }
        }
    }

    function toggleControlUnitRelayState(divID) {
        var obj = $('#' + divID);

        if (obj.prop('checked')) {
            obj.prop('checked', false);
            obj.val(false);
        } else {
            obj.prop('checked', true);
            obj.val(true);
        }
    }

    function toggleRelayDiv(deviceID, relayNumber, doHide) {

        if (doHide) {
            $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "none");
            $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "block");
            $('#editRelay' + relayNumber + '_' + deviceID).css("height", "57px");
        } else {
            $('#caretOpen_' + deviceID + '_' + relayNumber).css("display", "none");
            $('#caretClose_' + deviceID + '_' + relayNumber).css("display", "block");
            $('#editRelay' + relayNumber + '_' + deviceID).css("height", "auto");

        }
    }


    function showDelay(sensID, relayNum) {
        relayNumber = Number(relayNum);
        sensorID = Number(sensID);
    }



</script>
