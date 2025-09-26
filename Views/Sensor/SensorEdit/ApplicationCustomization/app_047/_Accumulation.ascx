<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3Chk" id="Calibration3Chk" <%= Model.Calibration3 == 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.Calibration3, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%: Html.ValidationMessageFor(model => model.Calibration3)%>
    </div>
</div>

<script type="text/javascript">

    $('#Calibration3Chk').change(function () {
        if ($('#Calibration3Chk').prop('checked')) {
            $('#Calibration3').val(1);
        } else {
            $('#Calibration3').val(0);
        }
    });
</script>


<%--Calval1 sets Edge Detection--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_047|Pulse Edge Detection","Pulse Edge Detection")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="edge" class="form-select ms-0" name="edge" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%:Model.Calibration1 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Positive Edge","Positive Edge")%></option>
            <option value="1" <%:Model.Calibration1 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Negative Edge","Negative Edge")%></option>
            <option value="2" <%:Model.Calibration1 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Both Edges","Both Edges")%></option>
        </select>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Stabilization Delay","Stabilization Delay")%> (ms)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="debounce" id="debounce" value="<%: Model.Calibration2 %>" />
        <a id="debounceNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%} %>

<script type="text/javascript">
    var debounceInterval_array = [12, 20, 50, 100, 200, 300, 500, 1000];

    $(function () {

        createSpinnerModal("debounceNum", "Milliseconds", "debounce", debounceInterval_array);

        $("#edge").addClass('editField editFieldSmall');
        $("#debounce").addClass('editField editFieldMedium');
        $("#debounce").change(function () {
            //Check if less than min
            if (isANumber($("#debounce").val())) {
                if ($("#debounce").val() < 12)
                    $("#debounce").val(12);
                //Check if greater than max
                if ($("#debounce").val() > 60000)
                    $("#debounce").val(60000);
            }
            else {
                $("#debounce").val(<%: Model.Calibration2 %>);
            }
        });
    });

</script>
