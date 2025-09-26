<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
               
                string Min = "";
                string Max = "";
                MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
                
     if (Model.MinimumThreshold == 4294967295)
        Model.MinimumThreshold = 0;
     string label = "";
     MonnitApplicationBase.ProfileLabelForScale(Model, out label);
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>
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

    //MobiScroll
    $(function () {
        var MinThresMinVal = 0;
        var MinThresMaxVal = 225;
     

        <% if (Model.CanUpdate)
           { %>

        let arrayForSpinner = arrayBuilder(0, 225, 10);
        createSpinnerModal("minThreshNum", "Amps", "MinimumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())){
                if ($("#MinimumThreshold_Manual").val() < MinThresMinVal)
                    $("#MinimumThreshold_Manual").val(MinThresMinVal);
                if ($("#MinimumThreshold_Manual").val() > MinThresMaxVal)
                    $("#MinimumThreshold_Manual").val(MinThresMaxVal);


                if (Number($("#MinimumThreshold_Manual").val()) > Number($("#MaximumThreshold_Manual").val()))
                    $("#MinimumThreshold_Manual").val($("#MaximumThreshold_Manual").val());

            }else{

                $("#MinimumThreshold_Manual").val(<%: Min%>);
            }
        });

    });
</script>
