<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Sensor>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    GatewaySensorList
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="container-fluid">
   

         <%Html.RenderPartial("GatewayLink", ViewBag.Gateway as Gateway); %>


<%using (Html.BeginForm()) { %>

<div class="x_panel shadow-sm rounded powertourhook" id="hook-five">



<div class="formtitle">
    <%: Html.TranslateTag("Overview/GatewaySensorList|Sensors whose last communication came through this gateway (Count","Sensors whose last communication came through this gateway (Count")%>: <%=Model.Count(m=>{return m.LastDataMessage != null && m.LastDataMessage.MessageDate >= DateTime.UtcNow.AddMinutes(-1 * (m.ReportInterval * 2));}) %>)
</div>

    <table style="width:100%">

    <thead>
        <tr>
            <th style="width:20px;"></th>
            <th><%: Html.TranslateTag("Sensor ID","Sensor ID")%>
            </th>
            <th><%: Html.TranslateTag("Sensor Name","Sensor Name")%>
            </th>
            <th style="text-align:right"><%: Html.TranslateTag("Overview/GatewaySensorList|Last Communication Date","Last Communication Date")%>
            </th>
            <th style="width:20px;"></th>
        </tr>
    </thead>
		<tr style="height:5px;"></tr>
    <tbody>
        <% foreach (Sensor a in Model)
            {
                if (a.LastDataMessage != null &&  a.LastDataMessage.MessageDate >= DateTime.UtcNow.AddMinutes(-1 * (a.ReportInterval * 2)))
               {

                   %>
        <tr>
            <td></td>
            <td>
                <%:a.SensorID%>
            </td>
            <td>
                <%=a.SensorName%>
            </td>
            <td style="text-align:right">
                <%=Monnit.TimeZone.GetLocalTimeById(a.LastCommunicationDate, MonnitSession.CurrentCustomer.Account.TimeZoneID).ToShortDateString()%> 
            </td>
            <td></td>
        </tr>
        <%}
           } %>
        <% if (Model.Count <=0) {%>
        <tr>
            <td colspan="5">
                 <%: Html.TranslateTag("Overview/GatewaySensorList|No sensors on this account are currently checking in on this gateway","No sensors on this account are currently checking in on this gateway")%>. 
            </td>
        </tr>
        <%} %>
    </tbody>

</table>

</div>
<%} %>

	</div>
</asp:Content>
