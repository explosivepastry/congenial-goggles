<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<iMonnit.Models.SensorNoficationModel>>" %>

<table border="0">


    <% if (ViewBag.AdvancedNoti != null) { }
        AdvancedNotification Advanced = ViewBag.AdvancedNoti;

        foreach (SensorNoficationModel item in Model)
        { %>
    <tr id="SentFromSensorList<%:item.Sensor.SensorID %>">
        <td width="39px">
            <div class="icon <%:item.Notify ? "icon-checked active" : "icon-unchecked inactive"%>  notiSensor<%:item.Sensor.SensorID%>_<%:item.DatumIndex%>" onclick="toggleSensor(<%:item.Sensor.SensorID%>, <%:item.DatumIndex %>); ">
            </div>

        </td>
        <td width="100">
            <label onclick="toggleSensor(<%:item.Sensor.SensorID%>, <%:item.DatumIndex %>); ">
                <%= item.Sensor.SensorName%>

            </label>
        </td>
        <td width="100">
            <label onclick="toggleSensor(<%:item.Sensor.SensorID%>, <%:item.DatumIndex %>); ">



                <%
                    string name = item.Sensor.GetDatumName(item.DatumIndex);

                    if (item.NotificationClass == eNotificationClass.Application || (item.NotificationClass == eNotificationClass.Advanced && Advanced != null && Advanced.UseDatums))
                    {
                        if (name != item.Sensor.SensorName)
                        {
                %>

                <span style="font-size: 0.8em;"><%: " : " + name%></span>

                <%}
                    }
                    else
                    {%>

                <%} %>
            </label>
        </td>
    </tr>
    <% } %>
</table>
