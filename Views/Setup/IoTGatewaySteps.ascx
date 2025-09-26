<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="container">
    <div style="display: flex; position: relative; justify-content: space-between; margin-bottom: 21px;">
        <div>
            <button class="btn btn-secondary" onclick="$('#ethernetSteps').show(); $('#cellularSteps').hide();">Use Ethernet</button>
        </div>
        <div>
            <button class="btn btn-secondary" onclick="$('#ethernetSteps').hide(); $('#cellularSteps').show();">Use Cellular</button>
        </div>
    </div>
</div>


<%-------------------------------     Ethernet     ------------%>
<div id="ethernetSteps" style="display: none;">
    <%:Html.Partial("EthernetGatewaySteps") %>
</div>


<%-------------------------------     Cellular     ------------%>

<div id="cellularSteps" style="display: none;">
    <%:Html.Partial("CellGatewaySteps") %>
</div>