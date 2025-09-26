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
        label = ZeroToTenVolts.GetLabel(Model.ScaleID);
        lowVal = ZeroToTenVolts.GetLowValue(Model.ScaleID);
        highVal = ZeroToTenVolts.GetHighValue(Model.ScaleID);
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == sens.ApplicationID; }).Select(sensid => sensid.SensorID).ToList();
    }
    else
    {
        lowVal = 0.0;
        highVal = 10.0;
        sensorIDList = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID).Where(appid => { return appid.ApplicationID == 74; }).Select(sensid => sensid.SensorID).ToList();
    }

    double revertedVal = Model.CompareValue.ToDouble().LinearInterpolation(0, lowVal, 10, highVal, 2);
    revertedVal = revertedVal < 0 ? 0 : revertedVal;
%>

<!--DatumType 42 - 0-10 Volt-->
<%--<div class="row sensorEditForm">
    <div class="col-12 ps-0">
        Notify when reading is 
    </div>
    <div class="col-12 ps-0">
        <select class="form-select" id="CompareType" name="CompareType">
            <option value="Greater_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Greater_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Greater Than","Greater Than")%></option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == Monnit.eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>
</div>--%>

<div class="rule-card">
    <%--scaled version needs to be put here--%>
    <div class="rule-title" for="subject">
        <%: Html.TranslateTag("Choose the Sensor with the scale you want to use", "Choose the Sensor with the scale you want to use")%>
    </div>
    <div class="col ps-0 sensorEditFormInput">
        <select id="ScaleID" name="ScaleID" class="form-select">
            <% for (int i = 0; i < sensorIDList.Count; i++)
                {
                    tempLabel = ZeroToTenVolts.GetLabel(sensorIDList[i]);%>
            <option value="<%: sensorIDList[i] %>" <%: Model.ScaleID == sensorIDList[i] ? "selected" : "" %>><%= Sensor.Load(sensorIDList[i]).SensorID == long.MinValue ? "Choose a Sensor Scale" : Sensor.Load(sensorIDList[i]).SensorName + " " + tempLabel %> </option>
            <%} %>
        </select>
    </div>
</div>


<div class="rule-card">
    <div class="rule-title" for="subject">
        <%: Html.TranslateTag("Notify when reading is", "Notify when reading is")%>
    </div>
    <div class="col-12 ps-0  datum42  ">
        <%: Html.DropDownList("CompareType", "form-select", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%>
        <span class="mx-1">(to)</span>
    </div>
    <div class="col-12 ps-0  datum42 ">
        <input class="short form-control mt-1 user-dets grt-less" id="CompareValueForConversion" name="CompareValue" type="number" value="<%: revertedVal%>">
   
    <div>
        <span class="scaleLabel"><%: Html.Label(label) %></span> <span class="baseLabel">(<%: (Model.CompareValue.ToDouble()).ToString()%> Volts)</span>
    </div>
         </div>
<tr>
    <td><%:Html.Hidden("Scale", label) %></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
</div>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValueForConversion').val();
        return settings;
    }
</script>

<input id="NotificationDatum" value="<%: (int)eDatumType.Voltage0To10%>" type="hidden" />
<script type="text/javascript">
    $('#CompareType').addClass('form-select');
    $(function () {
        var longMinVal = <%: long.MinValue%>;
                var labelVal = '';
        <%label = label.Replace("'", "");%>
                labelVal = '<%: label%>';

                $('#scaleLabel').hide();
                $('#scaleDropDown').hide();

                var scaleID = $('#ScaleID').val();
                var comparVal = parseFloat($("#CompareValueForConversion").val());
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
                        $('#CompareValueForConversion').val(data['realVal']);
                    }
                });

                var longMinVal = -1000;
                var labelVal = '';
                if ($('#ScaleID').val() != longMinVal) {
                    $('#scaleLabel').show();
                    $('#scaleLabel').html(labelVal);
                    $('#scale').val(labelVal);
                }
                else {
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
                        var comparVal = parseFloat($("#CompareValue").val());
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
                        var comparVal = parseFloat($("#CompareValueForConversion").val());
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
                                $('#CompareValueForConversion').val(data['realVal']);
                            }
                        });
                    }
                }
            });
</script>
