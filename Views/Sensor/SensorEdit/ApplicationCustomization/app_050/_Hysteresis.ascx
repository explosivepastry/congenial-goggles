<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";
        string label = "";
        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);
         
%>

<div class="form-group">
    <div class="bold col-md-3 col-sm-3 col-xs-12">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> &nbsp <%: label %>
    </div>
    <div class="col-md-9 col-sm-9 col-xs-12 mdBox" id="hyst3">

        <input class="aSettings__input_input" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=Hyst %>" />
        <a  id="hystNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="form-group">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <div id="Hysteresis_Slider"></div>
    </div>
</div>

<script type="text/javascript">

    //MobiScroll
    $(function () {
          <% if(Model.CanUpdate) { %>

        let arrayForSpinner = arrayBuilder(0, 10, 1);
        createSpinnerModal("hystNum", "Aware State Buffer", "Hysteresis_Manual", arrayForSpinner);

        <%}%>

        $("#Hysteresis_Manual").change(function () {
            if (isANumber($("#Hysteresis_Manual").val())) {
                if ($("#Hysteresis_Manual").val() < 0)
                    $("#Hysteresis_Manual").val(0);
                if ($("#Hysteresis_Manual").val() > 10)
                    $("#Hysteresis_Manual").val(10)

            }
            else {
                $('#Hysteresis_Manual').val(<%: Hyst%>);

        }


        });
    });
</script>
<%} %>