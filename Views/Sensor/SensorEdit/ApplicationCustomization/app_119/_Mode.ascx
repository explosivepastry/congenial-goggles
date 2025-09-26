<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<script type="text/javascript">
    $(document).ready(function () {

        var mode = '<%= Model.Calibration3%>';
        $('#Mode').val(mode);

        if (mode == 0) {
            $("#DW").hide();
        }

    });

</script>

<% int calVal3 = Current.GetCalVal3Upper(Model);  %>

<p class="useAwareState"></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_119|Mode","Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="Mode" class="form-select ms-0" name="Mode" <%: Model.CanUpdate ?"":"disabled" %>>
            <option value="0" <%: calVal3 == 0 ? "selected" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_119|One Axel Trigger","One Axle Trigger")%></option>
            <option value="1" <%: calVal3 == 1 ? "selected" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_119|Two Axel Trigger","Two Axle Trigger")%></option>
            <option value="2" <%: calVal3 == 2 ? "selected" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_119|Two Axel Count","Two Axle Count")%></option>
        </select>
    </div>
</div>

<%-- Detection --%>
<div class="row sensorEditForm" id="DW">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_119|Detection Window (seconds)","Detection Window (seconds)")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" class="form-control" <%: Model.CanUpdate ?"":"disabled" %> name="Detection" id="Detection" value="<%: (Model.Calibration1/1000.0) %>" />
        <a id="detNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>


<%--Rearm Time--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Re-Arm Time (seconds)","Re-Arm Time (seconds)")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> id="RearmWindow" name="RearmWindow" value="<%= (Model.Calibration2/1000.0) %>" />
        <a id="rwNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration2)%>
    </div>
</div>

<script type="text/javascript">

    $('#Mode').addClass("form-control");
    $('#Detection').addClass("form-control");
    $('#RearmWindow').addClass("form-control");


    <% if (Model.CanUpdate)
    { %>

    $(function () {

        var mode = '<%= Model.Calibration3%>';
        $('#Mode').val(mode);

        if (mode == 0) {
            $("#Detection").val(1).prop('disabled', true);
            $("#DW").hide();
        }


        $("#Mode").change(function () {

            $("#Detection").prop('disabled', false);
            $("#DW").show()

            if ($("#Mode").val() == 0) {
                $("#RearmWindow").val(<%: (Model.Calibration2/1000.0) %>);
                $("#DW").hide()
            }
            else if ($("#Mode").val() == 1) {
                $("#Detection").val(<%: (Model.Calibration1/1000.0) %>);
                $("#RearmWindow").val(<%: (Model.Calibration2/1000.0) %>);
            }
            else if ($("#Mode").val() == 2) {
                $("#Detection").val(<%: (Model.Calibration1/1000.0) %>);
                $("#RearmWindow").val(<%: (Model.Calibration2/1000.0) %>);
            }
        });

        const arrayForSpinner = arrayBuilder(1, 30, 1);
        createSpinnerModal("detNum", "Seconds", "Detection", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(1, 120, 1);
        createSpinnerModal("rwNum", "Seconds", "RearmWindow", arrayForSpinner1);

        $("#Detection").change(function () {
            if (isANumber($("#Detection").val())) {
                if ($("#Detection").val() < 1)
                    $("#Detection").val(1);
                if ($("#Detection").val() > 30)
                    $("#Detection").val(30)
            }
            else {
                $('#Detection').val(<%: (Model.Calibration1/1000.0) %>);
            }
        });

        $("#RearmWindow").change(function () {
            if (isANumber($("#RearmWindow").val())) {
                if ($("#RearmWindow").val() < 1)
                    $("#RearmWindow").val(1);
                if ($("#RearmWindow").val() > 120)
                    $("#RearmWindow").val(120)
            }
            else {
                $("#RearmWindow").val(<%: (Model.Calibration2/1000.0) %>);
            }
        });
    });
        <%}%>



</script>
