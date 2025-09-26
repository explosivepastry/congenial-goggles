<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%
    List<DataPoint> PlotVals;

    List<DataMessage> list = DataMessage.LoadForChart(
           Model.SensorID,
           Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryFromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
          Monnit.TimeZone.GetUTCFromLocalById(MonnitSession.HistoryToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
           150);

    PlotVals = (from dm in list
                select new DataPoint()
                    {
                        Date = dm.MessageDate,
                        Value = dm.AppBase.PlotValue,
                        SentNotification = (dm.State & 2) == 2
                    }).ToList();



    StringBuilder dataVals = new StringBuilder();
    foreach (DataPoint item in PlotVals)
    {
        dataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + item.Value + "," + item.SentNotification.ToInt() + "],");
    }

    List<AppDatum> datum = MonnitApplicationBase.GetAppDatums(Model.ApplicationID);
        
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {



        var tempScale = '<%: Temperature.IsFahrenheit(Model.SensorID)?"F":"C" %>';


        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({
            title: {
                text: '<%=Model.SensorName%>',
                subtext: '<%= datum[0].type%> ',
                x: 'center'
            },
            tooltip: {
                trigger: 'item',
                formatter: function (params) {
                    var date = new Date(params.value[0]);
                    data = date.getFullYear() + '-'
                           + (date.getMonth() + 1) + '-'
                           + date.getDate() + ' '
                           + date.getHours() + ':'
                           + date.getMinutes();
                    return "Date: " + echarts.format.formatTime('<%=(MonnitSession.CurrentCustomer.Preferences["Date Format"] + ' ' + MonnitSession.CurrentCustomer.Preferences["Time Format"]).Replace(" tt","") %>', data) + '<br/>' 
                           + 'Temperature : ' + params.value[1] + ' °' + tempScale + '<br/>'
                           + 'Status : ' + (params.value[2] == 1 ? 'Aware' : 'Not Aware');
                }
            },
            toolbox: {
                show: false,
            },
            dataZoom: {
                show: true,
                realtime: true,
                start: 0,
                end: 100,
            },
            legend: { type: 'scroll',
                data: ['Temperature'],
                x: 'left'
            },
            grid: {
                y2: 80
            },
            xAxis: [
                {
                    type: 'time',
                    
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: 'Temperature',
                    axisLabel: {
                        formatter: '{value} °' + tempScale
                    }
                }
            ],
            series: [
                {
                    name: 'Temperature',
                    type: 'line',
                    showAllSymbol: true,
                    data: [<%=dataVals.ToString().TrimEnd(',')%>],
          
                }
            ]
        });


    });

</script>


