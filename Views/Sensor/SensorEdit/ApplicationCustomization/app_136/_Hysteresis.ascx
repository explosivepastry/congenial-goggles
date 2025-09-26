<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isF = LCD_Temperature.IsFahrenheit(Model.SensorID);
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%: Html.Label(isF ?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput" id="hyst3">

        <input class="form-control" type="number"  <%=Model.CanUpdate ? "" : "disabled"  %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
    
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%
    double defaultHystMin = 0;
    double defaultHystMax = 5;

    if(isF)
    {
        defaultHystMin = (defaultHystMin * 1.8);
        defaultHystMax = (defaultHystMax * 1.8);
    }

%>


<script type="text/javascript">

    var hystMin = Number(<%=defaultHystMin%>);
    var hystMax = Number(<%=defaultHystMax%>);

    $(function () {
          <% if (Model.CanUpdate)
             { %>
        const arrayForSpinner = arrayBuilder(hystMin, hystMax, 1);
        createSpinnerModal("hystNum", " <%:Html.Raw(isF ? "\u00B0 F" : "\u00B0 C")%>", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < hystMin)
                    $("#Hysteresis_Manual").val(hystMin);
                if ($("#Hysteresis_Manual").val() > hystMax)
                    $("#Hysteresis_Manual").val(hystMax)

            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

        }


        });
    });
</script>
<%} %>