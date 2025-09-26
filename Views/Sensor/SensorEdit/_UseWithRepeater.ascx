<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<%if (!Model.IsWiFiSensor && new Version(Model.FirmwareVersion) < new Version("2.0.0.0"))
  {%>


<tr>
    <td><%: Html.TranslateTag("Overview/SensorEdit|Use this sensor through a repeater","Use this sensor through a repeater")%></td>
    <td><%: Html.CheckBox("UseRepeater", Model.StandardMessageDelay > 100, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></td>
    <td>
        <img alt="help" class="helpIcon" title="<%: Html.TranslateTag("Overview/SensorEdit|Default not checked. Check this box if sensor will be used in conjuction with a repeater.","Default not checked. Check this box if sensor will be used in conjuction with a repeater.") %>" src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>

<% } %>