<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    string valueForZero = QuadContact.GetZeroValue(Model.SensorID);
    string valueForOne = QuadContact.GetOneValue(Model.SensorID);
    int EventDetectionTypeInput1 = QuadContact.GetEventDetectionTypeInput1(Model);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Event Detection Type","Event Detection Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select ms-0" name="EventDetectionTypeInput1_Manual" id="EventDetectionTypeInput1_Manual" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
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
