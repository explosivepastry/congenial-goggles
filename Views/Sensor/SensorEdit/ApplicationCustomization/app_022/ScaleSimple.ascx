<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formtitle">
        <%: Html.TranslateTag("Overview/SensorScale|Scale Settings","Scale Settings")%>
    </div>
    <div class="formBody">

        <!--LowValue-->
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|4 mA Value","4 mA Value")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="lowValue" name="lowValue" value="<%: (Monnit.ZeroToTwentyMilliamp.Get4maValue(Model.SensorID)) %>" />
            </div>
            <br />

        </div>
        <!--HighValue-->
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|20 mA Value","20 mA Value")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="highValue" name="highValue" value="<%: (Monnit.ZeroToTwentyMilliamp.GetHighValue(Model.SensorID)) %>" />
            </div>
            <br />
        </div>
        <!--Label-->
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3"><%: Html.TranslateTag("Overview/SensorScale|Label","Label")%></div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="label" name="label" value="<%: (Monnit.ZeroToTwentyMilliamp.GetLabel(Model.SensorID)) %>" />
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

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });

        });

        function SetScaleValues() {
            var optionval = $("#badgeSelector option:selected")

            $('#lowValue').val($(optionval).data('low'));
            $('#highValue').val($(optionval).data('high'));
            $('#label').val($(optionval).data('lbl'));
        }

    </script>
</form>
