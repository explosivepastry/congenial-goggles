<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%-- 
    <!----------------------------------------------------------------------------------------------
                                    Top Grid Container 
                        1. SensorPie / 2. *GatewayPie* / 3. Profile / 4. Reports
    --------------------------------------------------------------------------------------------------> 
--%>

<!--    Gateway  PIE Card 2 -->

<%  
    //int GatePieAlertCount = 0;
    int GatePieWarnCount = 0;
    int GatePieActiveCount = 0;
    int GatePieOfflineCount = 0;

    foreach (var item in MonnitSession.OverviewHomeModel.Gateways)
    {
        if (item.Status == eSensorStatus.OK)
        {
            GatePieActiveCount++;
        }

        if (item.Status == eSensorStatus.Warning)
        {
            GatePieWarnCount++;
        }

        if (item.Status == eSensorStatus.Offline)
        {
            GatePieOfflineCount++;
        }

        //if (item.Gateway.Status == eSensorStatus.Alert) 
        //{ 
        //    GatePieAlertCount++;
        //}
    } 
%>

<%--<div class="gatewayGrid  top_card">--%>
    <div class="headerCard">
        <div class="l-corner-hug"><%:Html.GetThemedSVG("gateway") %> </div>
        <span><%:Html.TranslateTag("Gateways") %></span>
        <div class="r-corner-hug"><%=MonnitSession.OverviewHomeModel.TotalGateways %></div>
    </div>
    <div class="chart-numbers">

        <div id="sensorChartx" style="height: 100%; width: 300px"></div>
        <div class="sensor-icons-chart d-flex" style="flex-direction: column;">
            <%--                    <div class="chart-icons-home"><%:Html.GetThemedSVG("db-alert") %> <span id="gatePieAlertCount"><%:GatePieAlertCount %></span></div>--%>
            <div style="cursor: pointer;" onclick="goToGatewayList('Warning');" title="Warning" class="chart-icons-home"><%:Html.GetThemedSVG("db-low-battery") %> <span id="gatePieWarnAlert"><%:GatePieWarnCount %></span></div>
            <div style="cursor: pointer;" onclick="goToGatewayList('OK');" title="Active" class="chart-icons-home"><%:Html.GetThemedSVG("circle-check-green") %> <span id="gatePieActiveCount"><%:GatePieActiveCount %></span></div>
            <div style="cursor: pointer;" onclick="goToGatewayList('Offline');" title="Offline" class="chart-icons-home"><%:Html.GetThemedSVG("db-wifi-off") %> <span id="gatePieOfflineCount"><%:GatePieOfflineCount %></span></div>
        </div>
    </div>
<%--</div>--%>

<script>


    /*-----------------------------------------
                         Gateway Chart
* -------------------------------------------*/

    var chartDom = document.getElementById('sensorChartx');
    var xChart = echarts.init(chartDom);
    var optionx;

    optionx = {
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
                name: 'Gateways',
                type: 'pie',
                radius: '70%',
                center: ['50%', '50%'],
                data: [
<%--                            { value: <%:GatePieAlertCount%>, name: 'Alerting' },--%>
                        { value: <%:GatePieWarnCount%>, name: 'Warning', itemStyle: { color: '#EBDA48' } },
                        { value: <%:GatePieOfflineCount%>, name: 'Offline', itemStyle: { color: 'gray' } },
                    { value: <%:GatePieActiveCount%>, name: 'Active', itemStyle: { color: '#43BE5F' } }
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

    optionx && xChart.setOption(optionx);

    xChart.on('click', function (params) {
        var status = params.name;
        if (status == "Active")
            status = "OK";

        if (status == "Alerting")
            status = "Alert";
        goToGatewayList(status);
    });


</script>

<!---2 End -->
