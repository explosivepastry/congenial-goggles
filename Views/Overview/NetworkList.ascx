<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<CSNet>>" %>

<% 
    int SensorCount;
    List<SensorGroupSensorModel> sgsmList = iMonnit.ControllerBase.SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID, out SensorCount);

    foreach (CSNet network in Model)
    { %>

<div style="font-size: 1.4em;">
    <div class="col-xs-6">
        <h2><%=network.Name %></h2>
    </div>


    <div class="col-xs-2 ">
        <%=network.HoldingOnlyNetwork ? Html.TranslateTag("Holding","Holding") : Html.TranslateTag("Active","Active") %>
    </div>

    <div class="col-xs-2 ">
        <%List<Gateway> NonWiFiGateways = network.Gateways.Where(g => { return g.SensorID == long.MinValue; }).ToList(); %>
        <%: NonWiFiGateways.Count()%>/<%: SensorCount %>
        <%if (NonWiFiGateways.Count() > 0 && sgsmList.Where(s => { return s.Sensor.SensorTypeID != 4; }).Count() > (new Version(NonWiFiGateways.Min(g => g.APNFirmwareVersion)) < new Version("2.2.0.0") ? 100 : 500))
          { %>
        <span style="color: red;"><%: Html.TranslateTag("Overview/NetworkList|There are more sensors on this network than can be supported","There are more sensors on this network than can be supported")%>.</span>
        <%} %>
    </div>


    <div class="col-xs-2 ">
          <i title="<%: Html.TranslateTag("Edit","Edit")%>" onclick="editNetwork($(this)); return false;" data-networkid="<%: network.CSNetID %>" class="fa fa-wrench" style="vertical-align: top; cursor: pointer;"></i>
        <%if (network.Gateways.Count == 0 && SensorCount == 0)
          { %>
        <i title="<%: Html.TranslateTag("Delete","Delete")%>" onclick="deleteNetwork($(this)); return false;" data-networkid="<%: network.CSNetID %>" class="fa fa-trash " style="vertical-align: top; cursor: pointer;"></i>
        <%} %>
    </div>


</div>
<div class="clearfix"></div>
<hr />

<%} %>

<script>


    $(document).ready(function () {





    });






</script>