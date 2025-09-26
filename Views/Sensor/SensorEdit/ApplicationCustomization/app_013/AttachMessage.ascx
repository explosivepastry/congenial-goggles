<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group">

    <div class="clearfix"></div>
</div>
<div class="d-flex card_container__body hasScroll-sm" id="sensorList" style="min-height: 300px; flex-wrap: wrap;">
</div>




<script>
    var sensorFilterTimeout = null;
    $(document).ready(function () {

        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });

        loadSensorList();

        $('#userFilter').keyup(function () {
            if (sensorFilterTimeout != null)
                clearTimeout(sensorFilterTimeout);
            sensorFilterTimeout = setTimeout("loadSensorList();", 1000);
        });
    });

    function loadSensorList() {

        $.get("/Overview/SensorList/<%:Model.AccountID %>?NotiID=" + '<%: Model.SensorID%>' + "&q=" + $('#userFilter').val(), function (data) {
            $('#sensorList').html(data);
        });
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
        if ($('#sensor_' + id).hasClass('ListBorderActive')) {
            checked = true;
        }
        var add = !checked;
        var params = 'id=' + sensorId;

        params += "&notiferID=" +  <%: Model.SensorID%>;
        params += "&add=" + add;
        $.post("/Overview/ToggleSensor", params, function (data) {
            if (data == 'Success') {
                loadSensorList();
            }
            else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }


</script>
