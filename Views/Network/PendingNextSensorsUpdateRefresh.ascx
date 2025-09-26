<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Gateway>>" %>

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
        foreach (Gateway gate in Model)
        {

            Sensor sens = Sensor.Load(gate.SensorID);

%>
<div class="gridPanel trigger-card mx-1 col-12">
    <!-- History -->
    <div class=" trigger-card__title">


        <%if (sens != null)
            {%>
        <div class="col-6 col-lg-5">
        <div style="width: 30px; height: 30px; margin-right: 5px; float: left;" title="<%=applicationDict[sens.ApplicationID].ApplicationName %>">
            <%=Html.GetThemedSVG("app" + sens.ApplicationID) %>
        </div>

        <div style="font-weight: bold; font-size: .9rem; flex-basis: 250px; margin-top: .25rem"><%=sens.ApplicationName %></div>
        </div>

        <div class="col-1 text-end text-md-center" id="deleteDiv_<%=gate.GatewayID%>" onclick="removeOTAGatewayRequest(<%:gate.GatewayID%>)" style="margin-top: 3px; margin-bottom: 3px; word-wrap: break-word; cursor: pointer;">
            <%=Html.GetThemedSVG("delete") %>
        </div>
        <%} %>
        <div class="col-1 text-end text-md-center" style="display: none;">
            <div class="spinner-border spinner-border-sm text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>

    </div>

    <div style="font-size: 1.4em; font-weight: bold; padding-bottom: 10px;" class="dfjcsb trigger-card__innerTitle">
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Sensor ID")%></div>
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Name", "Name")%></div>
        <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Status", "Status")%></div>
    </div>


    <div style="font-size: 1.4em" class="eventHistoryList col-12 dfjcsb">
        <div class="col-3" style="font-size: 0.6em"><%:sens.SensorID %></div>
        <div class="col-3" style="font-size: 0.6em" title=""><%= sens.SensorName %></div>
        <div class="col-3" style="font-size: 0.6em">Queued</div>
    </div>
    <div class="clearfix"></div>
    <br />

</div>
<%}%>
<%}%>

