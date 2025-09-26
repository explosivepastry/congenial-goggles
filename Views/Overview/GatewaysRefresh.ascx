<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.Gateway>>" %>

<% 
    List<RefreshGatewayModel> RefreshGateways = new List<RefreshGatewayModel>();
    foreach (var item in Model)
    {
        if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36)//don't show wifi gateways here
            continue;

        RefreshGatewayModel RefreshGateway = new RefreshGatewayModel(item.GatewayID);

        
        RefreshGateway.StatusImageUrl = Html.GetThemedContent("/images/good.png");
        if (item.LastCommunicationDate == DateTime.MinValue || item.isEnterpriseHost)
            RefreshGateway.StatusImageUrl = Html.GetThemedContent("/images/sleeping.png");
        else if (item.ReportInterval != double.MinValue && item.LastCommunicationDate.AddMinutes(item.ReportInterval * 2 + 1) < DateTime.UtcNow)
            //Missed more than one heartbeat + one minute to take drift into account

            RefreshGateway.StatusImageUrl = Html.GetThemedContent("/images/alert.png");


        RefreshGateway.NotificationPaused = item.isPaused();
        RefreshGateway.IsDirty = item.IsDirty;
        
        //if (item.IsDirty || item.ForceToBootloader || item.SendResetNetworkRequest || item.SendSensorInterpretor)
        //    RefreshGateway.StatusImageUrl = RefreshGateway.StatusImageUrl.Replace(".png", "-dirty.png");
        
        
        string isCharging = "";
        RefreshGateway.PowerImageUrl = "/Content/Images/Battery/Line.png";
        if (item.CurrentPower != 0 && item.CurrentPower != 1)
        {
            if ((item.CurrentPower & 0x8000) == 0x8000)
                isCharging = "c";
                        
            if (item.Battery <= 0)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-0{0}.png", isCharging));
            else if (item.Battery <= 10)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-10{0}.png", isCharging));
            else if (item.Battery <= 25)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-25{0}.png", isCharging));
            else if (item.Battery <= 50)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-50{0}.png", isCharging));
            else if (item.Battery <= 75)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-75{0}.png", isCharging));
            else if (item.Battery <= 100)
                RefreshGateway.PowerImageUrl = Html.GetThemedContent(string.Format("/Images/Battery/Battery-100{0}.png", isCharging));

        }
        
               
        if (item.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && item.LastCommunicationDate > DateTime.MinValue)
        { 
            RefreshGateway.Date = Monnit.TimeZone.GetLocalTimeById(item.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString();
            RefreshGateway.Date += " " + Monnit.TimeZone.GetLocalTimeById(item.LastCommunicationDate,MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortTimeString();
        }
                 
        if (item.CurrentSignalStrength > 0)
        {
            if (item.CurrentSignalStrength <= 5)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/No-Bars.png");
            else if (item.CurrentSignalStrength <= 10)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/1-Bar.png");
            else if (item.CurrentSignalStrength <= 14)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/2-Bars.png");
            else if (item.CurrentSignalStrength <= 19)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/3-Bars.png");
            else if (item.CurrentSignalStrength <= 23)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/4-Bars.png");
            else if (item.CurrentSignalStrength < 99)
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/5-Bars.png");
            else
                RefreshGateway.SignalImageUrl = Html.GetThemedContent("/images/Signal Strength/No-Bars.png");
        }

        RefreshGateways.Add(RefreshGateway);
    }

    Response.Write(Json.Encode(RefreshGateways)); %>