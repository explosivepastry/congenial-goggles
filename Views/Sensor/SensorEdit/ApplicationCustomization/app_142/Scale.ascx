<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />



    <div class="formBody">

        <% 
            string accelerationScale = AdvancedVibration2.GetAccelerationScale(Model.SensorID);
            string velocityScale = AdvancedVibration2.GetVelocityScale(Model.SensorID);
            string displacementScale = AdvancedVibration2.GetDisplacementScale(Model.SensorID);
            bool isFahrenheit = true;
            bool isHertz = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
            }

            if (ViewData["measurementscale"] != null)
            {
                isHertz = ViewData["measurementscale"].ToStringSafe() == "Hz";
            }
            else
            {
                isHertz = AdvancedVibration.IsHertz(Model.SensorID);
            }
        %>

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
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Frequency Values in","Show Frequency Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="measurementscale" name="measurementscale" class="form-select">
                    <option value="<%="on"%>" <%=isHertz ? "selected='selected'" : "" %>><%: Html.TranslateTag("Hertz","Hertz")%></option>
                    <option value="<%="off"%>" <%=isHertz ? "" : "selected='selected'" %>><%: Html.TranslateTag("RPM","RPM")%></option>
                </select>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Acceleration Values in","Show Acceleration Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="AccelerationScale" name="AccelerationScale" class="form-select">
                    <option value="mm/s^2" <%=accelerationScale == "mm/s^2" ? "selected='selected'" : "" %>>mm/s^2</option>
                    <option value="in/s^2" <%=accelerationScale == "in/s^2" ? "selected='selected'" : "" %>>in/s^2</option>
                    <option value="g" <%=accelerationScale == "g" ?  "selected='selected'" : "" %>>g</option>
                </select>
            </div>
        </div>
         <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Velocity Values in","Show Velocity Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="VelocityScale" name="VelocityScale" class="form-select">
                    <option value="mm/s" <%=velocityScale == "mm/s" ? "selected='selected'" : "" %>>mm/s</option>
                    <option value="in/s rms" <%=velocityScale == "in/s rms" ? "selected='selected'" : "" %>>in/s rms</option>
                    <option value="in/s peak" <%=velocityScale == "in/s peak" ?  "selected='selected'" : "" %>>in/s peak</option>
                </select>
            </div>
        </div>

              <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Displacement Values in","Show Displacement Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="DisplacementScale" name="DisplacementScale" class="form-select">
                    <option value="mm p-p" <%=displacementScale == "mm p-p" ? "selected='selected'" : "" %>>mm p-p</option>
                    <option value="in p-p" <%=displacementScale == "in p-p" ? "selected='selected'" : "" %>>in p-p</option>
                    <option value="mil p-p" <%=displacementScale == "mil p-p" ?  "selected='selected'" : "" %>>mil p-p</option>
                </select>
            </div>
        </div>
    </div>

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

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            //$('#measurementscale').mobiscroll().select({
            //    theme: 'ios',
            //    display: popLocation,
            //    onSet: function (event, inst) {
            //        $('#save').click();

            //    }

            //});

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
