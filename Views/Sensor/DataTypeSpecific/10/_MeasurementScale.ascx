<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string attVal = string.Empty;
        foreach(SensorAttribute sensAttr in SensorAttribute.LoadBySensorID(Model.SensorID))
        {
            if(sensAttr.Name == "DistanceUnits")
              attVal = sensAttr.Value.ToString();
        }
%>

<tr>
    <td style="width: 250px;">Choose Measurement Scale:</td>
    <td>
        <select id="MeasurementScale" name="MeasurementScale">
            <option value="mm" <%: attVal == "Millimeter" ? "selected" : "" %>>Millimeter</option>
            <option value="cm" <%: attVal == "Centimeter" ? "selected" : "" %>>Centimeter</option>
            <option value="in" <%: attVal == "Inch" ? "selected" : "" %>>Inch</option>
            <option value="ft" <%: attVal == "Foot" ? "selected" : "" %>>Foot</option>
            <option value="yrd" <%: attVal == "Yard" ? "selected" : "" %>>Yard</option>
            <option value="M" <%: attVal == "Meter" ? "selected" : "" %>>Meter</option>
        </select>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Sets the temperature scale that the data is displayed in." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script>
    $('#MeasurementScale').addClass("editField editFieldMedium");
</script>