<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<%-- 
    <!----------------------------------------------------------------------------------------------
                                    Top Grid Container 
                        1. *SensorPie* / 2. GatewayPie / 3. Profile / 4. Reports
    --------------------------------------------------------------------------------------------------> 
--%>

<!-- START: Sensor PIE Card 1    -->

<%

    int SensPieAlertCount = 0;
    int SensPieWarnCount = 0;
    int SensPieActiveCount = 0;
    int SensPieOfflineCount = 0;

    foreach (var item in MonnitSession.OverviewHomeModel.Sensors)
    {


if (item.Sensor.Status == eSensorStatus.OK)
    {
        SensPieActiveCount++;

}
    if (item.Sensor.Status == eSensorStatus.Offline)
    {
        SensPieOfflineCount++;

}
    if (item.Sensor.Status == eSensorStatus.Alert)
    {
        SensPieAlertCount++;


}

    if (item.Sensor.Status == eSensorStatus.Warning)
    {
        SensPieWarnCount++;

}

}
    
%>

<%--<div class="sensorGrid top_card">--%>
    <div class="headerCard">
        <div class="l-corner-hug"><%:Html.GetThemedSVG("sensor") %> </div>
        <span><%:Html.TranslateTag("Sensors") %></span>
        <div class="r-corner-hug"><%= MonnitSession.OverviewHomeModel.TotalSensors %></div>
    </div>

    <div class="chart-numbers">
        <div id="sensorCharty" style="height: 100%; width: 300px"></div>
        <div class=" sensor-icons-chart d-flex" style="flex-direction: column;">
            <div style="cursor: pointer;" onclick="goToSensorList('Alert');" title="Alerting" class="chart-icons-home"><%:Html.GetThemedSVG("db-alert") %> <span id="sensPieAlertCount"></span></div>
            <div style="cursor: pointer;" onclick="goToSensorList('Warning');" title="Warning" class="chart-icons-home"><%:Html.GetThemedSVG("db-low-battery") %> <span id="sensPieWarnCount"></span></div>
            <div style="cursor: pointer;" onclick="goToSensorList('OK');" title="Active" class="chart-icons-home"><%:Html.GetThemedSVG("circle-check-green") %> <span id="sensPieActiveCount"></span></div>
            <div style="cursor: pointer;" onclick="goToSensorList('Offline');" title="Offline" class="chart-icons-home"><%:Html.GetThemedSVG("db-wifi-off") %> <span id="sensPieOfflineCount"></span></div>
        </div>
    </div>
<%--</div>--%>

<script>

    /*-----------------------------------------
                       Sensor Chart
       * ---------------------------------------*/

    $('#sensPieAlertCount').html(<%:SensPieAlertCount%>);
    $('#sensPieActiveCount').html(<%:SensPieActiveCount%>);
    $('#sensPieWarnCount').html(<%:SensPieWarnCount%>);
    $('#sensPieOfflineCount').html(<%:SensPieOfflineCount%>);

        var chartDom = document.getElementById('sensorCharty');
        var yChart = echarts.init(chartDom);
        var optiony;

        optiony = {
            tooltip: {
                trigger: 'item'
            },
            visualMap: {
                show: false,
                min: 50,
                max: 600,
                inRange: {
                    colorLightness: [0, 1]
                }
            },
            series: [
                {
                    name: 'Sensors',
                    type: 'pie',
                    radius: '70%',
                    center: ['50%', '50%'],
                    data: [
                        { value: <%:SensPieAlertCount%>, name: 'Alerting', itemStyle: { color: '#EE3D18' } },
                        { value: <%:SensPieWarnCount%>, name: 'Warning', itemStyle: { color: '#EBDA48' } },
                        { value: <%:SensPieOfflineCount%>, name: 'Offline', itemStyle: { color: 'gray' } },
                    { value: <%:SensPieActiveCount%>, name: 'Active', itemStyle: { color: '#43BE5F' } }
                ].sort(function (a, b) {
                    return a.value - b.value;
                }),
                label: {
                    color: 'black'
                },
                labelLine: {
                    lineStyle: {
                        color: '#515356'
                    },
                    smooth: 0.2,
                    length: 5,
                    length2: 2
                },
                itemStyle: {
                    color: '#c23531'
                },
                animationType: 'scale',
                animationEasing: 'elasticOut',
                animationDelay: function (idx) {
                    return Math.random() * 900;
                }
            }
        ]
    };

    optiony && yChart.setOption(optiony);

    yChart.on('click', function (params) {
        var status = params.name;
        if (status == "Active")
            status = "OK";

        if (status == "Alerting")
            status = "Alert";
        goToSensorList(status);
    });

</script>
<!-- END: Sensor PIE Card 1    -->