<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%--Calval1 sets Edge Detection--%>
<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_080|Pulse Edge Detection","Pulse Edge Detection")%>
	</div>
	<div class="col sensorEditFormInput">
		  <select id="edge" class="form-select" name="edge" style="margin-left: 0;">
              <option value="0" <%:Model.Calibration1 == 0 ? "selected='selected'" : "" %>>Positive Edge</option>
              <option value="1" <%:Model.Calibration1 == 1 ? "selected='selected'" : "" %>>Negative Edge</option>
          </select>
	</div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_080|Logged Delivery Time","Logged Delivery Time")%> Seconds
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="debounce" id="debounce" value="<%: Model.Calibration4 /  1000 %>" />
        <a  id="debounceNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>



<script type="text/javascript">


    $(function () {

        $("#edge").addClass('editField editFieldSmall');
  
        let arrayForSpinner = arrayBuilder(3, 30, 1);
        createSpinnerModal("debounceNum", "Seconds", "debounce", arrayForSpinner);

        $("#debounce").addClass('editField editFieldMedium');
        $("#debounce").change(function () {
            //Check if less than min
            if (isANumber($("#debounce").val())) {
                if ($("#debounce").val() < 3)
                    $("#debounce").val(3);

                //Check if greater than max
                if ($("#debounce").val() > 30)
                    $("#debounce").val(30);
            }
            else {
                $("#debounce").val(<%: Model.Calibration4 /  1000 %>);
        }
        });
    });



</script>
