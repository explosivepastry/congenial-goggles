<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    UInt16 max = 0xFFFF;
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        double temp = Hyst.ToDouble() / 60;
        Hyst = temp.ToString();

%>

<p class="useAwareState"></p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Poll Rate","Poll Rate")%> (Minutes)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<script type="text/javascript">

    //Use Report interval and minReport inteval from heartbeat above
    var Hysteresis_array = [0.25, 1, 2, 5, 10, 20, 30, 60];

    $(function () {
          <% if (Model.CanUpdate)
    { %>

        createSpinnerModal("hystNum", "Minutes", "Hysteresis_Manual", Hysteresis_array);

        $("#Hysteresis_Manual").addClass('editField editFieldSmall');
        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                //Check if less than min
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(.25);

                //Check if greater than max
                if ($("#Hysteresis_Manual").val() > 720)
                    $("#Hysteresis_Manual").val(720);
            }
            else {
                $("#Hysteresis_Manual").val(<%: Hyst%>);
                    }
                });
        <%}%>        
    });
</script>
<%}%> 