<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<% bool showPreference = false;

    if(new Version(Model.GatewayFirmwareVersion) > new Version("2.0.1.2") || new Version(Model.GatewayFirmwareVersion) > new Version("0.0.0.0"))//Support for Ethernet Server Communication started at 2.0.1.2
    {
        showPreference = true;
    }

    if (Model.SKU.Contains("-IN") || Model.SKU.Contains("-in"))//Industrial doesn't have Ethernet hardware loaded  (Cell Communication Only)
    {
        showPreference = false;
    }

    if (showPreference)
    { %>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Connection Preference","Connection Preference")%>
            </div>
            <div class="col sensorEditFormInput">
                <select class="form-select" name="GatewayCommunicationPreference" id="GatewayCommunicationPreference">
                    <option value="4" <%= Model.GatewayCommunicationPreference == eGatewayCommunicationPreference.Ethernet_Only ? "selected=''" : ""%>><%: Html.TranslateTag("Ethernet Only","Ethernet Only")%></option>
                    <option value="3" <%= Model.GatewayCommunicationPreference == eGatewayCommunicationPreference.Cellular_Only ? "selected=''" : ""%>><%: Html.TranslateTag("Cellular Only","Cellular Only")%></option>
                    <option value="0" <%= Model.GatewayCommunicationPreference == eGatewayCommunicationPreference.Prefer_Ethernet ? "selected=''" : ""%>><%: Html.TranslateTag("Ethernet Preferred","Ethernet Preferred")%></option>
                    <%--<option value="1" <%= Model.GatewayCommunicationPreference == eGatewayCommunicationPreference.Prefer_Cellular ? "selected=''" : ""%>>Cellular Default</option>--%>
            
                </select>
                <%: Html.ValidationMessageFor(model => model.GatewayCommunicationPreference)%>
            </div>
        </div>
<%} else { //Cell Only %>
        <input type="hidden" name="GatewayCommunicationPreference" value="3" />
<%} %>






