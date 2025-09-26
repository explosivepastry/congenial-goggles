<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.SensorGroupSensorModel>>" %>
<% 
    List<RefreshSensorModel> RefreshSensors = new List<RefreshSensorModel>();

    DataMessage.CacheLastByAccount(MonnitSession.CurrentCustomer.AccountID, new TimeSpan(0, 1, 0));

    //DataMessage.CacheLastByAccount(MonnitSession.CurrentCustomer.AccountID, new TimeSpan(0, 1, 0));

    foreach (var item in Model)
    {
        RefreshSensorModel RefreshSensor = new RefreshSensorModel(item.Sensor.SensorID);

        DataMessage message = item.DataMessage;
        if (message != null)
        {
            message.AddToCache();
            RefreshSensor.Reading = message.DisplayData;
            RefreshSensor.Date = String.Format("{0:g}", message.MessageDate.OVToLocalDateTimeShort());

            int Percent = DataMessage.GetSignalStrengthPercent(item.Sensor.GenerationType, item.Sensor.SensorTypeID, message.SignalStrength);

            if (Percent <= 0)
                //RefreshSensor.SignalImageUrl = "-0";   // display only -1, -3, -4 so on.
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-none").ToString();
            else if (Percent <= 10)
                //RefreshSensor.SignalImageUrl = "-1";
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-1").ToString();
            else if (Percent <= 25)
                //RefreshSensor.SignalImageUrl = "-2";
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-2").ToString();
            else if (Percent <= 50)
                //RefreshSensor.SignalImageUrl = "-3";
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-3").ToString();
            else if (Percent <= 75)
                //RefreshSensor.SignalImageUrl = "-4";
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-4").ToString();
            else
                //RefreshSensor.SignalImageUrl = "-5";
                RefreshSensor.SignalImageUrl = Html.GetThemedSVG("signal-5").ToString();

            MvcHtmlString PowerIcon = new MvcHtmlString("");
            if (item.Sensor.PowerSourceID == 3 || message.Voltage == 0)
            {
                PowerIcon = Html.GetThemedSVG("plugsensor1");
            }
            else if (item.Sensor.PowerSourceID == 4)
            {
                PowerIcon = new MvcHtmlString("<div style='font-size: 25px; color: #2d4780;'>" + message.Voltage + " volts</div><div>&nbsp;</div>");

            }
            else
            {
                if (message.Battery <= 0)
                    PowerIcon = Html.GetThemedSVG("bat-dead");
                else if (message.Battery <= 10)
                    PowerIcon = Html.GetThemedSVG("bat-low");
                else if (message.Battery <= 25)
                    PowerIcon = Html.GetThemedSVG("bat-low");
                else if (message.Battery <= 50)
                    PowerIcon = Html.GetThemedSVG("bat-half");
                else if (message.Battery <= 75)
                    PowerIcon = Html.GetThemedSVG("bat-full-ish");
                else
                    PowerIcon = Html.GetThemedSVG("bat-ful");
            }

            RefreshSensor.PowerImageUrl = PowerIcon.ToString();

            RefreshSensor.voltageString = "Battery: " + message.Battery.ToString() + "%, Voltage: " + message.Voltage.ToStringSafe() + " V";
            RefreshSensor.Voltage = message.Voltage;
            //RefreshSensor.voltageString = battType == "volt" ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "";
            
            RefreshSensors.Add(RefreshSensor);
        }
        RefreshSensor.StatusImageUrl = item.Sensor.Status.ToString();

        RefreshSensor.notificationPaused = item.Sensor.isPaused();
        RefreshSensor.isDirty = !item.Sensor.CanUpdate;


    }

    Response.Write(Json.Encode(RefreshSensors)); %>
