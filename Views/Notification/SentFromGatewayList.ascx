<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.GatewayNoficationModel>>" %>

<table border="0">
    <%foreach (GatewayNoficationModel item in Model)
      { %>
    <tr id="SentFromGatewayList<%:item.Gateway.GatewayID %>">
        <td width="39px">

            <div class="icon  <%:item.Notify ? "icon-checked active" : "icon-unchecked inactive"%> notiGateway<%:item.Gateway.GatewayID%>" onclick="toggleGateway(<%:item.Gateway.GatewayID%>);">
                </div>
            
        </td>
        <td width="100px">
            <label onclick="toggleGateway(<%:item.Gateway.GatewayID%>);"><%= item.Gateway.Name%></label>
        </td>
    </tr>
    <% } %>
</table>
