<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<OTARequest>>" %>

<% 

    Dictionary<long, Sensor> sensDict = new Dictionary<long, Sensor>();
    List<Sensor> senList = Sensor.LoadByAccountID(ViewBag.AccountID);

    foreach (Sensor sens in senList)
    {
        sensDict.Add(sens.SensorID, sens);
    }

    Dictionary<long, MonnitApplication> applicationDict = new Dictionary<long, MonnitApplication>();
    foreach (MonnitApplication application in MonnitApplication.LoadAll())
    {
        applicationDict.Add(application.ApplicationID, application);
    }

%>




<%if (Model.Count > 0)
    {

        foreach (OTARequest ota in Model)
        {
            int NumberOfGoodSignalSensors = 0;
            int NumberOfBadSignalSensors = 0;

            foreach (OTARequestSensor OTASensor in ota.OTARequestSensors)
            {
                Sensor sens;
                Boolean isValidSensor = sensDict.TryGetValue(OTASensor.SensorID, out sens);

                if (isValidSensor)
                {
                    if (sens.LastDataMessage != null)
                    {
                        int Percent;

                        Percent = DataMessage.GetSignalStrengthPercent(sens.GenerationType, sens.SensorTypeID, sens.LastDataMessage.SignalStrength);
                        if (Percent >= 76)
                        {
                            NumberOfGoodSignalSensors++;
                        }
                        else
                        {
                            NumberOfBadSignalSensors++;
                        }
                    }
                }
            }
            string timeToDownLoad = "";
            if (NumberOfGoodSignalSensors <= 5 && NumberOfBadSignalSensors == 0)
            {
                timeToDownLoad = "short";
            }
            if (NumberOfGoodSignalSensors > 5 && NumberOfBadSignalSensors == 0)
            {
                timeToDownLoad = "mid";
            }
            if (NumberOfBadSignalSensors > 0 || NumberOfGoodSignalSensors > 20)
            {
                timeToDownLoad = "long";
            }
%>

<div class="gridPanel trigger-card mx-1 col-12">
    <!-- History -->
    <div class="col-12 trigger-card__title">
        <div class="col-6 col-lg-5">
            <div style="width: 30px; height: 30px; margin-right: 5px; float: left;" title="<%=applicationDict[ota.ApplicationID].ApplicationName %>">
                <%=Html.GetThemedSVG("app" + ota.ApplicationID) %>
            </div>
            <div style="font-weight: bold;"><%=applicationDict[ota.ApplicationID].ApplicationName %></div>
            <div>
                <%: Html.TranslateTag("Update To", "Update To")%>: <%=ota.Version %>, 
                <% if (timeToDownLoad == "short")
                    { %>
                <span>In about 1 hour </span>
                <% } %>

                <% if (timeToDownLoad == "mid")
                    { %>
                <span>In about 2-3 hours </span>
                <% } %>

                <% if (timeToDownLoad == "long")
                    { %>
                <span>In over 3 hours </span>
                <% } %>
            </div>

        </div>
        <div class="col-2 col-lg-4">
            <span style="font-weight: bold;"><%: Html.TranslateTag("Gateway", "Gateway")%>:</span> <span><%=ota.GatewayID %></span>
        </div>
        <div class="col-3 col-lg-2 trigger-card__title__mobile-end"><strong>Status:</strong> <%=ota.Status %></div>
        <div class="col-1 text-end text-md-center" id="deleteDiv_<%=ota.OTARequestID%>" onclick="removeOTARequest(<%:ota.OTARequestID %>)" style="margin-top: 3px; margin-bottom: 3px; word-wrap: break-word; cursor: pointer;">
            <%=Html.GetThemedSVG("delete") %>
        </div>
        <div class="col-1 text-end text-md-center" style="display: none;" id="<%=ota.OTARequestID%>">
            <div class="spinner-border spinner-border-sm text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    </div>

    <div class="clearfix"></div>
    <br />
    <div style="font-size: 1.4em; font-weight: bold; padding-bottom: 10px;" class="dfjcsb trigger-card__innerTitle">
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Sensor ID")%></div>
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Name", "Name")%></div>
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Status", "Status")%></div>
    </div>
    <% 
        string progressString = "";
        foreach (OTARequestSensor OtaSens in ota.OTARequestSensors)
        {
            if (!sensDict.ContainsKey(OtaSens.SensorID))
                continue;

            if (OtaSens.CompletedDate > DateTime.MinValue)
            {
                progressString = Html.TranslateTag("Completed");
            }
            else if (OtaSens.DownloadedDate > DateTime.MinValue)
            {
                progressString = Html.TranslateTag("3 of 4, Installing Update");
            }
            else if (OtaSens.StartDate > DateTime.MinValue)
            {
                progressString = Html.TranslateTag("2 of 4, Downloading Update");
            }
            else if (OtaSens.Status == eOTAStatus.Processing)
            {
                progressString = Html.TranslateTag("1 of 4, Preparing for Update Process");
            }
            else
            {
                progressString = Html.TranslateTag("Waiting for sensor");
            }
    %>

    <%
        Sensor sens = Sensor.Load(OtaSens.SensorID);
        if (sens.SensorTypeID != 8)
        {
    %>
    <div style="font-size: 1.4em" class="eventHistoryList col-12 dfjcsb">
        <div class="col-3" style="font-size: 0.6em"><%= OtaSens.SensorID %></div>
        <div class="col-3" style="font-size: 0.6em" title="<%= OtaSens.SensorID %>"><%= sensDict[OtaSens.SensorID].SensorName %></div>
        <div class="col-3" style="font-size: 0.6em"><%= progressString %></div>
    </div>
    <% }%>

    <div class="clearfix"></div>
    <br />
    <%} %>
</div>
<%}%>
<%}%>
