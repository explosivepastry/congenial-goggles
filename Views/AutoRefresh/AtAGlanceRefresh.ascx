<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.SensorGroupSensorModel>>" %>

<%
    List<RefreshSensorModel> RefreshSensors = new List<RefreshSensorModel>();

    foreach (var item in Model)
    {
        RefreshSensorModel RefreshSensor = new RefreshSensorModel(item.Sensor.SensorID);


        switch (item.Sensor.Status)
        {
            case Monnit.eSensorStatus.OK:
                RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/good.png");
                break;
            case Monnit.eSensorStatus.Warning:
                RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/alert.png");
                break;
            case Monnit.eSensorStatus.Alert:
                RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/alarm.png");
                break;
            //case Monnit.eSensorStatus.Inactive:
            //    RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/inactive.png");
            //    break;
            //case Monnit.eSensorStatus.Sleeping:
            //    RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/sleeping.png");
            //    break;
            case Monnit.eSensorStatus.Offline:
                RefreshSensor.StatusImageUrl = Html.GetThemedContent("/images/sleeping.png");
                break;
        }

        RefreshSensor.notificationPaused = item.Sensor.isPaused();

        RefreshSensor.isDirty = !item.Sensor.CanUpdate;

        //if (!item.CanUpdate)
        //    RefreshSensor.StatusImageUrl = RefreshSensor.StatusImageUrl.Replace(".png", "-dirty.png");


        DataMessage message = item.DataMessage;
        if (message != null)
        {
            RefreshSensor.Reading = message.DisplayData;
            RefreshSensor.Date = String.Format("{0:g}", message.MessageDateLocalTime(MonnitSession.CurrentCustomer.Account.TimeZoneID));


            int Percent = DataMessage.GetSignalStrengthPercent(item.Sensor.GenerationType, item.Sensor.SensorTypeID, message.SignalStrength);

            if (Percent <= 0)
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/No-Bars.png");
            else if (Percent <= 10)
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/1-Bar.png");
            else if (Percent <= 25)
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/2-Bars.png");
            else if (Percent <= 50)
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/3-Bars.png");
            else if (Percent <= 75)
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/4-Bars.png");
            else
                RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/5-Bars.png");


            if (item.Sensor.PowerSourceID == 3 || message.Voltage == 0)
                RefreshSensor.PowerImageUrl = Html.GetThemedContent("/Images/Battery/Line.png");
            else if (item.Sensor.PowerSourceID != 4)
            {
                message.PowerSourceID = item.Sensor.PowerSourceID;

                if (message.Battery <= 0)
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-0.png");
                else if (message.Battery <= 10)
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-10.png");
                else if (message.Battery <= 25)
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-25.png");
                else if (message.Battery <= 50)
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-50.png");
                else if (message.Battery <= 75)
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-75.png");
                else
                    RefreshSensor.PowerImageUrl = Html.GetThemedContent("/images/Battery/Battery-100.png");
            }

            RefreshSensor.Voltage = message.Voltage;
        }
        else
        {
            RefreshSensor.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/unknown.png");
            RefreshSensor.PowerImageUrl = Html.GetThemedContent("/Images/Battery/unknown.png");
        }

        RefreshSensors.Add(RefreshSensor);

    }

    Response.Write(Json.Encode(RefreshSensors)); %>
