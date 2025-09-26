<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        string signalTestDuration = "";
        string autoShutoffTime = "";
        string signalReliabilityLevel = "";

        signalTestDuration = Model.Calibration1.ToString();
        autoShutoffTime = Model.Calibration2.ToString();
        signalReliabilityLevel = SiteSurvey.GetSignalReliabilityLevel(Model);

%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Signal Test Duration")%>
    </div>
    <div class="col sensorEditFormInput">

        <select class="form-select" name="signalTestDuration" id="signalTestDuration" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="5" <%=signalTestDuration.ToInt() == 5 ? "selected='selected'" : "" %>>5 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Seconds")%></option>
            <option value="10" <%=signalTestDuration.ToInt() == 10 ? "selected='selected'" : "" %>>10 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Seconds")%></option>
            <option value="30" <%=signalTestDuration.ToInt() == 30 ? "selected='selected'" : "" %>>30 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Seconds")%></option>
            <option value="60" <%=signalTestDuration.ToInt() == 60 ? "selected='selected'" : "" %>>60 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Seconds")%></option>
            <option value="90" <%=signalTestDuration.ToInt() == 90 ? "selected='selected'" : "" %>>90 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Seconds")%></option>
        </select>

    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Signal Reliability Level")%> 
        <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".siteSurveyHelp">
           <img src="../../Content/images/iconmonstr-help-2-240 (1).png" style="height: 18px; margin: 10px; margin-top: 5px; margin-right: 5px;">
        </a>
    </div>
    <div class="col sensorEditFormInput">

        <select class="form-select" name="signalReliabilityLevel" id="signalReliabilityLevel" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="2" <%=signalReliabilityLevel.ToInt() == 2  ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Mission-Critical")%></option>
            <option value="1" <%=signalReliabilityLevel.ToInt() == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Strong")%></option>
            <option value="0" <%=signalReliabilityLevel.ToInt() == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Functional")%></option>
        </select>
    </div>
</div>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Auto Shutoff Time")%>
    </div>
    <div class="col sensorEditFormInput">

        <select class="form-select" name="autoShutoffTime" id="autoShutoffTime" <%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="120" <%=autoShutoffTime.ToInt() == 120 ? "selected='selected'" : "" %>>2 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Minutes")%></option>
            <option value="300" <%=autoShutoffTime.ToInt() == 300 ? "selected='selected'" : "" %>>5 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Minutes")%></option>
            <option value="600" <%=autoShutoffTime.ToInt() == 600 ? "selected='selected'" : "" %>>10 <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_143|Minutes")%></option>
        </select>

    </div>
</div>

<%} %>