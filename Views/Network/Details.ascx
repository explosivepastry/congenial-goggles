<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.CSNet>>" %>

<%
    if (Model.Count < 1)
    {%>

<div class="row" style="padding-top: 15px; padding-left: 15px;">
    <%: Html.TranslateTag("Network/Details|No Networks found for this account","No Networks found for this account")%>.
</div>
<% }
    else
        foreach (CSNet item in Model)
        {
            string networkStatusString = "corp-off";

            if (!item.HoldingOnlyNetwork)
            {
                if (!item.SendNotifications)
                    networkStatusString = "corp-warn";
                else
                    networkStatusString = "corp-good";
            }

            %>
<a class="card-w-data justify-content-between" style="width:20rem;" href="/Network/Edit/<%: item.CSNetID %>" title="<%: Html.TranslateTag("Network/Details|Click to Edit","Click to Edit")%>">

   <%-- ---------------------Color Status-----------%>
               <div class="corp-status  <%=networkStatusString %>"></div>
<%----------------------------------------------------%>

            <div  class="viewSensorDetails eventsList__tr innerCard-holder w-100">
                <div class="innerCard-holder__icon">
                    <div class="divCellCenter holder holderInactive holdico">
                 
                        <%: item.HoldingOnlyNetwork ?  Html.GetThemedSVG("off-wifi-gray") : Html.GetThemedSVG("on-wifi-white")  %>
                    </div>
                </div>

                <div class="innerCard-holder__data">
                    <div class="innerCard-holder__data__title">
                        <div class="network_small-title"><%=item.Name %></div>
                        <%:item.HoldingOnlyNetwork ? "Holding" : "" %>
                    </div>
                 
                        <div class="innerCard-holder__data__gs">
                            <%List<Gateway> NonWiFiGateways = item.Gateways.Where(g => { return g.SensorID == long.MinValue; }).ToList(); %>
                            <div><%: Html.TranslateTag("Network/Details|Sensors","Sensors")%>: <%: item.Sensors.Count() %> </div>
                            <div><%: Html.TranslateTag("Network/Details|Gateways","Gateways")%>: <%: NonWiFiGateways.Count()%></div>
                        </div>
              
                        <div class="111 innerCard-holder__data__ed">
                            <div class="grey-container">
                                <%: Html.TranslateTag("Network/Details|Rules","Rules")%>:
								<%=item.SendNotifications ? "Enabled" : "Disabled" %>
                            </div>
                        </div>
                </div>
            </div>
  </a>
<%} %>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("Network_List_Details.aspx") %>
    $(document).ready(function () {
        $('#filterdNetworks').html('<%:Model.Count%>');
    });

</script>
