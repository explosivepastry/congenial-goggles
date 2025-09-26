<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%SensorGroup group = Session["chartSensorList"] as SensorGroup;
    if (group == null)
    {
        group = new SensorGroup();
        group.Type = "chart";
        group.AccountID = MonnitSession.CurrentCustomer.AccountID;
    }
%>
<%List<SensorDatumModel> sensors = ViewData["SensorList"] as List<SensorDatumModel>; %>

<%foreach (var item in sensors)
    {
        bool isChecked = false;
        string check = "";
        int gridCountLimit = 10;

        string datumName = item.datumStruct.customname + ": " + item.datumStruct.name;
%>

<%string icon = "app" + item.Sensor.ApplicationID; %>

<a class="card-w-data"  onclick="toggleChartSensor(<%:item.Sensor.SensorID%>, <%:item.datumStruct.datumindex%>,'<%= HttpUtility.UrlEncode(datumName) %>')">

    <!-- Do not remove the label tag. It is used to click on the text or icon to check the check box.-->
    <%foreach (SensorGroupSensor s in group.Sensors)
        {
            if (s != null && s.SensorID == item.Sensor.SensorID && s.DatumIndex == item.datumStruct.datumindex)
            {
                isChecked = true;
                check = "checked";
                break;
            }
        }
    %>
    <div  class="d-flex" title="<%=item.Sensor.SensorName%>">
        <div class="col-2 icon-color">
            <div style="width:35px; height: 30px; margin: 5px 0 13px 5px;"><%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %></div>
        
            <!--<div style="font-size: .6em !important;"><span class="sensor icon icon-<%:icon%> icon-xs"></span></div>-->
        </div>
        <div class="card-edit-details">
            <label style=" padding: 7px;display:flex; flex-wrap:wrap;">
                <%=item.Sensor.SensorName%>
                <br />
                <strong><%=item.datumStruct.customname %>: </strong>&nbsp;<%=item.datumStruct.name %></label>
   
        <div id="sensor_<%:item.Sensor.SensorID%>_<%=item.datumStruct.datumindex %>" class="gridPanel-sensor circle__status col-2 <%:isChecked?"ListBorderActive":"ListBorderNotActive"%> notiSensor<%:item.Sensor.SensorID%>_<%=item.datumStruct.datumindex %>">
             <%=Html.GetThemedSVG("circle-check") %>


        </div>
        <div>
            <input type="checkbox" id="ckb_<%:item.Sensor.SensorID%>_<%=item.datumStruct.datumindex %>" data-checkbox="<%:item.Sensor.SensorID%>" data-appid="<%:icon%>" class="checkbox checkbox-info" <%=check %> />
        </div>
     </div>
    </div>
</a>

<%} %>
<!-- End Sensor List -->
<style>
    .card-edit-details {
    display: flex;
    align-items:center;
}  
</style>
<script>
    var last_clicked = 0;
    var count = 0;
    var last_selection = 0;
    $(document).ready(function () {
        getSelectCount();
    });

    $('.checkbox').hide();

    function setClass(id, checked, datum) {
        if (checked) {
            $('#sensor_' + id + '_' + datum).removeClass('ListBorderNotActive').addClass('ListBorderActive');
        }
        else {
            $('#sensor_' + id + '_' + datum).removeClass('ListBorderActive').addClass('ListBorderNotActive');
        }
    }

    function toggleChartSensor(id, datum, name) {

        //prevents spam clicking 
        if ((last_selection == id + '_' + datum) && ((Date.now() - last_clicked) < 500)) {
            return;
        }
        last_clicked = Date.now();
        last_selection = id + '_' + datum;
        //prevents spam clicking ^^^

        var groupid = '<%=group.SensorGroupID %>';
        var sensorId = id;
        var checked = false;

        if ($("#ckb_" + sensorId + '_' + datum).is(':checked')) {
            $("#ckb_" + sensorId + '_' + datum).prop('checked', false);
        }
        else {
            $("#ckb_" + sensorId + '_' + datum).prop('checked', true);
            checked = true;
        }

        //alert('checked = ' + checked);
        var url = "";

        if (checked) {
            if (count >= 50) {
                $("#ckb_" + sensorId + '_' + datum).prop('checked', false);
                $('#selectedCount').html('Sensor Limit Reached.');
                return;
            } else {

                url = "/Chart/AddSensorToGroup";
                count++;
                $('#selectedCount').html(count);
            }
        }
        else {
            url = "/Chart/RemoveSensorFromGroup";
            count--;
            $('#selectedCount').html(count);
        }


        $.post(url, { id: sensorId, groupID: groupid, datumIndex: datum, customName: name }, function (data) {
            if (data == "Success")
                setClass(sensorId, checked, datum);
        });
    }


    function getSelectCount() {
        $.post("/Chart/SensorGroupCount/", { id: null }, function (data) {
            if (data != '-1') {
                count = Number(data)
                $('#selectedCount').html(count);

            } else {
                return -1;
            }
        });
    }




</script>
