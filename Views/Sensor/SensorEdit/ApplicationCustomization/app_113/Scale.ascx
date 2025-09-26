<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/Overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_113|0 Volt Value:","0 Volt Value:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" id="lowValue" type="text" name="lowValue" value="<%:Monnit.ZeroToTwoHundredVolts.GetLowValue(Model.SensorID) %>" />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_113|200 Volt Value:","200 Volt Value:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" id="highValue" type="text" name="highValue" value="<%:Monnit.ZeroToTwoHundredVolts.GetHighValue(Model.SensorID) %>" />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_113|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" id="label" type="text" name="label" value="<%:Monnit.ZeroToTwoHundredVolts.GetLabel(Model.SensorID) %>" />
        </div>
    </div>

    <span style="color: red;">
        <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
    </span>
    <span style="color: black;">
        <%: ViewBag.Message == null ? "":ViewBag.Message %>
    </span>

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

