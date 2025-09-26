<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

  <%  bool isDegrees = true;
            if (ViewData["Scale"] != null)
            {
                isDegrees = ViewData["Scale"].ToStringSafe() == "D";
            }
            else
            {
                isDegrees = Tilt.IsDegrees(Model.SensorID);
            }
        %>
        <tr>
            <td style="width: 250px;">Display as</td>
            <td>
                <input type="checkbox" name="Scale" id="Scale" <%if (isDegrees) Response.Write("checked='checked'"); %> />
            </td>
            <td>
                <img alt="help" class="helpIcon" title="Sets the temperature scale that the data is displayed in." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
<script type="text/javascript">

    setTimeout('$("#Scale").iButton({ labelOn: "Degrees" , labelOff: "Radians" });', 500);

</script>

