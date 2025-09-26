<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

<div class="rule-card_container w-100" style="max-height: 50vh!important; " >
    <div class="trigger-device__top">
        <div class="card_container__top" style="border-bottom: none;"" >
            <div class="card_container__top__title">
                <span class="me-2"><%= Html.GetThemedSVG("sensor") %></span>
                <%= Html.TranslateTag("Map/_SensorsListWithFilters|Sensors to Display on Map", "Sensors to Display on Map") %>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>

<%--    <div id="filters_sensor">
        <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
            <font color="gray">
                    <%: Html.TranslateTag("Click device to enable/disable","Click device to enable/disable")%>
            </font>
            <a id="sensorFiltersToggle" style="cursor:pointer;" data-rotation='-1' onclick="toggleSensorFilters()">
                <%=Html.GetThemedSVG("filter") %>
            </a>
        </div>
        <div id="sensorFiltersPartial" style="display: none;">
        <%= Html.Action("SensorFilters", "Map", Model.VisualMapID) %>
        </div>
    </div>--%>

    <div id="devices_sensor">
        <div class="trigger-device__top">
            <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                <font color="gray">
                    <%: Html.TranslateTag("Map/_SensorsListWithFilters|Click device to enable/disable","Click device to enable/disable")%>
                </font>
            <a id="sensorFiltersToggle" style="cursor:pointer;" onclick="toggleSensorFilters()">
                <%=Html.GetThemedSVG("filter") %>
            </a>
            </div>
        </div>       
                <div id="sensorFiltersPartial" style="display: none;">
        <%= Html.Action("SensorFilters", "Map", Model.VisualMapID) %>
        </div>

        <div id="filteredSensorsPartial" style="display: none;">
            <%= Html.Action("FilteredSensors", "Map", Model.VisualMapID) %>
        </div>
    </div>

</div>
<script>
    $(document).ready(function () {
        toggleSensors();
    })
    function toggleSensorFilters() {
        //var oneRotation = $('#sensorFiltersToggle').data('rotation');
        //$('#sensorFiltersToggle').data('rotation', -oneRotation);
        //$('#sensorFiltersToggle').children('#svg_filter').first().attr('transform', `scale(${-oneRotation}, ${oneRotation})`);
        $('#sensorFiltersPartial').toggle();
        return false;
    }
    function toggleSensors() {
        //var oneRotation = $('#sensorsToggle').data('rotation');
        //$('#sensorsToggle').data('rotation', -oneRotation);
        //$('#sensorsToggle').children('#svg_filter').first().attr('transform', `scale(${-oneRotation}, ${oneRotation})`);
        $('#filteredSensorsPartial').toggle();
        return false;
    }
</script>