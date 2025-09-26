<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%

    foreach (Sensor item in Model)
    {
        bool isLocked = !item.EqualToSensorPrint(new byte[32]);
        string icon = "app" + item.ApplicationID;
%>

<%if (isLocked)
    { %>
<a  class="d-flex" style="flex-basis:320px;" onclick="copyToClipboard('<%:item.SensorPrint.ToHex()%>');">

    <div class="turn-on-check-card" style="width:100%">

        <div class="check-card-icon">
            <span class="sensor icon icon-<%:icon%> icon-xs"></span>
        </div>

        <div class="check-card-name" >
            <label ><%:item.SensorName%></label>
        </div>

        <div id="sensor_<%:item.SensorID%>" class="check-card-check printCopy notiSensor<%:item.SensorID%>">
            <%=Html.GetThemedSVG("printCheck") %>
        </div>

        <div class="hidden-lg hidden-md hidden-sm hidden-xs">
            </div>
    </div>
</a>
<%}
    else
    { %>
<%--NOT LOCKED--%>

<a  class="d-flex" style="flex-basis:320px;" onclick="toggleSensor(<%:item.SensorID%>);">
    <div class="turn-on-check-card" style="width:100%">

        <div class="check-card-icon">
           <span class="sensor icon icon-<%:icon%> icon-xs"></span>
        </div>

        <div class="check-card-name" >
            <label ><%:item.SensorName%></label>
        </div>

        <div   id="sensor_<%:item.SensorID%>" class="check-card-check ListBorderNotActive notiSensor<%:item.SensorID%>">
            <%=Html.GetThemedSVG("finger-print") %>
        </div>

        <div class="hidden-lg hidden-md hidden-sm hidden-xs">
            <input type="checkbox" id="ckb_<%:item.SensorID%>" data-checkbox="<%:item.SensorID%>" data-appid="<%:icon%>" class="checkbox checkbox-info" />
        </div>
    </div>
</a>

<%} %>
<%} %>

<script>
    $('.checkbox').hide();
    $('#filterdSensors').html(<%=ViewBag.filterCount%>);
</script>

<style>
    .triggerDevice__status #svg_print {
    /*    fill: #808080;*/
        height: 50px;
        width: 50px;
    }
</style>
