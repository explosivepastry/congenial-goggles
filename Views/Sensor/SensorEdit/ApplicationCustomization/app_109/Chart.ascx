<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>

<%



    List<DataMessage> list = DataMessage.LoadAllForChart(
           Model.Sensors[0].SensorID,
           Model.FromDate,
           Model.ToDate
           );


    StringBuilder CurrentAccDataVals = new StringBuilder();
    StringBuilder Phase1AverageDataVals = new StringBuilder();
    StringBuilder Phase2AverageDataVals = new StringBuilder();
    StringBuilder Phase3AverageDataVals = new StringBuilder();
    string label = ThreePhaseCurrentMeter.GetLabel(Model.Sensors[0].SensorID);


    foreach (DataMessage item in list)
    {
        ThreePhaseCurrentMeter Data = Monnit.ThreePhaseCurrentMeter.Deserialize(Model.Sensors[0].FirmwareVersion, item.Data);
        Data.SetSensorAttributes(Model.Sensors[0].SensorID);
        CurrentAccDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Math.Round(Data.PlotValue.ToDouble(),1).ToString("#0.#") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'CurrentAccumulation' ,'" + item.DataMessageGUID +  "' ],");
        Phase1AverageDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Phase1Average.ToString("#0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Phase1Average' ,'" + item.DataMessageGUID +  "' ],");
        Phase2AverageDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Phase2Average.ToString("#0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Phase2Average' ,'" + item.DataMessageGUID +  "' ],");
        Phase3AverageDataVals.Append("[new Date(" + Monnit.TimeZone.GetLocalTimeById(item.MessageDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToEpochTime() + ")," + Data.Phase3Average.ToString("#0.##") + "," + ((item.State & 2) == 2 ).ToInt() + ", 'Phase3Average' ,'" + item.DataMessageGUID +  "' ],");
    }

    ViewBag.CurrentAccDataVals = CurrentAccDataVals;
    ViewBag.Phase1AverageDataVals = Phase1AverageDataVals;
    ViewBag.Phase2AverageDataVals = Phase2AverageDataVals;
    ViewBag.Phase3AverageDataVals = Phase3AverageDataVals;
    ViewBag.Label = label;

%>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/_Ammeter.ascx"); %>