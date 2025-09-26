<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
   
    string FundamentalInterval = "";
    FundamentalInterval = Vibration800.GetFundamentalInterval(Model).ToString();  
       
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="FundamentalInterval_Manual" id="FundamentalInterval_Manual" value="<%=FundamentalInterval %>" />
        <a id="MeasurementIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>


<script type="text/javascript">
    var pmInterval_array = [1, 2, 5, 10, 30, 60, 120, 240, 360, 720];

    $(function () {

               <% if (Model.CanUpdate)
                  { %>

        createSpinnerModal("MeasurementIntNum", "Minutes", "FundamentalInterval_Manual", pmInterval_array);

        <%}%>

        $("#FundamentalInterval_Manual").addClass('editField editFieldMedium');

        $("#FundamentalInterval_Manual").change(function () {
            if (isANumber($("#FundamentalInterval_Manual").val())) {
                if ($("#FundamentalInterval_Manual").val() < 0.017)
                    $("#FundamentalInterval_Manual").val(0.017);
                if ($("#FundamentalInterval_Manual").val() > 720)
                    $("#FundamentalInterval_Manual").val(720);
            }
            else {
                $('#FundamentalInterval_Manual').val(<%: FundamentalInterval%>);
            }
        });
    });


</script>




