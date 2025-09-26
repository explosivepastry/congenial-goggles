<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

  <%  bool isFahrenheit = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
            }
        %>
        <tr>
            <td style="width: 250px;">Scale</td>
            <td>
                <input type="checkbox" name="TempScale" id="TempScale" <%if (isFahrenheit) Response.Write("checked='checked'"); %> />
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Sets the temperature scale that the data is displayed in." src="<%:Html.GetThemedContent("/images/help.png")%>" />
            </td>
        </tr>
<script type="text/javascript">
    setTimeout('$("#TempScale").iButton({ labelOn: "Fahrenheit" , labelOff: "Celsius" });', 500);
</script>