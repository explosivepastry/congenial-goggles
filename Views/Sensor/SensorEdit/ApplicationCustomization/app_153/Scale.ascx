<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formtitle">
        <%: Html.TranslateTag("Overview/SensorScale|Channel 1","Channel 1")%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|1 Pulse Equals","1 Pulse Equals")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" min="0" id="transformValue_Channel1" name="transformValue_Channel1" value="<%:Monnit.TwoInputPulseCounter.GetTransform_Channel1(Model.SensorID) %>" />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="label_Channel1" name="label_Channel1" value="<%:Monnit.TwoInputPulseCounter.GetLabel_Channel1(Model.SensorID) %>" />
        </div>
    </div>

    <div class="formtitle">
        <%: Html.TranslateTag("Overview/SensorScale|Channel 2","Channel 2")%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|1 Pulse Equals","1 Pulse Equals")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" min="0" id="transformValue_Channel2" name="transformValue_Channel2" value="<%:Monnit.TwoInputPulseCounter.GetTransform_Channel2(Model.SensorID) %>" />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="label_Channel2" name="label_Channel2" value="<%:Monnit.TwoInputPulseCounter.GetLabel_Channel2(Model.SensorID) %>" />
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

    <script>
        $(document).ready(function () {

            $("#transformValue_Channel1").change(function () {
                if (parseFloat($("#transformValue_Channel1").val()) <= 0) {
                    $("#transformValue_Channel1").val(1);
                }
            });

            $("#transformValue_Channel2").change(function () {
                if (parseFloat($("#transformValue_Channel2").val()) <= 0) {
                    $("#transformValue_Channel2").val(1);
                }
            });

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });
    </script>
</form>
