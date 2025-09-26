<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%--<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Below (LUX)","Aware State Below (LUX)")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Light Meter - Aware State Below", "Assessments below this value will cause the meter to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Above (LUX)","Aware State Above (LUX)")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Light Meter - Aware State Above", "Assessments above this value will cause the meter to enter the Aware State.")%> 
        <hr />
    </div>
</div>--%>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Delta Light Report (LUX)","Delta Light Report (LUX)")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Light Meter - Delta Light Report", "Determines the value difference in LUX from the last reading that will trigger the sensor to report (not aware). For example, if this value is set to 300 LUX and your previous reading was 500 LUX, a value less than 200 LUX or greater than 800 LUX will trigger the sensor to report. Setting this to 0 will make it do nothing.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data Display","Data Display")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Light Meter - Data Display", "What values will be displayed; LUX, Light State, or both.")%> 
        <hr />
    </div>
</div>