<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="formtitle">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_003|Dry Contact Scale","Dry Contact Scale")%>
    </div>
    <div class="formBody">

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_003|Closed Value","Closed Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" id="zeroValueLabel" class="form-control" name="zeroValueLabel" value="<%:  (Monnit.DryContact.GetZeroValue(Model.SensorID)) %>" />
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_003|Open  Value","Open  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" id="oneValueLabel" class="form-control" name="oneValueLabel" value="<%: (Monnit.DryContact.GetOneValue(Model.SensorID)) %>" />
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
        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
