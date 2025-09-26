<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Max = AirFlow.MaxThreshForUI(Model);
    string Min = AirFlow.MinThreshForUI(Model);
    string hyst = AirFlow.HystForUI(Model);
%>
<p class="useAwareState"><%: Html.TranslateTag("Use Aware State","Use Aware State")%></p>

<%--Min Threshold--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (Kohms)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=Min %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<%-- Max Thresh --%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> (Kohms)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=Max %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<script>
    $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

    let min = Number($("#MinimumThreshold_Manual").val());
    let max = Number($("#MaximumThreshold_Manual").val());

    //MobiScroll
    $(function () {
                <% if (Model.CanUpdate)
                   { %>

        let arrayForSpinnerMin = arrayBuilder(0, 100, 1);
        createSpinnerModal("minThreshNum", "Below", "MinimumThreshold_Manual", arrayForSpinnerMin);

         <%}%>

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber(min)) {
                if (min < 0)
                    $("#MinimumThreshold_Manual").val(0);
                if (min > 100)
                    $("#MinimumThreshold_Manual").val(100);

                if (min > max)
                    $("#MinimumThreshold_Manual").val(max);
            }
            else{
                $('#MinimumThreshold_Manual').val(<%: Min%>);
        }
        });

    });


    $(function () {
        var MaxThresMinVal = <%=Min%>;
        var MaxThresMaxVal = <%=Max%>;

               <% if (Model.CanUpdate)
                  { %>

        let arrayForSpinnerMax = arrayBuilder(0, 100, 1);
        createSpinnerModal("maxThreshNum", "Above", "MaximumThreshold_Manual", arrayForSpinnerMax);

        <%}%>

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber(max)) {
                if (max < 0)
                    $("#MaximumThreshold_Manual").val(0);
                if (max > 100)
                    $("#MaximumThreshold_Manual").val(100);

                if (max < min)
                    $("#MaximumThreshold_Manual").val(min);
            }
            else{
                $('#MaximumThreshold_Manual').val(<%: Max%>);
        }
        });

    });
</script>