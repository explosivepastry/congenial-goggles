<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%
    
    string label = string.Empty;
    double lowVal = double.MinValue;
    double highVal = double.MinValue;
    List<long> sensorIDList = new List<long>();
    string tempLabel = string.Empty;
    if (Model.SensorID != long.MinValue)
    {
        Sensor sens = Sensor.Load(Model.SensorID);
        label = ZeroToFiveVolts.GetLabel(Model.SensorID);
        lowVal = ZeroToFiveVolts.GetLowValue(Model.SensorID);
        highVal = ZeroToFiveVolts.GetHighValue(Model.SensorID);
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == sens.ApplicationID; }).Select(sensid => sensid.SensorID).ToList();
    }
    else
    {
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == 72; }).Select(sensid => sensid.SensorID).ToList();
    }

    if (Model.SensorID == long.MinValue || (label.ToLower() == "v" && lowVal == 0 && highVal == 5))
    {%>

<tr>
    <td>Choose the Sensor with the scale you want to use</td>
    <td>
        <select id="SensorID" name="SensorID">
            <% 
                for (int i = 0; i < sensorIDList.Count; i++)
                {
                    tempLabel = ZeroToFiveVolts.GetLabel(sensorIDList[i]);%>
            <option value="<%: sensorIDList[i] %>" <%: Model.SensorID == sensorIDList[i] ? "selected" : "" %>><%= Sensor.Load(sensorIDList[i]).SensorID == long.MinValue ? "Choose a Sensor Scale":Sensor.Load(sensorIDList[i]).SensorName + " " + tempLabel %> </option>
            <%} %>
        </select>
</tr>
<tr>
    <td>Notify when reading is</td>
    <td>
        <%: Html.DropDownList("CompareType", "short", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%> 
        (to) 
        <input class="short" id="CompareValueForConversion" name="CompareValueForConversion" type="text" value="<%:Model.CompareValue %>">
    </td>
    <td>
       <span class="scaleLabel" >V</span> <span class="baseLabel">(<%: (Model.CompareValue.ToDouble()).ToString()%> Volts)</span>
    </td>
</tr>

<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
<%}
    else
    { %>
<%--scaled version needs to be put here--%>
<tr>
    <td>Choose the Sensor with the scale you want to use</td>
    <td>
        <select id="SensorID" name="SensorID">
            <% for (int i = 0; i < sensorIDList.Count; i++)
               {
                   tempLabel = ZeroToFiveVolts.GetLabel(sensorIDList[i]);%>
            <option value="<%: sensorIDList[i] %>" <%: Model.SensorID == sensorIDList[i] ? "selected" : "" %>><%= Sensor.Load(sensorIDList[i]).SensorID == long.MinValue ? "Choose a Sensor Scale":Sensor.Load(sensorIDList[i]).SensorName + " " + tempLabel %> </option>
            <%} %>
        </select>
</tr>
<tr>
    <td>Notify when reading is</td>
    <td>
        <%: Html.DropDownList("CompareType", "short", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%> 
        (to) 
        <input class="short" id="CompareValueForConversion" name="CompareValueForConversion" type="text" value="<%: (Model.CompareValue.ToDouble().LinearInterpolation(0, lowVal, 5, highVal)).ToString() %>">
        <input type="hidden" id="CompareValue" name="CompareValue" value="<%: Model.CompareValue.ToString() %>"
    </td>
    <td>
       <span class="scaleLabel" > <%: Html.Label(label) %></span> <span class="baseLabel">(<%: (Model.CompareValue.ToDouble()).ToString()%> Volts)</span>
    </td>
</tr>

<tr>
    <td><%:Html.Hidden("Scale",label) %></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>

<%} %>
<script type="text/javascript">
    $(function () {
        $('#SensorID').blur(function () {
           
            var sensorID = $('#SensorID').val();
            var comparVal = $("#CompareValueForConversion").val();
            $.ajax({
                method: "Get",
                url: "/Notification/NotificationValueEdit",
                data: { SensorID: sensorID, compareValue: comparVal },
                datatype: 'json',
                success: function (data) {
                   
                    data = $.parseJSON(data)
                    
                    $(".scaleLabel").html(data['Label']);
                    $('.baseLabel').html('(' + data['realVal'] + ' Volts)');
                    $('#CompareValue').val(data['realVal']);
                }
            });
        });

        $('#CompareValueForConversion').change(function () {
       
            var sensorID = $('#SensorID').val();
            var comparVal = $("#CompareValueForConversion").val();
            $.ajax({
                method: "Get",
                url: "/Notification/NotificationValueEdit",
                data: { SensorID: sensorID, compareValue: comparVal },
                datatype: 'json',
                success: function (data) {
                    
                    data = $.parseJSON(data)
                  
                    $(".scaleLabel").html(data['Label']);
                    $('.baseLabel').html('(' + data['realVal'] + ' Volts)');
                    $('#CompareValue').val(data['realVal']);
                }
            });
        });
    });
</script>