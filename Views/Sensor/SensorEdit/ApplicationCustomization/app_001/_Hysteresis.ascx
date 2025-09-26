<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);     
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Hysteresis","Hysteresis")%> <%: label %>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input class="form-control user-dets" type="number" <%=Model.CanUpdate ? "" : "disabled"  %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<p class="useAwareState"></p>

<script type="text/javascript">
    $('#Hysteresis_Manual').addClass("editField editFieldMedium");

    var lowVal = <%: Analog.GetLowValue(Model.SensorID).ToDouble()%>;
    var highVal = <%: Analog.GetHighValue(Model.SensorID).ToDouble()%>;

    $(function () {
          <% if (Model.CanUpdate)
             { %>
        createSpinnerModal("hystNum", "Hysteresis", "Hysteresis_Manual", [0, 1]);
      
        $('#Hysteresis_Manual').change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($('#Hysteresis_Manual').val() < 0)
                    $('#Hysteresis_Manual').val(0);

                if ($('#Hysteresis_Manual').val() > ((highVal - lowVal)))
                    $('#Hysteresis_Manual').val(((highVal - lowVal)));
            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);
            }

        });
        <%}%>
  
    });
</script>
<%} %>