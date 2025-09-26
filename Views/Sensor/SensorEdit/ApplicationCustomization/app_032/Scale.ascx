<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />
    <h4 >
        <%: Html.TranslateTag("Overview/SensorScale|Measurement Scale ","Measurement Scale ")%>
    </h4>
    <div class="formBody">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|0 Volt Value:","0 Volt Value:")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="lowValue" name="lowValue" value="<%: (Monnit.AC_DC_500V.GetLowValue(Model.SensorID)) %>" />
            </div>
            <br />
        </div>

        <!--HighValue-->
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|500 Volt Value:","500 Volt Value:")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="highValue" name="highValue" value="<%: (Monnit.AC_DC_500V.GetHighValue(Model.SensorID)) %>" />
            </div>
            <br />
        </div>

        <!--Label-->
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|Label:","Label:")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="label" name="label" value="<%:  (Monnit.AC_DC_500V.GetLabel(Model.SensorID)) %>" />
            </div>
        </div>

        <script>
            $("#highValue").change(function () {
                if (!isANumber($("#highValue").val()))
                    $("#highValue").val(500);
            });

            $("#lowValue").change(function () {
                if (!isANumber($("#lowValue").val()))
                    $("#lowValue").val(0);
            });
        </script>
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

    <script>
        $(document).ready(function () {
            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
    </script>
</form>
