<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <%
        string attVal = string.Empty;

        string measurementScale = AirSpeed.GetUnits(Model.SensorID).ToString().ToLower();
        double ductAreainMeters = AirSpeed.GetDuctAreaValue(Model.SensorID);

        foreach (SensorAttribute sensAttr in SensorAttribute.LoadBySensorID(Model.SensorID))
        {
            if (sensAttr.Name == "SpeedUnits")
                attVal = sensAttr.Value.ToString();


        }
    %>


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

    <%if (AirSpeed.GetShowFullDataValue(Model.SensorID))
        {%>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Temperature Values in","Show Temperature Values in")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="TempScale" name="tempscale" class="form-select">
                <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit","Fahrenheit")%></option>
                <option value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius","Celsius")%></option>
            </select>
        </div>
        <br />
    </div>
    <%}%>


    <div class="clear"></div>
    <br />

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_114|Air Speed Scale:","Air Speed Scale:")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="MeasurementScale" name="MeasurementScale" class="form-select">
                <option value="mps" <%: measurementScale == "mps" ? "selected" : "" %>>(mps) Meters Per Second</option>
                <option value="mph" <%: measurementScale == "mph" ? "selected" : "" %>>(mph) Miles Per Hour</option>
                <option value="kmph" <%: measurementScale == "kmph" ? "selected" : "" %>>(kmph) Kilometers Per Hour</option>
                <option value="knot" <%: measurementScale == "knot" ? "selected" : "" %>>(knot) Knots</option>
                <option value="cfm" <%: measurementScale == "cfm" ? "selected" : "" %>>(CFM) Cubic Feet Per Minute</option>
                <option value="cmh" <%: measurementScale == "cmh" ? "selected" : "" %>>(CMH) Cubic Meters Per Hour</option>
                <option value="cmm" <%: measurementScale == "cmm" ? "selected" : "" %>>(CMM) Cubic Meters Per Minute</option>
            </select>
        </div>
    </div>



    <div class="row sensorEditForm" id="ductAreaRow">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_114|Duct Area for volumetric airflow calculation ")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control user-dets" id="ductAreaValue" name="ductAreaValue" type="number" min="0" value="<%= measurementScale.ToLower() == "cfm" ?  (ductAreainMeters * 10.7639 ) : ductAreainMeters %>">
            <span id="meterSpan">Square Meters</span><span id="feetSpan">Square Feet</span>
        </div>
    </div>
    <div class="clear"></div>
    <br />



    <div class="col-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>
    <div class="clearfix"></div>


    <div class="text-end">
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script>
        $(document).ready(function () {


            setAreaVisibility();
            $('#MeasurementScale').change(function () {
                setAreaVisibility();
                //    $('#ductAreaValue').val("");
            });

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });

        function setAreaVisibility() {
            var label = $('#MeasurementScale').val();


            if (label.startsWith("c")) {
                $('#ductAreaRow').show();

            }
            else {
                $('#ductAreaRow').hide();
                $('#ductAreaValue').val("1");
            }


            if (label == 'cfm') {
                $('#meterSpan').hide();
                $('#feetSpan').show();
            } else {
                $('#meterSpan').show();
                $('#feetSpan').hide();
            }

        }


    </script>
</form>
