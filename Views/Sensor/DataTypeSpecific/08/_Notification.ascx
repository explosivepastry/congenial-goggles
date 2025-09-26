<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%
    
    string label = string.Empty;
    double lowVal = double.MinValue;
    double highVal = double.MinValue;
    List<long> sensorIDList = new List<long>();
    string tempLabel = string.Empty;
    if (Model.ScaleID != long.MinValue)
    {
        Sensor sens = Sensor.Load(Model.ScaleID);
        label = Monnit.Application_Classes.DataTypeClasses.MilliAmps.GetLabel(Model.ScaleID);
        lowVal = Monnit.Application_Classes.DataTypeClasses.MilliAmps.GetLowValue(Model.ScaleID);
        highVal = Monnit.Application_Classes.DataTypeClasses.MilliAmps.GetHighValue(Model.ScaleID);
    }
    sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.GetDatumTypes().Contains(eDatumType.MilliAmps); }).Select(sensid => sensid.SensorID).ToList();

    
%>
<tr>
    <td>Choose the Sensor with the scale you want to use</td>
    <td>
        <select id="scaleid" name="scaleid">
            <% for (int i = 0; i < sensorIDList.Count; i++)
               {
                   tempLabel = Monnit.Application_Classes.DataTypeClasses.MilliAmps.GetLabel(sensorIDList[i]);%>
            <option value="<%: sensorIDList[i] %>" <%: Model.ScaleID == sensorIDList[i] ? "selected" : "" %>><%= Sensor.Load(sensorIDList[i]).SensorID == long.MinValue ? "Choose a Sensor Scale":Sensor.Load(sensorIDList[i]).SensorName + " " + tempLabel %> </option>
            <%} %>
        </select>
</tr>
<tr>
    <td>Notify when reading is</td>
    <td>
        <%: Html.DropDownList("CompareType", "short", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%> 
        (to) 
        <%
            Double compValueinScale = Model.CompareValue.ToDouble();
            compValueinScale = compValueinScale.LinearInterpolation(0, lowVal, 20, highVal);
            if (compValueinScale < 0) compValueinScale = 0.0;
        %>
        <input class="short" id="CompareValueForConversion" name="CompareValueForConversion" type="text" value="<%: compValueinScale%>">
        <input type="hidden" id="CompareValue" name="CompareValue" value="" />
    </td>
    <td>
       <span class="scaleLabel" > <%: Html.Label(label) %></span> <span class="baseLabel">(<%: (Model.CompareValue.ToDouble()).ToString()%> mA)</span>
    </td>
</tr>

<tr>
    <td><%:Html.Hidden("Scale",label) %></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
<input type="hidden" id="NotificationDatum" name="NotificationDatum" value="<%:(int)eDatumType.MilliAmps %>" />
<script type="text/javascript">
    $(function () {
        $('#scaleid').blur(ConvertValue);
        $('#CompareValueForConversion').change(ConvertValue);

        ConvertValue();

        function ConvertValue () {
            var scaleid = $('#scaleid').val();
            var comparVal = $("#CompareValueForConversion").val();
            var EDT = $('#NotificationDatum').val();
            $.ajax({
                method: "Get",
                url: "/Notification/NotificationValueEdit",
                data: { ScaleID: scaleid, compareValue: comparVal, edt: EDT },
                datatype: 'json',
                success: function (data) {
                   
                    data = $.parseJSON(data)
                   
                    $(".scaleLabel").html(data['Label']);
                    $('.baseLabel').html('(' + data['realVal'] + ' mA)');
                    $('#CompareValue').val(data['realVal']);
                }
            });
        };
    });
</script>