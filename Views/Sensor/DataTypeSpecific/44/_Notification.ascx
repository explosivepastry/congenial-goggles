<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%

    string label = string.Empty;
    double lowVal = double.MinValue;
    double highVal = double.MinValue;
    List<long> sensorIDList = new List<long>();
    string tempLabel = string.Empty;
    if (Model.ScaleID > 0)
    {
        Sensor sens = Sensor.Load(Model.ScaleID);
        label = Analog.GetLabel(Model.ScaleID);
        lowVal = Analog.GetLowValue(Model.ScaleID);
        highVal = Analog.GetHighValue(Model.ScaleID);
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == sens.ApplicationID; }).Select(sensid => sensid.SensorID).ToList();
    }
    else
        {
            lowVal = 0;
            highVal = 1.2;
            sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == 1; }).Select(sensid => sensid.SensorID).ToList();
        }
        double revertedVal = Model.CompareValue.ToDouble().LinearInterpolation(0, lowVal, 1.2, highVal,2);
        // revertedVal = revertedVal < 0 ? 0 : revertedVal;
%>

<%--scaled version needs to be put here--%>
<tr>
    <td>Choose the Sensor with the scale you want to use</td>
    <td>
        <select id="ScaleID" name="ScaleID">
            <% for (int i = 0; i < sensorIDList.Count; i++)
               {
                   tempLabel = Analog.GetLabel(sensorIDList[i]);%>
            <option value="<%: sensorIDList[i] %>" <%: Model.ScaleID == sensorIDList[i] ? "selected" : "" %>><%= Sensor.Load(sensorIDList[i]).SensorID == long.MinValue ? "Choose a Sensor Scale":Sensor.Load(sensorIDList[i]).SensorName + " " + tempLabel %> </option>
            <%} %>
        </select>
        </td>
</tr>
<tr>
    <td>Notify when reading is</td>
    <td>
        <%: Html.DropDownList("CompareType", "short", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%> 
        (to) 
        <input class="short" id="CompareValueForConversion" name="CompareValueForConversion" type="text" value="<%: revertedVal %>">
        <input type="Hidden" id="CompareValue" name="CompareValue" value="<%:Model.CompareValue.ToDouble().ToString() %>" />
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

<input id="NotificationDatum" value="<%: (int)eDatumType.Voltage0To1Point2 %>" type="hidden"/>
<script type="text/javascript">
     $(function () {
         var longMinVal = <%: long.MinValue%>;
         var labelVal = '';
        <%label = label.Replace("'", "");%>
        labelVal = '<%: label%>';

        $('#scaleLabel').hide();
        $('#scaleDropDown').hide();

        var scaleID = $('#ScaleID').val();
        var comparVal = $("#CompareValueForConversion").val();
        var notificationDatum = $('#NotificationDatum').val();

        $.ajax({
            method: "Get",
            url: "/Notification/NotificationValueEdit",
            data: { ScaleID: scaleID, compareValue: comparVal, edt: notificationDatum },
            datatype: 'json',
            success: function (data) {
                data = $.parseJSON(data)
                $(".scaleLabel").html(data['Label']);
                $("#scale").html(data['Label']);
                $('.baseLabel').html('(' + data['realVal'] + ' V)');
                $('#CompareValue').val(data['realVal']);
            }
        });

        var longMinVal = -1000;
        var labelVal = '';
        if ($('#ScaleID').val() != longMinVal) {
            // //alert('if ' + $('#SensorID').val());
            $('#scaleLabel').show();
            $('#scaleLabel').html(labelVal);
            $('#scale').val(labelVal);
        }
        else {
            // //alert('else ' + $('#SensorID').val());
            $('#scaleDropDown').show();
            var scal = $('#scaleDropDown').val();
            $('#scale').val(scal);
        }

        $('#ScaleID').blur(function () {

            if ($('#ScaleID').val() > 0) {
                $('#scaleDropDown').hide();
                $('#scaleLabel').show();
                $('#scaleLabel').html(labelVal);
                $('#scale').val(labelVal);

                var scaleID = $('#ScaleID').val();
                var comparVal = $("#CompareValueForConversion").val();
                var notificationDatum = $('#NotificationDatum').val();

                $.ajax({
                    method: "Get",
                    url: "/Notification/NotificationValueEdit",
                    data: { ScaleID: scaleID, compareValue: comparVal, edt: notificationDatum },
                    datatype: 'json',
                    success: function (data) {

                        data = $.parseJSON(data)

                        $("#scale").html(data['Label']);
                        $('#scaleLabel').html(data['Label']);
                        $('.baseLabel').html('(' + data['realVal'] + ' V)');
                    }
                });
            }
            else {
                $('#scaleLabel').hide();
                $('#scaleDropDown').show();
                var scaled = $('#scaleDropDown').val();
                $('#scale').val(scaled);
            }
        });

        $('#scaleDropDown').blur(function () {

            var data = $('#scaleDropDown').val();
            $("#scale").val(data);
        });


        $('#ScaleID').change(ConvertValues);
        $('#CompareValueForConversion').change(ConvertValues);

        function ConvertValues() {
            ////alert('Calling ConvertValues');
            if ($('#ScaleID').val() != longMinVal) {
                var scaleID = $('#ScaleID').val();
                ////alert(sensorID);
                var comparVal = $("#CompareValueForConversion").val();
                var notificationDatum = $('#NotificationDatum').val();
                $.ajax({
                    method: "Get",
                    url: "/Notification/NotificationValueEdit",
                    data: { ScaleID: scaleID, compareValue: comparVal, edt: notificationDatum },
                    datatype: 'json',
                    success: function (data) {
                        data = $.parseJSON(data)
                        $(".scaleLabel").html(data['Label']);
                        $("#scale").html(data['Label']);
                        $('.baseLabel').html('(' + data['realVal'] + ' V)');
                        $('#CompareValue').val(data['realVal']);
                    }
                });
            }
        }
    });
</script>