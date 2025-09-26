<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  
           string datumName0 = Model.GetDatumName(0);
        string datumName1 = Model.GetDatumName(1);
        string datumName2 = Model.GetDatumName(2);
        string datumName3 = Model.GetDatumName(3);
        string datumName4 = Model.GetDatumName(4);

    string Contact1_valueForZero = FiveInputDryContact.GetContact_ZeroValue(Model.SensorID, 1);
    string Contact1_valueForOne = FiveInputDryContact.GetContact_OneValue(Model.SensorID, 1);
    string Contact2_valueForZero = FiveInputDryContact.GetContact_ZeroValue(Model.SensorID, 2);
    string Contact2_valueForOne = FiveInputDryContact.GetContact_OneValue(Model.SensorID, 2);
    string Contact3_valueForZero = FiveInputDryContact.GetContact_ZeroValue(Model.SensorID, 3);
    string Contact3_valueForOne = FiveInputDryContact.GetContact_OneValue(Model.SensorID, 3);
    string Contact4_valueForZero = FiveInputDryContact.GetContact_ZeroValue(Model.SensorID, 4);
    string Contact4_valueForOne = FiveInputDryContact.GetContact_OneValue(Model.SensorID, 4);
    string Contact5_valueForZero = FiveInputDryContact.GetContact_ZeroValue(Model.SensorID, 5);
    string Contact5_valueForOne = FiveInputDryContact.GetContact_OneValue(Model.SensorID, 5);
   // List<AppDatum> datums = FiveInputDryContact.GetAppDatums(Model.ApplicationID);
    long EventDetectionTypeInput1 = Model.MinimumThreshold;
    long EventDetectionTypeInput2 = Model.MaximumThreshold;
    long EventDetectionTypeInput3 = Model.Hysteresis;
    long EventDetectionTypeInput4 = Model.Calibration1;
    long EventDetectionTypeInput5 = Model.Calibration2;

%>
<p class="useAwareState"></p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%=datumName0 %> <%:Html.TranslateTag("Enter Aware State","Enter Aware State")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="EventDetectionTypeInput1" id="EventDetectionTypeInput1" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput1 == 0 ? "selected='selected'" : "" %>><%=Contact1_valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput1 == 1 ? "selected='selected'" : "" %>><%=Contact1_valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput1 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%=datumName1 %> <%:Html.TranslateTag("Enter Aware State","Enter Aware State")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="EventDetectionTypeInput2" id="EventDetectionTypeInput2" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput2 == 0 ? "selected='selected'" : "" %>><%=Contact2_valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput2 == 1 ? "selected='selected'" : "" %>><%=Contact2_valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput2 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%=datumName2 %> <%:Html.TranslateTag("Enter Aware State","Enter Aware State")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="EventDetectionTypeInput3" id="EventDetectionTypeInput3" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput3 == 0 ? "selected='selected'" : "" %>><%=Contact3_valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput3 == 1 ? "selected='selected'" : "" %>><%=Contact3_valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput3 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%=datumName3 %> <%:Html.TranslateTag("Enter Aware State","Enter Aware State")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="EventDetectionTypeInput4" id="EventDetectionTypeInput4" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput4 == 0 ? "selected='selected'" : "" %>><%=Contact4_valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput4 == 1 ? "selected='selected'" : "" %>><%=Contact4_valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput4 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
         <%=datumName4 %> <%:Html.TranslateTag("Enter Aware State","Enter Aware State")%>
    </div>

    <div class="col sensorEditFormInput">
        <select class="form-select ms-0 user-dets" name="EventDetectionTypeInput5" id="EventDetectionTypeInput5" <%=!Model.CanUpdate ? "disabled='disabled'" : ""  %>>
            <option value="0" <%:EventDetectionTypeInput5 == 0 ? "selected='selected'" : "" %>><%=Contact5_valueForZero %></option>
            <option value="1" <%:EventDetectionTypeInput5 == 1 ? "selected='selected'" : "" %>><%=Contact5_valueForOne %></option>
            <option value="2" <%:EventDetectionTypeInput5 == 2 ? "selected='selected'" : "" %>>State Change</option>
        </select>
    </div>
</div>
