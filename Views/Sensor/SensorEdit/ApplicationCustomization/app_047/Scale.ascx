<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />



    <div class="formBody">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_047|1 Pulse Equals","1 Pulse Equals")%>
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="number" id="transformValue" name="transformValue" value="<%:Monnit.FilteredPulseCounter64.GetTransform(Model.SensorID) %>" />
            </div>
        </div>
        <div class="clear"></div>
        <br />


        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_047|Label","Label")%>
            </div>
            <div class="col sensorEditFormInput">
                 <input class="form-control" type="text" id="label" name="label" value="<%:Monnit.FilteredPulseCounter64.GetLabel(Model.SensorID) %>" />
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

            //$('#TempScale').mobiscroll().select({
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
