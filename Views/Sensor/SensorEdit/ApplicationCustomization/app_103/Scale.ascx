<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="formtitle">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Temperature Scale","Temperature Scale")%>
    </div>
    <div class="formBody">

        <%  bool isFahrenheit = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
            }

            string label = DifferentialPressure.GetLabel(Model.SensorID);
            double getSavedVal = DifferentialPressure.GetSavedValue(Model.SensorID);

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

        <div class="formtitle">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Pressure Scale","Pressure Scale")%>
        </div>
        <div class="formBody">
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Scale","Scale")%>
                </div>
                <div class="col sensorEditFormInput">
                    <select id="label" name="label" class="form-select">
                        <option value="Pascal" <%: label.Contains("Pascal") ? "selected":"" %>>Pascal</option>
                        <option value="torr" <%: label.Contains("torr") ? "selected":"" %>>Torr</option>
                        <option value="psi" <%: label.Contains("psi") ? "selected":"" %>>PSI</option>
                        <option value="inAq" <%: label.Contains("inAq") ? "selected":"" %>>inH20</option>
                        <option value="inHg" <%: label.Contains("inHg") ? "selected":"" %>>inHg</option>
                        <option value="mmHg" <%: label.Contains("mmHg") ? "selected":"" %>>mmHg</option>
                        <option value="mmwc" <%: label.Contains("mmwc") ? "selected":"" %>>mm Water Column</option>
                        <option value="Custom" <%: label.Contains("Custom") ? "selected":"" %>>Custom</option>
                    </select>
                </div>
            </div>

            <div hidden>
                <img alt="help" class="helpIcon" title="Scale and label of the formatted output for this sensor" src="<%:Html.GetThemedContent("/images/help.png")%>" />
                <input class="aSettings__input_input" type="text" id="lowValue" name="lowValue" value="<%:Monnit.Pressure50PSI.GetLowValue(Model.SensorID) %>" />
                <input class="aSettings__input_input" type="text" id="highValue" name="highValue" value="<%:Monnit.Pressure50PSI.GetHighValue(Model.SensorID) %>" />
                <input class="aSettings__input_input" type="text" id="customLabel" name="customLabel" value="<%:Monnit.Pressure50PSI.GetCustomLabel(Model.SensorID) %>" />
                <input class="aSettings__input_input" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.PressureNPSI.GetDecimalTrunkValue(Model.SensorID) %>" />
                <img alt="help" class="decimalHelpIcon" title="Allowable decimal range is from 0 - 5" src="<%:Html.GetThemedContent("/images/help.png")%>" />
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
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
