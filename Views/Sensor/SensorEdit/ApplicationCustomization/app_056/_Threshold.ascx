<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    //H2S Instantaneous values
    double Max = Gas_H2S.GetGasMaxThresh(Model);
    double Min = Gas_H2S.GetGasMinThresh(Model);
%>

<p class="useAwareState"></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (ppm)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%
    double DefaultMin = 0;
    double DefaultMax = 50;                      
%>
<script>


    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinner = arrayBuilder(0, 50, 1);
        createSpinnerModal("maxThreshNum", "Above ppm", "MaximumThreshold_Manual", arrayForSpinner);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < <%:(DefaultMin)%>)
                $("#MaximumThreshold_Manual").val(<%:(DefaultMin)%>);
                if ($("#MaximumThreshold_Manual").val() > <%:(DefaultMax)%>)
                    $("#MaximumThreshold_Manual").val(<%:(DefaultMax)%>);
            }
            else{
                $('#MaximumThreshold_Manual').val(<%: Max%>);
            }
        });

    });
</script>
