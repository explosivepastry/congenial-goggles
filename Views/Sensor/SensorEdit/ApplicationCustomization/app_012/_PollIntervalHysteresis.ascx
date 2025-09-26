<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_012|Poll Interval","Poll Interval")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <%: label %>
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="form-group">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    //Use Report interval and minReport inteval from heartbeat above
    var Hysteresis_array = [1, 2, 5, 10, 20, 30, 60];

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("hystNum", "Minutes", "Hysteresis_Manual", Hysteresis_array);

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');
        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                //Check if less than min
                if ($("#Hysteresis_Manual").val() < 1)
                    $("#Hysteresis_Manual").val(1);

                //Check if greater than max
                var max = Number($('#ReportInterval').val());
                if ($("#Hysteresis_Manual").val() > max)
                    $("#Hysteresis_Manual").val(max);
            }
            else {
                $("#Hysteresis_Manual").val(<%: Hyst%>);
            }
        });
        <%}%>        
    });
</script>
<%}%> 