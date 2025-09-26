<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%="" %>

<div class="cool-grid-col-2">
    <form id="sendCommandPageForm">

        <div class="choose-task">
            <div class="control-unit_container w-100" >
                <div class="card_container__top ccu-custom">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Command Thermostat")%>
                    </div>
                </div>


                <div class="command-thermo_container">
                    <div class="control-search-header">
                      

                        <div class="container111">
                            <input class="nosubmit form-control user-dets" id="filter" type="search" placeholder="<%: Html.TranslateTag("Rule/SendCommand|Device Name","Device Name")%>">
                        </div>
                    </div>
                    <div class="control-unit_grid" id="deviceList">

                        <%  foreach (var device in Model)
                            {%>
                        <div filter="<%: device.SensorName.ToLower() %>" class="deviceHolder" id="thermostatDevice_<%=device.SensorID %>">
                            <%=Html.Partial("CommandThermostatDetails",device) %>
                        </div>
                        <%} %>
                    </div>

                </div>
                <div class="ccu-button">
                      <button id="go-back" type="button" class=" user-dets btn btn-secondary"  style="cursor: pointer;" title="<%=Html.TranslateTag("Back") %>"><%: Html.TranslateTag("Back")%></button>

                    <button type="button" class=" user-dets btn btn-primary" id="saveThermostatBtn"><%: Html.TranslateTag("Save")%></button>
                </div>
            </div>

        </div>
    </form>
</div>

<script>
    //document.getElementById("go-back").onclick = function () {
    //    window.location.href = "/Rule/ChooseTask/";
    //}


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



    function setThermostatVal(divToCheck, id) {

        if ($('#' + divToCheck + id).prop('checked')) {
            $('#' + divToCheck + id).removeAttr('checked');
            $('#' + divToCheck + id).val(false);
        } else {
            $('#' + divToCheck + id).attr('checked', 'checked');
            $('#' + divToCheck + id).val(true);
        }
    }

    function toggleThermostatDiv(deviceID, doHide) {

        if (doHide) {
            $('#caretClose_' + deviceID).css("display", "none");
            $('#caretOpen_' + deviceID).css("display", "block");
            $('#card_' + deviceID).css("height", "57px");

        } else {
            $('#caretOpen_' + deviceID).css("display", "none");
            $('#caretClose_' + deviceID).css("display", "block");
            $('#card_' + deviceID).css("height", "auto");

            
            $('#card_' + deviceID).css("transition", "height 10s ease-in 2s");
            $('.local-alert_container').css("transition", "height 10s ease-in 2s");

        }
    }



</script>
