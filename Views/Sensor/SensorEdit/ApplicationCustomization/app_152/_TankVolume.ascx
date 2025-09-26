<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string label = LN2Level.GetLabel(Model.SensorID);


    double TankVolume = 0;
    string customLabel = "";

    if (label == "Volume")
    {
        TankVolume = LN2Level.GetSavedValue(Model.SensorID);
        customLabel= LN2Level.GetCustomLabel(Model.SensorID);

        if (customLabel == "Gallons")
        {
            TankVolume = Math.Round(TankVolume * 3.785);
        }
    }
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Tank Volume","Tank Volume")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control user-dets" type="number" name="tankVolumeValue" id="TankVolume" <%=Model.CanUpdate ? "" : "disabled" %> value="<%=TankVolume %>" required/>
        <select id="dropdownMenu" name="volumeLabel" onchange="updateSelectedValue()">
            <option value="liters" <%: customLabel.Contains("liters") ? "selected" : "" %>><%:Html.TranslateTag("L") %></option>
            <option value="gallons" <%: customLabel.Contains("gallons") ? "selected" : "" %>><%:Html.TranslateTag("Gal") %></option>

        </select>
        <%--        <div class="selected-value" id="selectedValue"></div>--%>
    </div>
</div>


<script>

    function updateSelectedValue() {
        var dropdown = document.getElementById("dropdownMenu");
        var selectedValue = document.getElementById("selectedValue");
        selectedValue.textContent = dropdown.value;
    }
</script>
