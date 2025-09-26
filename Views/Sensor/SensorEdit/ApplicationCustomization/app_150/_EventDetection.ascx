<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string valueForZero = Motion_RH_WaterDetect.ValueForZero;
    string valueForOne = Motion_RH_WaterDetect.ValueForOne;
    int EventDetectionTypeInput1 = Motion_RH_WaterDetect.GetAwareOnWater(Model);
%>

<h5 style="font-weight:bold;">Water:</h5>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Enter Aware State when water is","Enter Aware State when water is")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select ms-0" name="AwareOnWater" id="EventDetectionTypeInput1_Manual" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput1 == 0 ? "selected='selected'" : "" %>><%=valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput1 == 1 ? "selected='selected'" : "" %>><%=valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput1 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>

<script>
    
          <% if (Model.CanUpdate)
             { %>
    //MobiScroll
    //$(function () {
    //    $('#EventDetectionTypeInput1_Manual').mobiscroll().select({
    //        theme: 'ios',
    //        display: popLocation,
    //        minWidth: 200
    //    });
    //});

    <%}%>

</script>
