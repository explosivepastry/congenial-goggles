<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Get Pressure label for profile
        string label = "";
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);

        //Get the Hyst, Max, Min Values for Pascal
        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Below","Below")%> (Amps)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
       
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<script>
    $('#MinimumThreshold_Manual').addClass("editField editFieldMedium");

    //MobiScroll
    $(function () {
        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(0, 120, 5);
        createSpinnerModal("minThreshNum", "Amps", "MinimumThreshold_Manual", arrayForSpinner);
<%}%>
         

        $('#MinimumThreshold_Manual').change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($('#MinimumThreshold_Manual').val() < 0)
                    $('#MinimumThreshold_Manual').val(0);

                if ($('#MinimumThreshold_Manual').val() > 120 )
                    $('#MinimumThreshold_Manual').val(120);

                if (parseFloat($("#MinimumThreshold_Manual").val()) > parseFloat($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val(parseFloat($("#MaximumThreshold_Manual").val()));
            }
            else
                $('#MinimumThreshold_Manual').val(<%: Min%>);

         });

    });
</script>
<%} %>