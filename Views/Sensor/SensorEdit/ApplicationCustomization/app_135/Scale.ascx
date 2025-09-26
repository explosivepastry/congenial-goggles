<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formBody">
        <%  
            string label = SoilMoisture.GetLabel(Model.SensorID);
            bool isFahrenheit = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
            }
        %>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Temperature Values in","Show Temperature Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="TempScale" name="tempscale" class="form-select">
                    <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit","Fahrenheit")%>
                    </option>
                    <option style="display: flex; justify-content: space-between;" value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius","Celsius")%>
                    </option>
                </select>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Moisture Tension Values in","Show Moisture Tension Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="moistureScale" name="label" class="form-select">
                    <option value="centibars" <%=label == "centibars" ? "selected='selected'" : "" %>>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_135|Centibars","Centibars")%>
                    </option>
                    <option style="display: flex; justify-content: space-between;" value="kPa" <%= label == "kPa" ?  "selected='selected'" : "" %>>
                        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_135|kPa","kPa")%>
                    </option>
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
        <button class="btn btn-primary" type="button" id="save" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            //$('#moistureScale').select({
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
