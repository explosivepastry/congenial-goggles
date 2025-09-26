<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<style>
    input[type="text"].shortTimer {
        width: 30px;
        margin: 2px 0px 2px 3px;
    }
</style>
<div class=" formBody" style="margin-top: 10px;">
    <div style="border: 1px solid #ccc" class="notiTable">
        <div class="blockSectionTitle">
            <div style="float: left; width: 33%;" class="blockTitle">Notification will be sent to</div>
            <div style="clear: both;"></div>

            <div style="float: left; width: 49%;" class="deviceSearch">
                <div class="searchInput">
                    <input id="deviceFilter" name="deviceFilter" type="text" />
                </div>
            </div>
            <!-- deviceSearch -->

            <div style="font-weight: normal; float: left; width: 49%;">Click the left icon to toggle device activity</div>
            <div style="clear: both;"></div>
        </div>
        <div id="divDeviceList" style="height: 200px; overflow-y: auto; padding: 10px;">
            <!--devicelist-->
        </div>
    </div>

</div>


<script type="text/javascript">
    var deviceFilterTimeout = null;
    $(document).ready(function () {
        loadDeviceRecipients();


        $('#deviceFilter').watermark('Device Search', {

        }).keyup(function () {
            if (deviceFilterTimeout != null)
                clearTimeout(deviceFilterTimeout);
            deviceFilterTimeout = setTimeout("loadDeviceRecipients();", 1000);
        });
    });

    function AddMinutes(e, deviceID, deviceType, field) {
        var minutes = $(e).val();
        var seconds = $(e).siblings('.seconds').val();
        var total = parseInt(minutes * 60) + parseInt(seconds);

        deviceState(deviceID, deviceType, field, total);
    }

    function AddSeconds(e, deviceID, deviceType, field) {
        var seconds = $(e).val();

        var minutes = $(e).siblings('.minutes').val();
        var total = parseInt(minutes * 60) + parseInt(seconds);

        //alert(total);
        deviceState(deviceID, deviceType, field, total);
    }

    /* function AddMinutes2(deviceID, deviceType, field) {
         var minutes2 = ~~$('#minutes2').val();
         var seconds2 = ~~$('#seconds2').val();
         var total = (minutes2 * 60) + seconds2;
 
         
         deviceState(deviceID, deviceType, field, total);
     }
 
     function AddMinutes3(deviceID, deviceType, field) {
         var minutes3 = ~~$('#minutes3').val();
         var seconds3 = ~~$('#seconds3').val();
         var total = (minutes3 * 60) + seconds3;
 
 
         deviceState(deviceID, deviceType, field, total);
     }*/

    function loadDeviceRecipients() {
        $.get("/Notification/RecipientDeviceList/<%:Model.NotificationID %>?q=" + $('#deviceFilter').val(), function (data) {
            $('#divDeviceList').html(data);
        });
    }

    function toggleDevice(deviceID, deviceType, add) {
        var url = "/Notification/ToggleDevice/<%:Model.NotificationID %>";
        var params = "deviceID=" + deviceID;
        params += "&deviceType=" + deviceType;
        params += "&add=" + add;
        $.post(url, params, function (data) {
            if (data == 'Success')
                loadDeviceRecipients();//$('.nrd' + deviceID + '.' + deviceType).toggle();
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }

    function deviceState(deviceID, deviceType, field, state) {
        var url = "/Notification/RecipientDeviceState/<%:Model.NotificationID %>";
        var params = "deviceID=" + deviceID;
        params += "&deviceType=" + deviceType;
        params += "&field=" + field;
        params += "&state=" + state;

        $.post(url, params, function (data) {
            if (data == 'Success')
                loadDeviceRecipients();
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }

</script>
