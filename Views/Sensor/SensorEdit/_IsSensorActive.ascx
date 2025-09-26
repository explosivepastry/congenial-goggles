<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsActive && MonnitSession.CustomerCan("Sensor_Active")){ %>
<tr>
    <td>Mark this sensor active</td>
    <td><%: Html.CheckBoxFor(model => model.IsActive)%></td>
    <td></td>
</tr>
<%} %>