<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string initialTypeOfBridge = Monnit.ResistiveBridgeMeter.GetScaleTypeOfBridge(Model.SensorID);
%>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formtitle boldText">
        <%: Html.TranslateTag("Overview/SensorScale|Sensor Scale","Sensor Scale")%>
    </div>
    <div class="formBody">

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Overview/SensorScale|Type of bridge","Type of bridge")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="typeOfBridgeSelect" class="form-select" onchange="changeUnitSelectorHandler(event, null)" name="TypeOfBridge">
                    <option value="null" <%= initialTypeOfBridge == null ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Raw Value","Raw Value")%></option>
                    <option value="Load Cell" <%= "Load Cell" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Load Cell","Load Cell")%></option>
                    <option value="Pressure Sensor" <%= "Pressure Sensor" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Pressure Sensor","Pressure Sensor")%></option>
                    <option value="Displacement Sensor(Half & Full Bridge)" <%= "Displacement Sensor(Half & Full Bridge)" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Displacement Sensor(Half & Full Bridge)","Displacement Sensor(Half & Full Bridge)")%></option>
                    <option value="Torque Sensor" <%= "Torque Sensor" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Torque Sensor","Torque Sensor")%></option>
                    <option value="Inclinometer" <%= "Inclinometer" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Inclinometer","Inclinometer")%></option>
                    <option value="Strain" <%= "Strain" == initialTypeOfBridge ? "selected='selected'" : "" %>><%: Html.TranslateTag("Overview/SensorScale|Strain","Strain")%></option>
                </select>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Overview/SensorScale|Unit","Unit")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="UnitSelect" class="form-select" name="Label">
                </select>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Overview/SensorScale|Capacity","Capacity")%> (<span id="CapacityUnitDisplay"><%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %></span>)
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="number" step="any" id="Capacity" name="Capacity" value="<%: Monnit.ResistiveBridgeMeter.GetScaleCapacity(Model.SensorID) %>" />
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Overview/SensorScale|Rated Output","Rated Output")%> (mV/V)
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="number" step="any" id="RatedOutput" name="RatedOutput" value="<%: Monnit.ResistiveBridgeMeter.GetRatedOutput(Model.SensorID) %>" />
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Overview/SensorScale|Precision","Precision")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="Precision" name="Precision" class="form-select">
                    <%
                        int precision = Monnit.ResistiveBridgeMeter.GetPrecision(Model.SensorID);
                        for (int i = 0; i < 11; i++)
                        {
                            String displayValue = "0";
                            if (i > 0)
                            {
                                displayValue += ".";
                                for (int j = 0; j < i; j++)
                                {
                                    displayValue += "0";
                                }
                            }
                    %>
                    <option value="<%= i %>" <%= precision == i ? "selected='selected'" : "" %>><%= displayValue %></option>
                    <%
                        }
                    %>
                </select>
            </div>
        </div>
    </div>

    <div class="formtitle boldText" style="margin-top: 0.5rem;">
        <%: Html.TranslateTag("Overview/SensorScale|Temperature Scale","Temperature Scale")%>
    </div>
    <div class="formBody">

        <%  
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
                <%: Html.TranslateTag("Overview/SensorScale|Show Temperature Values in","Show Temperature Values in")%>
            </div>
            <div class="col sensorEditFormInput">
                <select id="TempScale" name="TempScale" class="form-select">
                    <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %>><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></option>
                    <option value="<%="off"%>" <%=isFahrenheit ? "" : "selected='selected'" %>><%: Html.TranslateTag("Celsius","Celsius")%></option>
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
    <div class="ln_solid"></div>

    <div class="text-end">
        <button type="button" id="default" class="btn btn-secondary" style="float: none;">
            <%: Html.TranslateTag("Default","Default")%>
        </button>

        <button class="btn btn-primary" type="button" id="save" style="float: none; margin-right: 10px;">
            <%: Html.TranslateTag("Save","Save")%>
        </button>

        <div style="clear: both;"></div>
    </div>

    <input type="hidden" name="Reset" id="reset" value="0" />

    <script type="text/javascript">
        var defaultSure = "<%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons|Are you sure you want to set your calibration back to default?","Are you sure you want to set your calibration back to default?")%>";
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });

            $('#default').click(function () {
                $('#reset').val(1);
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

        const EnableSaveButton = (shouldEnable) => {
            const saveButton = document.querySelector("#save");

            if (shouldEnable) {
                saveButton.removeAttribute("disabled");
            } else {
                saveButton.setAttribute("disabled", "disabled");
            }
        }

        const unitOfMeasureObj = {
            "null": ["mV/V"],
            "Load Cell": ["gf", "kgf", "tf", "N", "kN", "lbs", "oz"],
            "Pressure Sensor": ["Bar", "kpa", "Mpa", "kg/cm²", "kg/m²", "PSI", "Torr"],
            "Displacement Sensor(Half & Full Bridge)": ["mm", "cm", "m", "in", "ft"],
            "Torque Sensor": ["N·m", "kgf·m", "kgf·cm", "ft·lb", "in·lb"],
            "Inclinometer": ["degree", "rad", "grade"],
            "Strain": ["με"]
        }

        const changeUnitSelectorHandler = (e, initialLoadValue) => {
            //if (e !== null && e.target.value !== "null") {
            //    EnableSaveButton(true);
            //} else {
            //    EnableSaveButton(false);
            //}

            const arrayToDisplay = unitOfMeasureObj[
                initialLoadValue ?
                    initialLoadValue :
                    e.target.value
            ];

            const selectElement = document.getElementById('UnitSelect');
            selectElement.innerHTML = '';


            const initialUnitsFromBackend = "<%: Monnit.ResistiveBridgeMeter.GetLabel(Model.SensorID) %>"

            arrayToDisplay.forEach(unitOfMeasure => {
                const option = document.createElement("option");
                option.value = unitOfMeasure;
                option.textContent = unitOfMeasure;
                if (option.value == initialUnitsFromBackend) {
                    option.selected = true;
                }
                selectElement.appendChild(option);
            });
        }

        const intialTypeOfBridgeValue = document.querySelector("#typeOfBridgeSelect").value;
        changeUnitSelectorHandler(null, intialTypeOfBridgeValue)

        $('#UnitSelect').on("change", function () {
            $('#CapacityUnitDisplay').html($(this).val());
        })

    </script>
</form>
