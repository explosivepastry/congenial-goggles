<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    
    int height = PeopleCounter.GetHeightUIValue(Model.SensorID);
    bool reportOnCount = Model.MinimumThreshold.ToInt().ToBool();
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_077|Height")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="height" name="height" class="form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%> >
            <option value="6" <%: height == 6 ?"selected":"" %>>6 to 8 ft / 180 to 240 cm</option>
            <option value="7" <%: height == 7 ?"selected":"" %>>7 to 9 ft / 210 to 275 cm</option>
            <option value="8" <%: height == 8 ?"selected":"" %>>8 to 10 ft / 240 to 305 cm</option>
            <option value="9" <%: height == 9 ?"selected":"" %>>9 to 11 ft / 275 to 335 cm</option>
        </select>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_077|Report on Count")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="ReportOnCount" id="ReportOnCount" <%= reportOnCount ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
    </div>
</div>

<h4>Aware State</h4>

<%--- Max Threshold ---%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_077|Occupancy Threshold")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="MaximumThreshold_Manual" name="MaximumThreshold_Manual" value="<%=Model.MaximumThreshold %>" />
        <!--<a id="maxNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>-->
    </div>
</div>

<script type="text/javascript">

    $('#MaximumThreshold_Manual').addClass("editField editFieldMedium");


    //MobiScroll Max Thresh
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        //let arrayForSpinner11 = arrayBuilder(-2147483647, 2147483647, 1);
        //createSpinnerModal("maxNum", "Occupancy Threshold", "MaximumThreshold_Manual", Hysteresis_array);

        <%}%>

        $('#MaximumThreshold_Manual').change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($('#MaximumThreshold_Manual').val() < -2147483647)
                    $('#MaximumThreshold_Manual').val(-2147483647);
                if ($('#MaximumThreshold_Manual').val() > 2147483647)
                    $('#MaximumThreshold_Manual').val(2147483647);
            }
            else {
                $('#MaximumThreshold_Manual').val(<%: Model.MaximumThreshold%>);
            }
        });

    });
</script>

