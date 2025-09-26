<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%using (Html.BeginForm()) { %>
<div class="formtitle">
    Sensors whose last communication came through this gateway (Count: <%=Model.Count(m=>{return m.LastDataMessage.MessageDate >= DateTime.UtcNow.AddMinutes(-1 * (m.ReportInterval * 2));}) %>)
</div>
<table style="width:100%">

    <thead>
        <tr>
            <th style="width:20px;"></th>
            <th>SensorID
            </th>
            <th>Sensor Name
            </th>
            <th style="text-align:right">Last Communication Date
            </th>
            <th style="width:20px;"></th>
        </tr>
    </thead>
    <tbody>
        <% foreach (Sensor a in Model)
           {
               if (a.LastDataMessage.MessageDate >= DateTime.UtcNow.AddMinutes(-1 * (a.ReportInterval * 2)))
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
                 No sensors on this account are currently checking in on this gateway. 
            </td>
        </tr>
        <%} %>
    </tbody>

</table>
<%} %>
