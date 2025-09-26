<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iMonnit.Models.ChartSensorDataModel>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    SensorAttribute ChartFormat = Monnit.SensorAttribute.LoadBySensorID(Model.Sensors.ElementAt(0).SensorID).Where(cf => { return cf.Name.ToLower() == "chartformat"; }).ToList().FirstOrDefault();
    if (ChartFormat != null)
    {
        ViewBag.chartformat = ChartFormat.Value;
    }
    
    List<DataPoint> PlotVals;
    if (Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval || ViewBag.chartformat == "Interval")
    {
        if (Model.Sensors.ElementAt(0).ApplicationID != 14)
        {
            //Interval Sensors

            List<DataMessage> list = DataMessage.LoadForChart(
                Model.Sensors.ElementAt(0).SensorID,
                Model.FromDate,
                Model.ToDate,
                150);
            
            //List<DataMessage> list = DataMessage.LoadForChart(
            //    Model.Sensors.ElementAt(0).SensorID,
            //    Monnit.TimeZone.GetUTCFromLocalById(Model.FromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
            //    Monnit.TimeZone.GetUTCFromLocalById(Model.ToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
            //    150);
            
            
            
            
            PlotVals = (from dm in list
                        select new DataPoint()
                        {
                            Date = dm.MessageDate,
                            Value = dm.AppBase.PlotValue,
                            SentNotification = (dm.State & 2) == 2 
                        }).ToList();
            
            
          //  PlotVals = DataMessage.CountAwareByDay_LoadForChart(
          //Model.Sensors.ElementAt(0).SensorID,
          //Monnit.TimeZone.GetUTCFromLocalById(Model.FromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
          //Monnit.TimeZone.GetUTCFromLocalById(Model.ToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
          //150);


        }
        else
        {
            //Active ID (14)
            //PlotVals = DataMessage.CountByDay_LoadForChart(
            //    Model.Sensors.ElementAt(0).SensorID,
            //    Monnit.TimeZone.GetUTCFromLocalById(Model.FromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
            //    Monnit.TimeZone.GetUTCFromLocalById(Model.ToDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
            //    150);

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
    
    Response.Write("[");
    
    bool first = true;

    foreach (DataPoint val in PlotVals)
    {
        if (val.Value != null)
        {
            if (!first) { Response.Write(","); } // to prevent the very last entry from having a ',' at the end
            
            DateTime LocalDate = MonnitSession.MakeLocal(val.Date);
            if (Model.Sensors.ElementAt(0).MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval && Model.Sensors.ElementAt(0).ApplicationID != 14 || ViewBag.chartformat == "Interval")
            {
                //Interval Chart
				Response.Write(string.Format("[new Date('{1}/{2}/{0} {3}:{4}:{5}'), {6}, {7}]", LocalDate.Year, LocalDate.Month, LocalDate.Day, LocalDate.Hour, LocalDate.Minute, LocalDate.Second, val.Value, val.SentNotification ? val.Value : "undefined"));
               // Response.Write(string.Format("[new Date('{1}/{2}/{0} {3}:{4}:{5}'), {6}]", LocalDate.Year, LocalDate.Month, LocalDate.Day, LocalDate.Hour, LocalDate.Minute, LocalDate.Second, val.Value));
            
            }
            else
            {
                //Trigger Chart
				Response.Write(string.Format("[new Date('{1}/{2}/{0} {3}:{4}:{5}'), {6}]", LocalDate.Year, LocalDate.Month, LocalDate.Day, 23, 59, 59, val.Value));
            }
            first = false;
        }
    }
    Response.Write("]");
%>