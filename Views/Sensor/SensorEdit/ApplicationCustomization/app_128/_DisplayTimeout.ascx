<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|LCD Display Timeout","LCD Display Timeout")%> (<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Seconds","Seconds")%>)
    </div>
    <div class="col sensorEditFormInput" id="dto">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="DisplayTimeOut_Manual" id="DisplayTimeOut_Manual" value="<%=Model.Calibration1 %>" />
        <a  id="dtoNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration1)%>
    </div>
</div>

<script type="text/javascript">

    $(function () {
          <% if(Model.CanUpdate) { %>

        const arrayForSpinner = arrayBuilder(5, 300, 5);
        createSpinnerModal("dtoNum", "Seconds", "DisplayTimeOut_Manual", arrayForSpinner);

        <%}%>

        $("#DisplayTimeOut_Manual").addClass('editField editFieldSmall');

        $("#DisplayTimeOut_Manual").change(function () {
            if (isANumber($("#DisplayTimeOut_Manual").val())) {
                if ($("#DisplayTimeOut_Manual").val() < 5)
                    $("#DisplayTimeOut_Manual").val(5);
                if ($("#DisplayTimeOut_Manual").val() > 300)
                    $("#DisplayTimeOut_Manual").val(300)
            }
            else {
                $('#DisplayTimeOut_Manual').val(<%:Model.Calibration1%>);
                }
        });
    });
</script>
