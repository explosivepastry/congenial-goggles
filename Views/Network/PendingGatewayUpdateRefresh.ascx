<%@ Control Language="C#"  Inherits="System.Web.Mvc.ViewUserControl<List<Gateway>>" %>

<% 
    
    
    
%>


    <%if (Model.Count > 0)
        {
            foreach (Gateway gate in Model)
            {

              

                %>
            <div class="information-card"style="max-width:48rem;">
                <!-- History -->
                <div class=" trigger-card__title">

                    
                        <div  title="<%=gate.GatewayType.Name %>">
                              <div class="icon gwicon "> <%=Html.GetThemedSVGForGateway(gate.GatewayTypeID) %></div>
                      <%--      <span class="icon <%:gatewayTypeIcon %>"></span>--%>
                        </div>

                        <div style="font-weight: bold; font-size:.9rem; flex-basis:250px"><%=gate.Name %></div>
                      
                

                    <div class="">
                        <span><%: Html.TranslateTag("Update Gateway To", "Update Gateway To")%>: <%=MonnitSession.LatestVersion(gate.SKU,false) %></span>
                    </div>

                    <div class=" trigger-card__title__mobile-end"><%: Html.TranslateTag("Update APN  To", "Update APN To")%>: <%= MonnitSession.LatestVersion(gate.SKU, true) %></div>

                    <div class="col-1 text-end text-md-center" id="deleteDiv_<%=gate.GatewayID%>" onclick="removeOTAGatewayRequest(<%:gate.GatewayID%>)" style="margin-top: 3px; margin-bottom: 3px; word-wrap: break-word; cursor: pointer;">
                        <%=Html.GetThemedSVG("delete") %>
                    </div>

                    <div class="col-1 text-end text-md-center" style="display: none;" >
                        <div class="spinner-border spinner-border-sm text-primary" role="status">
                            <span class="visually-hidden">Loading...</span>
                        </div>
                    </div>

                </div>
                   
           
                
                <div style="font-size: 1.4em; font-weight: bold; padding: 10px;" class="dfjcsb detailed-row">
                    <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Firmware")%></div>
                    <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Version", "Version")%></div>
                    <div class="col-3 ps-0" style="font-size: 0.7em"><%: Html.TranslateTag("Status", "Status")%></div>
                    <div class="col-3 ps-0 trigger-card__innerTitle__date" style="font-size: 0.7em"><%: Html.TranslateTag("Progress", "Progress")%></div>
                </div>
         <%if (gate.ForceToBootloader == true)
             { %>
                    <div style="font-size: 1.4em" class="eventHistoryList col-12 dfjcsb">
                        <div class="col-3" style="font-size: 0.6em">Gateway Firmware</div>
                        <div class="col-3" style="font-size: 0.6em" title=""><%= MonnitSession.LatestVersion(gate.SKU, false) %></div>
                        <div class="col-3" style="font-size: 0.6em">Queued</div>
                        <div class="col-3" style="font-size: 0.6em">
                            <div class="progress rounded" style="max-width: 120px;">
                                <div class="progress-bar" role="progressbar" style="width:10%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
               <%} %>

                       <%if (gate.RadioFirmwareUpdateInProgress == true || gate.UpdateRadioFirmware)
             { %>
                    <div style="font-size: 1.4em" class="eventHistoryList col-12 dfjcsb">
                        <div class="col-3" style="font-size: 0.6em">Gateway Firmware</div>
                        <div class="col-3" style="font-size: 0.6em" title=""><%= MonnitSession.LatestVersion(gate.SKU, true) %></div>
                        <div class="col-3" style="font-size: 0.6em"><%=gate.UpdateRadioFirmware ? "Queued" : "in Progress" %></div>
                        <div class="col-3" style="font-size: 0.6em">
                            <div class="progress rounded" style="width: 120px;">
                                <div class="progress-bar" role="progressbar" style="width:<%=gate.UpdateRadioFirmware ? "10" : "65" %>%" aria-valuenow="10" aria-valuemin="0" aria-valuemax="100"></div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <br />
               <%} %>
            </div>
            <%}%>
    <%}
    else
    {%>
    <div style="margin: 0 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
        No Pending Updates
    </div>
    <%}%>
