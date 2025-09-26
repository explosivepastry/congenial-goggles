<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (new Version(Model.FirmwareVersion) >= new Version("2.2.0.0"))
    {%>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="col sensorEditFormInput" style="justify-content:space-between; max-width:273px">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3Chk" id="Calibration3Chk" <%= Model.Calibration3 == 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <button class="btn btn-secondary" style="width: 112px;" type="button" id="ResetAccumulator" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Reset Accumulator","Reset Accumulator")%>">
            <%: Html.TranslateTag("Reset","Reset")%>
        </button>
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
<%} %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Pulse Edge Detection","Pulse Edge Detection")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="edge" class="form-select" name="edge" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="0" <%:Model.Calibration1 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Positive Edge","Positive Edge")%></option>
            <option value="1" <%:Model.Calibration1 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Negative Edge","Negative Edge")%></option>
            <option value="2" <%:Model.Calibration1 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Both Edges","Both Edges")%></option>
        </select>
    </div>
</div>
<p class="useAwareState"></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|Filter","Filter")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="filter" class="form-select" name="filter" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="2" <%:Model.Calibration4 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_090|No Filter","No Filter")%></option>
            <option value="1" <%:Model.Calibration4 == 1 ? "selected='selected'" : "" %>>40 Hz <%: Html.TranslateTag("Filter","Filter")%></option>
            <option value="0" <%:Model.Calibration4 == 0 ? "selected='selected'" : "" %>>4 Hz <%: Html.TranslateTag("Filter","Filter")%></option>
        </select>
    </div>
</div>

<script type="text/javascript">


    //MobiScroll
    $(function () {
        //$('#filter').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 200
        //});


        //$('#edge').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 200
        //});

    });



</script>
