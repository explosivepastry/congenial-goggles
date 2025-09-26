<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enter Aware State when the loop is","Enter Aware State when the loop is")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dry Contact - Aware State","Sets the device to detect when the loop is Closed, Open, or in a State Change.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time to Re-Arm","Time to Re-Arm")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dry Contact - Time to Re-Arm","The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dry Contact - Scale","Set what is displayed when the loop is Closed and Open.")%>
        <hr />
    </div>
</div>--%>