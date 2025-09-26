<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>

<style>
    .report img {
        width: 25px;
    }
</style>
<%using (Html.BeginForm())
  {%>

<table style="width:100%" class="report">
    <thead>
        <tr>
            <th style="text-align:center">SensorID
            </th>
            <th style="text-align:center">Sensor Name
            </th>
            <th style="text-align:center">Battery percentage
            </th>
            <th style="text-align:center">Battery Health
            </th>
            <th style="text-align:center">Voltage
            </th>
            




        </tr>
    </thead>
    <tbody>
        <% foreach (System.Data.DataRow a in Model.Rows)
           {
                 
        %>
        <tr>
            <td style="text-align:center">
                <%:a["SensorID"].ToStringSafe() %>
            </td>
            <td style="text-align:center">
                <%:a["SensorName"].ToStringSafe() %>
            </td>
              <td style="text-align:center">
                <%:a["Battery"].ToStringSafe()  %>%         
            </td>
            <td style="text-align:center">
                <% Html.RenderPartial("/Views/Gauge/Battery.ascx", a["Battery"].ToDouble()); %>
            </td>
          
            <td style="text-align:center">
                <%:a["Voltage"].ToStringSafe() %>
            </td>
            


        </tr>
        <% } %>
    </tbody>
</table>
<% } %>