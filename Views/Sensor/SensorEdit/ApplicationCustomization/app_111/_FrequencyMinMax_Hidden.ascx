<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    // check this section be removed?
    string MinFrequency = "";
    string MaxFrequency = "";
    string SampleRate = "";
    string scale = "";

    MinFrequency = AdvancedVibration.GetMinFrequency(Model).ToString();
    MaxFrequency = AdvancedVibration.GetMaxFrequency(Model).ToString();
    SampleRate = AdvancedVibration.GetSampleRate(Model).ToString();

    scale = AdvancedVibration.IsHertz(Model.SensorID) ? "Hz" : "rpm";



%>

<select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="SampleRate_Manual" name="SampleRate_Manual" class="editField editFieldMedium tzSelect" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
    <%--   <option <%: SampleRate == "15"? "selected":"" %> value="15">25600 Hz</option>--%>
    <option <%: SampleRate == "14"? "selected":"" %> value="14">12800 Hz</option>
    <option <%: SampleRate == "13"? "selected":"" %> value="13">6400 Hz</option>
    <option <%: SampleRate == "12"? "selected":"" %> value="12">3200 Hz</option>
    <option <%: SampleRate == "7"? "selected":"" %> value="7">1600 Hz</option>
    <option <%: SampleRate == "6"? "selected":"" %> value="6">800 Hz</option>
    <option <%: SampleRate == "5"? "selected":"" %> value="5">400 Hz</option>
    <option <%: SampleRate == "4"? "selected":"" %> value="4">200 Hz</option>
    <option <%: SampleRate == "3"? "selected":"" %> value="3">100 Hz</option>
    <option <%: SampleRate == "2"? "selected":"" %> value="2">50 Hz</option>
    <option <%: SampleRate == "1"? "selected":"" %> value="1">25 Hz</option>
</select>

<input hidden class="aSettings__input_input" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="MinFrequency_Manual" id="MinFrequency_Manual" value="<%=MinFrequency %>" />
<%: Html.ValidationMessageFor(model => model.Calibration3)%>

<input hidden class="aSettings__input_input" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="MaxFrequency_Manual" id="MaxFrequency_Manual" value="<%=MaxFrequency %>" />
<%: Html.ValidationMessageFor(model => model.Calibration3)%>

