<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string display = LN2Level.GetUsableSensorHeightDisplay(Model.SensorID);

    double TankHeight = LN2Level.GetUsableSensorHeightCM(Model.SensorID);
    if (display == "inches")
    {
        TankHeight = Math.Round(TankHeight / 2.54);
    }
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Tank Height","Tank Height")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="text" <%=Model.CanUpdate ? "" : "disabled"  %> name="tankHeightValue" value="<%=TankHeight %>" required />
        <select id="dropdownMenu" name="heightLabel" onchange="updateSelectedValue()">
            <option value="centimeters" <%: display.Contains("centimeters") ? "selected" : "" %>><%:Html.TranslateTag("cm") %></option>
            <option value="inches" <%: display.Contains("inches") ? "selected" : "" %>><%:Html.TranslateTag("in") %></option>
        </select>
    </div>
</div>

<script>

    function updateSelectedValue() {
        var dropdown = document.getElementById("dropdownMenu");
        var selectedValue = document.getElementById("selectedValue");
        selectedValue.textContent = dropdown.value;
    }
</script>

