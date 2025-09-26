<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<RapidUpdateSensorModel>>" %>


{ "Instances":[
	<%
        int Count = 0;
        foreach (RapidUpdateSensorModel Instance in Model)
        {
            if (Instance.Sensor != null)
            {
                //Add a "," if it is not the first record
                if (Count > 0){%>,<%} 
                
                //Create the JSON string for the Instance
                %>
		        { 
		        "SensorID":<%:Instance.Sensor.SensorID %>, 
		        "Version":"<%:Instance.Sensor.FirmwareVersion %>", 
		        "Duration":<%: Instance.Sensor.StartDate.ToElapsedSeconds() %>, 
		        "SensorCom":<%: Instance.Sensor.LastCommunicationDate.ToElapsedSeconds() %>, 
		        "GatewayPressed":<%: (Instance.Gateway.LastCommunicationDate > Instance.Sensor.StartDate) ? "true" : "false" %>, 
		        "FirmwareAck":<%: (Instance.Sensor.FirmwareUpdateInProgress || Instance.Sensor.FirmwareDownloadComplete) ? "true" : "false" %>, 
		        "FirmwareDownloaded":<%: (Instance.Sensor.FirmwareDownloadComplete) ? "true" : "false" %>, 
		        "ConfigsComplete":<%: (!Instance.Sensor.SendFirmwareUpdate && !Instance.Sensor.FirmwareUpdateInProgress && !Instance.Sensor.IsDirty) ? "true" : "false" %>
		        }				
			    <% 
                
                //Increase the count
                Count++;

                //Force the dirty flags once firmware download starts so they are already set before it finishes
                if(!Instance.Sensor.FirmwareVersion.Contains("25.44") && Instance.Sensor.FirmwareUpdateInProgress && !Instance.Sensor.IsDirty)
                {
					Instance.Sensor.GeneralConfigDirty = true;
                    Instance.Sensor.GeneralConfig2Dirty = true;
                    Instance.Sensor.GeneralConfig3Dirty = true;
                    Instance.Sensor.ProfileConfigDirty = true;
                    Instance.Sensor.ProfileConfig2Dirty = true;
                    Instance.Sensor.Save();
                }
            }
        }%>
   ]}
			
