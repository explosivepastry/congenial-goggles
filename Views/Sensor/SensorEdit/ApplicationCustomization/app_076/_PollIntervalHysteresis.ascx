<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string Cal2 = "";
    MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Cal2);
    Cal2 = BasicControl.Cal2ForUI(Model);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_076|Poll Interval","Poll Interval")%> &nbsp minutes
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Cal2" id="Cal2" value="<%=Cal2 %>" />
        <a  id="cal2Num" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<script type="text/javascript">

    //Use Report interval and minReport inteval from heartbeat above
    var Hysteresis_array = [0.25, 1, 2, 5, 10, 20, 30, 60];
    var max = Number($('#ReportInterval').val());

    //MobiScroll
    $(function () {
          <% if(Model.CanUpdate) { %>

        createSpinnerModal("cal2Num", "Minutes", "Cal2", Hysteresis_array);

        $("#Cal2").addClass('editField editFieldSmall');
        $("#Cal2").change(function () {
            if (isANumber($("#Cal2").val())) {
                //Check if less than min
                if ($("#Cal2").val() < 0)
                    $("#Cal2").val(.25);

                //Check if greater than max
                if ($("#Cal2").val() > max)
                    $("#Cal2").val(max);
            }
            else {
                $("#Cal2").val(<%: Cal2%>);
                }
                });
        <%}%>        
    });
</script>