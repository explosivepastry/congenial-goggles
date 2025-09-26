<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%
    DateTime fromDate = ViewBag.FromDate;
    DateTime toDate = ViewBag.ToDate;
	
    Dictionary<long, List<DataPoint>> sensorsDataPoints = new Dictionary<long, List<DataPoint>>();
    long ApplicationID = -1;
    foreach (Sensor s in Model )
    {
		if (!MonnitSession.IsCurrentCustomerMonnitAdmin && s.StartDate != DateTime.MinValue && s.StartDate.Ticks > fromDate.Ticks) {
			fromDate = s.StartDate;
		}
        List<DataMessage> list = DataMessage.LoadForChart(s.SensorID,
           Monnit.TimeZone.GetUTCFromLocalById(fromDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
           Monnit.TimeZone.GetUTCFromLocalById(toDate, MonnitSession.CurrentCustomer.Account.TimeZoneID),
            150);
        List<DataPoint> sensorPoints =
            (from dm in list
             select new DataPoint()
             {
                 Date = dm.MessageDate,
                 Value = dm.AppBase.PlotValue,
                 SentNotification = dm.MeetsNotificationRequirement
             }).ToList();

        sensorsDataPoints.Add(s.SensorID, sensorPoints);
        ApplicationID = s.ApplicationID;
    }
   
        int numOfSensors = sensorsDataPoints.Values.Count;
        List<string> DataTableNames = new List<string>();
        Response.Write("function loadApp" + ApplicationID + "Data(){");
        for (int i = 0; i < numOfSensors; i++)
        {
           
            
            //ViewBag.Multichart = true;

            DataTableNames.Add("data" + i);

            Response.Write(iMonnit.Controllers.ReportController.BuildMultiChartDataString(sensorsDataPoints.ElementAt(i).Value, Model.ElementAt(i).SensorName, i));
            
          
        }
        if (DataTableNames.Count > 1)
        {

        int jdCount = 0;
        while (DataTableNames.Count > 1)
        {

            string Table1 = DataTableNames.ElementAt(0);
            string Table2 = DataTableNames.ElementAt(1);
            DataTableNames.RemoveAt(0);
            DataTableNames.RemoveAt(0);
            Response.Write(@"
                    				var Table1ColumnCount  = " + Table1 + @".getNumberOfColumns();
                                    var Table2ColumnCount = " + Table2 + @".getNumberOfColumns();
                                    var Table1ColumnString = '1';
                                    var Table2ColumnString = '1';
                                    for(var i = 2; i < Table1ColumnCount; i++) {
                                        if(" + Table1 + @".getColumnLabel(Table1ColumnCount-1) != 'Sent Notifications')
                                            Table1ColumnString += ','+i;
                                    }
                                    for(var i = 2; i < Table2ColumnCount; i++) {
                                        if(" + Table2 + @".getColumnLabel(Table2ColumnCount-1) != 'Sent Notifications')
                                            Table2ColumnString += ','+i;
                                    } 
                                    var joinedData" + jdCount + " = google.visualization.data.join(" + Table1 + ", " + Table2 + @", 'full', [[0, 0]], eval('['+Table1ColumnString+']'), eval('['+Table2ColumnString+']'));");
    
            DataTableNames.Add("joinedData" + jdCount++);
        }

        //Response.Write("\nvar finalJoinedData = joinedData" + (jdCount - 1) + ";");
        Response.Write("\nreturn joinedData" + (jdCount - 1) + ";");
       
    }
        else if (DataTableNames.Count == 1)
    {
            Response.Write("\nreturn " + DataTableNames.ElementAt(0) + ";");
    }
    
        Response.Write("\n}");
   
     
    
    
    //}
%>