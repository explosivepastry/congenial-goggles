<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>
<%List<VisualMapSensor> vms = VisualMapSensor.LoadByVisualMapID(Model.VisualMapID); %>
<%List<Sensor> sensors = ViewData["SensorList"] as List<Monnit.Sensor>; %>
<%foreach (var item in sensors)
    {
        bool isChecked = false;
        string check = "";
        int gridCountLimit = 10;
        int maxLength = 44;
        
            if (item.SensorName.Length > 44)
                item.SensorName = item.SensorName.Substring(0, 44) + "...";
        %>
<%string icon = "app" + item.ApplicationID; %>
<a class="gridPanel  eventsList2 rounded" style="cursor: pointer; box-shadow: none;" onclick="toggleMapSensor(<%:item.SensorID%>, <%:item.ApplicationID%>, `<%:Html.GetThemedSVG("app" + item.ApplicationID).ToString()%>`)">

    <!-- GridView for more than 10 list items -->
    <%foreach (VisualMapSensor s in vms)
        {
            if (s.SensorID == item.SensorID)
            {
                isChecked = true;
                check = "checked";
                break;
            }
        }
    %>
    <div class="triggerDevice__container" style="justify-content: space-between;">
        <div class="triggerDevice__icon">
            <div style="font-size: .6em !important;"><span class="sensor icon icon-<%:icon%> icon-xs"></span></div>
        </div>
        <div class="triggerDevice__name" style="width: 200px;">
            <label class="text-wrap" style="font-weight: normal !important; font-size: small !important;  padding: 7px;"><%=item.SensorName%></label>
        </div>
        <div class="gridPanel triggerDevice__status <%:isChecked?"ListBorderActive":"ListBorderNotActive"%>" id="sensor_<%:item.SensorID%>" title="<%:item.SensorName%>">
            <svg xmlns="http://www.w3.org/2000/svg" width="25.806" height="19.244" viewBox="0 0 25.806 19.244">
                <path id="check-circle-solid" d="M25.687,38.59,40.525,23.751a1.29,1.29,0,0,0,0-1.825L38.7,20.1a1.29,1.29,0,0,0-1.825,0l-12.1,12.1-5.65-5.65a1.29,1.29,0,0,0-1.825,0l-1.825,1.825a1.29,1.29,0,0,0,0,1.825l8.387,8.387a1.29,1.29,0,0,0,1.825,0Z" transform="translate(-15.097 -19.724)" fill="#fff" />
            </svg>
        </div>
        <div class="hidden-lg hidden-md hidden-sm hidden-xs">
            <input type="checkbox" id="ckb_<%:item.SensorID%>" data-checkbox="<%:item.SensorID%>" data-appid="<%:icon%>" class="checkbox checkbox-info" <%=check %> />
        </div>
    </div>
</a>
<%} %>
<script>
    $('.checkbox').hide();
    function setClass(id, checked) {
        if (checked) {
            $('#sensor_' + id).removeClass('ListBorderNotActive').addClass('ListBorderActive');
        }
        else {
            $('#sensor_' + id).removeClass('ListBorderActive').addClass('ListBorderNotActive');
        }
    }
    function toggleMapSensor(id, appId, image) {
        var sensorId = id;
        var applicationId = 'app' + appId;
        //var image = '/Content/images/Wit/' + applicationId + '.png';
        var checked = false;
        if ($("#ckb_" + sensorId).is(':checked')) {
            $("#ckb_" + sensorId).prop('checked', false);
        }
        else {
            $("#ckb_" + sensorId).prop('checked', true);
            checked = true;
        }

        if (checked) {
            startPlacing(sensorId, image);
        }
        else {
            remove(sensorId);
        }
        setClass(sensorId, checked);
    }
</script>
