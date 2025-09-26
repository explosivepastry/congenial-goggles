<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
         
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row">
    <div class="col-12 col-md-3">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(0, 10, 1);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>
        $("#Hysteresis_Manual").addClass('editField editFieldSmall');

        $("#Hysteresis_Manual").change(function () {
            if(isANumber($("#Hysteresis_Manual").val())){
                if ($("#Hysteresis_Manual").val() < 0)
                $("#Hysteresis_Manual").val(0);
            if ($("#Hysteresis_Manual").val() > 10)
                $("#Hysteresis_Manual").val(10)
        }
        else
        {
            $("#Hysteresis_Manual").val(<%: Hyst%>);
        }
         });
    });
</script>
<%} %>