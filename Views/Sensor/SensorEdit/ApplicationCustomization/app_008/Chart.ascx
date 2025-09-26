<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%
    // no need to fix this chart no sensors in production 
    List<DataPoint> PlotVals;
    if (Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
    {
        if (Model.Sensors.ElementAt(0).ApplicationID != 14)
        {
            List<DataMessage> list = DataMessage.LoadForChart(
                   Model.Sensors.ElementAt(0).SensorID,
                   Model.FromDate,
                   Model.ToDate,
                   150);

            PlotVals = (from dm in list
                        select new DataPoint()
                            {
                                Date = dm.MessageDate,
                                Value = dm.AppBase.PlotValue,
                                SentNotification = (dm.State & 2) == 2,
                               
                            }).ToList();

        }
        else
        {
            PlotVals = DataMessage.CountByDay_LoadForChart(Model.Sensors.ElementAt(0).SensorID, Model.FromDate, Model.ToDate, 150);
        }
    }
    else
    {
        Monnit.TimeZone TimeZone = Monnit.TimeZone.Load(MonnitSession.CurrentCustomer.Account.TimeZoneID);
        TimeSpan offset = Monnit.TimeZone.GetCurrentLocalOffset(TimeZone.Info);

        //Trigger Sensors
        PlotVals = DataMessage.CountAwareByDay_LoadForChart(Model.Sensors.ElementAt(0).SensorID, Model.FromDate, Model.ToDate, offset.Hours, 150);
    }

    StringBuilder dataVals = new StringBuilder();
    foreach (DataPoint item in PlotVals)
    {
        dataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.Date, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + item.Value + "," + item.SentNotification.ToInt() + "],");
    }



    List<AppDatum> datum = MonnitApplicationBase.GetAppDatums(Model.Sensors[0].ApplicationID);
        
%>

<div id="sensorChartDiv" style="height: 350px;"></div>


<script type="text/javascript">


    $(document).ready(function () {

        var echartBar = echarts.init(document.getElementById('sensorChartDiv', 'infographic'));

        echartBar.setOption({
        
            tooltip: {
                backgroundColor: "#ffffff96",
                textStyle: {
                    fontWeight: 'bold',
                    color: 'black',
                },
             trigger: 'axis'
         },
         toolbox: {
             feature: {
                 saveAsImage: {
                     show: false,
                     title: 'Save',
                 },
             }
         },
         dataZoom: {
             show: true,
             realtime: true,
             start: 0,
             end: 100,
         },
         legend: { type: 'scroll',
             data: ['<%= datum[0].type%>'],
             x: 'left'
            },
         grid: {
             y2: 80
         },
         xAxis: [{
             type: 'time',
             
         }],
         yAxis: [{
             type: 'value',min:'dataMin',max:'dataMax',
             name: '<%= datum[0].type%>',
            }],
         series: [{
             name: '<%= datum[0].type%>',
                type: 'line',
                showAllSymbol: true,
                data: [<%=dataVals.ToString().TrimEnd(',')%>],

            }]
     });


     });



</script>


